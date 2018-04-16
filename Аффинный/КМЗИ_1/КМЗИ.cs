using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КМЗИ_1
{
    public partial class КМЗИ : Form
    {
        public КМЗИ()
        {
            InitializeComponent();            
        }
        public string check = "";
        private void button1_Click(object sender, EventArgs e)
        {    
            int a = int.Parse(textBoxOp1.Text);
            int b = int.Parse(textBoxOp2.Text);
            string alfa = " `1234567890-=~!@#$%^&*()_+[]{}|,./<>?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            int m = alfa.Length;            

            if (radioButtonDecoding.Checked)
            {
                
                textBoxOriginal.Clear();
                string Text2 = textBoxDecoding.Text.ToString();
                int a2 = 1;
                while (a * a2 % m != 1) a2++;
                
                int[] arr2 = new int[Text2.Length];
                for (int i = 0; i < Text2.Length; i++)
                    for (int j = 0; j < alfa.Length; j++)
                    {
                        if (Text2[i] == alfa[j])
                        {
                            arr2[i] = j;
                            if ((arr2[i] - b) < 0) arr2[i] += m;
                            arr2[i] = (a2 * (arr2[i] - b))%m;
                            textBoxOriginal.Text += alfa[arr2[i]];
                        }
                        else continue;
                    }
            }
                else
            {
                
                textBoxCipherText.Clear();                
                while ((NOD(a, m) != 1) & (a != 1) & (a < m))
                {
                    MessageBox.Show("Введенный параметр а должен быть взаимнопростым с параметром m!", "Ошибка",
 MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                };
                string Text = textBoxCoding.Text.ToString();
                int[] arr = new int[Text.Length];
                for (int i = 0; i < Text.Length; i++)
                    for (int j = 0; j < alfa.Length; j++)
                    {
                        if (Text[i] == alfa[j])
                        {
                            arr[i] = j;
                            arr[i] = ((a * arr[i] + b) % m);
                            textBoxCipherText.Text += alfa[arr[i]];
                        }
                        else continue;
                    }                
            }
        }
        private int NOD(int a, int m)
        {
            int t;
            if (a < m) { t = a; a = m; m = t; }
            while (m != 0)
            {
                t = m;
                m = a % m;
                a = t;
            }
            return a; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string alfa = " `1234567890-=~!@#$%^&*()_+[]{}|,./<>?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            int m = alfa.Length;
            
            if (radioButtonDecoding2.Checked)
            {

                textBoxOriginal2.Clear();
                string Text2 = textBoxDecoding2.Text.ToString();
                int[] a = new int[Text2.Length];
                int[] b = new int[Text2.Length];
                a[0] = int.Parse(textBoxArg11.Text); ;
                a[1] = int.Parse(textBoxArg12.Text); ;
                b[0] = int.Parse(textBoxArg21.Text);
                b[1] = int.Parse(textBoxArg22.Text);
                for (int i = 2; i < Text2.Length; i++)
                {
                    a[i] = (a[i - 2] * a[i - 1]) % m;
                    b[i] = (b[i - 2] + b[i - 1]) % m;
                }
                int[] obr = new int[Text2.Length];
                for (int i = 0; i < Text2.Length; i++)
                {
                    obr[i] = 1;
                }
                for (int i = 0; i < Text2.Length; i++)
                {
                    while (a[i] * obr[i] % m != 1) obr[i]++;
                }
                int[] arr2 = new int[Text2.Length];
                for (int i = 0; i < Text2.Length; i++)
                    for (int j = 0; j < alfa.Length; j++)
                    {
                        if (Text2[i] == alfa[j])
                        {
                            arr2[i] = j;
                        }
                        else continue;
                    }

                for (int i = 0; i < Text2.Length; i++)
                {
                    if ((arr2[i] - b[i]) < 0) arr2[i] += m;
                    arr2[i] = (obr[i] * (arr2[i] - b[i]) % m);                    
                    textBoxOriginal2.Text += alfa[arr2[i]];
                }
            }
            else
            {
                textBoxCipherText2.Clear();
                string Text = textBoxCoding2.Text.ToString();
                check = Text;
                int[] a = new int[Text.Length];
                int[] b = new int[Text.Length];
                a[0] = int.Parse(textBoxArg11.Text); ;
                a[1] = int.Parse(textBoxArg12.Text); ;
                b[0] = int.Parse(textBoxArg21.Text);
                b[1] = int.Parse(textBoxArg22.Text);
                for (int i = 2; i < Text.Length; i++)
                {
                    a[i] = (a[i - 2] * a[i - 1]) % m;
                    b[i] = (b[i - 2] + b[i - 1]) % m;
                }
                while ((NOD(a[0], m) != 1) & (a[0] != 1) & (a[0] < m) & (NOD(a[1], m) != 1) & (a[1] != 1) & (a[1] < m))
                {
                    MessageBox.Show("Введенные параметры а1/a2 должены быть взаимнопростыми с параметром m!", "Ошибка",
 MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                };               
               
                int[] arr = new int[Text.Length];
                for (int i = 0; i < Text.Length; i++)
                    for (int j = 0; j < alfa.Length; j++)
                    {
                        if (Text[i] == alfa[j])
                        {
                            arr[i] = j;                            
                        }
                        else continue;
                    }            
                   
                for (int i = 0; i < Text.Length; i++)
                {
                    arr[i] = ((a[i] * arr[i] + b[i])) % m;
                    textBoxCipherText2.Text += alfa[arr[i]];
                }
            }
        }        

        private void label15_TextChanged(object sender, EventArgs e)
        {
            string alfa = " `1234567890-=~!@#$%^&*()_+[]{}|,./<>?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            int m = alfa.Length;
            label15.Text = m.ToString();
            label16.Text = m.ToString();
        }
    }
}
