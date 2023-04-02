namespace PracticumPrograming.Products
{
    using Contracts;

    /// <summary>
    /// Base class for all products
    /// </summary>
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

        /// <summary>
        /// Method for calculating all Products objects
        /// </summary>
        /// <param name="quantity">Quantity in grams or mililiters</param>
        /// <param name="coeficient">Coeficient for calculating the quantity</param>
        /// <returns></returns>
        public virtual double CalculateCalories(double quantity, double coeficient)
        {
            return quantity * coeficient;
        }
    }
}
