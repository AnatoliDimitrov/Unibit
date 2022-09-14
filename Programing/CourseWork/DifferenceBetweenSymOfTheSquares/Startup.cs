using System;

namespace DifferenceBetweenSymOfTheSquares
{
    public class Startup
    {
        static void Main()
        {
            double sum = 0;
            double sumOfNumbers = 0;

            for (int i = 1; i <= 100; i++)
            {
                sum += Math.Pow(i, 2);
                sumOfNumbers += i;
            }

            //Console.WriteLine(sum);
            //Console.WriteLine(Math.Pow(sumOfNumbers, 2));
            Console.WriteLine($"the difference is: {Math.Pow(sumOfNumbers, 2) - sum}");
        }
    }
}
