using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcSimple
{
    public partial class CalcSimple : Form
    {
        public CalcSimple()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            this.BackColor = Color.LightSeaGreen;

            string buttonName = null;
            Button button = null;
            for (int i=0; i<10; i++)
            {
                buttonName = "button" + i;
                button = (Button)this.Controls[buttonName];
                button.Text = i.ToString();
            }
           
        }
    }

}
