namespace PracticumPrograming
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
            InputProducts();

            InputOrders();

            PrintSales();
        }

        /// <summary>
        /// Method for saving orders from clients in the program 
        /// </summary>
        private static void InputOrders()
        {
            var input = "";
            Console.WriteLine("Добре дошли в ресторант \"ФАНТАЗИЯТА НА БИБЛИОТЕКРАКАТА\" - за изход напишете \"изход\".");
            Console.WriteLine("Моля въведете вашата поръчка във формат - 1, продукт1, продукт2, ...");
            while ((input = Console.ReadLine()).ToLower() != "изход")
            {
                //// Prints summary of the current state of the sales
                if (input.ToLower() == "продажби")
                {
                    PrintSales();
                    continue;
                }

                //// Prints info of given product      
                if (input.ToLower().StartsWith("инфо"))
                {
                    PrintProductInfo(input.Replace("инфо", "").Trim());
                    continue;
                }

                string[] instructions = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                int table;

                //// Handler for table number
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

                    orders.Add(products.FirstOrDefault(p => p.Name == product));
                }
            }
        }

        /// <summary>
        /// Method for creating products that can be orderedfrom clients
        /// </summary>
        private static void InputProducts()
        {
            var input = "";
            Console.WriteLine("Въвеждайте ястия и след като се готови напишете \"край\".");
            while ((input = Console.ReadLine()).ToLower() != "край")
            {
                string[] instructions = input.Split(", ", StringSplitOptions.RemoveEmptyEntries);

                //// Handler for adding product successor
                if (instructions.Length < 4)
                {
                    Console.WriteLine("Моля въведете ястието с правилен формат (пример: салата, Шопска салата, 350, 3.50)!");
                    continue;
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
                    continue;
                }

                //// Hadler for correct price
                if (!decimal.TryParse(instructions[3], out price))
                {
                    Console.WriteLine("Въведете коректна цена!");
                    continue;
                }

                //// Creating products
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
                Console.WriteLine($"   -   {Constants.Map[name]}: {orders.Count(p => p.GetType().Name == name)} - {orders.Where(p => p.GetType().Name == name).Sum(p => p.Price)}");
            }
        }
    }
}