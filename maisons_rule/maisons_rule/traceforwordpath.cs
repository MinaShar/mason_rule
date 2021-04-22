using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maisons_rule
{
    class traceforwordpath
    {
        public int loop_valid = 1;
        public int[] values=new int[100];
        public int index = 0;
        public int overallcost;
        public void addnode(int value)
        {
            this.values[index++] = value;
        }
        public traceforwordpath copy()
        {
            traceforwordpath z = new traceforwordpath();
            z.index = this.index;
            int[] newarray = new int[100];
            for(int i=0; i<index;i++)
            {
                newarray[i] = this.values[i];
            }
            z.values = newarray;
            return z;
        }
        public void removelastnode()
        {
            index--;
        }
        public bool testforloop(int n,out int not_loop)
        {
            if (n == values[0])
            {
                not_loop = 0;
                return true;
            }
            for(int i=0; i<index;i++)
            {
                for(int j=0; j<index;j++)
                {
                    if (i == j)
                        continue;
                    if(values[i]==values[j])
                    {
                        not_loop = 1;
                        return true;
                    }
                }
            }
            not_loop = 0;
            return false;
        }
        public int test_if_two_loos_equale(traceforwordpath t)
        {
            for(int i=0; i<index;i++)
            {
                if(values[i]==t.values[0])
                {
                    int j = 1;
                    while(j<t.index-1 && values[(i+j)%(t.index-1)]==t.values[j])
                    {
                        j++;
                    }
                    if(j==t.index-1 && j==this.index-1)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
        public void printpath()
        {
            if (loop_valid == 0)
            {
                return;
            }
            Console.WriteLine();
            for (int i = 0; i < index; i++)
            {
                Console.Write("->{0}", values[i]);
            }
            Console.WriteLine();
        }
        public static void remove_repeated_loops(traceforwordpath[] t, int size_array)
        {
            for (int i = 0; i < size_array; i++)
            {
                if (t[i].loop_valid == 0)
                    continue;
                for (int j = i+1; j < size_array; j++)
                {
                    if (t[i].test_if_two_loos_equale(t[j]) == 1)
                    {
                        t[j].loop_valid = 0;
                    }
                }
            }
        }
    }
}
