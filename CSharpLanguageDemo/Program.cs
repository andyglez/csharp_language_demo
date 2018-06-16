using System;

namespace CSharpLanguageDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try { ClosureExampleClass.BadVariableCaptureToExecutionError(); }
            catch (Exception e) { Console.WriteLine($"{e}\n \n === ****** === \n"); }

            ClosureExampleClass.BadVariableCapture();
            Example1();
            
        }

        static void Example1()
        {
            Console.WriteLine("\n === Captured Variables Extended Life === \n");
            var action = ClosureExampleClass.CapturedVariablesExtendedLife();
            for (int i = 0; i < 10; i++)
                action();
            Console.WriteLine("\n === ******** === \n");
        }
    }
}
