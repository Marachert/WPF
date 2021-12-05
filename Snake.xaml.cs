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
        private Pyton pyton;
        FoodFor apple;
        DispatcherTimer timer;
        MoveDirection moveDirection;
        private Random random;
        private double speed;

        RotateTransform rotate0 = new RotateTransform(0, Segment.FIGURE_SIZE / 2, Segment.FIGURE_SIZE / 2);
        RotateTransform rotate90 = new RotateTransform(90, Segment.FIGURE_SIZE / 2, Segment.FIGURE_SIZE / 2);
        RotateTransform rotate180 = new RotateTransform(180, Segment.FIGURE_SIZE / 2, Segment.FIGURE_SIZE / 2);
        RotateTransform rotate270 = new RotateTransform(270, Segment.FIGURE_SIZE / 2, Segment.FIGURE_SIZE / 2);

        public Snake()
        {
            InitializeComponent();
            speed = 100;
            timer = new DispatcherTimer(); //Системный ресурс, зависит от системы. Как "запускатель" функции
            timer.Interval = TimeSpan.FromMilliseconds(speed);
            timer.Tick += TimerTick;
            timer.Tick += TimerTickFood;
            random = new Random();

            pyton = new Pyton(Field);
            apple = new FoodFor(pyton, Field);
            moveDirection = MoveDirection.Right;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    pyton.Body[0].Figure.RenderTransform = rotate180;
                    pyton.Move(-Segment.FIGURE_SIZE, 0d);
                    break;

                case MoveDirection.Right:
                    pyton.Body[0].Figure.RenderTransform = rotate0;
                    pyton.Move(Segment.FIGURE_SIZE, 0d);
                    break;

                case MoveDirection.Up:
                    pyton.Body[0].Figure.RenderTransform = rotate270;
                    pyton.Move(0d, -Segment.FIGURE_SIZE);
                    break;

                case MoveDirection.Down:
                    pyton.Body[0].Figure.RenderTransform = rotate90;
                    pyton.Move(0d, Segment.FIGURE_SIZE);
                    break;
            }
        }

        private void TimerTickFood(object sender, EventArgs e)
        {
            if (pyton.Body.First().X == apple.Fruit.X && 
                pyton.Body.First().Y == apple.Fruit.Y)
            {
                speed -= 1;

                if (speed == 50)
                {
                    speed = 100;
                }

                timer.Interval = TimeSpan.FromMilliseconds(speed);
                pyton.Body.Add(apple.Fruit);
                apple.Remove();
                apple = new FoodFor(pyton, Field);
                apple.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pyton.Show();
            apple.Show();
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

        private enum MoveDirection
        {
            Left,
            Right,
            Up,
            Down
        }
    }
}
