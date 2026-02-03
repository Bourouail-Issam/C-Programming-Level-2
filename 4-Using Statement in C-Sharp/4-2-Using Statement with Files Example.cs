using System;
using System.IO;

class Program
{
    static void Main()
    {
        string filePath = "example.txt"; // Replace with the path to your text file.

        try
        {
            // Use the StreamReader with a 'using' statement to read from the file.
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;

                // Read and display each line from the file.
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File not found: {filePath}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }
    }
}
