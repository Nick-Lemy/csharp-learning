using System;
using System.Collections.Generic;
public class Program
{
    public static void Main()
    {
        Console.WriteLine(Switcher(["24", "12", "23", "22", "4", "26", "9", "8"]));
    }
    public static string Switcher(string[] x)
    {
        List<char> resultChars = [];
        List<char> alphabet = [.. " ?!abcdefghijklmnopqrstuvwxyz-".Reverse()];
        foreach (string str in x) resultChars.Add(alphabet[Convert.ToInt32(str)]);
        return string.Join("", resultChars);
    }

}

