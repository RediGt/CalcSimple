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
        string firstOperation;
        string secondOperation;
        string secondExpression;
        int lengthOfNumOne = 0;
        bool operationInserted = false;
        bool scifiMode = false;
        bool illegalOperation = false;
        bool dotInserted = false;
        const int widthSmall = 360;
        const int widhtLarge = 570;
        public CalcSimple()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {           
        //READING LOCAL DECIMAL SEPARATOR
            decimalSeperator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            this.BackColor = Color.Azure;
            this.Width = widthSmall;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
           
        //DESIGN    
            foreach (Control c in this.Controls)
                {
                    c.BackColor =  Color.LightSteelBlue;
                    c.ForeColor = Color.Black;
                    c.TabStop = false;
                }
            Display.BackColor = Color.LightGray;
            buttonResult.BackColor = Color.DarkSlateBlue;
            label1.BackColor = Color.Transparent;
            buttonPi.Text = "\u03C0";
            buttonSqrt.Text = "\u221A";
            Display.Text = "0";


        //NUBERING BUTTONS
            string buttonName = null;
            Button button = null;
            for (int i=0; i<10; i++)
            {
                buttonName = "button" + i;
                button = (Button)this.Controls[buttonName];
                button.Text = i.ToString();
                button.Font = new Font("Tahome", 22f);               
                button.BackColor = Color.LightBlue;
            }           
        }

        //READING DIGITS FROM BUTTONS
        private void Button_Click(object sender, EventArgs e)
        {         
            Button button = (Button)sender;
            EnableControls();
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
            if (!operationInserted && !(dotInserted = Display.Text.Contains(decimalSeperator)))
            {
                if (Display.Text == "")
                {
                    Display.Text += "0" + decimalSeperator;
                }
                else
                    Display.Text += decimalSeperator;
            }

            secondExpression = Display.Text.Substring(lengthOfNumOne + 1, Display.Text.Length - lengthOfNumOne - 1);
            if (operationInserted && !(dotInserted = secondExpression.Contains(decimalSeperator)))
            {              
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

        //IF OPERATION SING IS PRESSED
        private void Operation_Click(object sender, EventArgs e)
        {
            EnableControls();
            DisableOperationButtons();
            if (!operationInserted)
            {
                lengthOfNumOne = Display.Text.Length;
                numOne = Convert.ToDouble(Display.Text);
                firstOperation = ((Button)sender).Text;
                Display.Text += firstOperation;
                operationInserted = true;              

                //SQRT---------------------------------------------------
                if (firstOperation == "\u221A")//==Sqrt
                {
                    numOne = Math.Sqrt(numOne);
                    Display.Text = numOne.ToString();
                    operationInserted = false;
                }                               
            }
            else
            {
                //INPUT 2nd NUMBER
                secondOperation = ((Button)sender).Text;
                Calculate();
                
                /* else if (firstOperation == "Sqrt" )
                 {
                     numOne = Math.Sqrt(numOne);
                     return;
                 }*/

                Display.Text = numOne.ToString();
                lengthOfNumOne = Display.Text.Length;
                Display.Text += secondOperation;
                firstOperation = secondOperation;
            }           
            //Display.Text = string.Empty;
        }
      
        //GETTING RESULT
        private void buttonResult_Click(object sender, EventArgs e)
        {
            if (operationInserted)
                Calculate();
            else
                numOne = Convert.ToDouble(Display.Text);
            if (!illegalOperation)
                Display.Text = numOne.ToString();
            operationInserted = false;
            buttonDecimal.Enabled = false;
            DisableNumButtons();
        }

        public void Calculate()
        {
            secondExpression = Display.Text.Substring(lengthOfNumOne + 1, Display.Text.Length - lengthOfNumOne - 1);
            numTwo = Convert.ToDouble(secondExpression);
            switch(firstOperation)
            {
                case "+":
                    numOne = numOne + numTwo;
                    break;
                case "-":
                    numOne = numOne - numTwo;
                    break;
                case "x":
                    numOne = numOne * numTwo;
                    break;
                case "/":
                    if (numTwo == 0)
                    {
                        DivisionByZero();
                        break;
                    }                      
                    numOne = numOne / numTwo;
                    break;
                case "^":
                    numOne = Math.Pow(numOne, numTwo);
                    break;
            }            
            /* else if (firstOperation == "Sqrt" )
                 {
                     numOne = Math.Sqrt(numOne);
                     return;
                 }*/
        }

        private void DisplayResult()
        {
            
        }

        public void DivisionByZero()
        {
            illegalOperation = true;
            Display.Text = "Division by 0!";

            foreach (Control c in this.Controls)
            {
                c.Enabled = false;
            }
            buttonClear.Enabled = true;
            Display.Enabled = true;
        }

        public void DisableOperationButtons()
        {
            buttonAdd.Enabled = false;
            buttonCos.Enabled = false;
            buttonCtg.Enabled = false;
            buttonDivide.Enabled = false;
            buttonLg.Enabled = false;
            buttonLn.Enabled = false;
            buttonLog.Enabled = false;
            buttonMultiply.Enabled = false;
            buttonPower.Enabled = false;
            buttonResult.Enabled = false;
            buttonSin.Enabled = false;
            buttonSqrt.Enabled = false;
            buttonSubstract.Enabled = false;
            buttonTg.Enabled = false;
        }

        public void DisableNumButtons()
        {
            string buttonName = null;
            Button button = null;
            for (int i = 0; i < 10; i++)
            {
                buttonName = "button" + i;
                button = (Button)this.Controls[buttonName];
                button.Enabled = false;
            }
        }

        public void EnableControls()
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
        }

        //CLEAR TEXTBOX
        private void buttonClear_Click(object sender, EventArgs e)
        {
            Display.Text = "0";
            numOne = 0;
            numTwo = 0;
            operationInserted = false;
            illegalOperation = false;
            EnableControls();
        }

        //SCi Fi MODE
        private void buttonSciFi_Click(object sender, EventArgs e)
        {
            if (scifiMode)
            {
                this.Width = widthSmall;
                scifiMode = !scifiMode;
            }
            else
            {
                this.Width = widhtLarge;
                scifiMode = !scifiMode;
            }
        }
        //BUTTON PI
        private void button14_Click(object sender, EventArgs e)
        {
            double pi = Math.PI;
            if (Display.Text == "0")
            {
                Display.Text = pi.ToString();
            }
            else if (Display.Text == "-0")
            {
                Display.Text = pi.ToString();
            }
            else
            {
                Display.Text += pi.ToString();
            }
            
        }
    }

}
