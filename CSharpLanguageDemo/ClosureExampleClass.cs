﻿using System;
using System.Collections.Generic;

namespace CSharpLanguageDemo
{
    class ClosureExampleClass
    {
        public static Action CapturedVariablesExtendedLife()
        {
            int counter = 0;
            return () =>
            {
                counter++;
                Console.WriteLine($"CapturedVariablesExtendedLife call no. {counter}");
            };
        }
        public static void BadVariableCapture()
        {
            Console.WriteLine("\n === Bad Variable Capture === \n");
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
            foreach (var act in actions)
                act();                                 // Executes actions code

            Console.WriteLine("\n === ****** === \n");
        }

        public static void BadVariableCaptureToExecutionError()
        {
            Console.WriteLine("\n === Bad Variable Capture To Execution Error === \n");
            var actions     = new List<Action>();
            string[] urls =                                    // A simple array and a list of actions
            {
                "http://www.url.com",
                "http://www.someurl.com",
                "http://www.someanotherurl.com",
                "http://www.yetanotherurl.com"
            };

            for (int i = 0; i < urls.Length; i++)               // As one instance is really the same
                actions.Add(() => Console.WriteLine(urls[i]));  //  value for them all, the 'i' last modified by the for loop

            foreach (var act in actions)
                act();

            Console.WriteLine("\n === ****** === \n");
        }
    }
}
