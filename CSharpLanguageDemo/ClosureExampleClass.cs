using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLanguageDemo
{
    class ClosureExampleClass
    {
        public static void BadVariableCapture()
        {
            var actions = new Action[10];              // One instance for the loop's variable
            for (int x = 0; x < actions.Length; x++)
            {
                int y = x;                             // Multiple instances of variable y
                actions[x] = () =>
                {
                    int z = x;                         // Local Variable
                    Console.WriteLine($"{x} {y} {z}"); // Capturing outer variables
                };
            }
        }
    }
}
