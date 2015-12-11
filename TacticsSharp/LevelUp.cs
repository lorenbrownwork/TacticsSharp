using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    class LevelUp
    {
        public static void LevelCharacter(ref Character character, int points)
        {
            int[] oldPoints = new int[5];
            int[] newPoints = new int[5];
            Array.Clear(newPoints, 0, 5);
            int menuPos = 0;

            //Grab Character Stats
            oldPoints[0] = character.str;
            oldPoints[1] = character.dex;
            oldPoints[2] = character.con;
            oldPoints[3] = character.intel;
            oldPoints[4] = character.wis;

            for (int i = points; i > 0; i--)
            {
                Console.Clear();
                if (i > 0)
                    levelMenu(oldPoints, ref newPoints, i, ref menuPos);
            }

            //Update Character Stats
            character.str += newPoints[0];
            character.dex += newPoints[1];
            character.con += newPoints[2];
            character.intel += newPoints[3];
            character.wis += newPoints[4];
        }

        private static bool levelMenu(int[] oldPoints, ref int[] newPoints, int pointsAvailable, ref int position)
        {
            //Change background color
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            int posMax = 4;
            

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
                else if ((int)keypressed.Key == (char)ConsoleKey.Enter)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.CursorVisible = true;
                    Console.Clear();
                    newPoints[position]++;
                    return true;
                }
            }
        }
    }
}
