using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsCore
{
    public class SmartButton : Button
    {
        public SmartButton() : base()
        {
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.DodgerBlue;
            ForeColor = Color.White;
            FlatAppearance.BorderSize = 0;
            Font = new Font("Segoe UI", 9, FontStyle.Bold);
        }
    }
}
