namespace PracticumPrograming
{
    using PracticumPrograming.Products.Beverages;
    using PracticumPrograming.Products.Foods;
    using Products;

    public class Startup
    {

        private static Dictionary<int, Dictionary<string, int>> tables = new();
        private static List<Product> products = new();
        private static List<string> currentProducts = new();
        private static Dictionary<string, string> map = new()
        {
            { "Soup", "Супа" },
            { "Dessert","Десерт" },
            { "Dish", "Основно ястие" },
            { "Salad", "Салата" },
            { "Beverage", "Напитка" }
        };

        static void Main()
        {
            var pr = new Dessert("Палачинка", 150, 3.0m);

            InputProducts();

            InputOrders();

            PrintSales();
        }

        private static void InputOrders()
        {
            var input = "";
            Console.WriteLine("Добре дошли в ресторант \"Фантазияита на библиотекарката\" - за изход напишете \"изход\".");
            Console.WriteLine("Моля въведете вашата поръчка във формат - 1, продукт1, продукт2, ...");
            while ((input = Console.ReadLine()).ToLower() != "изход")
            {
                if (input.ToLower() == "продажби")
                {
                    PrintSales();
                    continue;
                }

                if (input.ToLower().StartsWith("инфо"))
                {
                    PrintProductInfo(input.Replace("инфо", "").Trim());
                    continue;
                }

                string[] instructions = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                int table;

                if (!int.TryParse(instructions[0], out table) && table >= 1 && table <= 30)
                {
                    Console.WriteLine("Номера на масата може да бъде цяло число от 1 до 30 включително!");
                    continue;
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
                }
            }
        }

        private static void InputProducts()
        {
            var input = "";
            Console.WriteLine("Въвеждайте ястия и след като се готови напишете \"край\".");
            while ((input = Console.ReadLine()).ToLower() != "край")
            {
                string[] instructions = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                if (instructions.Length < 4)
                {
                    Console.WriteLine("Моля въведете ястието с правилен формат (пример: салата, Шопска салата, 350, 3.50)!");
                    continue;
                }

                var type = instructions[0];
                var name = instructions[1];
                double grams;
                decimal price;

                if (!name.All(char.IsLetter))
                {
                    Console.WriteLine("Името може да садържа само букви!");
                }

                if (!double.TryParse(instructions[2], out grams))
                {
                    Console.WriteLine("Въведете коректен грамаж!");
                    continue;
                }

                if (!decimal.TryParse(instructions[3], out price))
                {
                    Console.WriteLine("Въведете коректна цена!");
                    continue;
                }

                try
                {
                    if (type == "салата") products.Add(new Salad(name, grams, price));
                    else if (type == "супа") products.Add(new Soup(name, grams, price));
                    else if (type == "основно ястие") products.Add(new Dish(name, grams, price));
                    else if (type == "десерт") products.Add(new Dessert(name, grams, price));
                    else if (type == "напитка") products.Add(new Beverage(name, grams, price));
                    else
                    {
                        Console.WriteLine("Въведете една от опциите 'салата', 'супа', 'основно ястие', 'десерт' или 'напитка'.");
                        continue;
                    }

                    currentProducts.Add(name);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
            }
        }

        private static void PrintProductInfo(string product)
        {
            if (!currentProducts.Contains(product))
            {
                Console.WriteLine($"В менюто не присъства продукт с име {product}!");
                return;
            }

            var chosenProduct = products.FirstOrDefault(p => p.Name == product);
            Console.WriteLine(chosenProduct.ToString());
        }

        private static void PrintSales()
        {
            Console.WriteLine($"Общо заети маси през деня: {tables.Keys.Count}");
            Console.WriteLine($"Общо продажби: {currentProducts.Count} – {products.Sum(p => p.Price)}");
            Console.WriteLine("По категории:");

            var set = products.Select(p => p.GetType().Name).Distinct().ToList();

            foreach (var name in set)
            {
                Console.WriteLine($"   -   {map[name]}: {products.Count(p => p.GetType().Name == name)} - {products.Where(p => p.GetType().Name == name).Sum(p => p.Price)}");
            }
        }
    }
}