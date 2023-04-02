namespace PracticumPrograming.Products.Foods
{
    using Contracts;

    /// <summary>
    /// Soup class successor of Product
    /// </summary>
    public class Soup : Product, IFood
    {
        private double _grams;

        public Soup(string name, double grams, decimal price) : base(name, price)
        {
            Grams = grams;
        }

        public double Grams
        {
            get
            {
                return _grams;
            }
            private set
            {
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentException("Грамажа на супата не може да бъде по-малък от 0 и по-голям от 1000 грама!");
                }

                _grams = value;
            }
        }

        public override string ToString()
        {
            return $"Информация за продукт: {Name}{Environment.NewLine}Грамаж: {Grams}";
        }
    }
}
