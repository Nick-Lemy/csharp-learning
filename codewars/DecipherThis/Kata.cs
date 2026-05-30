using System;
using System.Text;

public class Kata
{
    public static string DecipherThis(string s)
    {
        string[] splittedS = s.Split(" ");
        StringBuilder result = new();
        foreach (string str in splittedS) result.Append(DesipherWord(str) + " ");
        return result.ToString().Trim() ?? ""; // Implement me! :
    }

    private static string DesipherWord(string w)
    {
        StringBuilder strBuilder = new(w);
        string numberAsString = "";
        while (char.IsDigit(strBuilder[0]))
        {
            numberAsString += strBuilder[0];
            strBuilder.Remove(0, 1);
        }
        int number = Convert.ToInt32(numberAsString);
        char firstLetter = (char)number;
        strBuilder = new(firstLetter.ToString() + strBuilder.ToString());
        (strBuilder[^1], strBuilder[1]) = (strBuilder[1], strBuilder[^1]);

        return strBuilder.ToString();
    }
}