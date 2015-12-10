using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    class Team
    {
        private string name;
        private int wins;
        private int losses;
        public List<Character> roster;
        
        //builds a team of 5 characters
        public Team()
        {
            roster = new List<Character>();
            Console.Write("Team Name: ");
            this.name = Console.ReadLine();
            for(int i = 0; i < 5; i++)
            {
                addCharacter();
            }
        }

        public void displayTeam()
        {
            Console.WriteLine("");
            Console.WriteLine("Team " + name);
            for (int i = 0; i < roster.Count; i++)
            {
                string temp = String.Format("    {0,5}  {1,5}", roster[i].getName(), roster[i].getHP());
                Console.WriteLine(temp);
            }
        }

        //Add existing character
        public bool addCharacter(Character guy)
        {
            //Check for identical character name
            //if (roster.Exists(x => (x.getName() == guy.getName())))
            //{
            //    return false;
            //}
            roster.Add(guy);
            return true;
        }

        //Create and add a character
        public bool addCharacter()
        {
            while (true)
            {
                Character rookie = new Character();
                if (addCharacter(rookie))
                    return true;
                Console.WriteLine("Teams must contain unique names.  Please select a different name");
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Wins
        {
            get { return wins; } 
            set { wins = value; }
        }

        public int Losses
        {
            get { return losses; }
            set { losses = value; }
        }
    }
}
