using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace maisons_rule
{
    public partial class Form1 : Form
    {
        traceforwordpath[] allloops = new traceforwordpath[20];
        int size_of_allloops = 0;
        traceforwordpath[] allforwordpaths = new traceforwordpath[20];
        int size_of_allforwordpaths = 0;
        Bitmap bm;
        mygragh myproblem = new mygragh();
        Rectangle[] recarray = new Rectangle[30];
        int recarrayindex = 0;
        int index = 0;
        int x = 2;
        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(panel2.Width, panel2.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            scroll_panel.scrollright(panel2, x * 10);
            Rectangle rec = new Rectangle(x * 10, 50, 15, 15);
            recarray[recarrayindex++] = rec;
            myproblem.addnode(index++);
            using (Graphics bfrgrph = Graphics.FromImage(bm))
            {
                bfrgrph.DrawEllipse(System.Drawing.Pens.Black, rec);
                bfrgrph.DrawString(string.Format("{0}", index - 1),DefaultFont, Brushes.Aqua, new PointF(x * 10 + 3, 52));
                x += 4;
            }
            panel2.Invalidate();
            myproblem.printallvalues();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int source = int.Parse(textBox1.Text);
            int destination = int.Parse(textBox2.Text);
            int cost = int.Parse(textBox3.Text);
            bool check=myproblem.checknodeexist(source);
            if(source<destination)
            {//forword
                using (Graphics bfrgrph = Graphics.FromImage(bm))
                {
                    Point[] points = new Point[3];
                    points[0] = new Point(recarray[source].Location.X+5, 60);
                    points[2] = new Point(recarray[destination].Location.X + 5, 60);
                    points[1] = new Point((points[0].X+points[2].X)/2, 80);
                    bfrgrph.DrawCurve(Pens.Black, points);
                    bfrgrph.DrawString(string.Format("{0}", cost), DefaultFont, Brushes.Aqua, points[1]);
                }
            }
            else
            {//backword
                using (Graphics bfrgrph = Graphics.FromImage(bm))
                {
                    Point[] points = new Point[3];
                    points[0] = new Point(recarray[source].Location.X + 5, 50);
                    points[2] = new Point(recarray[destination].Location.X + 5, 50);
                    points[1] = new Point((points[0].X + points[2].X) / 2, 30);
                    bfrgrph.DrawCurve(Pens.Black, points);
                    bfrgrph.DrawString(string.Format("{0}", cost), DefaultFont, Brushes.Aqua, points[1]);
                }
            }
            Console.WriteLine("check = {0}", check);
            if (check == false)
            {
                myproblem.addnode(source);
                Console.WriteLine("{0} created", source);
            }
            myproblem.addneighbour(source, destination, cost);
            panel2.Invalidate();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            if (bm == null)
                return;
            e.Graphics.DrawImageUnscaled(bm,Point.Empty);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myproblem.calculateallforwordpaths(allforwordpaths,ref size_of_allforwordpaths);
            Console.WriteLine("i have returned back with this forwordpaths :");
            for (int i = 0; i < size_of_allforwordpaths; i++)
            {
                allforwordpaths[i].printpath();
                Console.WriteLine("and the cost of f.path = {0}", allforwordpaths[i].overallcost);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            myproblem.calculateallloops(allloops,ref size_of_allloops);
            Console.WriteLine("i have returned back with this loops :");
            for (int i = 0; i < size_of_allloops; i++)
            {
                allloops[i].printpath();
                Console.WriteLine("and the cost of loop = {0}", allloops[i].overallcost);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            myproblem.calculateallforwordpaths(allforwordpaths, ref size_of_allforwordpaths);
            Console.WriteLine("i have returned back with this forwordpaths :");
            for (int i = 0; i < size_of_allforwordpaths; i++)
            {
                allforwordpaths[i].printpath();
                Console.WriteLine("and the cost of f.path = {0}", allforwordpaths[i].overallcost);
            }


            myproblem.calculateallloops(allloops, ref size_of_allloops);////then remove repeatd loops
            traceforwordpath.remove_repeated_loops(allloops, size_of_allloops);
            Console.WriteLine("i have returned back with this loops :");
            for (int i = 0; i < size_of_allloops; i++)
            {
                if(allloops[i].loop_valid==1)
                {
                    allloops[i].printpath();
                    Console.WriteLine("and the cost of loop = {0}", allloops[i].overallcost);
                }
            }



            calculations mycalculations = new calculations(allforwordpaths, size_of_allforwordpaths, allloops, size_of_allloops);
            string text;
            print_everything(out text,mycalculations.result());
            Form2 f = new Form2();
            f.mylabel2 = text;
            if (f.ShowDialog()==DialogResult.OK)
            {
                if (string.Compare(f.mytextbox1,"clear") == 0)
                {
                    Console.WriteLine("entered new form");
                    this.Hide();
                    (new Form1()).Show();
                }
            }
        }
        public void print_everything(out string s,Double result)
        {
            s = "forwordpaths\n";
            for(int i=0; i<size_of_allforwordpaths;i++)
            {
                for(int j=0; j<allforwordpaths[i].index;j++)
                {
                    s += string.Format("{0}->", allforwordpaths[i].values[j]);
                }
                s += "       and its cost = " + allforwordpaths[i].overallcost.ToString();
                s += "\n";
            }
            s += "\n";
            s += "loops:\n";
            for(int i=0; i<size_of_allloops;i++)
            {
                if(allloops[i].loop_valid==0)
                {
                    continue;
                }
                for(int j=0; j<allloops[i].index;j++)
                {
                    s += string.Format("{0}->", allloops[i].values[j]);
                }
                s += "       and its cost = " + allloops[i].overallcost.ToString();
                s += "\n";
            }
            s += "\n";
            s += "and finally result = : \n";
            s += result.ToString();
        }
    }
}
