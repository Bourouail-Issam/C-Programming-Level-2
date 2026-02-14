using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_Type_Class
{
    public class Program
    {
        static void Main(string[] args)
        {

            Type type = typeof(String);

            Console.WriteLine("\nType Information : \n");
            Console.WriteLine($"Name : {type.Name}");
            Console.WriteLine($"Full Name : {type.FullName}");
            Console.WriteLine($"Is Class: {type.IsClass}");
        }
    }
}
