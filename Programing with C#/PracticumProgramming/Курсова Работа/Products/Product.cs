namespace PracticumPrograming.Products
{
    using Contracts;

    public abstract class Product : IProduct
    {
        private string _name;
        private decimal _price;

        protected Product(string name, decimal price) 
        { 
            Name = name;
            Price = price;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            }
            private set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Цената не може да бъде по-малка от 0 и по-голяма от 100!");
                }
                _price = value;
            }
        }

        public virtual double CalculateCalories(double quantity, double coeficient)
        {
            return quantity * coeficient;
        }
    }
}
