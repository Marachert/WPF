using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    public partial class Strings : Window
    {

        delegate String StringProcessor(String str);
        delegate String StringArrProcessor(String[] strArr);

        StringProcessor reverser;
        StringArrProcessor reverserArr;

        String wordsReverse(String str)
        {
            String[] words = str.Split(' ');
            string res = null;

            foreach (string word in words)
            {
                res = word + " " + res;
            }

            return res;
        }

        String wordsShuffle(String str)
        {
            String[] words = str.Split(' ');
            String res = "";

            int N = words.Length;
            int[] nums = new int[N];

            for (int i = 0; i < N; i++)
            {
                nums[i] = i;
            }
            //Перемешиваем массив - генерируем два случайных
            //индекса и меняем значения в массиве
            var rnd = new Random();
            int n1, n2;

            for (int i = 0; i < N; i++)
            {
                n1 = rnd.Next(N);

                do { n2 = rnd.Next(N); }
                while (n1 == n2);

                int tmp = nums[n2];
                nums[n2] = nums[n1];
                nums[n1] = tmp;
            }

            for (int i = 0; i < N; i++)
            {
                res += words[nums[i]] + " ";
            }

            return res;
        }

        String wordsArrShuffle(String[] strArr)
        {
            String res = "";

            int N = strArr.Length;
            int[] nums = new int[N];

            for (int i = 0; i < N; i++)
            {
                nums[i] = i;
            }

            var rnd = new Random();
            int n1, n2;

            for (int i = 0; i < N; i++)
            {
                n1 = rnd.Next(N);

                do { n2 = rnd.Next(N); }
                while (n1 == n2);

                int tmp = nums[n2];
                nums[n2] = nums[n1];
                nums[n1] = tmp;
            }

            for (int i = 0; i < N; i++)
            {
                res += strArr[nums[i]] + " ";
            }

            return res;
        }

        private void Shuffle_Click(object sender, RoutedEventArgs e)
        {
            reverserArr = wordsArrShuffle;
            LabelResult.Text = reverserArr(textBlock.Text.Split(' '));
        }


        public Strings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            reverser = wordsReverse;
            LabelResult.Text = reverser(textBlock.Text);
        }


        private void Digits_Click(object sender, RoutedEventArgs e)
        {
            // Регулярные выражения (Regular Expressions, Redex)
            // Специальные инструменты для работы со строками:
            // - проверка по шаблону
            // - разбиение по шаблону
            // - поиск-замена
            // Особенности: использование спец-символов:
            // \d (digit) - любая цифра
            // \D (non-digit) - любая не-цифра
            // \s (space)
            // \S (non-space)
            // \w (word-sym) - то, что может быть в слове
            // \W (non-word)

            // - квантификатор (quantifiers - указатели количества):
            // +  1 и более
            // *  0 и более
            // ?  0-1
            // {3} ровно 3
            // {3,5} от 3 до 5

            // - символы-якоря
            // ^ - начало
            // $ - конец

            // - множества (один из)
            // [123] - 1 или 2 или 3
            // [1,2,3] - 1 или 2 или 3
            // [1 2 3] - 1 или 2 или 3 или запятая
            // [0-9] - диапазон
            // [a-z] [a-zA-Z] [0-9a-f]
            // [^123] - любой символ, кроме 1,2,3

            //Regex digit = new Regex(@"^\s+\d{3}\s+$");
            Regex digit = new Regex(@"^([a-z]{1}')?[A-Z]{1}[a-z]+(-[A-Z]{1}[a-z]+)?$");
           
            if (digit.IsMatch(textBlock2.Text))
            {
                result2.Text = "Yes";
            }
            else
            {
                result2.Text = "No";
            }

        }
        
        private void Email_Click(object sender, RoutedEventArgs e)
        {
            Regex digit = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");


            if (digit.IsMatch(textBlock2.Text))
            {
                result2.Text = "Yes";
            }
            else
            {
                result2.Text = "No";    
            }
        }
    }
}
