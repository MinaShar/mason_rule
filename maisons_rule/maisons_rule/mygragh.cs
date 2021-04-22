using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace maisons_rule
{
    class mygragh
    {
        node head;
        int lastnode;
        public mygragh()
        {
            head = null;
        }
        public void addnode(int newvalue)
        {
            lastnode = newvalue;
            node current = head;
            if(current==null)
            {
                head = new node(newvalue);
                Console.WriteLine("{0} node created head", newvalue);
                return;
            }
            while (current.nextnode != null)
            {
                current = current.nextnode;
            }
            current.nextnode = new node(newvalue);
            Console.WriteLine("{0} node created", newvalue);
        }
        public void addneighbour(int valuesource,int valuedestination,int cost)
        {
            node current = head;
            neigbour myneibour = null;
            for (; current.value != valuesource; current = current.nextnode) ;
            Console.WriteLine("i arrived to {0}(source)", current.value);
            myneibour = current.nextneigbour;
            if(current.nextneigbour == null)
            {
                current.nextneigbour = new neigbour(valuedestination, cost);
                Console.WriteLine("i added {0}(destination) and cost = {1}",current.nextneigbour.destination, current.nextneigbour.cost);
                return;
            }
            while(myneibour.next!=null)
            {
                myneibour = myneibour.next;
            }
            myneibour.next = new neigbour(valuedestination, cost);
            Console.WriteLine("i added {0}(destination) and cost = {1}", myneibour.next.destination, myneibour.next.cost);
        }
        public bool checknodeexist(int value)
        {
            node current = head;
            if(current==null)
            {
                return false;
            }
            while (current.nextnode != null && current.value != value)
                current = current.nextnode;
            if (current.value == value)
                return true;
            return false;
        }
        public void printallvalues()
        {
            node current = head;
            while(current!=null)
            {
                neigbour currneighbour = current.nextneigbour;
                while(currneighbour!=null)
                {
                    Console.WriteLine("{0} connected to {1} with cost {2}", current.value, currneighbour.destination, currneighbour.cost);
                    currneighbour = currneighbour.next;
                }
                current = current.nextnode;
            }
        }
        public void calculateallforwordpaths(traceforwordpath [] forwordpaths,ref int size)
        {
            traceforwordpath t = new traceforwordpath();
            calculatecurrentpath(head, new calcforpath(0, 1, head.nextneigbour),t,forwordpaths,ref size);
        }
        public void calculatecurrentpath(node equivelant,calcforpath c,traceforwordpath t,traceforwordpath[] forwordpaths,ref int size)
        {
            node current = equivelant;
            while(c.nodereached!=lastnode)
            {
                int searchforit = c.valuebeingprogressed.destination;
                if(c.valuebeingprogressed.next!=null)
                {
                    calcforpath e = new calcforpath(c.nodereached, c.costtillnow, c.valuebeingprogressed.next);
                    traceforwordpath z = t.copy();
                    calculatecurrentpath(current, e, z,forwordpaths,ref size);
                }
                if (c.valuebeingprogressed.destination <= c.nodereached)
                    return;
                while (current.nextnode!=null&&current.value!=searchforit)
                {
                    current = current.nextnode;
                }
                if(current.value==searchforit)
                {
                    t.addnode(c.nodereached);
                    c.nodereached = current.value;
                    c.costtillnow *= c.valuebeingprogressed.cost;
                    c.valuebeingprogressed = current.nextneigbour;
                }
            }
            t.addnode(c.nodereached);
            if(c.nodereached==lastnode)
            {
                Console.WriteLine("forword path value = {0}", c.costtillnow);
                for(int i=0;i<t.index ;i++)
                {
                    Console.Write("{0} -> ", t.values[i]);
                }
                forwordpaths[size] = t.copy();
                forwordpaths[size++].overallcost = c.costtillnow;
            }
        }
        public void calculateallloops(traceforwordpath[] allloops,ref int size)
        {
            node current = head;
            traceforwordpath t = new traceforwordpath();
            calculateloop(current, current.nextneigbour, new calcforpath(0, 1, current.nextneigbour), t,allloops,ref size,current);
            while (current.nextnode != null)
            {
                current = current.nextnode;
                traceforwordpath nt = new traceforwordpath();
                calculateloop(current, current.nextneigbour, new calcforpath(0, 1, current.nextneigbour), nt, allloops, ref size,current);
            }
        }
        public void calculateloop(node begin,neigbour end,calcforpath c,traceforwordpath t, traceforwordpath[] allloops, ref int size,node begin_node)
        {
            int not_loop;
            if (end == null)
                return;
            t.addnode(begin.value);
            if(t.testforloop(end.destination,out not_loop))
            {
                if (not_loop == 1)
                    return;
                c.costtillnow *= end.cost;
                t.addnode(end.destination);
                Console.WriteLine("thats a loop :");
                t.printpath();
                Console.WriteLine("cost of the loop = {0}", c.costtillnow);
                allloops[size] = t.copy();
                allloops[size++].overallcost = c.costtillnow;
            }
            node currentnode = begin;
            neigbour currentneigbour = end;
            if(currentneigbour.next!=null)
            {
                traceforwordpath z = t.copy();
                z.removelastnode();
                calculateloop(currentnode, currentneigbour.next,c.copy(), z,allloops,ref size,currentnode);
            }
            node search = currentnode;
            if (currentnode.value == currentneigbour.destination)
            {
                return;
            }
            while (search.value!=currentneigbour.destination&&search.nextnode!=null)
            {
                search = search.nextnode;
            }
            if (search.value == currentneigbour.destination)
            {
                c.costtillnow *= c.valuebeingprogressed.cost;
                c.nodereached = search.value;
                c.valuebeingprogressed = search.nextneigbour;
                calculateloop(search, search.nextneigbour, c, t, allloops, ref size, search);
            }
            if (search.nextnode==null)
            {
                search = head;
                while (search.value != currentneigbour.destination && search.nextnode != currentnode)
                {
                    search = search.nextnode;
                }
                if (search.value == currentneigbour.destination)
                {
                    c.costtillnow *= c.valuebeingprogressed.cost;
                    c.nodereached = search.value;
                    c.valuebeingprogressed = search.nextneigbour;
                    calculateloop(search, search.nextneigbour, c, t, allloops, ref size, search);
                }
                if (search.nextnode==currentnode)
                {
                    return;
                }
            }
        }
    }
}
