using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// <summary>
    /// Interaction logic for Minesweeper.xaml
    /// </summary>
    public partial class Minesweeper : Window
    {
        private Random rand;
        public Minesweeper()
        {
            InitializeComponent();

            rand = new Random();

            for (int y = 0; y < App.SizeY; y++)
            {
                for (int x = 0; x < App.SizeX; x++)
                {
                    var mineLabel = new MineLabel
                    {
                        labelState = LabelState.Unvisited,
                        X = x,
                        Y = y,
                        FontSize = 12,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        BorderThickness = new Thickness(0.5),
                        BorderBrush = Brushes.Black,
                        Background = Brushes.LightGray,
                    };
                    
                    mineLabel.MouseDown += MineLabel_MouseDown;
                    this.RegisterName("label_" + x + "_" + y, mineLabel);
                    Field.Children.Add(mineLabel);
                }
            }
            Restart();
        }

        private void MineLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var mineLabel = sender as MineLabel;

            if (mineLabel == null)
            {
                return;
            }

            if (e.RightButton == MouseButtonState.Pressed)
            {
                mineLabel.labelState = (mineLabel.labelState == LabelState.Unvisited) ? 
                                        LabelState.Marked : LabelState.Unvisited;    
                
                if (mineLabel.labelState == LabelState.Marked)
                {
                    mineLabel.Content = "\x2623";
                    mineLabel.Foreground = Brushes.Red;
                }
                else
                {
                    mineLabel.Content = "";
                    mineLabel.Foreground = Brushes.Black;
                }

            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (mineLabel.IsMine)
                {
                    OpenField(mineLabel);

                    if (MessageBoxResult.Yes ==
                    MessageBox.Show("Еще раз", "Игра провалена", MessageBoxButton.YesNo))
                    {
                        Restart();
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }

                InitLabel(mineLabel);

                if (GameOver())
                {
                    MessageBox.Show("YOU WON!!!");
                }
            }
        }

        private void Restart()
        {
            foreach(var child in Field.Children)
            {
                MineLabel label = child as MineLabel;
                if (label != null)
                {
                    bool isMine = rand.Next(4) == 0;
                    label.IsMine = isMine;
                    label.Content = "";
                    label.Foreground = Brushes.Black;
                    label.labelState = LabelState.Unvisited;
                    label.BorderThickness = new Thickness(0.5);
                    label.BorderBrush = Brushes.Black;
                    label.Background = Brushes.LightGray;
                }
            }
        }

        private bool GameOver()
        {
            foreach (var child in Field.Children)
            {
                MineLabel label = child as MineLabel;

                if (!label.IsMine && (label.labelState == LabelState.Unvisited || 
                                     label.labelState == LabelState.Marked))
                {
                    return false;
                }
            }

            return true;
        }

        private void OpenField(MineLabel mineLabel)
        {
            for (int y = 0; y < App.SizeY; y++)
            {
                for (int x = 0; x < App.SizeX; x++)
                {
                    mineLabel = mineLabel.FindName("label_" + x + "_" + y) as MineLabel;
                    if (mineLabel.IsMine)
                    {
                        mineLabel.Content = "\u235f";
                        mineLabel.Background = Brushes.Red;
                    }
                }
            }

            for (int y = 0; y < App.SizeY; y++)
            {
                for (int x = 0; x < App.SizeX; x++)
                {
                    mineLabel = mineLabel.FindName("label_" + x + "_" + y) as MineLabel;
                    if (!mineLabel.IsMine)
                    {
                        InitLabel(mineLabel);
                    }
                }
            }

        }

        private void InitLabel(MineLabel mineLabel)
        {
            mineLabel.labelState = LabelState.Open;
            mineLabel.BorderBrush = Brushes.White;
            mineLabel.Background = Brushes.White;

            // Определяем соседей
            int x = mineLabel.X;
            int y = mineLabel.Y;

            // Массив предполагаемых имен:
            String[] names =
            {
                "label_" + (x-1) + "_" + (y-1),
                "label_" + (x-1) + "_" + (y),
                "label_" + (x-1) + "_" + (y+1),
                "label_" + (x)   + "_" + (y-1),
                "label_" + (x)   + "_" + (y+1),
                "label_" + (x+1) + "_" + (y-1),
                "label_" + (x+1) + "_" + (y),
                "label_" + (x+1) + "_" + (y+1),
            };

            int mines = MinesCounter(names);

            LabelContent(mines, mineLabel);

            // открываем все ячейки, сопряженные с пустыми
            if (mines == 0)
            {
                foreach (String name in names)
                {
                    MineLabel label = this.FindName(name) as MineLabel;

                    if (label != null && label.labelState != LabelState.Open)
                    {
                        InitLabel(label);
                    }
                }
            }
        }

        private int MinesCounter(String[] names)
        {
            //счетчик мин
            int mines = 0;

            foreach (String name in names)
            {
                //Ищем по имени (ссылку на Label)
                MineLabel label = this.FindName(name) as MineLabel;

                if (label != null) //такое имя найдено
                {
                    // Проверяем или это мина
                    if (label.IsMine)
                    {
                        mines++; //увеличиваем счетчик мин
                    }
                }
            }

            return mines;

        }

        private void LabelContent(int mines, MineLabel mineLabel)
        {
            mineLabel.Content = mines;

            var numb = new ColorNumber(mines);

            mineLabel.Foreground = (Brush)System.ComponentModel.TypeDescriptor
                                    .GetConverter(typeof(Brush)).ConvertFromInvariantString(numb.Color);
        }
    }
    class ColorNumber
        {
            public int Num { get; set; }
            public string Color { get; set; }

            public ColorNumber(int num)
            {
                Num = num;

                switch (num)
                {
                    case 0:
                        Color = "White";
                        break;
                    case 1:
                        Color = "DarkGreen";
                        break;
                    case 2:
                        Color = "Blue";
                        break;
                    case 3:
                        Color = "Brown";
                        break;
                    case 4:
                        Color = "Orange";
                        break;
                    case 5:
                        Color = "Pink";
                        break;
                    case 6:
                        Color = "Purple";
                        break;
                    case 7:
                        Color = "Silver";
                        break;
                    case 8:
                        Color = "Beige";
                        break;
                    default:
                        break;
                }
            }

            public override string ToString()
            {
                return Num.ToString();
            }
        }
    class MineLabel : Label
    {
        public bool IsMine { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public LabelState labelState;
    }

    enum LabelState
    {
        Unvisited,
        Marked,
        Open
    }   
}
