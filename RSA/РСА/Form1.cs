using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace РСА
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            // Генерация двух чисел и выбор двух простых чисел.

            Random x = new Random(); // объявление переменной для генерации чисел

            int p = x.Next(4, 20);
            int q = x.Next(4, 20);
            int p_simple = sundaram(p);
            int q_simple = sundaram(q);
            //int p_simple = 11;
            //int q_simple = 13;
            int fun_E = ((p_simple - 1) * (q_simple - 1));
            uint n = (uint)(p_simple * q_simple);
            //Генерация числа d и проверка его на взаимопростоту с числом ((p_simple-1)*(q_simple-1)).
            int E = 0, e_simple = 0;
            while (e_simple != 1)
            {
                E = x.Next(2, fun_E - 1);
                e_simple = gcd(E, fun_E);
            }
            //E = 7;
            //Определение числа e, для которого является истинным
            //соотношение (e*d)%((p_simple-1)*(q_simple-1))=1.
            int d = 0, d_simple = 0;
            while (d_simple != 1)
            {
                d += 1;
                d_simple = (E * d) % fun_E;
            }
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label3.Text = E.ToString();
            label4.Text = d.ToString();
            label5.Text = n.ToString();
            label6.Text = n.ToString();

            //Ввод шифруемых данных
            string Text = textBox1.Text.ToString();
            int MAX = Text.Length;
            //Массив для хранения шифротекста.
            uint[] CryptoText = new uint[MAX];
            
            string alfa = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            BigInteger[] text_num = new BigInteger[MAX];
            int exponent = E;
            BigInteger modulus = n;
            for (int i = 0; i < MAX; i++)
                for (int j = 0; j < alfa.Length; j++)
                {
                    if (Text[i] == alfa[j]) text_num[i] = (uint)j;
                    else continue;
                }
            for (int i = 0; i < MAX; i++)
            {
                int c = 1;
                uint k = 0;
                
                while (k < E)//последовательное взведение в степень
                {
                    c = (int)(c * text_num[i]);
                    c = (int)(c % n);
                    k++;
                }
                text_num[i] = c;                
                textBox2.Text += text_num[i].ToString() + "-";
            }          
                        
        }
               
        //подбираем простое число
            private int sundaram(int n)
        {
            int[] a = new int[n]; //Создание массива 
            int k; //индекс составного числа в массиве а
            for (int i = 0; i < n; i++) //Инициализация массива 0
            {
                a[i] = 0;
            }
            for (int i = 1; 3 * i + 1 < n; i++) //Цикл по массиву с 1
            {
                for (int j = 1; (k = i + j + 2 * i * j) < n && j <= i; j++) //Вычеркивание всех составных чисел
                {
                    a[k] = 1;
                }
            }
            //Выбирает из списка простых чисел ближайшее к заданному.
            for (int i = n - 1; i >= 1; i--)
            {
                if (a[i] == 0)
                {
                    return (2 * i + 1);
                }                
            }
            return 0;                         
        }

        //НОД
        int gcd(int a, int b)
        {
            int c;
            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }
            return Math.Abs(a);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            string alfa = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            int d = Int32.Parse(label4.Text.ToString());
            int n = Int32.Parse(label6.Text.ToString());
            string Text = textBox1.Text.ToString();
            int MAX = Text.Length;           

            string[] separators = { "-" };            
            string[] words = Text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int[] Tdecrypt = new int[words.Length];
            for (int i = 0; i < words.Length; i++)
                Tdecrypt[i] = Int32.Parse(words[i]);

            for (int i = 0; i < words.Length; i++)
            {
                int c = 1;
                uint k = 0;

                while (k < d)//последовательное взведение в степень
                {
                    c = (int)(c * Tdecrypt[i]);
                    c = (int)(c % n);
                    k++;
                }
                Tdecrypt[i] = c;
                
            }
            for (int i = 0; i < words.Length; i++)
                for (int j = 0; j < alfa.Length; j++)
                {
                    if (Tdecrypt[i] == j) { textBox2.Text += alfa[j].ToString(); }
                    else continue;
                }

                       
            }              
        

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text = textBox2.Text;
            textBox2.Clear();
        }
    }
}
