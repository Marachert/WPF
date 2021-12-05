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
    public class Pyton
    {
        public List<Segment> Body { get; set; }
        private Random random;
        Canvas canvas;

        public Pyton(Canvas canvas)
        {
            Body = new List<Segment>();
            random = new Random();
            this.canvas = canvas;

            Image myImage = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("SnakeHead.png", UriKind.Relative);
            bi3.EndInit();
            myImage.Stretch = Stretch.Fill;
            myImage.Source = bi3;
            myImage.Width = Segment.FIGURE_SIZE + 3;
            myImage.Height = Segment.FIGURE_SIZE + 3;

            Segment segment = new Segment
            {
                Figure = myImage,
                X = Segment.FIGURE_SIZE,
                Y = Segment.FIGURE_SIZE,                
            };

            Body.Add(segment);

            for (int i = 1; i < 5; ++i)
            {
                Body.Add(new Segment
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
                    X = Segment.FIGURE_SIZE,
                    Y = Segment.FIGURE_SIZE
                });
            }
        }

        public void Show()
        {
            foreach (var segment in Body)
            {
                segment?.Show(canvas);
            }
        }

        public void Remove()
        {
            foreach(var segment in Body)
            {
                segment?.Remove(canvas);
            }
        }

        public void Move(double horizontal, double vertical)
        {
            this?.Remove();

            for (int i = this.Body.Count - 1; i >= 1; i--)
            {
                var segment = this.Body[i];
                var segment1 = this.Body[i - 1];

                segment.X = segment1.X;
                segment.Y = segment1.Y;
            }                   

            if (this.Body[0].X >= canvas.Width)
            {
                this.Body[0].X = 0;
                this.Body[0].Y += vertical;
            }
            else if (this.Body[0].X < 0)
            {
                this.Body[0].X = canvas.Width - Segment.FIGURE_SIZE;
                this.Body[0].Y += vertical;

            }
            else if (this.Body[0].Y >= canvas.Height)
            {
                this.Body[0].Y = 0;
                this.Body[0].X += horizontal;

            }
            else if (this.Body[0].Y < 0)
            {
                this.Body[0].Y = canvas.Height - Segment.FIGURE_SIZE;
                this.Body[0].X += horizontal;
            }
            else
            {
                this.Body[0].X += horizontal;
                this.Body[0].Y += vertical;
            }
            this.Show();

        }

        public bool PartOf(double horizontal, double vertical)
        {
            foreach (Segment part in this.Body)
            {
                if (horizontal == part.X && vertical == part.Y)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
