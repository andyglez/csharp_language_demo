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

## LINQ and iterators

It stands for Language Integrated Query, providing a SQL-like syntax to retrieve information from data sources, so at its core there is the query that will result in an iterable collection or IEnumerable, perfect for integrating C# with all relational database services or even XML and some others. 

About its syntax:

~~~csharp
var query = from range_variable in data_source
            where boolean_expression
            orderby field_to_sort_by
            select expression;
~~~

A query must always begin with a *from* keyword and end with either a *select* or *group* clause. Of course there are other keywords that begin clauses, such as:
  * from
  * group
  * join
  * let
  * orderby
  * select
  * where

A range variable is declared to iterate through the data source and its type is inferred from it, the *where* clause is a boolean expression or a condition for the query, the *orderby* clause defines which field or value from the range variable will the sorting parameter for the result query and finally *select* states what is to be returned by the query. Types of expressions are inferred from context for that reason the query variable should be declared using *var* keyword, so it would an IEnumerable like type for iteration standards in the language.

So, a query is basically an IEnumerable which means that with this definition its body was declared and no a call to it, a call for it is defined whenever used in a foreach loop.

> Enumerables are lazy which means that they consume only whats needed and no more.

~~~csharp
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
~~~

A collection of prime numbers can be created like this and can go to infinity, at least in appearence, because there isn't declared a final value. For that reason, using lazy iterators it is possible to do the next call and never get stuck within the *while(true)* loop.

~~~csharp
foreach (var item in GetPrimes().Where((prime) => prime.ToString().StartsWith("2")).Take(10))
    Console.WriteLine(item);
~~~ 

It calls like this: "get 10 prime numbers where their first digit is 2", so lazy goes in action, to infinity in appereance but only the necessary.

C# uses, by default, an impatient evaluation so a classic Haskell example defined to C# hoping for the same its completely useless since Haskell uses lazy evaluation.

~~~csharp
public static ulong Infinity() => 1 + Infinity();
public static int Four(ulong x) => 4;
~~~

The main way to seize lazy evaluation in C# is through iterators, but they aren't the only ones evaluated this way.

~~~csharp
bool logical = true || Four(Infinity());
bool bitwise = true |  Four(Infinity());
~~~

First, it's clear that the call to Four with Infinity results in a runtime error, and then logical operation recognizes that the left operand is true, so there's no need to evaluate the second. That is because logical operators uses lazy evaluation, instead of bitwise operator who needs to check both operands to compute the result and thus it is impatient.

## Variance

Due to inheritance and polymorphism there exists the term variance which refers to the types and context of the objects and about being able to use an object as another in a type-safe way. At first, there was some collections that supported covariance such as arrays.

~~~csharp
string[] str = new string[10];
object[] obj = str;
obj[0] = new Button();
~~~

This is permissible since object is a parent class for string, and button is a subclass for object, but results eventually will show as runtime errors.
Variance is applied only within interfaces and delegates. There are two types of variance: covariance and contravariance 

Covariance is all about types of values being returned that is, the type parameter will only be used in an output context allowing it to behave and be treated as a parent type but never allowes to assign down in a class hierarchy.

> Covariance specifies output-only type parameters and its polymorphism is permitted only up in a class hierarchy.

Contravariance is the opposite way around, is used to specify the type parameter that will only be used in an input context, allowing it to behave and be treated as a subclass type but never allows to go up in a class hierarchy.

> Contravariance specifies input-only type parameters and its polymorphism is permitted only down in a class hierarchy.

~~~csharp
delegate R MyFunc<in T, out R>(T p);
~~~