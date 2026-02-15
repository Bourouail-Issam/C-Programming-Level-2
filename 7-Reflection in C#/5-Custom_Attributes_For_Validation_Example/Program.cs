using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_Custom_Attributes_For_Validation_Example
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RangeAttribute : Attribute
    {
        public int Min { get; }
        public int Max { get; }

        public string ErrorMessage { get; set; }

        public RangeAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }

    public class clsPerson
    {
        [Range(19, 90, ErrorMessage = "Age must be between 18 and 99.")]
        public int Age { get; set; }
        public string Name { get; set; }

        [RangeAttribute(3, 10, ErrorMessage = "Age must be between 3 and 10.")]
        public int Experience { get; set; }

        public clsPerson(int age, string name, int experience)
        {
            Age = age;
            Name = name;
            Experience = experience;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            
            clsPerson person = new clsPerson(24,"Issam BR", 1);
            if (ValidatePerson(person))
            {
                Console.WriteLine("Person is valid.");
            }
            else
            {
                Console.WriteLine("Validation failed.");
            }
            Console.ReadKey();
        }

        static public bool ValidatePerson(clsPerson person)
        {
            Type type = typeof(clsPerson);


            foreach(var property in type.GetProperties())
            {
                if(Attribute.IsDefined(property, typeof(RangeAttribute)))
                {
                    var rangeAttribute = (RangeAttribute)Attribute.GetCustomAttribute(property,typeof(RangeAttribute));
                    int Value =  (int)property.GetValue(person);

                    if (Value < rangeAttribute.Min || Value > rangeAttribute.Max)
                    {
                        Console.WriteLine($"Validation failed for property '{property.Name}': {rangeAttribute.ErrorMessage}");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
