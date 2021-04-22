using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maisons_rule
{
    class node
    {
        public int value;
        public neigbour nextneigbour;
        public node nextnode;
        public node(int value)
        {
            this.value = value;
            this.nextneigbour = null;
            this.nextnode = null;
        }
        public node(int value,neigbour nextneigbour,node nextnode)
        {
            this.value = value;
            this.nextneigbour = nextneigbour;
            this.nextnode = nextnode;
        }
    }
}
