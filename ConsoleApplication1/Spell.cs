using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Spell
    {
        public int ID { get; set; }
        private static int counter = 0;

        private string name, type;
        private int cost, damage, effect, id;

        public Spell(string name, int cost, int damage, string type, int effect)
        {
            this.name = name;
            this.cost = cost;
            this.damage = damage;
            this.type = type;
            this.effect = effect;
            this.id = ID;
        }

        public void increment()
        {
            this.ID = Interlocked.Increment(ref counter);
        }
    }
}
