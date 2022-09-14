namespace Fibonacci
{
    public class Startup
    {
        static void Main()
        {
            var currentTerm = 0;
            var oneBack = 0;
            var twoBack = 0;

            long sumEvenNumbers = 0;

            while (true)
            {
                if (oneBack == 0)
                {
                    oneBack = 2;
                    twoBack = 1;
                    sumEvenNumbers += 2;
                }

                currentTerm = oneBack + twoBack;

                if (currentTerm > 4_000_000)
                {
                    break;
                }

                if (currentTerm % 2 == 0)
                {
                    sumEvenNumbers += currentTerm;
                }

                twoBack = oneBack;
                oneBack = currentTerm;
            }

            Console.WriteLine($"The sum of even numbers is: {sumEvenNumbers}");
        }
    }
}