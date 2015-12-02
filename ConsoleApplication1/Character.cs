using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    [Serializable]
    public class Character
    {
        private string name;
        private int maxHP, maxMP, HP, MP;
        private double strength, toughnes, manaGen;

        private Weapon weapon;
        private Armor armor;

        //Constructors
        public Character() {
            this.name = "Jerk";
        }

        public Character(string name, int maxHP, int maxMP, double toughnes, double strength, double manaGen, Weapon weapon, Armor armor) {
            this.name = name;
            this.maxHP = maxHP;
            this.maxMP = maxMP;
            this.toughnes = toughnes;
            this.strength = strength;
            this.manaGen = manaGen;
            this.weapon = weapon;
            this.armor = armor;
            this.HP = maxHP;
            this.MP = maxMP;
        }

        //Getters
        public string getName(){return name;}
        public int getMP() {return MP;}
        public int getHP() { return HP; }
        public double getStrength() { return strength; }
        public Weapon getWeapon() { return weapon; }

        //Heal by points
        private void heal(int value)
        {
            this.HP += value;
            if (this.HP > this.maxHP)
                this.HP = this.maxHP;
        }

        //Heal by percentage
        private void heal(float percent)
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
            int physDamage = (int) (character.getWeapon().getPhysicalDamage() * character.getStrength());
            int magDamage = (int) (character.getWeapon().getMagicDamage() * character.getStrength());
            int physResist = (int) (this.armor.getPhysicalResist() * this.toughnes);
            int magResist = (int) (this.armor.getMagicResist() * this.toughnes);

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
    }
}
