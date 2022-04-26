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
        {
            string input = "|(-3+2*2)*7*+1*(1+5*4)*(3+5*6+2)*|(1+4*5+1)";
            string input2 = "-|*(256)+8";
            string output = GetPoland(input); 
            string output3 = DoConvert(output);
            string output4 = GetPoland(output3);
            textBox1.Text = Counting(output4).ToString();                   
        }


        private string DoConvert(string input)
        {               
            Stack<string> temp = new Stack<string>();
            for (int i = 0; i < input.Length; i++) 
            {
              
                if (Char.IsDigit(input[i]))
                {
                    string a = "";

                    while (!Separator(input[i]) && !Operator(input[i])) 
                    {
                        a += input[i]; 
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(a); 
                    i--;
                }
                else if (Operator(input[i])) 
                {
                    string a = "";
                    string b = "";
                    if (temp.Count > 0) a = temp.Pop();
                    if (temp.Count > 0 && input[i] != '|') b = temp.Pop();
      
                    switch (input[i])
                    {
                        case '+': temp.Push("("+b + input[i] + a+")") ; break;
                        case '-': temp.Push("(" + b + input[i] + a + ")"); break;
                        case '*': temp.Push("(" + b + input[i] + a + ")"); break;
                        case '/': temp.Push("(" + b + input[i] + a + ")"); break;
                        case '^': temp.Push("(" + b + input[i] + a + ")"); break;
                        case '|': temp.Push("(" + input[i] + a + ")"); break;
                    }

                }
            }    
            return temp.Pop();
        }
    

        private bool Separator(char c)
        {
            if ((" ".IndexOf(c) != -1))
                return true;
            return false;
        }
   
        private bool Operator(char с)
        {
            if ("+-/*^()|".IndexOf(с) != -1)
                return true;
            return false;
        }
        
        private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                case '|': return 6;//корень
                default: return 7;
   
            }
        }
         
        private double Counting(string input)
        {
            double result = 0; 
            Stack<double> temp = new Stack<double>(); 

            for (int i = 0; i < input.Length; i++) 
            {
                
                if (Char.IsDigit(input[i]))
                {
                    string a = "";

                    while (!Separator(input[i]) && !Operator(input[i])) 
                    {
                        a += input[i]; 
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(double.Parse(a)); 
                    i--;
                }
                else if (Operator(input[i])) 
                {
                    
                    double a = 0;
                    double b = 0;
                    if (temp.Count > 0) a = temp.Pop();
                    if (temp.Count >0 && input[i]!= '|') b= temp.Pop();


                    switch (input[i]) 
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/': result = b / a; break;
                        case '^': result = Math.Pow(b,a); break;
                        case '|': result = Math.Sqrt(a); break;
                    }
                    temp.Push(result); 
                }
            }
            return temp.Peek(); 
        }

        private string GetPoland(string input)
        {
            string result = string.Empty; 
            Stack<char> operStack = new Stack<char>(); 

            for (int i = 0; i < input.Length; i++) 
            {
              
                if (Separator(input[i]))
                    continue;  
                if (Char.IsDigit(input[i])) 
                {
                  
                    while (!Separator(input[i]) && !Operator(input[i]))
                    {
                        result += input[i]; 
                        i++; 
                        if (i == input.Length) break; 
                    }

                    result += " "; 
                    i--; 
                }
         
                if (Operator(input[i])) 
                {
                    if (input[i] == '(') 
                        operStack.Push(input[i]); 
                    else if (input[i] == ')') 
                    {
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            result += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else 
                    {
                        if (operStack.Count > 0) 
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                result += operStack.Pop().ToString() + " "; 
                                operStack.Push(char.Parse(input[i].ToString())); 
                    }
                }
            }
            while (operStack.Count > 0)
                result += operStack.Pop() + " ";

            return result; 
        }
    }
}
