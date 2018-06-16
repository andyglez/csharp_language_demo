# C# language demo

This project contains some basic definitions of C# features as a language such as:
  * Closure


## Closure

It is called closure to a function, generally speaking, that is able to interact with an environment beyond the parameters provided to it. But that is just an abstract definition.

An **outer variable** is a local variable or parameter (excluding *ref* and *out* parameters) whose scope includes an anonymous method, so in that spirit, the *this* reference also
counts as an outer variable of any anonymous method within an instance member of a class.

A **captured outer variable** is an outer variable that it's used within an anonymous method. So, removing the abstract part in the previous definition, the function part is an anonymous
method and the environment it interacts with is the set of variables captured by it.
