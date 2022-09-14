using System;

namespace DevidingFrom1To10WithoutReminder
{
    public class Startup
    {
        static void Main()
        {
            int number = 20;
            while (true)
            {
                var isDevisible = true;

                for (int i = 1; i <= 20; i++)
                {
                    if (number % i != 0)
                    {
                        isDevisible = false;
                        break;
                    }
                }

                if (isDevisible)
                {
                    Console.WriteLine($"The smallest number is: {number}");
                    break;
                }

                number++;
            }
        }
    }
}
