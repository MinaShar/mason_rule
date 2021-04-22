using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maisons_rule
{
    class calculations
    {
        traceforwordpath[] allloops = new traceforwordpath[20];
        int size_of_allloops;
        traceforwordpath[] allforwordpaths = new traceforwordpath[20];
        int size_of_allforwordpaths;
        public calculations(traceforwordpath[] fpaths,int soffpaths,traceforwordpath[] alops,int sofloops)
        {
            this.allforwordpaths = fpaths;
            this.allloops = alops;
            this.size_of_allforwordpaths = soffpaths;
            this.size_of_allloops = sofloops;
        }
        public double result()
        {
            return calculate_numerator() / calculate_denomenator();
        }
        public double calculate_numerator()
        {
            double total_gain = 0;
            for(int i=0; i<size_of_allforwordpaths;i++)
            {
                total_gain += allforwordpaths[i].overallcost * (1 - get_cost_non_touching_loops(allforwordpaths[i].values, allforwordpaths[i].index, allloops));
            }
            return total_gain;
        }
        public double calculate_denomenator()
        {
            return 1 - get_gain_all_loops() + get_cost_of_two_nontouching_loops();
        }
        public double get_gain_all_loops()
        {
            double total_cost = 0;
            for(int i=0; i<size_of_allloops;i++)
            {
                if(allloops[i].loop_valid==0)
                {
                    continue;
                }
                total_cost += allloops[i].overallcost;
            }
            return total_cost;
        }
        public double get_cost_of_two_nontouching_loops()
        {
            bool flag = true;
            double total_cost = 0;
            List<KeyValuePair<int, int>> pairs = new List<KeyValuePair<int, int>>();
            for(int i=0; i<size_of_allloops;i++)
            {
                for(int j=0; j<size_of_allloops;j++)
                {
                    foreach(KeyValuePair<int,int> x in pairs)
                    {
                        if (x.Key == i && x.Value == j || x.Key == j && x.Value == i)
                        {
                            flag = false;
                        }
                    }
                    if(allloops[i].loop_valid==1 && allloops[j].loop_valid==1)
                    {
                        if (test_touching(allloops[i].values, allloops[i].index, allloops[j].values, allloops[j].index) == false && flag == true)
                        {
                            total_cost = total_cost + allloops[i].overallcost * allloops[j].overallcost;
                            pairs.Add(new KeyValuePair<int, int>(i, j));
                        }
                        flag = true;
                    }
                }
            }
            return total_cost;
        }
        public double get_cost_non_touching_loops(int[] forwordpath,int size_of_forwordpath,traceforwordpath[] allloopsihave)
        {
            double costtillnow = 0;
            for(int i=0; i<size_of_allloops;i++)
            {
                if(allloops[i].loop_valid==0)
                {
                    continue;
                }
                if(test_touching(forwordpath,size_of_forwordpath,allloopsihave[i].values,allloopsihave[i].index)==false)
                {//non-touching loop of forword path
                    costtillnow += allloopsihave[i].overallcost;
                }
            }
            return costtillnow;
        }
        public bool test_touching(int[] array1,int index1,int[] array2,int index2)
        {
            for(int i=0; i<index1;i++)
            {
                for(int j=0; j<index2;j++)
                {
                    if (array1[i] == array2[j])
                        return true;
                }
            }
            return false;
        }
    }
}
