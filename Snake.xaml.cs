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
        MoveDirection moveDirection;

        private Random random;
        private Segment food;

        public Snake()
        {
            InitializeComponent();
            timer = new DispatcherTimer(); //Системный ресурс, зависит от системы. Как "запускатель" функции
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerTick;
            timer.Tick += TimerTickFood;
            random = new Random();

            Pyton = new LinkedList<Segment>();

            for (int i = 0; i < 5; ++i)
            {
                Pyton.AddLast(new Segment
                {
                    Figure = new Ellipse
                    {
                        Width = Segment.FIGURE_SIZE,
                        Height = Segment.FIGURE_SIZE,
                        Fill = new SolidColorBrush(
                            Color.FromRgb(
                                (byte)random.Next(100,250),
                                (byte)random.Next(100, 250),
                                (byte)random.Next(100, 250)))
                    },
                    X = Segment.FIGURE_SIZE * 10 + i * Segment.FIGURE_SIZE,
                    Y = Segment.FIGURE_SIZE
                });
            }
            food = RandFood();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    PytonStep(-Segment.FIGURE_SIZE, 0d);
                    break;

                case MoveDirection.Right:
                    PytonStep(Segment.FIGURE_SIZE, 0d);
                    break;

                case MoveDirection.Up:
                    PytonStep(0d, -Segment.FIGURE_SIZE);
                    break;

                case MoveDirection.Down:
                    PytonStep(0d, Segment.FIGURE_SIZE);
                    break;
            }
        }

        private void TimerTickFood(object sender, EventArgs e)
        {
            if (Pyton.First().X == food.X && Pyton.First().Y == food.Y)
            {
                Pyton.AddLast(food);
                food.Remove(Field);
                food = RandFood();
                food.Show(Field);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var segment in Pyton)
            {
                segment.Show(Field);
            }
            food.Show(Field);
            timer.Start();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (moveDirection != MoveDirection.Right)
                    {
                        moveDirection = MoveDirection.Left;
                    }
                    break;
                case Key.Right:
                    if (moveDirection != MoveDirection.Left)
                    {
                        moveDirection = MoveDirection.Right;
                    }
                    break;
                case Key.Up:
                    if (moveDirection != MoveDirection.Down)
                    {
                        moveDirection = MoveDirection.Up;
                    }
                    break;
                case Key.Down:
                    if (moveDirection != MoveDirection.Up)
                    {
                        moveDirection = MoveDirection.Down;
                    }
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
                    Width = Segment.FIGURE_SIZE,
                    Height = Segment.FIGURE_SIZE,
                    Fill = new SolidColorBrush(
                            Color.FromRgb(
                                (byte)random.Next(100, 250),
                                (byte)random.Next(100, 250),
                                (byte)random.Next(100, 250)))
                },
                X = Pyton.First().X + horizontal,
                Y = Pyton.First().Y + vertical,
            });

            Pyton.Remove(Pyton.Last());

            Pyton.First().Show(Field);
        }

        private bool IsPytonPart(double coordX, double coordY)
        {
            foreach (Segment part in Pyton)
            {
                if (coordX == part.X && coordY == part.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private Segment RandFood()
        {
            double randomX;
            double randomY;

            do
            {
                randomX = random.Next(80) * Segment.FIGURE_SIZE;
                randomY = random.Next(50) * Segment.FIGURE_SIZE;

            } while (IsPytonPart(randomX, randomY));

            Segment food = new Segment
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

            return food;
        }

        private enum MoveDirection
        {
            Left,
            Right,
            Up,
            Down
        }
    }
}
