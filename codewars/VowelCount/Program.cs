using System;
using System.Linq;

public static class Kata
{
    public static void Main(string[] args)
    {
        Console.WriteLine(GetVowelCount("abracadabra"));
    }
    public static int GetVowelCount(string str)
    {
        int vowelCount = 0;
        foreach (char letter in str) vowelCount += "aeiou".Contains(letter) ? 1 : 0;

        return vowelCount;
    }
}
