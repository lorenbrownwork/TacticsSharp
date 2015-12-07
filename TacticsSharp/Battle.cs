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
                
            }
        }

        private void autoTurn()
        {

            //Team A's turn
            foreach (var teamMember in aTeam.roster)
            {
                //Check if battle is over
                if (battleRages() == false)
                {
                    return;
                }

                //Check if team member is alive (skip turn if not) 
                if (teamMember.getHP() > 0)
                {
                    while (true)
                    {
                        int r = rnd.Next(bTeam.roster.Count); //Randomly pick aponent
                        if (bTeam.roster[r].getHP() > 0) //Check if aponent is alive.
                        {
                            bTeam.roster[r].hurt(teamMember); //teamMember hurts bTeam.roster[r]
                        }
                    }
                }
            }

            //Team B's turn
            foreach (var teamMember in aTeam.roster)
            {
                //Check if battle is over
                if (battleRages() == false)
                {
                    return;
                }

                //Check if team member is alive (skip turn if not) 
                if (teamMember.getHP() > 0)
                {
                    while (true)
                    {
                        int r = rnd.Next(aTeam.roster.Count); //Randomly pick aponent
                        if (aTeam.roster[r].getHP() > 0) //Check if aponent is alive.
                        {
                            aTeam.roster[r].hurt(teamMember); //teamMember hurts aTeam.roster[r]
                        }
                    }
                }
            }
        }

        private bool battleRages()
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
    }
}
