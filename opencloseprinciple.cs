using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesignPatternsusingC_
{
    public enum Size
    {
        Small = 0, Medium = 1, Large = 2
    }

    public enum Color
    {
        Red = 0, Blue = 1, Green = 2, Magenta = 3, Yellow = 4
    }

    public class Product
    {
        public string name { get; set; }

        public Size size { get; set; }

        public Color color { get; set; }

        public List<Product> products { get; set; }

        public Product(string name, Color color, Size size)
        {
            this.name = name;
            this.color = color;
            this.size = size;
        }
    }
    public class FindProduct
    {
        public Product FindProductBySize(List<Product> products, Size s)
        {
            foreach (var item in products)
            {
                if (item.size == s)
                    return item;
            }
            return null;
        }

        public Product FindProductByColor(List<Product> products, Color c)
        {
            foreach (var item in products)
            {
                if (item.color == c)
                    return item;
            }
            return null;
        }

        // If new requiremnet comes in which we have to create a filter to search both by Size and Color then it will voilate open close principle and we will have to rewirte our existing method / rewrite again FindProduct class
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    public class ColorSpeicification : ISpecification<Product>
    {
        private Color _color;

        public ColorSpeicification(Color color)
        {
            _color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.color == _color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product p)
        {
            return p.size == size;
        }
    }

    public class MultiSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public MultiSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(T p)
        {
            return first.IsSatisfied(p) && second.IsSatisfied(p);
        }
    }

    public class GenericFilter : IFilter<Product>
    {
        // this functionality will remain same, every time if a new filter is required, we will implement a new specification interface
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var product in items)
            {
                if (specification.IsSatisfied(product))
                {
                    yield return product;
                }
            }
        }
    }

    internal class opencloseprinciple
    {
        GenericFilter gf = new GenericFilter();
        public void Test()
        {
            var dress1 = new Product("dress 1", Color.Green, Size.Small);
            var dress2 = new Product("dress 2", Color.Green, Size.Large);
            var dress3 = new Product("dress 3", Color.Blue, Size.Large);

            Product[] products = { dress1, dress2, dress3 };

            foreach (var item in gf.Filter(products, new ColorSpeicification(Color.Green)))
            {
                Console.WriteLine("Green dress is " + item.name);               
            }

            foreach (var item in gf.Filter(products, new SizeSpecification(Size.Large)))
            {
                Console.WriteLine("Large dress is " + item.name);
            }

            /// Check both conditions
            foreach (var item in gf.Filter(products, new MultiSpecification<Product>(new ColorSpeicification(Color.Green),new SizeSpecification(Size.Large))))
            {
                Console.WriteLine("Green Large dress is " + item.name);
            }
        }
    }
}
