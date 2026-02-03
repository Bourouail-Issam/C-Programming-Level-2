using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_LambadaExpression
{
    public class Program
    {
        // Define a Func delegate for squaring a number using a lambda expression
        static Func<int, int> square = x => x * x;

        // A delegate that represents an operation
        delegate int Operation(int x, int y);

        // A function that takes a delegate with parameters and invokes it
        static void ExecuteOperation(int x, int y, Operation operation)
        {
            int result = operation(x, y); // Invoke the provided delegate
            Console.WriteLine("Result: " + result);
        }

        // now we use fun and lambda Expression 
        static void ExecuteOperation(int x, int y, Func<int, int, int> Operation)
        {
            int result = Operation(x, y); // Invoke the provided delegate
            Console.WriteLine("Result: " + result);
        }

        // A method that performs addition
        static int Add(int x, int y)
        {
            return x + y;
        }

        // A method that performs subtraction
        static int Sub(int x, int y)
        {
            return x - y;
        }

        static void Main(string[] args)
        {

            //#####################################
            //############# Exemple 1 #############
            //          Lambda Expression  
            //#####################################
            Console.WriteLine("=> Exemple 1 : use Lambda Expression");

            // Use the square Func to square the number 5
            int result = square(5);

            // Print the result
            Console.WriteLine("The square of 5 is: " + result);

            //#########################################
            //############### Exemple 2 ###############
            //  Action Delegate With Lambda Expression
            //#########################################
            Console.WriteLine("\n=> Exemple 2 : use Action Delegate With Lambda Expression");

            Action parameterlessAction = () =>
            {
                Console.WriteLine("This is a parameterless action.");
            };

            Action<int> actionWithIntParameter = (x) =>
            {
                Console.WriteLine($"Action with int parameter: {x}");
            };

            Action<string, int> actionWithMultipleParameters = (str, num) =>
            {
                Console.WriteLine($"Action with string and int parameters: {str}, {num}");
            };

            // Invoking the actions

            parameterlessAction();
            actionWithIntParameter(42);
            actionWithMultipleParameters("Hello, World!", 100);

            //#########################################
            //############### Exemple 3 ###############
            //       Delegate Example No Lambda
            //#########################################
            Console.WriteLine("\n=> Exemple 3 : Use Delegate Example No Lambda");

            // Use the Add method with the delegate
            Operation AddOp = Add;
            Operation SubOp = Sub;

            ExecuteOperation(10, 20, AddOp); // Pass the delegate as an argument
            ExecuteOperation(10, 20, SubOp); // Pass the delegate as an argument


            //#########################################
            //############### Exemple 4 ###############
            //   Func Delegate with lambda Example
            //#########################################
            Console.WriteLine("\n=> Exemple 4 : Use Func Delegate with lambda Example");

            // now use the way 2 instead of previous way
            // Use a lambda expression for addition
            Func<int, int, int> AddUseFunc = (x, y) => x + y;
            Func<int, int, int> SubUseFunc = (x, y) => x - y;

            ExecuteOperation(10, 20, AddUseFunc); // Pass the lambda expression as an argument
            ExecuteOperation(10, 20, SubUseFunc); // Pass the lambda expression as an argument

            Console.ReadKey();
        }
    }
}
