using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace генерация_ключей_Виженер
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Необходимо ввести текст для генерации и хотя бы 1 символ ключа! Также необходим исходный текст для зашифрования!");
            }
            else
            {
                if (textBox1.Text.Length > 1)
                {
                    MessageBox.Show("Необходимо ввести один символ для автоключа!");
                }
                else
                {                    
                    {
                        if (textBox3.Text.Length != textBox4.Text.Length)
                        {
                            MessageBox.Show("Необходимо ввести текст для шифрования той же длины, что и автоключ!");
                            return;
                        }

                        //алфавит
                        string Alphabet = " abcdefghijklmnopqrstuvwxyz";
                        int m = Alphabet.Length;

                        //ключевое слово
                        string key = textBox3.Text;
                        int length_key = key.Length;

                        //исходный текст
                        string text = textBox4.Text.ToString();
                        int length_text = textBox4.Text.Length;
                        textBox5.Clear();

                        //под 1 буквой исходного текста 1 буква ключа и т.д.
                        while (length_key < length_text)
                        {
                            key += key;
                            length_key = key.Length;
                        }

                        for (int i = 0; i < length_text; i++)
                        {
                            for (int j = 0; j < m; j++)
                            {
                                if (Alphabet[j] == text[i])
                                {
                                    for (int t = 0; t < m; t++)
                                    {
                                        if (Alphabet[t] == key[i])
                                        {                                            
                                                int C = j + t;
                                                C = C % m;
                                                textBox5.Text += Alphabet[C].ToString();                                            
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length != textBox4.Text.Length)
            {
                MessageBox.Show("Необходимо ввести текст для расшифрования той же длины, что и автоключ!");
                return;
            }
            //алфавит
            string Alphabet = " abcdefghijklmnopqrstuvwxyz";
            int m = Alphabet.Length;

            //ключ
            string key = textBox3.Text;
            int length_key = key.Length;

            //исходный текст
            int length_text = textBox4.Text.Length;
            string text = textBox4.Text.ToString();
            textBox5.Clear();

            while (length_key < length_text)
            {
                key += key;
                length_key = key.Length;
            }

            for (int i = 0; i < length_text; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (Alphabet[j] == text[i])
                    {
                        for (int t = 0; t < m; t++)
                        {
                            if (Alphabet[t] == key[i])
                            {                                
                                    int P = j - t;
                                    P = P % m;
                                    if (P < 0) P = P + m;
                                    textBox5.Text += Alphabet[P].ToString();                                
                            }
                        }
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { textBox3.Text = textBox1.Text + textBox2.Text; }
            else
            {
                textBox3.Text = textBox1.Text;
                //алфавит
                string Alphabet = " abcdefghijklmnopqrstuvwxyz";
                int m = Alphabet.Length;

                //ключевое слово
                string key = textBox1.Text;
                int length_key = key.Length;

                //исходный текст
                string text = textBox2.Text.ToString();
                int length_text = textBox2.Text.Length;

                //под 1 буквой исходного текста 1 буква ключа и т.д.
                while (length_key < length_text)
                {
                    key += key;
                    length_key = key.Length;
                }

                for (int i = 0; i < length_text; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (Alphabet[j] == text[i])
                        {
                            for (int t = 0; t < m; t++)
                            {
                                if (Alphabet[t] == key[i])
                                {
                                    int C = j + t;
                                    C = C % m;
                                    textBox3.Text += Alphabet[C].ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = ""; textBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            textBox4.Text = textBox5.Text;
            textBox5.Clear();
        }
    }
}

