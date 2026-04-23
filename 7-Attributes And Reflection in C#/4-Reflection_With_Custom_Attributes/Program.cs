using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _4_Reflection_With_Custom_Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
    public class MyCustomAttribute : Attribute
    {
        public string Description { get; }
        public MyCustomAttribute(string description)
        {
            Description = description;
        }
    }


    [MyCustom("This is a class To Create or Update person")]
    class clsPerson
    {
        public string Name { get; set; }
        public int Age {  get; set; }

        [MyCustom("This is a class To Create or Update person")]
        public int Permission {  get; }

        [MyCustom("This is a method attribute for save person")]
        public void Save()
        {
            // Method implementation
        }

        public void Add()
        {
            // Method implementation
        }
        [MyCustom("This is a method attribute for Update person")]
        public void Update()
        {
            // Method implementation
        }
    }

    class Program
    {
        static void Main()
        {
            // Get the type of MyClass
            Type typeClsPerson = typeof(clsPerson);


            // Get class-level attributes
            object[] classAttributes = typeClsPerson.GetCustomAttributes(typeof(MyCustomAttribute), false);
            foreach (MyCustomAttribute attribute in classAttributes)
            {
                Console.WriteLine($"Class Attribute: {attribute.Description}");
            }


            // Get method-level attributes
            //MethodInfo methodInfo = typeClsPerson.GetMethod("MyMethod");
            //object[] methodAttributes = methodInfo.GetCustomAttributes(typeof(MyCustomAttribute), false);
            //foreach (MyCustomAttribute attribute in methodAttributes)
            //{
            //    Console.WriteLine($"Method Attribute: {attribute.Description}");
            //}

            MethodInfo[] meths_clsPerson = typeClsPerson.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (MethodInfo m in meths_clsPerson)
            {
                if (Attribute.IsDefined(m, typeof(MyCustomAttribute)))
                {
                    object[] methodAttributes = m.GetCustomAttributes(typeof(MyCustomAttribute), false);
                    foreach(MyCustomAttribute attribute in methodAttributes)
                    {
                        Console.WriteLine($"Method Attribute: {attribute.Description}");
                    }
                }
            }
        }
    }
}
