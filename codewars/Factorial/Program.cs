using System;

public static class Kata
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Factorial(5));
    }
    public static int Factorial(int n)
    {
        int result = 1;

        if (n < 0 || n > 12) throw new ArgumentOutOfRangeException();
        for (int i = 1; i <= n; i++) result *= i;

        return result;
    }
}