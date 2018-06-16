# C# language demo

This project contains some basic definitions of C# features as a language such as:
  * Closure
  * Interfaces


## Closure

It is called closure to a function, generally speaking, that is able to interact with an environment beyond the parameters provided to it. But that is just an abstract definition.

An **outer variable** is a local variable or parameter (excluding *ref* and *out* parameters) whose scope includes an anonymous method, so in that spirit, the *this* reference also
counts as an outer variable of any anonymous method within an instance member of a class.

A **captured outer variable** is an outer variable that it's used within an anonymous method. So, removing the abstract part in the previous definition, the function part is an anonymous
method and the environment it interacts with is the set of variables captured by it.

> Captured Variables eliminate the need to write extra classes just to store information a delegate needs to act on, beyond what it's passed via parameters.

> Captured Variables lives for at least as long as any delegate instance referring it.

The C# compiler creates such extra classes automatically, allowing it to create a copy from the value currently in the stack frame of its scope into a whole new object and thus reusing it wherever the anonymous function is called into action.

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

> A captured variable to an anonymous function depends on its instance

~~~csharp
var actions  = new List<Action>();
string[] num = {1, 2, 3, 4};                        // A simple array and a list of actions

for (int i = 0; i < num.Length; i++)               // As one instance is really the same foreach
    actions.Add(() => Console.WriteLine(num[i]));  // action in list, the i modified by the for loop

foreach (var act in actions)
    act();           // This will result on runtime error (since it's calling Console.WriteLine(num[4]))
~~~

As a solution, a variable must be created inside the for loop so that foreach iteration a new and different instance of a local variable is captured.

~~~csharp
for (int i = 0; i < num.Length; i++)
{
    int j = i;                                      // Create a new instance for the value of 'i'
    actions.Add(() => Console.WriteLine(num[j]));   // Capture that new instance instead
}   // No runtime errors for this one
~~~

## Interfaces

Interfaces are syntactically similar to abstract classes. That is, an interface provides no implementation, once it is defined what must be done, any number of classes can implement it, just like one class can implement any number of interfaces, which allows C# to offer a solution for multiple inheritance without explicitly allowing it.


