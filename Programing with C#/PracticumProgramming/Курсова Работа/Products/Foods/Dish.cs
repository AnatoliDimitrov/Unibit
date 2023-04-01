namespace PracticumPrograming.Products.Foods
{
    using Contracts;

    public class Dish : Product, IFood, ICalories
    {
        private double _grams;

        public Dish(string name, double grams, decimal price) : base(name, price)
        {
            Grams = grams;
            Calories = CalculateCalories(Grams, 1.0);
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
                    throw new ArgumentException("Грамажа на ястието не може да бъде по-малък от 0 и по-голям от 1000 грама!");
                }

                _grams = value;
            }

        }

        public double Calories { get; private set; }

        public override string ToString()
        {
            return $"Информация за продукт: {Name}{Environment.NewLine}Грамаж: {Grams}{Environment.NewLine}Калории: {Calories}";
        }
    }
}
