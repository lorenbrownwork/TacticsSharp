using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    class PlayGame
    {
        public PlayGame()
        {
            mainMenu();
        }

        //Number of selections - 1
        private int posMax = 1;

        public int mainMenu()
        {
            //Change background color
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.CursorVisible = false;
            Console.Clear();

            int position = 0;
            while (true)
            {
                //Print Menu
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine();
                if (position == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    New Game");
                    Console.ForegroundColor = ConsoleColor.White;
                } else { Console.WriteLine("    New Game"); }
                if (position == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Load Game");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Load Game"); }

                //Read Key Input
                ConsoleKeyInfo keypressed = Console.ReadKey(false);
                if ((int)keypressed.Key == (char)ConsoleKey.DownArrow && position < posMax) {
                    position+=1;
                } else if ((int)keypressed.Key == (char)ConsoleKey.UpArrow && position > 0) {
                    position -= 1;
                } else if ((int)keypressed.Key == (char)ConsoleKey.Enter) {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.CursorVisible = true;
                    Console.Clear();
                    return position;
                }
            }
        }
    }
}
