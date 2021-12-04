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
using System.Windows.Threading;


namespace WpfApp1
{
    public class FoodFor
    {
        public Segment Fruit { get; set; }
        Random random;

        public FoodFor(Pyton pyton)
        {
            double randomX;
            double randomY;
            random = new Random();

            do
            {
                randomX = random.Next(80) * Segment.FIGURE_SIZE;
                randomY = random.Next(50) * Segment.FIGURE_SIZE;

            } while (pyton.PartOf(randomX, randomY));

            Fruit = new Segment
            {
                Figure = new Ellipse
                {
                    Width = Segment.FIGURE_SIZE,
                    Height = Segment.FIGURE_SIZE,
                    Fill = new SolidColorBrush(
                            Color.FromRgb(
                                (byte)random.Next(100, 250),
                                (byte)random.Next(100, 250),
                                (byte)random.Next(100, 250)))
                },
                X = randomX,
                Y = randomY
            };
        }

        public void Remove(Canvas canvas)
        {
            Fruit?.Remove(canvas);
        }

        public void Show(Canvas canvas)
        {
            Fruit?.Show(canvas);
        }
    }
}
