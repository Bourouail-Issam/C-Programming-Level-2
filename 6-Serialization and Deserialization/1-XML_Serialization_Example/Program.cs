using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _1_XML_Serialization_Example
{
    // It is not necessary to add the [Serializable] attribute to the Person class
    // to enable XML serialization/deserialization.
    [Serializable] 
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Age { get; set; }

        public Person(int ID,string Name,short Age)
        {
            this.Id = ID;
            this.Name = Name;
            this.Age = Age;
        }
        // Required by XmlSerializer to create the object during deserialization
        public Person()
        {
        }   
    }
    public class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Person class
            Person person = new Person(1,"Micheal",42);
            Person person2 = new Person {Id=2,Name="Jeon",Age=29};


            // XML serialization
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            using (StreamWriter writer = new StreamWriter("person.xml"))
            {
                serializer.Serialize(writer, person);
            }

            // Deserialize the object back
            using (StreamReader reader = new StreamReader("person.xml"))
            {
                Person Deserialize  = (Person)serializer.Deserialize(reader);

                Console.WriteLine($"ID : {Deserialize.Id} \nName : {Deserialize.Name} \nAge : {Deserialize.Age}");
            }
        }
    }
}
