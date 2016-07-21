using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Priklad2
{
    class Program
    {
        static void Main()
        {
            // Nacitame a
            Console.Write("Enter a: ");
            string input = Console.ReadLine();
            int a;

            while (!int.TryParse(input, out a))
            {
                Console.Write("Wrong input a. Enter a: ");
                input = Console.ReadLine();
            }

            
            Console.Write("Enter b: ");
            int b = Convert.ToInt32(Console.ReadLine()); // iny sposob konverzie, pozor - ak zadame nieco ine ako cislo, vyvola sa vynimka

            
            for (int i = 2; i <= b; i++)
            {
                Console.WriteLine("{0}^{1} = {2}", a, i, Math.Pow(a, i)); // Na vypocet mocniny mozeme pouzit staticku metodu Pow() triedy Math z menneho priestoru System
            }


            Console.ReadLine();
        }
    }
}
