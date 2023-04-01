namespace PracticumPrograming.Products.Beverages
{
    using Contracts;

    public class Beverage : Product, IBeverage, ICalories
    {
        private double _mililiters;

        public Beverage(string name, double mililiters, decimal price)
            : base(name, price)
        {
            Mililiters = mililiters;
            Calories = CalculateCalories(Mililiters, 1.5);
        }

        public double Mililiters 
        {
            get 
            {
                return _mililiters;
            }
            private set
            {
                if (value < 0 || value > 1000)
                {
                    throw new ArgumentException("Грамажа на напитката не може да бъде по-малък от 0 и по-голям от 1000 грама!");
                }

                _mililiters = value;
            } 
        }

        public double Calories { get; set; }

        public override string ToString()
        {
            return $"Информация за продукт: {Name}{Environment.NewLine}Милилитри: {Mililiters}{Environment.NewLine}Калории: {Calories}";
        }
    }
}
