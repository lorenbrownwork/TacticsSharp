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

            Character Joe = new Character ("Joe", 100, 20, 2.0, 2.0, 1.0, RustySword, WoodenSheild);
            Character Bob = new Character ("Bob", 100, 20, 1.0, 2.0, 1.0, RustySword, WoodenSheild);

            Console.WriteLine(Joe.getHP());
            Joe.hurt(Bob);
            Console.WriteLine(Joe.getHP());
            Console.ReadKey();
        }
    }
}

