using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfExplosions
{
    public class Data
    {
        public int User { get; set; }
        public string Action { get; set; }
        public String Direction { get; set; }

        public Tuple<int, int> Position { get; set; }
    }
}
