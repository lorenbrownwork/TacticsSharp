using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    class Battle
    {
        private string winner = "None";
        private static Team aTeam, bTeam;
        private static Random rnd;

        public static void NewBattle(Team a, Team b)
        {
            rnd = new Random();
            aTeam = a;
            bTeam = b;

            string winner = "None";

            List<Character> sortedList = turnOrder(aTeam, bTeam);

            while (battleRages(aTeam, bTeam))
            {
                //autoTurn(aTeam, bTeam);
                foreach (Character character in sortedList)
                {
                    if (character.getHP() > 0)
                    {
                        takeTurn(character);
                    }
                }
            }

            int aHealth = aTeam.roster.Sum(x => x.getHP());
            int bHealth = bTeam.roster.Sum(x => x.getHP());

            if (aHealth > bHealth)
            {
                winner = "A";
                Console.WriteLine("Winner: " + winner);
            }
            else if(bHealth > aHealth)
            {
                winner = "B";
                Console.WriteLine("Winner: " + winner);
            }
        }

        private static void autoTurn(Team aTeam, Team bTeam)
        {

            //Team A's turn
            foreach (var teamMember in aTeam.roster)
            {
                //Check if battle is over
                if (battleRages(aTeam, bTeam) == false)
                {
                    return;
                }

                //Check if team member is alive (skip turn if not) 
                if (teamMember.getHP() > 0)
                {
                    int r = rnd.Next(bTeam.roster.Count); //Randomly pick aponent
                    while (bTeam.roster[r].getHP() <= 0)
                    {
                        r = rnd.Next(bTeam.roster.Count);
                    }
                    teamMember.Attack(bTeam.roster[r]); //teamMember hurts bTeam.roster[r]
                    Console.WriteLine(bTeam.roster[r].getName() + ": " + bTeam.roster[r].getHP());
                    
                }
            }

            //Team B's turn
            foreach (var teamMember in aTeam.roster)
            {
                //Check if battle is over
                if (battleRages(aTeam, bTeam) == false)
                {
                    return;
                }

                //Check if team member is alive (skip turn if not) 
                if (teamMember.getHP() > 0)
                {
                    int r = rnd.Next(aTeam.roster.Count); //Randomly pick aponent
                    while (aTeam.roster[r].getHP() <= 0)
                    {
                        r = rnd.Next(aTeam.roster.Count);
                    }
                    teamMember.Attack(aTeam.roster[r]); //teamMember hurts aTeam.roster[r]
                    Console.WriteLine(aTeam.roster[r].getName() + ": " + aTeam.roster[r].getHP());
                     
                }
            }
        }

        private static bool battleRages(Team aTeam, Team bTeam)
        {
            int aHealth = aTeam.roster.Sum(x => x.getHP());
            int bHealth = bTeam.roster.Sum(x => x.getHP());

            if (aHealth <= 0 || bHealth <=0)
            {
                return false;
            }
            return true;
        }

        public string Winner
        {
            get
            {
                return winner;
            }
        }

        private static void takeTurn(Character character)
        {
            if (character.getTeam() == 1)
            {
                //int r = rnd.Next(bTeam.roster.Count); //Randomly pick opponent
                //while (bTeam.roster[r].getHP() <= 0)
                //{
                //    r = rnd.Next(bTeam.roster.Count);
                //}
                int r = pickEnemy(character);
                takeAction(character, r);
                //character.Attack(bTeam.roster[r]);
                Console.WriteLine(bTeam.roster[r].getName() + ": " + bTeam.roster[r].getHP());
            }
            else if (character.getTeam() == 2)
            {
                //int r = rnd.Next(aTeam.roster.Count); //Randomly pick opponent
                //while (aTeam.roster[r].getHP() <= 0)
                //{
                //    r = rnd.Next(aTeam.roster.Count);
                //}
                int r = pickEnemy(character);
                takeAction(character, r);
                //character.Attack(aTeam.roster[r]); //teamMember hurts aTeam.roster[r]
                Console.WriteLine(aTeam.roster[r].getName() + ": " + aTeam.roster[r].getHP());
            }
            else
            {
                Console.WriteLine("You're on the wrong team!");
            }
        }

        private static int pickEnemy(Character character)
        {
            int position = 0;
            int posMax = 0;
            if (character.getTeam() == 1)
            {
                posMax = bTeam.roster.Count();
            }
            else if (character.getTeam() == 2)
            {
                posMax = aTeam.roster.Count();
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("    Attacking: {0}", character.getName());
                if (character.getTeam() == 1)
                {
                    int i = 0;
                    foreach (Character enemy in bTeam.roster)
                    {
                        if (position == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("    Name: {0}, HP: {1}", bTeam.roster[i].getName(), bTeam.roster[i].getHP());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else { Console.WriteLine("    Name: {0}, HP: {1}", bTeam.roster[i].getName(), bTeam.roster[i].getHP()); }
                        i++;
                    }
                }

                if (character.getTeam() == 2)
                {
                    int i = 0;
                    foreach (Character enemy in aTeam.roster)
                    {
                        if (position == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("    Name: {0}, HP: {1}", aTeam.roster[i].getName(), aTeam.roster[i].getHP());
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else { Console.WriteLine("    Name: {0}, HP: {1}", aTeam.roster[i].getName(), aTeam.roster[i].getHP()); }
                        i++;
                    }
                }

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

        private static void takeAction(Character character, int enemy)
        {
            int position = 0;
            int posMax = 2;
            bool turnTaken = false;

            while (!turnTaken)
            {
                Console.Clear();
                Console.WriteLine("    Attacking: {0}", character.getName());

                if (position == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Attack");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Attack"); }
                if (position == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Cast Spell");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Cast Spell"); }
                if (position == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Defend");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Defend"); }

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
                    if (position == 0)
                    {
                        character.Attack(bTeam.roster[enemy]);
                        turnTaken = true;
                    }
                    else if (position == 1)
                    {
                        bool success = character.CastSpell(bTeam.roster[enemy]);
                        if (success)
                        {
                            turnTaken = true;
                        }
                        else
                        {
                            turnTaken = false;
                        }
                    }
                    else if (position == 2)
                    {
                        character.setDefenseFlag(true);
                        turnTaken = true;
                    }
                }
            }
        }

        //takes the two teams, puts them into a big list, and sorts them by dex
        //gives a simple turn order, not just the two teams all going at once
        //need to put in some way to distinguish teams(?)
            //might not, allows team damage
        private static List<Character> turnOrder(Team a, Team b)
        {
            //make the list
            List<Character> characterList = new List<Character>();
            int i = 0;
            //put characters into the list
            foreach (Character character in a)
            {
                character.setTeam(1);
                characterList.Add(character);
            }
            foreach (Character character in b)
            {
                character.setTeam(2);
                characterList.Add(character);
            }

            //sort and return the list
            List<Character> sortedList = characterList.OrderByDescending(x => x.dex).ToList();
            return sortedList;
        }
    }
}
