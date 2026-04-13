namespace ReversedStrings;

public static class Program
{
  public static void Main(string[] args)
  {
    Console.WriteLine(Solution("Reverse"));
  }

  private static string Solution(string str) 
  {
    string result = "";
    
    for (int i=str.Length-1; i >=0; i--)
    {   
      result+=str[i];
    }

    return result;
  }
}