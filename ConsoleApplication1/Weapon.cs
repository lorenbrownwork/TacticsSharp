using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Weapon
    {
        private string name, physicalType, magicType;
        private int physicalDamage, magicDamage;

        //Constructor
        public Weapon(string name, int physicalDamage, string physicalType, int magicDamage, string magicType)
        {
            this.name = name;
            this.physicalDamage = physicalDamage;
            this.physicalType = physicalType;
            this.magicDamage = magicDamage;
            this.magicType = magicType;
        }

        //Getters
        public string getName() { return name; }
        public string getPhysicalType() { return physicalType; }
        public string getMagicType() { return magicType; }
        public int getMagicDamage() { return magicDamage; }
        public int getPhysicalDamage() { return physicalDamage; }

    }
}
