using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSVtoLINQ
{
    // Define the Person class
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Pratcice
    {
        static void LetsDoIt(string[] args)
        {
            // Path to the CSV file
            string csvFilePath = "people.csv";

            // Read the CSV file and create a list of Person objects
            List<Person> people = ReadCSVFile(csvFilePath);

            // Example LINQ queries
            var youngPeople = from person in people
                              where person.Age < 30
                              select person;

            var orderedByAge = from person in people
                               orderby person.Age
                               select person;

            // Display results of the queries
            Console.WriteLine("People younger than 30:");
            foreach (var person in youngPeople)
            {
                Console.WriteLine($"{person.Name}, Age: {person.Age}");
            }

            Console.WriteLine("\nPeople ordered by age:");
            foreach (var person in orderedByAge)
            {
                Console.WriteLine($"{person.Name}, Age: {person.Age}");
            }
        }

        // Method to read CSV file and return a list of Person objects
        static List<Person> ReadCSVFile(string filePath)
        {
            var people = new List<Person>();

            using (var reader = new StreamReader(filePath))
            {
                // Read the header line
                reader.ReadLine();

                // Read each line in the CSV file
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var person = new Person
                    {
                        Name = values[0],
                        Age = int.Parse(values[1])
                    };

                    people.Add(person);
                }
            }

            return people;
        }
    }
}
