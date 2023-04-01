namespace PracticumPrograming.Products.Foods
{
    using Contracts;

    public class Salad : Product, IFood
    {
        private double _grams;

        public Salad(string name, double grams, decimal price) : base(name, price)
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
                    throw new ArgumentException("Грамажа на салатата не може да бъде по-малък от 0 и по-голям от 1000 грама!");
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
