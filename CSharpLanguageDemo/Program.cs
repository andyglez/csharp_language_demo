﻿using System;
using System.Collections.Generic;
using System.Linq;
using CSharpLanguageDemo.Hierarchy;

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
            Example2();
            Example3();
            Example4();
            Example5();
            Example6();
            Example7();
        }

        static void Example1()
        {
            Console.WriteLine("\n === Captured Variables Extended Life === \n");
            var action = ClosureExampleClass.CapturedVariablesExtendedLife();
            for (int i = 0; i < 10; i++)
                action();
            Console.WriteLine("\n === ******** === \n");
        }

        static void Example2()
        {
            Console.WriteLine("\n === Examples LINQ MyGroupBy === \n");
            var students = new List<MyStudent>() {
                new MyStudent(20, "Andy", "C-312") ,
                new MyStudent(21, "Rafa", "C-312") ,
                new MyStudent(21, "Pedro", "C-311") ,
                new MyStudent(20, "Diana", "C-312") ,
                new MyStudent(20, "Daniela", "C-311") ,
                new MyStudent(21, "Fernando", "C-311") ,
                new MyStudent(21, "Eduardo", "C-312") ,
                new MyStudent(21, "Jose Carlos","C-312"),
                new MyStudent(17, "Claudia", "C-111"),
                new MyStudent(17, "Fulana", "C-111")};
            
            foreach (var group in students.MyGroupBy((x) => x.Group))
            {
                string aux = group.Key.ToString();
                foreach (var student in group)
                {
                    aux += "\t" + student.Name + "\n";
                }
                Console.WriteLine(aux);
            }
            Console.WriteLine("\n === ******** === \n");
        }

        static void Example3()
        {
            Console.WriteLine("\n === Primes Infinite Iterator === \n");

            foreach (var item in Helper.GetPrimes().Where((prime) => prime.ToString().StartsWith("23")).Take(10))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n === ******** === \n");
        }

        static void Example4()
        {
            Console.WriteLine("\n === LINQ syntax === \n");

            var query = from prime in Helper.GetPrimes()
                        where prime < 100
                        select prime;

            Console.WriteLine(query);

            Console.WriteLine("\n === ******** === \n");
        }

        static void Example5()
        {
            Console.WriteLine("\n === Variance Example === \n");
            
            var people = new List<Person>
            {
                new Student  ("Andy"),
                new Assistant("Rafa"),
                new Student  ("Diana"),
                new Teacher  ("KM"),
            };

            foreach (var person in people)
                Console.WriteLine(person.Name);

            PrintByConsole(x => x(new Student("Pedro")));

            Console.WriteLine("\n === ******** === \n");
        }

        static void Example6()
        {
            Console.WriteLine("\n === Operator Overloading Example === \n");

            Complex a = 5.2;
            Complex b = 2.3;
            Console.WriteLine(a ^ b);
            double unbox = (double)a;
            Console.WriteLine(unbox);

            Console.WriteLine("\n === ******** === \n");
        }

        static void Example7()
        {
            Console.WriteLine("\n === Combinations Example === \n");

            var list = new List<int>{ 1, 2, 3, 4};
            int count = 0;
            int k = 3;
            foreach (var comb in list.Combinations(k))
            {
                comb.Print(k, x => Console.Write(x));
                count++;
            }
            Console.WriteLine("\n" + count);
            Console.WriteLine("\n === ******** === \n");
        }

        static void PrintEnumerables<T>(IEnumerable<T> source, int size)
        {

        }
        static void PrintByConsole(Action<Action<Person>> person) => person(x => Console.WriteLine(x.Name));
        static void PrintStudentsOnly(IEnumerable<object> people)
        {
            foreach (var student in people.Where(x => (x is Student)).Cast<Student>())
                Console.WriteLine(student.Name);
        }
        delegate R MyFunc<in T, out R>(T p);
    }
}
