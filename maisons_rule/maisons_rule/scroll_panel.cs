using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maisons_rule
{
    class scroll_panel
    {
        public static void scrollright(Panel p,int x_offset)
        {
            using (Control c = new Control() { Parent = p, Left = x_offset })
            {
                p.ScrollControlIntoView(c);
            }
        }
    }
}
