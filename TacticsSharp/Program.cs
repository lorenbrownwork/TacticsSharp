using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace TacticsSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Armor WoodenShield = new Armor ("Wooden Sheild", 10, "Normal", 10, "Fire");
            Weapon RustySword = new Weapon ("Rusty Sword", 30, "Normal", 40, "Fire");

            Weapon BetterSword = new Weapon("Better Sword", 50, "Normal", 60, "Electric");
            Armor BetterArmor = new Armor("Better Armor", 50, "Normal", 20, "Electric");

            SpellEffect poison = new SpellEffect("Poison", "Normal", 2, 3);

            Spell NewSpell = new Spell("testSpell", 5, 20, "Fire", poison);
            Spell NextSpell = new Spell("nextSpell", 2, 5, "Fire");

            Character Joe = new Character("Joe", 5, 5, 5, 5, 5, RustySword, WoodenShield);
            Character Bob = new Character ("Bob", 10, 10, 5, 2, 2, RustySword, WoodenShield);

            Console.WriteLine(Bob.getWeapon().getName());
            Bob.changeWeapon(BetterSword);
            Console.WriteLine(Bob.getWeapon().getName());
            Bob.changeWeapon(RustySword);

            Console.WriteLine(Joe.getHP());
            Joe.hurt(Bob);
            Console.WriteLine(Joe.getHP());
            Joe.heal(100F);
            Console.WriteLine(Joe.getHP());

            //change weapon test
            Bob.changeWeapon(BetterSword);
            Bob.Attack(Joe);
            Console.WriteLine(Joe.getHP());
            Joe.heal(100F);
            Console.WriteLine(Joe.getHP());

            Joe.changeArmor(BetterArmor);
            Bob.Attack(Joe);
            Console.WriteLine(Joe.getHP());
            Joe.heal(100F);
            Console.WriteLine(Joe.getHP());

            //string path = serializer(Joe);
            //Character Joe2 = deserializer(path);

            Joe.LearnSpell(NewSpell);
            Joe.LearnSpell(NewSpell);

            List<SpellEffect> effectList = new List<SpellEffect>();
            effectList = SpellEffect.ImportEffects("../../ImportFiles/effect-list.csv", effectList);

            List<Spell> spellList = new List<Spell>();
            spellList = Spell.ImportSpells("../../ImportFiles/spell-list.csv", spellList, effectList);

            foreach (Spell spell in spellList)
            {

                Console.WriteLine(spell.getName());
            }
            Console.WriteLine(Environment.CurrentDirectory);

            //StartMenu game = new StartMenu();
            StartMenu menu = new StartMenu();

            menu.startMenu();
            
            //Wait
            Console.ReadKey();

        }

        /*  MOVED TO START MENU
        static string serializer(Character character)
        {
            //Path to Desktop
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
        }*/
    }
}

