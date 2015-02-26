using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship
{
    class Method
    {
  	     private static string FirstLine;
         private static int x_length = 10;
         private static int y_length = 11;
         private static string [,] Map = new string [x_length,y_length];
         private static bool[,] Ship_exist = new bool [x_length,y_length];

        
        public static void init()
        {
            FirstLine  =  "  0 1 2 3 4 5 6 7 8 9 ";
            for(int i = 0; i< x_length;i++)
            {
                for(int j = 0; j < y_length; j++)
                {

					if(j == 0)
                    {
						Map[i,j] = i.ToString() + " ";
                    }
					else
					{
						Map[i,j] = "* ";
					}
                    

                }
        
            }

            for(int i = 0; i< x_length;i++)
            {
                for(int j = 0; j < y_length; j++)
                {
                    Ship_exist[i,j] = false;
                }
        
            }
			Buildship();
        }

        public static void DisplayMap(bool giveup)
    	{
    		Console.WriteLine(FirstLine);
            if(giveup)
			{
				for(int i = 0; i < 10 ; i++)
				{	
					for(int j = 0; j < 10 ; j++)
					{
						if(Ship_exist[i,j] == true)
						{
							Map[i,j] = "S ";					
						}
					}
				}
				for(int i = 0; i < x_length;i++)
				{
					DisplayLine(Map,i);
				}
			}
			else 
			{	
				for(int i = 0; i < x_length;i++)
				{
					DisplayLine(Map,i);
				}
			}
    	}

        private static void DisplayLine(string [,] map , int x)
        {
            for(int i = 0; i< y_length;i++)
            {
                Console.Write(map[x,i]);
            }                   
            Console.WriteLine();
        }
        private static bool CheckInput(int x, int y)
        {
            if(x < x_length || x >= 0 || y < y_length - 1 || y >= 0)
            {
                return true;
            }
            return false;
        }
        public static bool Hitship(int x,int y)
        {
            int x_value = 0;
			int y_value = 0;
			

			
			y_value = y_value + 1;
			
            if(!CheckInput(x_value,y_value))
            {
                Console.WriteLine("The coordinate that you have" 
                + "entered is not on the map");
				
            }
            
			//if hit or not hit
			if(Ship_exist[x_value,y_value] == true)
            {
                ChangeStatus(x_value,y_value,true);
				Ship_exist[x_value,y_value] = false;
				DisplayMap(false);
				return true;
            }
            else
			{
				ChangeStatus(x_value,y_value,false);
				DisplayMap(false);
				return false;
			}

        }
		
		private static bool CheckSpace(int x,int y,int n)
		{
			if(n == 2)
			{
				if(Ship_exist[x,y]||Ship_exist[x+1,y])
				{
					return false;
				}
				return true;
			}
			
			if(n == 3)
			{
				if(Ship_exist[x,y]||Ship_exist[x,y+1]||Ship_exist[x,y+2])
				{
					return false;
				
				}
				return true;
			}
			if(n == 4)
			{
				if(Ship_exist[x,y]||Ship_exist[x+1,y]||Ship_exist[x+2,y]
				||Ship_exist[x+3,y])
				{
					return false;
				}
				return true;	
			}
			Console.WriteLine("The input n is incorrect");
			return false;
		}
		
		private static void Buildship()
		{
			Random r = new Random();
			int xInt = r.Next(1,7);
			int yInt = r.Next(1,10);
			int tmp = 0;
			int flag = 10;

			//case where the length of the ship is 4
			tmp = 4;
			//make sure the space is not taken(need 4)
			while(!CheckSpace(xInt,yInt,4) )
			{
				yInt = Math.Abs(yInt-1);
				flag--;
				if(flag == 0)
				{
					Console.WriteLine("No space for the ship in the map");
					break;
				}	
				
			}
			
			while(tmp > 0)
			{

				Ship_exist[xInt,yInt] = true;
				xInt++;
				tmp--;
			}
			//case where the length of the ship is 3
			flag = 10;
			tmp = 3;
			xInt = r.Next(5,10);
			yInt = r.Next(1,8);
			
			//make sure the space is not taken(need 3)
			//error
			while(!CheckSpace(xInt,yInt,3))
			{
				if(xInt == 0)
				{
					xInt = 9;
				}
				xInt = Math.Abs(xInt-1);
				flag--;
				if(flag == 0)
				{
					Console.WriteLine("No space for the ship in the map");
					break;
				}	
			}
			
			while(tmp > 0)
			{

				Ship_exist[xInt,yInt] = true;
				yInt++;
				tmp--;
			}
			//case where the length of the ship is 2
			flag = 10;
			tmp = 2;
			xInt = r.Next(3,9);
			yInt = r.Next(3,7);
			
			//make sure the space is not taken(need 2)
			while(!CheckSpace(xInt,yInt,2))
			{
				yInt = Math.Abs(yInt-1);
				flag--;
				if(flag == 0)
				{
					Console.WriteLine("No space for the ship in the map");
					break;
				}	
				
			}
			
			while(tmp > 0)
			{

				Ship_exist[xInt,yInt] = true;
				xInt++;
				tmp--;
			}			
			Console.WriteLine("build successful");
		}

		private static void ChangeStatus(int x, int y,bool hit)
		{
			if(hit)
			{
				Map[x,y] = "X ";
			}
			else
			{
				Map[x,y] = "O ";
			}
		}
    }

 class Program
    {
        static void Main()
        {
            Console.Clear();
			Console.Title = "Battleship";
            Console.CursorVisible = false;
			Method.init();
            Method.DisplayMap(false);
			//Console.Clear();
			int count = 30;
			int hit = 0;
			int x = 0;
			int y = 0;
			string x_input;
			string y_input;
			bool empty;
			while(count > 0)
			{
				Console.WriteLine("hit:" + hit);

				//case where the game is win by the player
				if(hit == 9)
				{
					Console.Clear();
					Console.WriteLine("Amazing You have just won the game");
					Console.WriteLine("Hope you enjoy play this game");
					Console.WriteLine("if you have found anything you would want me to" +
						" add or to fix please leave a message on my Github");
					Console.WriteLine("Press any key to Quit...");
					Console.ReadKey();
					break;
				}
				
				do
				{	
					empty = false;	//prevent user input null x and null y
					Console.WriteLine("Please Enter the x-axis number ");
					x_input = Console.ReadLine();
					Console.WriteLine("Please Enter the y-axis number ");
					y_input = Console.ReadLine();
					if(int.TryParse(x_input, out y)&&int.TryParse(y_input, out x)){}
					else
					{
						Console.WriteLine("The coordinate that you have entered"
						+ "is not an integer");
						empty = true;
					}
					
				}while(empty);
				
				count--;
				
				if(Method.Hitship(x,y) && hit!= 9)
				{
					Console.Clear();
					Console.WriteLine("Awesome You just hit a battleship");
					Console.WriteLine("Missile left: " + count);
					Method.DisplayMap(false);
					hit++;
					
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Oh! You missed it, Please Try again");
					Console.WriteLine("Missile left: " + count);
					Method.DisplayMap(false);

				}

			}
				Console.Clear();
				Console.WriteLine("Sorry Game is Over");
				Console.WriteLine("Hope you enjoy play this game");
				Console.WriteLine("Wanna try again?");	
				Console.WriteLine("if you have found anything you would want me to" +
				"add or to fix please leave a message on my github");
				Console.WriteLine("Press any key to Quit...");
				Console.ReadKey();
        }
    }
}
