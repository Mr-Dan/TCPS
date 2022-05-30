using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPS_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {//-1*√(-3+2*2)*7+1*(1+5*4)*(3+5*6+2)*√(1+4*5+1)
            //string input = "sqrt(-3+2*2)*7+1*(1+5*4)*(3+5*6+2)*sqrt(1+4*5+1)";
            string input2 = "cos(1)+sin(1)";
           // string input2 = " sqrt(4*4 )  * 2    ";
            
            string output2 = GetPoland2(input2);      
            textBox1.Text = Counting2(output2) + "\n";  
            
        }

        private bool Separator(string text)
        {
            if ((" ".IndexOf(text) != -1))
                return true;
            return false;
        }
        public bool IsNumeber(string text)
        {  
            return double.TryParse(text, out _); 
        }

        private bool Operator(string s)
        {
            switch (s)
            {
                case "(":
                case ")": 
                case "+": 
                case "-": 
                case "*": 
                case "/": 
                case "^": 
                case "sqrt":
                case "cos": 
                case "sin": return true;
                default: return false;

            }
            
        }
        private byte GetPriority(string s)
        {
            switch (s)
            {
                case "(": return 0;
                case ")": return 1;
                case "+": return 2;
                case "-": return 2;
                case "*": return 3;
                case "/": return 3;
                case "^": return 4;
                case "sqrt": return 5;
                case "cos": return 5;
                case "sin": return 5;
                default: return 6;

            }
        }

        private List<string> StringReturn (string input)
        {
            string operators = "";
            string number = "";
            List<string> text = new List<string>();
            for (int i = 0; i < input.Length; i++)
            {              
                if (!Separator(input[i].ToString()))
                {
                    if (IsNumeber(input[i].ToString()))
                    {
                        if (operators.Length > 0)
                        {
                            text.Add(operators);
                            operators = "";
                        }
                           
                        number += input[i];
                    }
                    else
                    {
                        if (number.Length > 0)
                        {
                            text.Add(number);
                            number = "";
                        }
                        else if(Operator(operators))
                        {
                            text.Add(operators);
                            operators = "";
                        }
                        operators += input[i];
                    }
                }
                else
                {
                    if (operators.Length > 0)
                    {
                        text.Add(operators);
                        operators = "";
                    }
                    if (number.Length > 0)
                    {
                        text.Add(number);
                        number = "";
                    }
                }
        
            }
            if (number.Length > 0)
            {
                text.Add(number);
                number = "";
            }
            if (operators.Length > 0)
            {
                text.Add(operators);
                operators = "";
            }
            return text;
        }


        private string GetPoland2(string input)
        {
            string result = "";
            List<string> text = StringReturn(input);
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < text.Count; i++)
            {
                if (!Separator(text[i]))
                {
                    if (IsNumeber(text[i]))
                    {
                        result +=text[i] + " " ;
                       
                    }

                    if (Operator(text[i]))
                    {
                        if (text[i] == "(")
                            stack.Push(text[i]);
                        else if (text[i] == ")")
                        {
                            string s = stack.Pop();

                            while (s != "(")
                            {
                                result += s.ToString() + " ";
                                s = stack.Pop();
                            }
                        }
                        else
                        {
                            if (stack.Count > 0)
                                if (GetPriority(text[i]) <= GetPriority(stack.Peek()))
                                    result += stack.Pop().ToString() + " ";
                            stack.Push(text[i].ToString());
                        }
                    }
                }
            }
            while (stack.Count > 0)
                result += stack.Pop() + " ";

            return result;
        }

        private double Counting2(string input)
        {
            double result = 0;
            List<string> text = StringReturn(input);
            Stack<double> temp = new Stack<double>();

            for (int i = 0; i < text.Count; i++)
            {

                if (IsNumeber(text[i]))
                {                  
                    temp.Push(double.Parse(text[i]));
                  
                }
                else if (Operator(text[i]))
                {

                    double a = 0;
                    double b = 0;
                    if (temp.Count > 0) a = temp.Pop();
                    if (temp.Count > 0 && !text[i].Contains("sqrt")) b = temp.Pop();
                    if (temp.Count > 0 && !text[i].Contains("cos")) b = temp.Pop();
                    if (temp.Count > 0 && !text[i].Contains("sin")) b = temp.Pop();

                    switch (text[i])
                    {
                        case "+": result = b + a; break;
                        case "-": result = b - a; break;
                        case "*": result = b * a; break;
                        case "/": result = b / a; break;
                        case "^": result = Math.Pow(b, a); break;
                        case "sqrt": result = Math.Sqrt(a); break;
                        case "cos": result = Math.Cos(a); break;
                        case "sin": result = Math.Sin(a); break;
                    }
                    temp.Push(result);
                }
            }
            return temp.Peek();
        }
    }
}
