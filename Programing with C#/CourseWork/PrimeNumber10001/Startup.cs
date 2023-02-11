using System;

namespace PrimeNumber10001
{
    internal class Startup
    {
        static void Main()
        {
            long x = 2;
            int counter = 1;
            while (true)
            {
                int prime = 0;
                for (int y = 1; y < x; y++)
                {
                    if (x % y == 0)
                        prime++;

                    if (prime == 2) break;
                }
                if (prime != 2)
                {
                    if (counter == 10001)
                    {
                        Console.WriteLine($"The 10 001-st prime number is: {x}");
                        break;
                    }
                    counter++;
                }

                prime = 0;
                x++;
            }
        }
    }
}
