using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        List<Product> products = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            #region Добавление_объекта
            //создать объект с данными из полей
            Product product = null;
            try
            {
                product = new Product()
                {
                    SN = Convert.ToInt32(SN.Text),
                    Name = Name.Text,
                    Price = Convert.ToDecimal(Price.Text),
                    Discount = Convert.ToInt32(Discount.Text)
                };
            }
            catch (FormatException)
            {
                MessageBox.Show("Неправильное число");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Слишком большое число");
            }
            if (product == null) return;
            #endregion

            #region добавление объекта в коллекции
            products.Add(product);
            text1.Text += "\n SN: " + product.SN + "\t Name: " + product.Name + "\t Price: " + 
                           product.Price + "\t Discount: " + product.Discount;

            SN.Clear();
            Name.Clear();
            Price.Clear();
            Discount.Clear();
            #endregion
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            #region сериализация коллекции
            XmlSerializer serializer = new XmlSerializer(products.GetType());

            //создаеем файл, в котором будем сохранять результаты серриализации
            using (var writer = new StreamWriter("products.xml"))
            {
                serializer.Serialize(writer, products);
                MessageBox.Show("     Saved");
            }
            #endregion
        }


        public enum ProductComparers
        {
            BySN,
            ByPrice,
            ByName,
            ByDiscountAsc,
            ByDiscountDesc
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                using (StreamReader reader = new StreamReader("products.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
                    products = (List<Product>)serializer.Deserialize(reader);
                }

                ShowProducts();
            }
            catch
            {
                MessageBox.Show("Ошибка чтения файла!!!!)");
            }
        }

        private void ShowProducts(ProductComparers sorter = ProductComparers.BySN)
        {
            switch (sorter)
            {
                case ProductComparers.BySN:
                    products.Sort();
                    break;
                case ProductComparers.ByPrice:
                    products.Sort(new Product.PriceComparer());
                    break;
                case ProductComparers.ByName:
                    products.Sort(new Product.NameComparer());
                    break;
                case ProductComparers.ByDiscountAsc:
                    products.Sort(new Product.DiscountAscComparer());
                    break;
                case ProductComparers.ByDiscountDesc:
                    products.Sort(new Product.DiscountDescComparer());
                    break;
            }

            text1.Clear();
 
            foreach (Product p in products)
            {
                text1.Text += p + "\n";
            }
        }

        private void BySerialNumber(object sender, RoutedEventArgs e)
        {
            ShowProducts(ProductComparers.BySN);
        }

        private void ByPrice(object sender, RoutedEventArgs e)
        {
            ShowProducts(ProductComparers.ByPrice);
        }

        private void ByName(object sender, RoutedEventArgs e)
        {
            ShowProducts(ProductComparers.ByName);
        }

        private void ByDiscountDesc(object sender, RoutedEventArgs e)
        {
            ShowProducts(ProductComparers.ByDiscountDesc);
        }

        private void ByDiscountAsc(object sender, RoutedEventArgs e)
        {
            ShowProducts(ProductComparers.ByDiscountAsc);
        }
    }
}
