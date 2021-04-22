using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maisons_rule
{
    class neigbour
    {
        public int destination;
        public int cost;
        public neigbour next;
        public neigbour(int value,int cost)
        {
            this.destination = value;
            this.cost = cost;
            this.next = null;
        }
    }
}
