public static class Kata
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Solution(10));
    }
    public static int Solution(int value)
    {
        int count = 0;
        for (int i = 2; i < value; i++) if (i % 3 == 0 || i % 5 == 0) count += i;
        return count;
    }
}