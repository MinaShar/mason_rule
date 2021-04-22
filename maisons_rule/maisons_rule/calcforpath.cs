using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maisons_rule
{
    class calcforpath
    {
        public int nodereached;
        public int costtillnow;
        public neigbour valuebeingprogressed;
        public calcforpath(int nodereached,int costtillnow,neigbour value)
        {
            this.nodereached = nodereached;
            this.costtillnow = costtillnow;
            this.valuebeingprogressed = value;
        }
        public calcforpath copy()
        {
            return new calcforpath(this.nodereached, this.costtillnow, this.valuebeingprogressed);
        }
    }
}
