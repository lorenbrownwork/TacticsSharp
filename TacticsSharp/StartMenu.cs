using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TacticsSharp
{
    class StartMenu
    {

        //Number of selections - 1
        private int posMax = 4;
        private Team aTeam;
        private Team bTeam;
        private List<Character> userClan; //Holds all characters (team or not)
        private List<Character> npcClan;  //Holds all NPC characters (team or not)

        //Constructor
        //public StartMenu()
        //{
        //    userClan = new List<Character>();
        //    npcClan = new List<Character>();

        //    int selection = mainMenu();
        //    switch (selection)
        //    {
        //        case 0:
        //            Console.WriteLine("New Game Selected");
        //            break;
        //        case 1:
        //            Console.WriteLine("Load Game Selected");
        //            loadUserCharacters();
        //            break;
        //        case 2:
        //            Console.WriteLine("Save Game Selected");
        //            saveUserCharacters();
        //            break;
        //        case 3:
        //            Console.WriteLine("Quick Battle Selected");
        //            quickBattle();
        //            break;
        //    }
        //}

        public void startMenu()
        {
            userClan = new List<Character>();
            npcClan = new List<Character>();

            int selection = mainMenu();
            switch (selection)
            {
                case 0:
                    Console.WriteLine("New Game Selected");
                    break;
                case 1:
                    Console.WriteLine("Load Game Selected");
                    loadUserCharacters();
                    break;
                case 2:
                    Console.WriteLine("Save Game Selected");
                    saveUserCharacters();
                    break;
                case 3:
                    Console.WriteLine("Quick Battle Selected");
                    quickBattle();
                    break;
                case 4:
                    Console.WriteLine("Exiting Game");
                    break;
            }
        }

        private void loadUserCharacters()
        {
            string userClanPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TacticsSharp\\UserClans";

            if (!Directory.Exists(userClanPath))
            {
                Directory.CreateDirectory(userClanPath); //Create directory if it does not exist
                Console.WriteLine("Directory was not found.  Directory has been created.");
            }
            else
            {
                //Grab each file in the Clan Directory
                string[] fileEntries = Directory.GetFiles(userClanPath);
                foreach (string fileName in fileEntries)
                {
                    userClan.Add(deserializer(fileName));
                }

                if (userClan.Count > 0)
                {
                    Console.WriteLine(userClan[0].getName());
                }
                else
                {
                    Console.WriteLine("There are no characters to load.");
                }
            }
        }
        private void saveUserCharacters()
        {
            //TEMP: Create some characters
            /*for (int i = 0; i < 3; i++)
            {
                //Character temp = new Character();
                userClan.Add(new Character());
            }*/

            string userClanPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TacticsSharp\\UserClans";
            if (!Directory.Exists(userClanPath))
            {
                Directory.CreateDirectory(userClanPath); //Create directory if it does not exist
                Console.WriteLine("Directory was not found.  Directory has been created.");
            }
            //Iterate over Character list
            for (int i = 0; i < userClan.Count; i++)
            {
                serializer(userClan[i], userClanPath);
            }

            //Display files
            /*
            Console.WriteLine(userClanPath);
            string[] fileEntries = Directory.GetFiles(userClanPath);
            foreach (string fileName in fileEntries)
                Console.WriteLine("   " + fileName);
            */

        }

        static string serializer(Character character, string path)
        {
            //Path to Desktop
            path = path + "\\" + character.getName() + ".bin";
            Console.WriteLine(path);

            //Serialize
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, character);
            stream.Close();

            return path;
        }

        static Character deserializer(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Character obj = (Character)formatter.Deserialize(stream);
            stream.Close();

            // Here's the proof.
            Console.WriteLine("Name: {0}", obj.getName());

            return obj;
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
            //Battle myBattle = new Battle(aTeam, bTeam);
            //Console.WriteLine(myBattle.Winner);

            Battle.NewBattle(aTeam, bTeam);
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
                    Console.WriteLine("    Save Game");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Save Game"); }
                if (position == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Quick Battle");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Quick Battle"); }
                if (position == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("    Exit Game");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.WriteLine("    Exit Game"); }

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
