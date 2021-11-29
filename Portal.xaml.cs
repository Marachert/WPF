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
    /// Interaction logic for Portal.xaml
    /// </summary>
    public partial class Portal : Window
    {
        public Portal()
        {
            InitializeComponent();
        }

        private void Serialization_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }

        private void FracCalc_Click(object sender, RoutedEventArgs e)
        {
            new FracCalc().Show();
        }

        private void Strings_Click(object sender, RoutedEventArgs e)
        {
            new Strings().Show(); // немодальный режим
        }

        private void Tabs_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Tabs().ShowDialog(); // модальный режим блокирует доступ к предыдущему окну
            this.Show();
        }

        private void Tanks_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Tanks().ShowDialog();
            this.Show();
        }

        private void Minesweeper_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Minesweeper().ShowDialog();
            this.Show();
        }

        private void arkanoid_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Arkanoid().ShowDialog();
            this.Show();
        }

        private void Styles_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new Styles().ShowDialog();
            this.Show();
        }

        private void DnD_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            new DnD().ShowDialog();
            this.Show();
        }
    }
}
