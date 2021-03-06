﻿using System;
using System.Windows.Forms;

namespace Hill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int col = 1;
        int m = 53;
        string Alphabet = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz.";
        
        private void Encrypt_bnt_Click(object sender, EventArgs e)
        {
            string proverka = "1";
            while (proverka != "Stringtxt")
            {
                char[] seperator = new char[] { ' ' };
                char[] seperator2 = new char[] { ' ' };
                int n = richTextBox2.Lines.Length;
                int n2 = richTextBox3.Lines.Length;                
                if (n <= 2)
                {
                    MessageBox.Show("Введен слишком коротки ключ! Пожалуйста, введите другой ключ.");
                }
                else
                {
                    double[,] arr = new double[n, n];
                    double[,] arr2 = new double[n2, n2];

                    if (n != n2)
                    {
                        MessageBox.Show("Размеры ключей не совпадают!");
                    }

                    int count = 0;

                    // ----------------------- Ввод ключа 1 (матрицы) ---------------------------
                    try
                    {
                        foreach (string str in richTextBox2.Lines)
                        {

                            string[] p = str.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < p.Length; i++)
                                arr[count, i] = Int32.Parse(p[i]);
                            count++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка считывания 1 ключа!");
                    }
                    // ----------------------------------------------------------------------

                    double[,] proof = FindingInverseMatrix(arr, n);
                    bool correct_matrix = true;
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (proof[i, j] == 0)
                            {
                                correct_matrix = false;
                            }
                        }
                    }
                    count = 0;
                    // ----------------------- Ввод ключа 2 (матрицы) ---------------------------
                    try
                    {
                        foreach (string st in richTextBox3.Lines)
                        {

                            string[] r = st.Split(seperator2, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < r.Length; i++)
                                arr2[count, i] = Int32.Parse(r[i]);
                            count++;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка считывания 2 ключа!");
                    }
                    // ----------------------------------------------------------------------

                    proof = FindingInverseMatrix(arr2, n);
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            if (proof[i, j] == 0)
                            {
                                correct_matrix = false;
                            }
                        }
                    }

                    if (correct_matrix == false)
                    {
                        MessageBox.Show("Введен неверный ключ! Невозможно получить обратную матрицу для расшифрования.");
                    }

                    {

                        int length_text = richTextBox1.Text.Length;
                        string text = richTextBox1.Text.ToString();
                        richTextBox1.Clear();


                        // ---------- Деление входного текста на блоки и составление векторов --------
                        double leng_v = (double)length_text / n;
                        int length_v = (int)Math.Ceiling(leng_v);
                        int[,] v = new int[length_v, n];
                        count = 0; int count2 = 0;

                        for (int i = 0; i < length_text; i++)
                        {
                            for (int j = 0; j < m; j++)
                            {
                                if (Alphabet[j] == text[i])
                                {
                                    if (count != n)
                                    {
                                        v[count2, count] = j;
                                        count++;
                                    }
                                    else
                                    {
                                        count = 0;
                                        count2++;
                                        v[count2, count] = j;
                                        count++;
                                    }

                                }
                            }
                        }
                        // -------------------------------------------------------------
                        int[,] trans_v = new int[length_v, n];
                        int mult = 0;

                        // ---------- Умножение 1 матрицы----------------

                        for (int j = 0; j < n; j++)
                        {
                            for (int m = 0; m < n; m++)
                            {
                                mult += v[0, m] * (int)arr[m, j];
                            }
                            mult = mult % 53;
                            trans_v[0, j] = mult;
                            mult = 0;
                        }

                        // ---------------------------------------------

                        double[,] arr3 = new double[n, n];
                        arr3 = proiz(arr, arr2, n);
                        arr = arr2;
                        arr2 = arr3;


                        // ---------- Умножение матриц ----------------


                        for (int i = 1; i <= count2; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                for (int m = 0; m < n; m++)
                                {
                                    mult += v[i, m] * (int)arr[m, j];
                                }
                                mult = mult % 53;
                                if (mult < 0)
                                    mult += 53;
                                trans_v[i, j] = mult;
                                mult = 0;
                            }
                            arr3 = proiz(arr, arr2, n);
                            arr = arr2;
                            arr2 = arr3;
                        }
                        // -----------------------------------------

                        richTextBox4.Text += " " + col.ToString() + ") ";
                        col++;
                        for (int i = 0; i <= count2; i++)
                        {
                            for (int j = 0; j < n; j++)
                            {
                                richTextBox1.Text = richTextBox1.Text + Alphabet[trans_v[i, j]];
                                richTextBox4.Text += Alphabet[trans_v[i, j]];
                            }
                        }

                    }
                }
                proverka = richTextBox1.Text;
            }
        }

        private void Decrypt_btn_Click(object sender, EventArgs e)
        {
            char[] seperator = new char[] { ' ' };
            char[] seperator2 = new char[] { ' ' };
            int n = richTextBox2.Lines.Length;
            int n2 = richTextBox3.Lines.Length;

            if (n <= 2)
            {
                MessageBox.Show("Введен слишком коротки ключ! Пожалуйста, введите другой ключ.");
            }
            else
            {
                double[,] arr = new double[n, n];
                double[,] arr2 = new double[n2, n2];

                if (n != n2)
                {
                    MessageBox.Show("Размеры ключей не совпадают!");
                }

                int count = 0;

                // ----------------------- Ввод ключа 1 (матрицы) ---------------------------
                try
                {
                    foreach (string str in richTextBox2.Lines)
                    {

                        string[] p = str.Split(seperator, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < p.Length; i++)
                            arr[count, i] = Int32.Parse(p[i]);
                        count++;
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка считывания 1 ключа!");
                }
                // ----------------------------------------------------------------------

                double[,] proof = FindingInverseMatrix(arr, n);
                bool correct_matrix = true;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (proof[i, j] == 0)
                        {
                            correct_matrix = false;
                        }
                    }
                }
                count = 0;
                // ----------------------- Ввод ключа 2 (матрицы) ---------------------------
                try
                {
                    foreach (string st in richTextBox3.Lines)
                    {

                        string[] r = st.Split(seperator2, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < r.Length; i++)
                            arr2[count, i] = Int32.Parse(r[i]);
                        count++;
                    }
                }
                catch
                {
                    //MessageBox.Show("Произошла ошибка считывания 2 ключа!");
                }
                // ----------------------------------------------------------------------

                proof = FindingInverseMatrix(arr2, n);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (proof[i, j] == 0)
                        {
                            correct_matrix = false;
                        }
                    }
                }

                if (correct_matrix == false)
                {
                    MessageBox.Show("Введен неверный ключ! Невозможно получить обратную матрицу для расшифрования.");
                }

                {

                    int length_text = richTextBox1.Text.Length;
                    string text = richTextBox1.Text.ToString();
                    richTextBox1.Clear();


                    // ---------- Деление входного текста на блоки и составление векторов --------
                    double leng_v = (double)length_text / n;
                    int length_v = (int)Math.Ceiling(leng_v);
                    int[,] v = new int[length_v, n];
                    count = 0; int count2 = 0;

                    for (int i = 0; i < length_text; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            if (Alphabet[j] == text[i])
                            {
                                if (count != n)
                                {
                                    v[count2, count] = j;
                                    count++;
                                }
                                else
                                {
                                    count = 0;
                                    count2++;
                                    v[count2, count] = j;
                                    count++;
                                }

                            }
                        }
                    }
                    // -------------------------------------------------------------
                    int[,] trans_v = new int[length_v, n];
                    int mult = 0;


                    double [,] inverse_arr = FindingInverseMatrix(arr, n);
                    if (inverse_arr == null)
                    {
                        MessageBox.Show("Ключ введен неверно!");
                    }

                    // ---------- Умножение 1 матрицы----------------

                    for (int j = 0; j < n; j++)
                    {
                        for (int m = 0; m < n; m++)
                        {
                            mult += v[0, m] * (int)inverse_arr[m, j];
                        }
                        mult = mult % 53;
                        trans_v[0, j] = mult;
                        mult = 0;
                    }

                    // ---------------------------------------------

                    double[,] arr3 = new double[n, n];
                    arr3 = proiz(arr, arr2, n);
                    arr = arr2;
                    arr2 = arr3;
                    inverse_arr = FindingInverseMatrix(arr, n);


                    // Умножение матриц

                    for (int i = 1; i <= count2; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            for (int m = 0; m < n; m++)
                            {
                                mult += v[i, m] * (int)inverse_arr[m, j];
                            }
                            mult = mult % 53;
                            if (mult < 0)
                                mult += 53;
                            trans_v[i, j] = mult;
                            mult = 0;
                        }
                        arr3 = proiz(arr, arr2, n);
                        arr = arr2;
                        arr2 = arr3;
                        inverse_arr = FindingInverseMatrix(arr, n);
                    }
                    // -----------------------------------------

                    for (int i = 0; i <= count2; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            richTextBox1.Text = richTextBox1.Text + Alphabet[trans_v[i, j]];
                        }
                    }
                }
            }
        }


        // ------------------- Функции ---------------------------------------

        static public double [,] proiz(double[,] matrix, double [,] matrix2, int n)
        {
            double[,] rezult = new double[n,n];

            int mult = 0;
            for (int i = 0; i <n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int m = 0; m < n; m++)
                    {
                        mult += (int)matrix[i, m] * (int)matrix2[m, j];
                    }
                    mult = mult % 53;
                    rezult[i, j] = mult;
                    mult = 0;
                }
            }

            return rezult;
        }



            /// <summary>
            /// Поиск определителя матрицы
            /// </summary>
            /// <param name="matrix"></param>
            static public double GetDeterminant(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new Exception("Матрица должна быть квадратной!");
            /// Если матрица 2Г—2, то возвращаем определитель по формуле Крамера
            if (matrix.GetLength(0) == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            int sign = 1;//Знак минора
            double result = 0;
            int j = 0;//Номер столбца, по которому раскладывается матрица
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                /// Если номер столбца и строки одновременно чётные, то
                /// знак будет «+», иначе — «-»
                sign = ((i + 1) % 2 == (j + 1) % 2) ? 1 : -1;
                result += sign * matrix[i, j] * GetDeterminant(GetMinorMatrix(matrix, i, j));
            }
            return result;
        }
        /// <summary>
        /// Метод для вычисления минорной матрицы для заданного элемента
        /// </summary>
        /// <param name="matrix">Исходная матрица</param>
        /// <param name="row">Номер строки</param>
        /// <param name="col">Номер столбца</param>
        static public double[,] GetMinorMatrix(double[,] matrix, int row, int col)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            int m = 0, k;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i == row) continue;
                k = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == col) continue;
                    result[m, k++] = matrix[i, j];
                }
                m++;
            }
            return result;
        }

        static double[,] MatrixMinor(double[,] fmatrix, int N)
        {
            double[,] smatrix = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    smatrix[i, j] = GetDeterminant(GetMinorMatrix(fmatrix, i, j));
                }
            }
            return (smatrix);
        }


        static double[,] Transponiruem(double[,] fmatrix, int N)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if ((i + j) % 2 != 0) fmatrix[i, j] *= (-1);
                }
            }
            double[,] smatrix = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    smatrix[j, i] = fmatrix[i, j];
                }
            }
            return (smatrix);
        }

        public static int ConvertTo31(double value)
        {
            int cel = (int)value / 53;
            if (value > 0)
            {
                value -= cel * 53;
            }
            else
            {
                if (Math.Abs(cel) != 0) value += (Math.Abs(cel) + 1) * 53;
                else value = 0;
            }
            return ((int)value);
        }

        public static void ConvertMatrixTo31(ref double[,] matrix, int N)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = ConvertTo31((int)matrix[i, j]);
                }
            }
        }


        static double[,] ObrMatrix(double[,] matrix, int N, int opr)
        {
            opr = get_inverse_mod(Convert.ToInt32(opr), 53);
            if (opr == -1) return (null);
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] *= opr;
                }
            }
            ConvertMatrixTo31(ref matrix, N);
            return (matrix);
        }

        public static double[,] FindingInverseMatrix(double[,] Matrix, int Count)
        {
            int opredelitel = ConvertTo31(GetDeterminant(Matrix));
            if (opredelitel == -1) return (null);
            if (opredelitel == 0) return (null);
            double[,] Mmatrix = MatrixMinor(Matrix, Count);
            double[,] TMmatrix = Transponiruem(Mmatrix, Count);
            double[,] obrMatrix = ObrMatrix(TMmatrix, Count, opredelitel);
            return (obrMatrix);
        }


        private static int get_inverse_mod(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }

        private static int GCD(int a, int b, out int x, out int y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            int x1, y1;
            int d = GCD(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //-------------------------------------------------------------------------------------------------------



    }
}
