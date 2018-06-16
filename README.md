# C# language demo

This project contains some basic definitions of C# features as a language such as:
  * Closure


## Closure

It is called closure to a function, generally speaking, that is able to interact with an environment beyond the parameters provided to it. But that is just an abstract definition.

An **outer variable** is a local variable or parameter (excluding *ref* and *out* parameters) whose scope includes an anonymous method, so in that spirit, the *this* reference also
counts as an outer variable of any anonymous method within an instance member of a class.

A **captured outer variable** is an outer variable that it's used within an anonymous method. So, removing the abstract part in the previous definition, the function part is an anonymous
method and the environment it interacts with is the set of variables captured by it.

> Captured Variables eliminate the need to write extra classes just to store information a delegate needs to act on, beyond what it's passed via parameters.

> Captured Variables lives for at least as long as any delegate instance referring it.

~~~csharp
public Action CapturedVariablesExtendedLife()
{
    int counter = 0;                //normal variable
    return () =>
    {
        counter++;                  //capture counter
        Console.WriteLine(counter); //log method call
    };
}

void Example1()
{
    var action = CapturedVariablesExtendedLife(); //create anonymous function
    for (int i = 0; i < 10; i++)                  //stack frame has ended for above method
        action();                                 //prints 1..10, counter still lives
}
~~~
