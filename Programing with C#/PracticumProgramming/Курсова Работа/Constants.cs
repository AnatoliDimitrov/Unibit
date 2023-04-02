using PracticumPrograming.Products.Beverages;
using PracticumPrograming.Products.Foods;

namespace PracticumPrograming
{
    public static class Constants
    {
        public static readonly Dictionary<string, string> Map = new()
        {
            { nameof(Salad), "Салата" },
            { nameof(Soup), "Супа" },
            { nameof(Dish), "Основно ястие" },
            { nameof(Dessert),"Десерт" },
            { nameof(Beverage), "Напитка" }
        };
    }
}
