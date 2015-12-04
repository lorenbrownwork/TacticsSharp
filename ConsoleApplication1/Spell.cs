using System;
using System.Collections.Generic;
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

        public int getDamage() { return damage; }
        public string getType() { return type; }
        public SpellEffect getEffect() { return effect; }
    }
}
