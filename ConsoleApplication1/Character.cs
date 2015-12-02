using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Character
    {
        private string name;
        private int maxHP, maxMP, HP, MP, physDamage, magDamage, physResist, magResist;
        private double strength, toughness, manaGen;

        private Weapon weapon;
        private Armor armor;

        //Constructor
        public Character(string name, int maxHP, int maxMP, double toughness, double strength, double manaGen, Weapon weapon, Armor armor) {
            this.name = name;
            this.maxHP = maxHP;
            this.maxMP = maxMP;
            this.toughness = toughness;
            this.strength = strength;
            this.manaGen = manaGen;
            this.weapon = weapon;
            this.armor = armor;
            this.HP = maxHP;
            this.MP = maxMP;

            //setting attack values on construction
            this.physDamage = (int)(this.weapon.getPhysicalDamage() * this.strength);
            this.magDamage = (int)(this.weapon.getMagicDamage() * this.strength);

            //setting resists on construction
            this.physResist = (int)(this.armor.getPhysicalResist() * this.toughness);
            this.magResist = (int)(this.armor.getMagicResist() * this.toughness);
        }

        //Getters
        public string getName(){return name;}
        public int getMP() {return MP;}
        public int getHP() { return HP; }
        public double getStrength() { return strength; }
        public Weapon getWeapon() { return weapon; }

        //Equipment changers
        public void changeWeapon(Weapon newWeapon)
        {
            //setting the character's new weapon
            this.weapon = newWeapon;

            //setting the new attack values
            this.physDamage = (int)(this.weapon.getPhysicalDamage() * this.strength);
            this.magDamage = (int)(this.weapon.getMagicDamage() * this.strength);
        }

        public void changeArmor(Armor newArmor)
        {
            //setting the character's new armor
            this.armor = newArmor;

            //setting the new resists
            this.physResist = (int)(this.armor.getPhysicalResist() * this.toughness);
            this.magResist = (int)(this.armor.getMagicResist() * this.toughness);
        }

        //Heal by points
        public void heal(int value)
        {
            this.HP += value;
            if (this.HP > this.maxHP)
                this.HP = this.maxHP;
        }

        //Heal by percentage
        public void heal(float percent)
        {
            this.HP += (int)(percent * this.maxHP);
            if (this.HP > this.maxHP)
            {
                this.HP = this.maxHP;
            }
        }

        //Character passed hurts this character
        public void hurt(Character character)
        {
            int physDamage = character.physDamage;
            int magDamage = character.magDamage;
            int physResist = this.physResist;
            int magResist = this.magResist;

            string magicDefType = this.armor.getMagicType();
            string magicAtcType = character.getWeapon().getMagicType();

            string physDefType = this.armor.getPhysicalType();
            string physAtcType = character.getWeapon().getPhysicalType();

            //cout << "Magic Damage Before Armor: " << magDamage << endl;

            //Modify Damage
            if (0 == string.Compare(magicDefType, magicAtcType))
            { //Matching
                magDamage = (int)(magDamage * .5);
            }
            else if (0 == string.Compare(magicDefType, "Ice"))
            {
                if (0 == string.Compare(magicAtcType, "Electric"))
                    magDamage = (int)(magDamage * 1.5);
            }
            else if (0 == string.Compare(magicDefType, "Fire"))
            {
                if (0 == string.Compare(magicAtcType, "Ice"))
                    magDamage = (int)(magDamage * 1.5);
            }
            else if (0 == string.Compare(magicDefType, "Electric"))
            {
                if (0 == string.Compare(magicAtcType, "Fire"))
                    magDamage = (int)(magDamage * 1.5);
            }

            //cout << "Magic Damage After Armor: " << magDamage << endl;
            //cout << "Magic Damage After Resist: " << magDamage - magResist << endl;

            int actDamage = (physDamage - physResist) + (magDamage - magResist);

            //cout << actDamage << endl;

            //prevent Negative Health
            if (actDamage > 0)
            {
                if (this.HP > actDamage)
                    this.HP -= actDamage;
                else
                    this.HP = 0;
            }
        }

        //hurting the character being passed in
        public void altHurt(Character character)
        {
            //local variable to calc magic damage
            int magDamage = this.magDamage;

            //types of damage and resist
            //probably remove magic from most weapons
            string magicAtkType = weapon.getMagicType();
            string magicResType = character.armor.getMagicType();

            //check if magic resistances/weaknesses make sense
            if (magicAtkType.Equals(magicResType))
            {
                magDamage = (int)(magDamage * .5);
            }
            else if (magicAtkType.Equals("Fire") && magicResType.Equals("Electric"))
            {
                magDamage = (int)(magDamage * 1.5);
            }
            else if (magicAtkType.Equals("Ice") && magicResType.Equals("Fire"))
            {
                magDamage = (int)(magDamage * 1.5);
            }
            else if (magicAtkType.Equals("Electric") && magicResType.Equals("Ice"))
            {
                magDamage = (int)(magDamage * 1.5);
            }

            //calc total damage
            int totalDam = (this.physDamage - character.physResist) + (magDamage - character.magResist);

            //making sure HP doesn't go negative
            if (totalDam > 0)
            {
                if (character.HP > totalDam)
                {
                    character.HP -= totalDam;
                }
                else
                {
                    character.HP = 0;
                }

            }
        }
    }
}
