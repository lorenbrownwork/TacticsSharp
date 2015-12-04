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

        public int GetId() { return id; }

        public static List<SpellEffect> ImportEffects(string path, List<SpellEffect> effectList)
        {
            var reader = new StreamReader(File.OpenRead(path));

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] vals = line.Split(',');
                effectList.Add(new SpellEffect(vals[0], vals[1], Int32.Parse(vals[2]), Int32.Parse(vals[3])));
            }

            return effectList;
        }
    }
}
