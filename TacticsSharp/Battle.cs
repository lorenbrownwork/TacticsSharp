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
        private Team aTeam, bTeam;
        private static Random rnd;

        public static void NewBattle(Team a, Team b)
        {
            rnd = new Random();
            Team aTeam = a;
            Team bTeam = b;

            string winner = "None";

            //while (battleRages(aTeam, bTeam))
            //{
            //    autoTurn(aTeam, bTeam);
            //}
            List<Character> sortedList = turnOrder(aTeam, bTeam);
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

        private static void listTurn(List<Character> charList)
        {
            foreach (var member in charList)
            {

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

        private static void takeTurn()
        {

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
                characterList.Add(character);
            }
            foreach (Character character in b)
            {
                characterList.Add(character);
            }

            //sort and return the list
            List<Character> sortedList = characterList.OrderByDescending(x => x.dex).ToList();
            return sortedList;
        }
    }
}
