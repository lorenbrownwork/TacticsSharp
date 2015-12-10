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
        private int maxHP, maxMP, HP, MP, physDamage, magDamage, physResist, magResist;
        private double strength, toughness, manaGen;

        private List<SpellEffect> activeEffects = new List<SpellEffect>();
        private List<Spell> knownSpells = new List<Spell>();

        private Weapon weapon;
        private Armor armor;

        //Default Constructor (Build a character)
        public Character()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Character Builder!");
            Console.Write("    Please Select a name: ");
            this.name = Console.ReadLine();
            Console.WriteLine("    Max HP: 100");
            Console.WriteLine("    Max MP: 100");
            Console.WriteLine("    Physical Damage: 20");
            Console.WriteLine("    Magic Damage: 20");
            Console.WriteLine("    Physical Resist: 10");
            Console.WriteLine("    Magic Resist: 10");
            Console.WriteLine("    Stregth Multiplier: 1.0");
            Console.WriteLine("    Toughness Multiplier: 1.0");
            Console.WriteLine("    ManaGen Multiplier: 1.0");
            Console.WriteLine("    Weapon: Rusty Sword");
            Console.WriteLine("    Armor: Wooden Sheild");
            Console.WriteLine("");
            Console.WriteLine("    Press Any key to continue...");
            //Console.ReadLine(); //Wait
            Console.Clear();

            maxHP = 100;
            maxMP = 100;
            HP = 100;
            MP = 100;
            physDamage = 20;
            magDamage = 20;
            physResist = 10;
            magResist = 10;
            strength = 1.0;
            toughness = 1.0;
            manaGen = 1.0;
            HP = maxHP;
            MP = maxMP;
            weapon = new Weapon("Rusty Sword", 20, "Normal", 0, "None");
            armor = new Armor("Wooden Sheild", 10, "Normal", 0, "None");
        }

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
        public void Attack(Character character)
        {
            //local variable to calc magic damage
            int magDamage = this.magDamage;

            
            //type of damage
            //probably remove magic from most weapons
            string magicAtkType = weapon.getMagicType();

            magDamage = checkResist(magDamage, magicAtkType, character.armor.getMagicType());

            //calc total damage
            int totalDam = (this.physDamage - character.physResist) + (magDamage - character.magResist);

            takeDamage(character, totalDam);
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
