using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Armor WoodenSheild = new Armor ("Wooden Sheild", 10, "Normal", 10, "Fire");
            Weapon RustySword = new Weapon ("Rusty Sword", 30, "Normal", 40, "Fire");

            Weapon BetterSword = new Weapon("Better Sword", 50, "Normal", 60, "Electric");
            Armor BetterArmor = new Armor("Better Armor", 50, "Normal", 20, "Electric");

            Spell NewSpell = new Spell("testSpell", 5, 20, "Fire", 0);

            Character Joe = new Character ("Joe", 100, 20, 2.0, 2.0, 1.0, RustySword, WoodenSheild);
            Character Bob = new Character ("Bob", 100, 20, 1.0, 2.0, 1.0, RustySword, WoodenSheild);

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
            Joe.hurt(Bob);
            Console.WriteLine(Joe.getHP());
            Joe.heal(100F);
            Console.WriteLine(Joe.getHP());

            Joe.changeArmor(BetterArmor);
            Joe.hurt(Bob);
            Console.WriteLine(Joe.getHP());
            Joe.heal(100F);
            Console.WriteLine(Joe.getHP());

            Console.ReadKey();
        }
    }
}

