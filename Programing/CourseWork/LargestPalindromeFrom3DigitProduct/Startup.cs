namespace LargestPalindromeFrom3DigitProduct
{
    public class Startup
    {
        static void Main()
        {
            var result = 0;
            for (int i = 999; i >= 100; i--)
            {
                for (int j = 999; j >= 100; j--)
                {
                    int num = i * j;
                    string str = num.ToString();
                    char[] charArray = str.ToCharArray();
                    Array.Reverse(charArray);
                    string reversedStr = new string(charArray);
                    if (str == reversedStr)
                    {
                        result = Math.Max(result, num);
                    }
                }
            }

            Console.WriteLine($"The largest palindrome is: {result}");
        }
    }
}