using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TacticsSharp
{
    [Serializable]
    public class SpellEffect
    {
        private static int counter = 0;

        private string name, type;
        private int damage, id, duration;

        public SpellEffect(string name, string type, int damage, int duration)
        {
            this.name = name;
            this.type = type;
            this.damage = damage;
            this.duration = duration;
            this.id = counter;
            Interlocked.Increment(ref counter);
        }
    }
}
