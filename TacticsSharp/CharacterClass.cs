using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    public class CharacterClass
    {
        private string name;
        private int maxHP, maxMP, HP, MP, physDamage, magDamage, physResist, magResist;
        public int str, dex, con, intel, wis;
        private double manaGen, critChance;

        private double hpMultiplier = 2.0;
        private double mpMultiplier = 2.0;
        private double manaGenMultiplier = 1.0;

        private List<Spell> knownSpells = new List<Spell>();

        private Weapon weapon;
        private Armor armor;

        public CharacterClass(string name, int str, int dex, int con, int intel, int wis)
        {
            Armor WoodenShield = new Armor("Wooden Sheild", 10, "Normal", 10, "Fire");
            Weapon RustySword = new Weapon("Rusty Sword", 30, "Normal", 40, "Fire");

            this.name = name;
            this.str = str;
            this.dex = dex;
            this.con = con;
            this.intel = intel;
            this.wis = wis;

            this.maxHP = (int)((double)con * hpMultiplier);
            this.maxMP = (int)((double)intel * mpMultiplier);
            this.manaGen = (int)((double)wis * manaGenMultiplier);
            this.weapon = RustySword;
            this.armor = WoodenShield;
            this.HP = maxHP;
            this.MP = maxMP;

            //setting attack values on construction
            this.physDamage = (int)(this.weapon.getPhysicalDamage() + this.str);

            //setting resists on construction
            this.physResist = (int)(this.armor.getPhysicalResist() + (this.str/2));
            this.magResist = (int)(this.armor.getMagicResist() + this.wis);
        }
    }

}
