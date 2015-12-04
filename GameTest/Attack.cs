using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TacticsSharp;

namespace GameTest
{
    [TestClass]
    public class Attack
    {
        [TestMethod]
        public void testAttack()
        {
            Armor WoodenSheild = new Armor("Wooden Sheild", 10, "Normal", 10, "Fire");
            Weapon RustySword = new Weapon("Rusty Sword", 30, "Normal", 40, "Fire");

            Character Joe = new Character("Joe", 100, 20, 2.0, 2.0, 1.0, RustySword, WoodenSheild);
            Character Bob = new Character("Bob", 100, 20, 1.0, 2.0, 1.0, RustySword, WoodenSheild);
            Bob.Attack(Joe);

            Assert.AreEqual(40, Joe.getHP());
        }
    }
}
