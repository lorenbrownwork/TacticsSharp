using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    class LevelUp
    {
        public static int[] levelMenu(int[] oldPoints, int[] newPoints, int pointsAvailable)
        {
            //Change background color
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            int posMax = 4;
            int position = 0;
            

            //int position = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Level UP!");
                Console.WriteLine("Points Available: " + pointsAvailable);

                //Print Menu
                if (position == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    " + oldPoints[0] + " Strength [+" + newPoints[0] + "]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    " + oldPoints[0] + " Strength [+" + newPoints[0] + "]"); }
                if (position == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    " + oldPoints[1] + " Dexterity [+" + newPoints[1] + "]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    " + oldPoints[1] + " Dexterity [+" + newPoints[1] + "]"); }
                if (position == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    " + oldPoints[2] + " Constitution [+" + newPoints[2] + "]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    " + oldPoints[2] + " Constitution [+" + newPoints[2] + "]"); }
                if (position == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    " + oldPoints[3] + " Intelligence [+" + newPoints[3] + "]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    " + oldPoints[3] + " Intelligence [+" + newPoints[3] + "]"); }
                if (position == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    " + oldPoints[4] + " Wisdom [+" + newPoints[4] + "]");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    " + oldPoints[4] + " Wisdom [+" + newPoints[4] + "]"); }

                //Read Key Input
                ConsoleKeyInfo keypressed = Console.ReadKey(false);
                if ((int)keypressed.Key == (char)ConsoleKey.DownArrow && position < posMax)
                {
                    position += 1;
                }
                else if ((int)keypressed.Key == (char)ConsoleKey.UpArrow && position > 0)
                {
                    position -= 1;
                }
                //else if ((int)keypressed.Key == (char)ConsoleKey.Enter)
                //{
                //    Console.BackgroundColor = ConsoleColor.Black;
                //    Console.CursorVisible = true;
                //    Console.Clear();
                //    newPoints[position]++;
                //    return true;
                //}
                else if ((int)keypressed.Key == (char)ConsoleKey.LeftArrow)
                {
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.CursorVisible = true;
                    Console.Clear();
                    if (newPoints[position] > 0)
                    {
                        newPoints[position]--;
                        pointsAvailable++;
                    }
                }
                else if ((int)keypressed.Key == (char)ConsoleKey.RightArrow)
                {
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.CursorVisible = true;
                    Console.Clear();
                    if (pointsAvailable > 0)
                    {
                        newPoints[position]++;
                        pointsAvailable--;
                    }
                }

                if (pointsAvailable <= 0)
                {
                    Console.WriteLine("To accept these choices, please press enter.");
                    keypressed = Console.ReadKey(false);

                    if ((int)keypressed.Key == (char)ConsoleKey.Enter)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.CursorVisible = true;
                        return newPoints;
                    }
                }
            }
        }
    }
}
