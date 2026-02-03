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

        static void Main(string[] args)
        {

            //#####################################
            //############# Exemple 1 #############
            //          Lambda Expression  
            //#####################################
            Console.WriteLine("=>Exemple 1 : use Lambda Expression");

            // Use the square Func to square the number 5
            int result = square(5);

            // Print the result
            Console.WriteLine("The square of 5 is: " + result);

            //#########################################
            //############### Exemple 2 ###############
            //  Action Delegate With Lambda Expression
            //#########################################
            Console.WriteLine("\n=>Exemple 2 : use Action Delegate With Lambda Expression");

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

            Console.ReadKey();
        }
    }
}
