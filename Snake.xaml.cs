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
    /// <summary>
    /// Interaction logic for Snake.xaml
    /// </summary>
    public partial class Snake : Window
    {
        private LinkedList<Segment> Pyton;
        DispatcherTimer timer;
        HoldKey holdkey;
        bool headRight = true;
        bool headUp = false;
        bool headDown = false;
        bool headLeft = false;

        public Snake()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerTick;

            Pyton = new LinkedList<Segment>();
            for (int i = 0; i < 5; ++i)
            {
                Pyton.AddLast(new Segment
                {
                    Figure = new Ellipse
                    {
                        Width = 20,
                        Height = 20,
                        Fill = Brushes.Green
                    },
                    X = 100 + i * 10,
                    Y = 100
                });
            }

        }

        private void TimerTick(object sender, EventArgs e)
        {
            switch (holdkey)
            {
                case HoldKey.Left:

                    if (headRight == false)
                    {
                        headRight = false;
                        headLeft = true;
                        headUp = false;
                        headDown = false;

                        PytonStep(-10d, 0d);
                    }
                    else
                    {
                        goto case HoldKey.Right;
                    }


                    break;

                case HoldKey.Right:
                    if (headLeft == false)
                    {
                        headLeft = false;
                        headRight = true;
                        headUp = false;
                        headDown = false;

                        PytonStep(10d, 0d);
                    }
                    else
                    {
                        goto case HoldKey.Left;
                    }

                    break;

                case HoldKey.Up:

                    if (headDown == false)
                    {
                        headLeft = false;
                        headRight = false;
                        headUp = true;
                        headDown = false;

                        PytonStep(0d, -10d);
                    }
                    else
                    {
                        goto case HoldKey.Down;
                    }

                    break;

                case HoldKey.Down:

                    if (headUp == false)
                    {
                        headLeft = false;
                        headRight = false;
                        headUp = false;
                        headDown = true;

                        PytonStep(0d, 10d);
                    }
                    else
                    {
                        goto case HoldKey.Up;
                    }

                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
            foreach (var segment in Pyton)
            {
                segment.Show(Field);
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    holdkey = HoldKey.Left;
                    break;
                case Key.Right:
                    holdkey = HoldKey.Right;
                    break;
                case Key.Up:
                    holdkey = HoldKey.Up;
                    break;
                case Key.Down:
                    holdkey = HoldKey.Down;
                    break;
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
        }

        private void PytonStep(double horizontal, double vertical)
        {
            Field.Children.Remove(Pyton.Last().Figure);

            Pyton.AddFirst(new Segment
            {
                Figure = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Green
                },
                X = Pyton.First().X + horizontal,
                Y = Pyton.First().Y + vertical,
            });

            Pyton.Remove(Pyton.Last());

            Field.Children.Add(Pyton.First().Figure);
            Canvas.SetLeft(Pyton.First().Figure, Pyton.First().X);
            Canvas.SetTop(Pyton.First().Figure, Pyton.First().Y);
        }

    }
}
