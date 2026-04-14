using System;
using System.Text;

public class Kata
{
    public static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
        Console.WriteLine(CreatePhoneNumber(numbers));
    }
    public static string CreatePhoneNumber(int[] numbers)
    {
        StringBuilder result = new StringBuilder("(");
        for (int i = 0; i < numbers.Length; i++)
        {
            if (i == 3) result.Append($") {numbers[i]}");
            else if (i == 6) result.Append($"-{numbers[i]}");
            else result.Append($"{numbers[i]}");
        }
        return result.ToString();
    }
}