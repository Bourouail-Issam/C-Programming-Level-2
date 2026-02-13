using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
namespace _2_Json_Serialization_Example
{
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Person class
            Person person = new Person { Name = "Marouane Manar", Age = 30 };

            // JSON serialization
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Person));
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, person);
                string jsonString = System.Text.Encoding.UTF8.GetString(stream.ToArray());


                // Save the JSON string to a file (optional)
                File.WriteAllText("person.json", jsonString);
            }

            // Deserialize the object back
            using (FileStream stream = new FileStream("person.json", FileMode.Open))
            {
                Person deserializedPerson = (Person)serializer.ReadObject(stream);
                Console.WriteLine($"Name: {deserializedPerson.Name}, Age: {deserializedPerson.Age}");
            }
        }
    }
}
