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
            Segment pytonHead = null;
            Segment pytonTail = null;

            switch (holdkey)
            {
                case HoldKey.Left:
                    pytonHead = Pyton.First();
                    pytonTail = Pyton.Last();

                    Field.Children.Remove(pytonTail.Figure);

                    Pyton.AddFirst(new Segment 
                    {
                        Figure = new Ellipse
                        {
                            Width = 20,
                            Height = 20,
                            Fill = Brushes.Green
                        },
                        X = pytonHead.X - 10,
                        Y = pytonHead.Y
                    });

                    Pyton.Remove(pytonTail);
                    pytonHead = Pyton.First();

                    Field.Children.Add(pytonHead.Figure);
                    Canvas.SetLeft(pytonHead.Figure, pytonHead.X);
                    Canvas.SetTop(pytonHead.Figure, pytonHead.Y);

                    break;

                case HoldKey.Right:
                    pytonHead = Pyton.Last();
                    pytonTail = Pyton.First();

                    Field.Children.Remove(pytonTail.Figure);

                    Pyton.AddLast(new Segment //попробуй использовать делегат 
                    {
                        Figure = new Ellipse
                        {
                            Width = 20,
                            Height = 20,
                            Fill = Brushes.Green
                        },
                        X = pytonHead.X + 10,
                        Y = pytonHead.Y
                    });

                    Pyton.Remove(pytonTail);
                    pytonHead = Pyton.Last();

                    Field.Children.Add(pytonHead.Figure);
                    Canvas.SetLeft(pytonHead.Figure, pytonHead.X);
                    Canvas.SetTop(pytonHead.Figure, pytonHead.Y);

                    break;


                    //    case HoldKey.Right:
                    //        Field.Children.Remove(Pyton[0].Figure);

                    //        Pyton.Add(new Segment
                    //        {
                    //            Figure = new Ellipse
                    //            {
                    //                Width = 20,
                    //                Height = 20,
                    //                Fill = Brushes.Green
                    //            },
                    //            X = Pyton.Last().X + 10,
                    //            Y = Pyton.Last().Y
                    //        });
                    //        Pyton.RemoveAt(0);
                    //        Field.Children.Add(Pyton.Last().Figure);

                    //        Canvas.SetLeft(Pyton.Last().Figure, Pyton.Last().X);
                    //        Canvas.SetTop(Pyton.Last().Figure, Pyton.Last().Y);

                    //        break;

                    //    case HoldKey.Up:
                    //        Field.Children.Remove(Pyton[0].Figure);

                    //        Pyton.Add(new Segment
                    //        {
                    //            Figure = new Ellipse
                    //            {
                    //                Width = 20,
                    //                Height = 20,
                    //                Fill = Brushes.Green
                    //            },
                    //            X = Pyton.Last().X,
                    //            Y = Pyton.Last().Y - 10,
                    //        });
                    //        Pyton.RemoveAt(0);
                    //        Field.Children.Add(Pyton.Last().Figure);

                    //        Canvas.SetLeft(Pyton.Last().Figure, Pyton.Last().X);
                    //        Canvas.SetTop(Pyton.Last().Figure, Pyton.Last().Y);

                    //        break;


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


                }
            }

            private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        if (holdkey == HoldKey.Left)
                        {
                            holdkey = HoldKey.None;
                        }
                        break;
                    case Key.Right:
                        if (holdkey == HoldKey.Right)
                        {
                            holdkey = HoldKey.None;
                        }
                        break;
                    case Key.Up:
                        if (holdkey == HoldKey.Up)
                        {
                            holdkey = HoldKey.None;
                        }
                        break;

                }
            }
        
    }
}
