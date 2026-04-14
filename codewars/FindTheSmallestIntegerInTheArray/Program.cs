using System.Linq;

public class Kata
{
    public static void Main(string[] args)
    {
        int[] numbers = [5, 3, 2, -74, 1, 4];
        Console.WriteLine(FindSmallestInt(numbers));
    }
    public static int FindSmallestInt(int[] args)
    {
        return args.Min();
    }
}