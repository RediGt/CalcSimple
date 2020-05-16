using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcSimple
{
    public partial class CalcSimple : Form
    {

        char decimalSeperator;
        double numOne = 0;
        double numTwo = 0;
        string operation;
        public CalcSimple()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {           
        //READING LOCAL DECIMAL SEPARATOR
            decimalSeperator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            this.BackColor = Color.LightSeaGreen;

        //NUBERING BUTTONS
            string buttonName = null;
            Button button = null;
            for (int i=0; i<10; i++)
            {
                buttonName = "button" + i;
                button = (Button)this.Controls[buttonName];
                button.Text = i.ToString();
                //button.Font = new Font("Tahome", 22f);
                Display.Text = "0";
            }
           
        }

        //READING DIGITS FROM BUTTONS
        private void Button_Click(object sender, EventArgs e)
        {
          
            Button button = (Button)sender;
            if (Display.Text == "0")
            {
                Display.Text = button.Text;
            }
            else if (Display.Text == "-0")
            {
                Display.Text = button.Text;
            }
            else
            {
                Display.Text += button.Text;
            }
        }

        //ENSURE ONE DOT
        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            bool weHaveDot = Display.Text.Contains(decimalSeperator);
            if (!weHaveDot) 
            {  
                if (Display.Text == "")
            {
                Display.Text += "0" + decimalSeperator;
            }
            else
                Display.Text += decimalSeperator;
            }
          
            
        }

        //DELETING THE DIGITS FROM TEXTBOX
        private void buttonBackspace_Click(object sender, EventArgs e)
        {
                string s = Display.Text;
            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "0";
            }    
                Display.Text = s;

        }

        //ADDING MINUS SIGN BEFORE THE NUMBER
        private void buttonSign_Click(object sender, EventArgs e)
        {
           /* string s = Display.Text;
              if (s.Substring(0, 1) != "-")
                  Display.Text = "-" + Display.Text;
              else
                  Display.Text = s.Substring(1, s.Length-1);*/
            try
            {
                double number = Convert.ToDouble(Display.Text);
                number *= -1;
                Display.Text = Convert.ToString(number);
            }
            catch 
            { 
            
            }
        }

        //BY MISTAKE
        private void button10_Click(object sender, EventArgs e)
        {

        }

        //IF OPERATION SING IS PRESSED
        private void Operation_Click(object sender, EventArgs e)
        {
            //Button button = (Button)sender;
            numOne = Convert.ToDouble(Display.Text);
            Display.Text = string.Empty;
            operation = ((Button)sender).Text;
        }
      
        //GETTING RESULT
        private void buttonResult_Click(object sender, EventArgs e)
        {
            double result = 0;
            numTwo = Convert.ToDouble(Display.Text);            
            if (operation == "+")
            {
                result = numOne + numTwo;
            }
            else if (operation == "-")
            {
                result = numOne - numTwo;
            }
            else if (operation == "x")
            {
                result = numOne * numTwo; ;
            }
            else if (operation == "/")
            {
                result = numOne / numTwo; ;
            }
            Display.Text = result.ToString();
        }

        //CLEAR TEXTBOX
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Display.Text = "0";
            numOne = 0;
            numTwo = 0;
        }
    }

}
