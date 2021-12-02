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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Snake.xaml
    /// </summary>
    public partial class Snake : Window
    {
        private List<Segment> Pyton;
        public Snake()
        {
            InitializeComponent();
            Pyton = new List<Segment>();
            for (int i = 0; i < 5; ++i)
            {
                Pyton.Add(new Segment
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var segment in Pyton)
            {
                segment.Show(Field);
            }
        }
    }
}
