﻿namespace PracticumPrograming
{
    using Products.Beverages;
    using Products.Foods;
    using Products;

    public class Startup
    {

        private static Dictionary<int, Dictionary<string, int>> tables = new();
        private static List<Product> products = new();
        private static List<string> currentProducts = new();
        private static List<Product> orders = new();

        static void Main()
        {
            var input = "";

            Console.WriteLine("Добре дошли в ресторант \"ФАНТАЗИЯТА НА БИБЛИОТЕКРАКАТА\" - за изход напишете \"изход\".");
            Console.WriteLine("Въведете ново ястие, поръчка или команда.");

            while ((input = Console.ReadLine()).ToLower() != "изход")
            {
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Моля въвдете команда!");
                    continue;
                }
                if (Constants.Map.Values.Select(x => x.ToLower()).Contains(input.Split(", ", StringSplitOptions.RemoveEmptyEntries)[0]))
                {
                    InputProducts(input);
                    continue;
                }

                InputOrders(input);
            }

            PrintSales();
        }

        /// <summary>
        /// Method for creating products that can be ordered from clients
        /// </summary>
        private static void InputProducts(string input)
        {
            string[] instructions = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

            //// Handler for adding product successor
            if (instructions.Length != 4)
            {
                Console.WriteLine("Моля въведете ястието с правилен формат (пример: салата, Шопска салата, 350, 3.50)!");
                return;
            }

            var type = instructions[0];
            var name = instructions[1];
            double grams;
            decimal price;

            var correct = true;

            //// Checks if the name contains only letters and white spaces
            foreach (var character in name)
            {
                if (!char.IsLetter(character) && !char.IsWhiteSpace(character))
                {
                    correct = false;
                    break;
                }
            }

            if (!correct)
            {
                Console.WriteLine("Името може да садържа само букви и интервали!");
            }

            //// Hadler for correct quantity
            if (!double.TryParse(instructions[2], out grams))
            {
                Console.WriteLine("Въведете коректно количество!");
                return;
            }

            //// Hadler for correct price
            if (!decimal.TryParse(instructions[3], out price))
            {
                Console.WriteLine("Въведете коректна цена!");
                return;
            }

            //// Handler for already created product
            if (currentProducts.Contains(name))
            {
                Console.WriteLine("Ястие с това име вече присъства в базата!");
                return;
            }

            //// Creating products
            try
            {
                if (type == Constants.Map[nameof(Salad)].ToLower()) products.Add(new Salad(name, grams, price));
                else if (type == Constants.Map[nameof(Soup)].ToLower()) products.Add(new Soup(name, grams, price));
                else if (type == Constants.Map[nameof(Dish)].ToLower()) products.Add(new Dish(name, grams, price));
                else if (type == Constants.Map[nameof(Dessert)].ToLower()) products.Add(new Dessert(name, grams, price));
                else if (type == Constants.Map[nameof(Beverage)].ToLower()) products.Add(new Beverage(name, grams, price));
                else
                {
                    Console.WriteLine($"Въведете една от опциите - {string.Join(", ", Constants.Map.Values.Select(x => x.ToLower()))}");
                    return;
                }

                currentProducts.Add(name);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        /// <summary>
        /// Method for saving orders from clients in the program 
        /// </summary>
        private static void InputOrders(string input)
        {
            //// Prints summary of the current state of the sales
            if (input.ToLower() == "продажби")
            {
                PrintSales();
                return;
            }

            //// Prints info of given product      
            if (input.ToLower().StartsWith("инфо"))
            {
                PrintProductInfo(input.Replace("инфо", "").Trim());
                return;
            }

            string[] instructions = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

            if (instructions.Length < 2)
            {
                Console.WriteLine("Моля въвдете правилна команда!.");
                Console.WriteLine(" - ястието във формат  - продукт, наименование, грамаж, цена!");
                Console.WriteLine(" - поръчка във формат - 1, продукт1, продукт2, ...");
                Console.WriteLine(" - или една от командите \"инфо\" или \"продажби\"");
                return;
            }

            int table;

            //// Handler for table number
            if (!int.TryParse(instructions[0], out table))
            {
                Console.WriteLine($"Ястията могат да бъдат една от опциите - {string.Join(", ", Constants.Map.Values.Select(x => x.ToLower()))}!");
                return;
            }

            if (table < 1 || table > 30)
            {
                Console.WriteLine("Номера на масата може да бъде цяло число от 1 до 30 включително!");
                return;
            }

            if (!tables.ContainsKey(table))
            {
                tables[table] = new Dictionary<string, int>();
            }

            for (int i = 1; i < instructions.Length; i++)
            {
                var product = instructions[i];

                if (!currentProducts.Contains(product))
                {
                    Console.WriteLine($"{product} не присъства в менюто и няма да бъде включен в поръчката!");
                    continue;
                }

                if (!tables[table].ContainsKey(product))
                {
                    tables[table].Add(product, 0);
                }

                tables[table][product]++;

                orders.Add(products.FirstOrDefault(p => p.Name == product));
            }
        }


        /// <summary>
        /// Method for printing info for given product
        /// </summary>
        /// <param name="product">Object of type Product</param>
        private static void PrintProductInfo(string product)
        {
            //// Handler for existing product
            if (!currentProducts.Contains(product))
            {
                Console.WriteLine($"В менюто не присъства продукт с име {product}!");
                return;
            }

            var chosenProduct = products.FirstOrDefault(p => p.Name == product);
            Console.WriteLine(chosenProduct.ToString());
        }

        /// <summary>
        /// Method that prints summary for the current saved orders
        /// </summary>
        private static void PrintSales()
        {
            Console.WriteLine($"Общо заети маси през деня: {tables.Keys.Count}");
            Console.WriteLine($"Общо продажби: {orders.Count} – {orders.Sum(p => p.Price)}");
            Console.WriteLine("По категории:");

            var set = products.Select(p => p.GetType().Name).Distinct().ToList();

            foreach (var name in set)
            {
                Console.WriteLine($"   -   {Constants.Map[name]}: {orders.Count(p => p.GetType().Name == name)} - {orders.Where(p => p.GetType().Name == name).Sum(p => p.Price).ToString("F2")}");
            }
        }
    }
}