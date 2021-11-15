using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Product : IComparable<Product>
    {
        public Int32 SN { get; set; }
        public String Name { get; set; }
        public Decimal Price { get; set; }
        public Int32 Discount { get; set; }

        //должно вернуть отрицательое если наш продукт меньше чем "other", 0 если равно и положительное если больше
        public int CompareTo(Product other)
        {
            //return SN.CompareTo(other.SN);

            if (this.Price < other.Price)
            {
                return -1;
            }

            if (this.Price > other.Price)
            {
                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}{2}{3}", SN.ToString().PadRight(10), Name.PadRight(15), 
                                                    Price.ToString().PadRight(15), Discount.ToString().PadRight(1));
        }

        public class NameComparer : IComparer<Product>
        {
            public int Compare(Product x, Product y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }

        public class PriceComparer : IComparer<Product>
        {
            public int Compare(Product x, Product y)
            {
                return x.Price.CompareTo(y.Price);
            }
        }

        public class DiscountDescComparer : IComparer<Product>
        {
            public int Compare(Product x, Product y)
            {
                return y.Discount.CompareTo(x.Discount);
            }
        }

        public class DiscountAscComparer : IComparer<Product>
        {
            public int Compare(Product x, Product y)
            {
                return x.Discount.CompareTo(y.Discount);
            }
        }
    }
}
