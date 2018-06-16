using System;

namespace CSharpLanguageDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ClosureExampleClass.BadVariableCapture();
                ClosureExampleClass.BadVariableCaptureToExecutionError();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e}\n \n === ****** === \n");
            }
            finally
            {
                Console.WriteLine("dasdas");
            }
        }
    }
}
