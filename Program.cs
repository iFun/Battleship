using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ship
{
    class Method
    {
  	     public static string FirstLine;
         public static int x_length = 10;
         public static int y_length = 11;
         public static string [,] Map = new string [x_length,y_length];
         public static bool[,] Ship_exist = new bool [x_length,y_length];

        
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
        private static bool hitship(string x,string y)
        {
            int x_value = 0;
			int y_value = 0;
			
			if(int.TryParse(y, out x_value)&&int.TryParse(x, out y_value)){}
			else
			{
				Console.WriteLine("The coordinate that you have entered"
				+ "is not an integer");
			}
			
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
		public bool Hitship
		{
			get{return hitship(string x,string y);}
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
            Method.init();
            Method.DisplayMap(false);
			Method.DisplayMap(true);
			//Console.Clear();
			int count = 30;
			string x;
			string y;
			while(count > 0)
			{
				Console.WriteLine("Please Enter the x-axis number: ");
				x = Console.ReadLine();
				Console.WriteLine("Please Enter the y-axis number: ");
				y = Console.ReadLine();
				count--;
				if(Method.Hitship)
				{
					Console.Clear();
					Console.WriteLine("Awesome You just hit a battleship");
					Console.WriteLine("Missile left: " + count);
					Method.DisplayMap(false);
					
				}
				else
				{
					Console.Clear();
					Console.WriteLine("Oh! You missed it, Please Try again");
					Console.WriteLine("Missile left: " + count);
					Method.DisplayMap(false);
				}		
			}

        }
    }
}
