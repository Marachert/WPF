using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class FracCalc : Window
    {
        public FracCalc()
        {
            InitializeComponent();
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            if (numerator1.Text.Equals(String.Empty) || denominator1.Text.Equals(String.Empty) ||
                numenator2.Text.Equals(String.Empty) || denumenator2.Text.Equals(String.Empty))
            {
                MessageBox.Show("Введите корректное значение");
                return;
            }

            //Первая дробь
            Fraction frac1;

            try
            {
                frac1 = new Fraction
                {
                    Numerator = Convert.ToInt32(numerator1.Text),
                    Denumerator = Convert.ToInt32(denominator1.Text)
                };
            }
            catch
            {
                MessageBox.Show("Неправильная запись дроби 1");
                return;
            }

            //Вторая дробь
            Fraction frac2;

            try
            {
                frac2 = new Fraction
                {
                    Numerator = Convert.ToInt32(numenator2.Text),
                    Denumerator = Convert.ToInt32(denumenator2.Text)
                };
            }
            catch
            {
                MessageBox.Show("Неправильная запись дроби 2");
                return;
            }

            //Результат действий с дробями

            Fraction res = null;
            if(rbMult.IsChecked.Value)
            {
                res = frac1 * frac2;
            }

            if (rbDiv.IsChecked.Value)
            {
                res = frac1 / frac2;
            }

            if (rbPlus.IsChecked.Value)
            {
                res = frac1 + frac2;
            }

            if (rbMinus.IsChecked.Value)
            {
                res = frac1 - frac2;
            }

            if (res == null)
            {
                MessageBox.Show("Выберите операцию");
                return;
            }

            res.Reduction();

            // Отображение
            numenatorRes.Content = res.Numerator.ToString();
            denumenatorRes.Content = res.Denumerator.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            numerator1.Text = "";
            numenator2.Text = "";
            denominator1.Text = "";
            denumenator2.Text = "";            
        }
    }

    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denumerator { get; set; }

        //Приведение дроби
        public void Reduction()
        {
            int min = (Numerator < Denumerator) ? Numerator : Denumerator;

            for (int i = min; i > 1; i--)
            {
                if (Numerator % i == 0 && Denumerator % i == 0)
                {
                    Numerator /= i;
                    Denumerator /= i;
                } 
            }
        }

        //Перегрузка оператора
        //Отличия от С++
        //Описываются как методы, но принимают 2 параметра
        //задаются для класса, т.е. в статическом виде

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            return new Fraction
            {
                Numerator = f1.Numerator * f2.Numerator,
                Denumerator = f1.Denumerator * f2.Denumerator
            };
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            return new Fraction
            {
                Numerator = f1.Numerator * f2.Denumerator,
                Denumerator = f1.Denumerator * f2.Numerator
            };
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            return new Fraction
            {
                Numerator = f1.Numerator * f2.Denumerator + f2.Numerator * f1.Denumerator,
                Denumerator = f1.Denumerator * f2.Denumerator
            };
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            return new Fraction
            {
                Numerator = f1.Numerator * f2.Denumerator - f2.Numerator * f1.Denumerator,
                Denumerator = f1.Denumerator * f2.Denumerator
            };
        }
    }
}
