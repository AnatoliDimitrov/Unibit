using System;

namespace PythagoreanTriplet
{
    public class Startup
    {
        static void Main()
        {
            for (int i = 1; i <= 500; i++)
            {
                for (int j = i + 1; j <= 500; j++)
                {
                    double rightSide = Math.Pow(i, 2) + Math.Pow(j, 2);
                    double result = i + j + Math.Sqrt(rightSide);
                    // Console.WriteLine(result);
                    if (result == 1000)
                    {
                        Console.WriteLine($"{i} + {j} + {Math.Sqrt(rightSide)} = {result}");
                        return;
                    }
                }
            }
            Console.WriteLine(Math.Sqrt(25));
        }
    }
}
