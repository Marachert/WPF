using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public class Segment
    {
        // Композиция - сильная связь с объектом FrameWorkElement
        public FrameworkElement Figure { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public static double FIGURE_SIZE = 15;

        //Связи:
        // Наследование - самая сильная
        // Композиция - сильная
        // Агрегация - менее сильная
        // Ассоциация - слабая
        // Зависимость - менее слабая
        public void Show(Canvas field)
        {
            if (!field.Children.Contains(Figure))
            {
                field.Children.Add(Figure);
            }

            Canvas.SetLeft(Figure, X);
            Canvas.SetTop(Figure, Y);
        }

        public void Remove(Canvas field)
        {
            if (field.Children.Contains(Figure))
            {
                field.Children.Remove(Figure);
            }

            Canvas.SetLeft(Figure, X);
            Canvas.SetTop(Figure, Y);
        }

    }
}
