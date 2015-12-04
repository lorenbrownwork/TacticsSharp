using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TacticsSharp;

namespace GameTest
{
    [TestClass]
    public class Attack
    {
        Character Joe = new Character("Joe", 100, 20, 2.0, 2.0, 1.0, RustySword, WoodenSheild);
        Character Bob = new Character("Bob", 100, 20, 1.0, 2.0, 1.0, RustySword, WoodenSheild);

        [TestMethod]
        public void Attack()
        {
        }
    }
}
