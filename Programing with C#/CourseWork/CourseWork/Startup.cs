namespace CourseWork
{
    public class Startup
    {
        static void Main()
        {
            var result = 0;
            for (int i = 1; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    result += i;
                }
            }
            Console.WriteLine($"The sum of numbers multilied by 3 or 5 below 1000 is: {result}");
        }
    }
}