using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cars = { "Nissan", "Aston Martin", "Chevrolet", "Alfa Romeo", "Chrysler", "Dodge", "BMW",
                            "Ferrari", "Audi", "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru", "Жигули :)"};

            Array sequence = cars.Select(p => new { p, p.Length }).ToArray();

            string stringjson = JsonConvert.SerializeObject(sequence);
            Console.WriteLine(stringjson);
            foreach (var i in sequence)
                Console.WriteLine(i);
        }
    }
}
