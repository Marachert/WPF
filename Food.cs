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
        Canvas canvas;

        public FoodFor(Pyton pyton, Canvas canvas)
        {
            double randomX;
            double randomY;
            this.canvas = canvas; 
            random = new Random();

            do
            {
                randomX = random.Next(10) * Segment.FIGURE_SIZE;
                randomY = random.Next(5) * Segment.FIGURE_SIZE;

            } while (pyton.PartOf(randomX, randomY) &&
                     randomX > canvas.Width &&
                     randomY > canvas.Height);

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

        public void Remove()
        {
            Fruit?.Remove(canvas);
        }

        public void Show()
        {
            Fruit?.Show(canvas);
        }
    }
}
