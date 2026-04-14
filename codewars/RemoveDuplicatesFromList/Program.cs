using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int[] a = [1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 9];
            Console.WriteLine(string.Join(", ", distinct(a)));
        }
        public static int[] distinct(int[] a)
        {
            return new HashSet<int>(a).ToArray();
        }

    }
}