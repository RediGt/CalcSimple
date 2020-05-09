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
                //button.Font = new Font("Tahome", 22f);
            }
           
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Display.Text += button.Text;
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            bool weHaveDot = Display.Text.Contains(".");
            if (!weHaveDot) 
            {  
                if (Display.Text == "")
            {
                Display.Text += "0.";
            }
            else
                Display.Text += buttonDecimal.Text;
            }
          
            
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
                string s = Display.Text;
                if (s.Length >= 1)
                {
                    s = s.Substring(0, s.Length - 1);
                }
               // else
               // {
                //    s = "";
                //}

                Display.Text = s;

        }
    }

}
