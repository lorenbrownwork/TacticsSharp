using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    class StartMenu
    {

        //Number of selections - 1
        private int posMax = 2;
        private Team aTeam;
        private Team bTeam;

        //Constructor
        public StartMenu()
        {
            int selection = mainMenu();
            switch (selection)
            {
                case 0:
                    Console.WriteLine("New Game Selected");
                    break;
                case 1:
                    Console.WriteLine("Load Game Selected");
                    break;
                case 2:
                    Console.WriteLine("Quick battle Selected");
                    quickBattle();
                    break;
            }

        }

        //Generat two teams to battle and start battle
        private void quickBattle()
        {
            this.aTeam = new Team();
            this.bTeam = new Team();
            startBattle();
        }

        //Start battle
        public bool startBattle()
        {
            if (this.aTeam == null || this.bTeam == null)
            {
                Console.WriteLine("Must have two teams to battle");
                return false;
            }
            Console.WriteLine("Starting battle");
            Battle myBattle = new Battle(aTeam, bTeam);
            Console.WriteLine(myBattle.Winner);
            return true;
        }

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
                }
                else { Console.WriteLine("    New Game"); }
                if (position == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Load Game");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Load Game"); }
                if (position == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Quick Battle");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Quick Battle"); }

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
                    return position;
                }
            }
        }
    }
}
