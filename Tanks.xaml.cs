﻿using System;
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
    /// Interaction logic for Tanks.xaml
    /// </summary>
    public partial class Tanks : Window
    {
        RotateTransform rotate0 = new RotateTransform(0, 50, 50);
        RotateTransform rotate90 = new RotateTransform(90, 50, 50);
        RotateTransform rotate180 = new RotateTransform(180, 50, 50);
        RotateTransform rotate270 = new RotateTransform(270, 50, 50);

        DispatcherTimer timer; // внутренее событие - генерируется приложением
        // Наиболее популярные события таймера - периодически посылаемое события с запуском своего делегата

        // признаки нажатия кнопок
        HoldKey holdKey;

        // снаряды
        private List<Bullet> bullets;

        public Tanks()
        {
            InitializeComponent();
            
            // создаем таймер
            timer = new DispatcherTimer();
            
            // задаем интервал тиков
            timer.Interval = TimeSpan.FromMilliseconds(100);

            // делегат - запускаемая функция
            timer.Tick += timer_tick;

            bullets = new List<Bullet>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TankImage.RenderTransform = rotate0;
            // запускаем таймер
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            double coord;
            switch (holdKey)
            {
                case HoldKey.None:
                    break;

                case HoldKey.Left:
                    coord = Canvas.GetLeft(TankImage);
                    if (coord >= 10)
                    {
                        coord -= 10;
                        Canvas.SetLeft(TankImage, coord);
                    }

                    TankImage.RenderTransform = rotate270;
                    break;

                case HoldKey.Right:
                    coord = Canvas.GetLeft(TankImage);

                    if (coord <= this.Width - TankImage.Width - 20)
                    {
                        coord += 10;
                        Canvas.SetLeft(TankImage, coord);
                    }
                    TankImage.RenderTransform = rotate90;
                    break;

                case HoldKey.Up:
                    coord = Canvas.GetTop(TankImage);

                    if (coord > 0)
                    {
                        coord -= 10;
                        Canvas.SetTop(TankImage, coord);
                    }
                    TankImage.RenderTransform = rotate0;
                    break;

                case HoldKey.Down:
                    coord = Canvas.GetTop(TankImage);

                    if (coord <= this.Height - TankImage.Width - 37)
                    {
                        coord += 10;
                        Canvas.SetTop(TankImage, coord);
                    }
                    TankImage.RenderTransform = rotate180;
                    break;
            }

            // полет снарядов

            Bullet toRemove = null;
            foreach(var bullet in bullets)
            {
                double bulletX = Canvas.GetLeft(bullet.Image) + bullet.Vx;
                double bulletY = Canvas.GetTop(bullet.Image) + bullet.Vy;

                Canvas.SetLeft(bullet.Image, bulletX);
                Canvas.SetTop(bullet.Image, bulletY);


                bullet.Trace += Math.Abs((bullet.Vx) + Math.Abs(bullet.Vy));
                // излет
                if (bullet.Trace > 300 || bulletX < 0 || bulletY < 0 ||
                    bulletX > Field.Width || bulletY > Field.Height)
                {
                    // Убираем с холста
                    Field.Children.Remove(bullet.Image);
                    // помечаем на удаление
                    toRemove = bullet;
                }
            }

            if (toRemove != null) bullets.Remove(toRemove);

            Collisions();
        }

        private void Collisions() // попадания снарядов в мишень
        {
            Bullet toRemove = null;
            foreach (var bullet in bullets)
            {
                double centerX = Canvas.GetLeft(bullet.Image) + bullet.Image.Width / 2;
                double centerY = Canvas.GetTop(bullet.Image) + bullet.Image.Height / 2;
                double targetX = Canvas.GetLeft(Enemy) + Enemy.Width / 2;
                double targetY = Canvas.GetTop(Enemy) + Enemy.Height / 2;

                //попадание - расстояние между центрами меньше суммы радиусов
                double distance = Math.Sqrt((centerX - targetX) * (centerX - targetX) +
                                            (centerY - targetY) * (centerY - targetY));

                if (distance < bullet.Image.Width / 2 + Enemy.Width / 2)
                {
                    Enemy.Width--;
                    Enemy.Height--;
                    toRemove = bullet;
                }
            }

            if (toRemove != null)
            {
                bullets.Remove(toRemove);
                Field.Children.Remove(toRemove.Image);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    holdKey = HoldKey.Left;
                    break;

                case Key.Right:
                    holdKey = HoldKey.Right;
                    break;

                case Key.Up:
                    holdKey = HoldKey.Up;
                    break;

                case Key.Down:
                    holdKey = HoldKey.Down;
                    break;

                case Key.Space: // Выстрел
                    // Создаем пулю (если его нет)
                    if (bullets.Count < 6)
                    { 
                        double tankX = Canvas.GetLeft(TankImage);
                        double tankY = Canvas.GetTop(TankImage);
                        int vx = 0;
                        int vy = 0;
                        int bulletRadius = 5;

                        // как повернут?
                        if (TankImage.RenderTransform == rotate0)
                        {
                            //вверх
                            tankX += TankImage.Width / 2 - bulletRadius;
                            vy = -20;
                        }

                        if (TankImage.RenderTransform == rotate90)
                        {
                            // вправо        
                            tankX += TankImage.Width;
                            tankY += TankImage.Height / 2 - bulletRadius;
                            vx = 20;
                        }

                        if (TankImage.RenderTransform == rotate180)
                        {
                            //вниз
                            tankX += TankImage.Width / 2  - bulletRadius;
                            tankY += TankImage.Height;
                            vy = 20;
                        }

                        if (TankImage.RenderTransform == rotate270)
                        {
                            //влево
                            tankY += TankImage.Height / 2 - bulletRadius;
                            vx = -20;
                        }

                        var bullet = new Bullet
                        {
                            Image = new Ellipse
                            {
                                Fill = Brushes.RosyBrown,
                                Width = 2 * bulletRadius,
                                Height = 2 * bulletRadius,
                            },
                            Vx = vx,
                            Vy = vy,
                            Trace = 0
                        };
                        // Добавляем на холст
                        Field.Children.Add(bullet.Image);
                        // Выставляем координаты
                        // Где танк?
                        Canvas.SetLeft(bullet.Image, tankX);
                        Canvas.SetTop(bullet.Image, tankY);
                        // включаем в коллекцию
                        bullets.Add(bullet);
                    }
                    break;
            }
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (holdKey == HoldKey.Left)
                    {
                        holdKey = HoldKey.None;
                    }
                    break;

                case Key.Right:
                    if (holdKey == HoldKey.Right)
                    {
                        holdKey = HoldKey.None;
                    }

                    break;

                case Key.Up:
                    if (holdKey == HoldKey.Up)
                    {
                        holdKey = HoldKey.None;
                    }
                    break;

                case Key.Down:
                    if (holdKey == HoldKey.Down)
                    {
                        holdKey = HoldKey.None;
                    }
                    break;
            }
        }

    }

    public enum HoldKey
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public class Bullet
    {
        public Ellipse Image { get; set; }
        public int Vx { get; set; }
        public int Vy { get; set; }
        public int Trace { get; set; } // проделанный путь
    }
}
