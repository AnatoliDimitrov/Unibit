namespace BiggestPrimeFactor
{
    public class Startup
    {
        static void Main()
        {
            long number = 600851475143;
            long CurrMaxPrime = -1;
            if (number % 2 == 0)
            {
                CurrMaxPrime = 2;
                while (number % 2 == 0)
                {
                    number = number / 2;
                }
            }
            for (long i = 3; i <= Math.Sqrt(number); i += 2)
            {
                while (number % i == 0)
                {
                    CurrMaxPrime = i;
                    number = number / i;
                }
            }
            if (number > 2)
            {
                CurrMaxPrime = number;
            }

            Console.WriteLine($"The largest prime factor of 600851475143 is: {CurrMaxPrime}");
        }
    }
}