using System;
using System.Collections.Generic;

namespace CSharpLanguageDemo
{
    static class Helper
    {
        public static bool IsPrime(int x)
        {
            for (int i = 2; i <= Math.Sqrt(x); i++)
                if (x % i == 0)
                    return false;
            return true;
        }


        // Iterators are lazy
        public static IEnumerable<int> GetPrimes()
        {
            int i = 2;
            while (true)
            {
                if (IsPrime(i))
                    yield return i;
                i++;
            }
        }
        
        // C# as an imperative language doesn't have lazy evaluation by default
        public static ulong Infinity() => 1 + Infinity();
        public static int Four(ulong x) => 4;
    }
}
