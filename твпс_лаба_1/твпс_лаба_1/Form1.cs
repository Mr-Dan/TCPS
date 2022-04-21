using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace твпс_лаба_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            richTextBox3.Clear();
            string reg = "(bcc)+|(bd*)+";
            string text = "bccbdddbccdsvbbbdddasewjhgbcbccc";
            text = richTextBox1.Text+" ";
            // text = "bb";
            List<string> res =  castom(text);
           foreach(string test in res)
            {
                richTextBox2.Text = richTextBox2.Text + test.Trim('\n') + '\n';
            }
            regex(text);
        }

        private List<string> castom(string text)
        {
            List<string> list = new List<string>();
            int state = 1;
            int start = 0;
            string bcc = "";
            int pos =0;
            for (int i = 0; i < text.Length; i++)
            {
                switch(state)
                {
                    case 1:
                        switch(text[i])
                        {
                            case 'b': state = 2; break;
                            default:
                              state = 1;
                              start = i + 1;
                             break;
                        }
                    break;
                    case 2:
                        switch(text[i])
                        {
                            case 'b': state = 3; break;
                            case 'd': state = 3; break;
                            case 'c': state = 4; break;
                            default:
                                list.Add(text.Substring(start, i - start) + "2");
                                state = 1;
                                start = i + 1; 
                                break;
                        }
                    break;
                    case 3:
                        switch (text[i])
                        {
                            case 'b':  break;
                            case 'd':  break;
                            default:
                                list.Add(text.Substring(start, i - start) + "3");
                                state = 1;
                                start = i + 1;
                                break;
                        }
                    break;
                    case 4:
                        switch(text[i])
                        {                          
                            case 'c': 
                                state = 5;
                             
                                break;                         
                            default:
                                if (bcc != "") { 
                                    list.Add(bcc + "4");
                                    list.Add("b4/2");
                                }
                                else if (bcc == "") list.Add(text.Substring(start, i - start - 1) + "4/1");
                                bcc = "";
                                i--;
                                state = 1;
                                start = i+1;
                                break;
                        }
                    break;
                    case 5:
                        bcc = text.Substring(start, i - start);
                        switch (text[i])
                        {
                            case 'b': state = 6;
                               
                             break;
                            default:
                                list.Add(bcc + "5");
                                bcc = "";
                                state = 1;
                                start = i + 1;
                              
                             break;
                        }                     
                        break;
                    case 6:
                        switch (text[i])
                        {
                            case 'c': state = 4; break;                        
                            default:
                                list.Add(bcc + "6");
                                bcc = "";
                                state = 2;
                                i--;
                                start = i;
                                break;
                        }
                    break;
                }


            }
            return list;
        }
        private void castomReg(string text)
        {
            int countB = 0;
            int countC = 0;
            int countD = 0;
            int pos = 0;
            while (true)
            {

                switch (text[pos])
                {
                    case 'b':
                        countB++;
                    
                        if (text[pos + 1] != 'd'  && text[pos + 1] != 'b')
                        {
                         
                            if (checkPattern(text, pos) == false)
                            {
                                if (countB == 1)
                                {
                                    richTextBox2.Text = richTextBox2.Text  + text.Substring(pos, 1) + '\n';
                                    countB = 0;
                                }
                                else if (countB > 1 && countD == 0)
                                {
                                    richTextBox2.Text = richTextBox2.Text  + text.Substring(pos - countB+1, countB) + '\n';
                                    countB = 0;
                                }
                                else if(countD > 0)
                                {
                                    richTextBox2.Text = richTextBox2.Text + text.Substring(pos - countD - countB + 1, countD + countB) + '\n';
                                    countB = 0;
                                    countD = 0;
                                }                            
                            }
                            else
                            {
                              
                                if  (countD > 0)
                                {
                                    richTextBox2.Text = richTextBox2.Text  + text.Substring(pos - countD - countB + 1, countD + countB) + '\n';
                                    countB = 0;
                                    countD = 0;
                                }
                                else if (countB > 1 && countD == 0)
                                {
                                    richTextBox2.Text = richTextBox2.Text + text.Substring(pos - countB + 1, countB) + '\n';
                                    countB = 0;
                                }
                            }


                        }

                        break;

                    case 'c':
                        countC++;
                        if (countC == 2 && countB == 1)
                        {
                            richTextBox2.Text = richTextBox2.Text + text.Substring(pos - 2, 3) + '\n';
                            countB = 0;
                            countC = 0;
                        }
                        if (text[pos + 1] != 'c')
                        {
                            countB = 0;
                            countC = 0;
                        }
                        break;

                    case 'd':
                       if(countB>0) countD++;
                        if (text[pos + 1] != 'd' && text[pos + 1] != 'b' && countB >0)
                        {
                            richTextBox2.Text = richTextBox2.Text + text.Substring(pos - countD- countB+1, countD+ countB) + '\n';
                            countB = 0;
                            countD = 0;
                        }
                       
                        break;

                    default:
                        countB = 0;
                        countC = 0;
                        countD = 0;
                        break;

                }
                pos++;
                if (pos > text.Length - 1) break;

            }
        }
        bool checkPattern(string text,int pos)
        {
            if(pos+3 <= text.Length-1)
            {
                if (text[pos + 1] == 'c' && (text[pos + 2] == 'c')) return true;
                else return false;
            }
            else return false;
        }
        private void regex(string text)
        {
           
            Regex regex = new Regex("(bcc)+|(bd*)+");
            MatchCollection matchCollection = regex.Matches(text);
            if (matchCollection.Count > 0)
            {
                foreach (Match match in matchCollection)
                    richTextBox3.Text = richTextBox3.Text  + match.Value + '\n';
            }
        }
    } 

    /*
#include <iostream>
#include <string>
using namespace std;
bool result(string text);
    int main()
    {//a(b|c|d)c
        string text = "abcdc";
        cout << result(text);
        return 0;
    }

    bool result(string text)
    {
        int state = 1;
        for (int i = 0; i < text.size(); i++)
        {
            switch (state)
            {
                case 1:
                    switch (text[i])
                    {
                        case 'a': state = 2; break;
                        default: return false;
                    }
                    break;
                case 2:
                    switch (text[i])
                    {
                        case 'b': break;
                        case 'd': break;
                        case 'c': state = 3; break;
                        default: return false;
                    }

                    break;

                case 3:

                    switch (text[i])
                    {
                        case 'b': state = 2; break;
                        case 'd': state = 2; break;
                        case 'c': break;
                        default: return false;
                    }
                    break;
            }
        }
        if (state == 3) return true;
        else return false;
    }


     */

}
