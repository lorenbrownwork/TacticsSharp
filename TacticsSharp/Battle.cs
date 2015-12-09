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

        public Battle(Team a, Team b)
        {
            rnd = new Random();
            this.aTeam = a;
            this.bTeam = b;

            while (battleRages())
            {
                autoTurn();
            }
        }

        private void autoTurn()
        {
            //Team A's turn
            for (int i = 0; i < aTeam.roster.Count; i++)
            {
                //Check if battle is over
                if (battleRages() == false)
                {
                    return;
                }

                //Check if team member is alive (skip turn if not) 
                if (aTeam.roster[i].getHP() > 0)
                {
                    while (true)
                    {
                        int r = rnd.Next(bTeam.roster.Count); //Randomly pick aponent
                        if (bTeam.roster[r].getHP() > 0) //Check if aponent is alive.
                        {
                            bTeam.roster[r].hurt(aTeam.roster[i]); //teamMember hurts bTeam.roster[r]
                            break;
                        }
                    }

                    //Display Team
                    aTeam.displayTeam();
                    bTeam.displayTeam();
                    Console.ReadLine();
                    Console.Clear();
                }
            }

            //Team B's turn
            for (int i = 0; i < bTeam.roster.Count; i++)
            {
                //Check if battle is over
                if (battleRages() == false)
                {
                    return;
                }

                //Check if team member is alive (skip turn if not) 
                if (bTeam.roster[i].getHP() > 0)
                {
                    while (true)
                    {
                        int r = rnd.Next(aTeam.roster.Count); //Randomly pick aponent
                        if (aTeam.roster[r].getHP() > 0) //Check if aponent is alive.
                        {
                            aTeam.roster[r].hurt(bTeam.roster[i]); //teamMember hurts aTeam.roster[r]
                            break;
                        }
                    }
                    //Display Team
                    aTeam.displayTeam();
                    bTeam.displayTeam();
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        //Check if the battle is still going
        private bool battleRages()
        {
            int aHealth = aTeam.roster.Sum(x => x.getHP());
            int bHealth = bTeam.roster.Sum(x => x.getHP());

            //Console.WriteLine(aHealth);
            //Console.WriteLine(bHealth);

            //if (aHealth <= 0 || bHealth <=0)
            //{
            //    return false;
            //}
            return true;
        }

        //winner getter
        public string Winner
        {
            get
            {
                return winner;
            }
        }
    }
}
