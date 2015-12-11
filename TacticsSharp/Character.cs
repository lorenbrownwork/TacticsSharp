using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacticsSharp
{
    [Serializable]
    public class Character
    {
        private string name;
        private int maxHP, maxMP, HP, MP, XP, physDamage, magDamage, physResist, magResist, level;
        private double manaGen, critChance;
        public int str, dex, con, intel, wis;

        private double hpMultiplier = 4.0;
        private double mpMultiplier = 2.0;
        private double manaGenMultiplier = 1.0;

        private List<SpellEffect> activeEffects = new List<SpellEffect>();
        private List<Spell> knownSpells = new List<Spell>();

        private Weapon weapon;
        private Armor armor;

        //Default Constructor (Build a character)
        public Character(CharacterClass charClass)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Character Builder!");
            Console.Write("    Please Select a name: ");
            this.name = Console.ReadLine();
            //Console.ReadLine(); //Wait
            Console.Clear();

            str = charClass.str;
            dex = charClass.dex;
            con = charClass.con;
            intel = charClass.intel;
            wis = charClass.wis;

            this.maxHP = (int)((double)con * hpMultiplier);
            this.maxMP = (int)((double)intel * mpMultiplier);
            this.manaGen = (int)((double)wis * manaGenMultiplier);
            weapon = new Weapon("Rusty Sword", 20, "Normal", 0, "None");
            armor = new Armor("Wooden Sheild", 10, "Normal", 0, "None");
            this.HP = maxHP;
            this.MP = maxMP;

            //setting attack values on construction
            this.physDamage = (int)(this.weapon.getPhysicalDamage() + this.str);

            //setting resists on construction
            this.physResist = (int)(this.armor.getPhysicalResist() + this.str);
            this.magResist = (int)(this.armor.getMagicResist() + this.wis);
        }

        //Constructor
        public Character(string name, int str, int dex, int con, int intel, int wis, Weapon weapon, Armor armor) {
            this.name = name;
            this.str = str;
            this.dex = dex;
            this.con = con;
            this.intel = intel;
            this.wis = wis;

            this.maxHP = (int)((double)con * hpMultiplier);
            this.maxMP = (int)((double)intel * mpMultiplier);
            this.manaGen = (int)((double)wis * manaGenMultiplier);
            this.weapon = weapon;
            this.armor = armor;
            this.HP = maxHP;
            this.MP = maxMP;

            //setting attack values on construction
            this.physDamage = (int)(this.weapon.getPhysicalDamage() + this.str);

            //setting resists on construction
            this.physResist = (int)(this.armor.getPhysicalResist() + this.str);
            this.magResist = (int)(this.armor.getMagicResist() + this.wis);
        }

        //Getters
        public string getName(){return name;}
        public int getMP() {return MP;}
        public int getHP() { return HP; }
        public double getStrength() { return str; }
        public Weapon getWeapon() { return weapon; }

        //Equipment changers
        public void changeWeapon(Weapon newWeapon)
        {
            //setting the character's new weapon
            this.weapon = newWeapon;

            //setting the new attack values
            this.physDamage = (int)(this.weapon.getPhysicalDamage() * this.str);
        }

        public void changeArmor(Armor newArmor)
        {
            //setting the character's new armor
            this.armor = newArmor;

            //setting the new resists
            this.physResist = (int)(this.armor.getPhysicalResist() + this.str);
            this.magResist = (int)(this.armor.getMagicResist() * this.wis);
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
        /*public void hurt(Character character)
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

            if (physResist > physDamage)
            {
                physResist = physDamage;
            }

            if (magResist > magDamage)
            {
                magResist = magDamage;
            }

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
        }*/

        //hurting the character being passed in
        public void Attack(Character character)
        {
            //local variable to calc magic damage
            int magDamage = this.magDamage;
            int magResist = character.magResist;

            
            //type of damage
            //probably remove magic from most weapons
            string magicAtkType = weapon.getMagicType();

            magDamage = checkResist(magDamage, magicAtkType, character.armor.getMagicType());

            //ensure that you aren't doing negative damage
            if (physResist > physDamage)
            {
                physResist = physDamage;
            }

            if (magResist > magDamage)
            {
                magResist = magDamage;
            }

            //calc total damage
            int totalDam = (this.physDamage - character.physResist) + (magDamage - magResist);

            takeDamage(character, totalDam);
            if (character.getHP() == 0)
            {

            }
                    
        }

        //single target and AoE?
        //won't need a character in the latter case
        public void CastSpell(Character character, Spell spell)
        {
            int damage = spell.getDamage();
            string type = spell.getType();

            damage = checkResist(damage, type, character.armor.getMagicType());

            int totalDam = damage - character.magResist;

            if (spell.getEffect() != null)
            {
                character.activeEffects.Add(spell.getEffect());
            }

            takeDamage(character, totalDam);
        }

        public void LearnSpell(Spell spell)
        {
            bool alreadyInList = knownSpells.Contains(spell);

            if (alreadyInList)
            {
                Console.WriteLine("You already know that spell!");
            }
            else
            {
                knownSpells.Add(spell);
                Console.WriteLine("Spell learned.");
            }
        }

        public void takeDamage(Character character, int damage)
        {
            //making sure HP doesn't go negative
            if (damage > 0)
            {
                if (character.HP > damage)
                {
                    character.HP -= damage;
                }
                else
                {
                    character.HP = 0;
                }
            }
        }

        public int checkResist(int damage, string atkType, string defType)
        {
            //check if magic resistances/weaknesses make sense
            if (atkType.Equals(defType))
            {
                damage = (int)(damage * .5);
            }
            else if (atkType.Equals("Fire") && defType.Equals("Electric"))
            {
                damage = (int)(magDamage * 1.5);
            }
            else if (atkType.Equals("Ice") && defType.Equals("Fire"))
            {
                damage = (int)(magDamage * 1.5);
            }
            else if (atkType.Equals("Electric") && defType.Equals("Ice"))
            {
                damage = (int)(magDamage * 1.5);
            }

            return damage;
        }
    }
}
