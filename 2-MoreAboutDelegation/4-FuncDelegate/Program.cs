using System;


public class Program
{
    // Define a delegate type for squaring a number
    delegate int SquareDelegate(int x);


    //// Define a Func delegate for squaring a number
    static Func<int, int> squareForFuncDelegate = SquareMethod;

    // Define a method that squares a number
    static int SquareMethod(int x) { return x * x; }

    static void Main()
    {
        // ###############################################
        // ############### Normal Delegate ###############
        // ###############################################

        // Create an instance of the SquareDelegate and associate it with the SquareMethod
        SquareDelegate squareForNormalDelegate = new SquareDelegate(SquareMethod);

        //SquareDelegate squareForNormalDelegate = new SquareDelegate(SquareMethod);

        // Use the square delegate to square the number 5
        int resultNormalDelegate = squareForNormalDelegate(5);

        // Print the result
        Console.WriteLine("The square of 5 is: " + resultNormalDelegate);




        // ###############################################
        // ############### Func Delegate ###############
        // ###############################################

        // Use the square Func to square the number 5
        int resultFuncDelegate = squareForFuncDelegate(20);

        // Print the result
        Console.WriteLine("The square of 20 is: " + resultFuncDelegate);
        Console.ReadKey();
    }
}
