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

> Methods of an interface are implicitly public, and no explicit access specifier is allowed. No member can be declared static.

Every class that implements an interface can be referenced, that is whenever stands a declaration it can also be made the same way as inheritance. Interfaces can have properties and indexers also, but still no explicit access specifier is allowed because its permissions can be declared with a *get* or read permission and *set* write permission.

They also can inherit between them, so when a class is declared to implement an interface it must also implement its parent interface and so on. When is needed a member of an class can be declared *new* so it won't cause any conflict with the interface member's name or desambiguation. Another way for desambiguation is also an explicit interface member implementation, but this one has another hiding use, declaring a member with an explicit syntax means that it can't be accessed by an object instance of the class but as a interface reference.

Interfaces has many uses, to be able to compare any given pair objects there is IComparable, to create collections, read from them or write from them, there is ICollection, but there is one which has much bigger impact on the language starting with taking advantage of the foreach loop syntax, that is IEnumerable and IEnumerator, together they are called iterators and are fully prepared to be lazy which enables some optimization against the overhead of needing an entire collection loaded into memory.

> The term **yield** is a contextual keyword. Outside of an iterator it can be used as any other identifier.

The importance of these interfaces takes a major role whenever working with collections of data, so for that C# introduces a sub-language within itself based on these interfaces that is LINQ.

## LINQ
