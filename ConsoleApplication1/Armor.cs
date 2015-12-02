using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class Armor
    {
        private string name, physicalType, magicType;
        private int physicalResist, magicResist;

        //Constructor
        public Armor(string name, int physicalResist, string physicalType, int magicResist, string magicType)
        {
            this.name = name;
            this.physicalResist = physicalResist;
            this.physicalType = physicalType;
            this.magicResist = magicResist;
            this.magicType = magicType;
        }

        //getters
        public string getName() { return name; }
        public int getPhysicalResist() { return physicalResist; }
        public string getPhysicalType() { return physicalType; }
        public int getMagicResist() { return magicResist; }
        public string getMagicType() { return magicType; }
    }
}
