using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TacticsSharp
{
    [Serializable]
    public class Spell
    {
        private static int counter = 0;

        private string name, type;
        private int cost, damage, id;
        private SpellEffect effect;

        public Spell(string name, int cost, int damage, string type, SpellEffect effect)
        {
            this.name = name;
            this.cost = cost;
            this.damage = damage;
            this.type = type;
            this.effect = effect;
            this.id = counter;
            Interlocked.Increment(ref counter);
        }
        public Spell(string name, int cost, int damage, string type)
        {
            this.name = name;
            this.cost = cost;
            this.damage = damage;
            this.type = type;
            this.effect = null;
            this.id = counter;
            Interlocked.Increment(ref counter);
        }

        public string getName() { return name; }
        public int getDamage() { return damage; }
        public string getType() { return type; }
        public SpellEffect getEffect() { return effect; }

        public static List<Spell> ImportSpells(string path, List<Spell> spellList, List<SpellEffect> effectList)
        {
            var reader = new StreamReader(File.OpenRead(path));

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] vals = line.Split(',');
                if (vals.Length == 5)
                {
                    spellList.Add(new Spell(vals[0],  Int32.Parse(vals[1]), Int32.Parse(vals[2]), vals[3]));
                }
                else
                {
                    SpellEffect effect = effectList.Find(x => x.GetId() == Int32.Parse(vals[5]));
                    spellList.Add(new Spell(vals[0], Int32.Parse(vals[1]), Int32.Parse(vals[2]), vals[3], effect));
                }
            }

            return spellList;
        }
    }
}
