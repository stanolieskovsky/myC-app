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
            // Na konverziu mozeme pouzit tiez int.Parse() alebo Convert.ToInt32(), pricom Int16 = short, Int32 == int, Int64 = long, atd.
            // int.TryParse - bezpecny, tu s prikladom pouzitia cykla while
            while (!int.TryParse(input, out a))
            {
                Console.Write("Wrong input a. Enter a: ");
                input = Console.ReadLine();
            }

            // Nacitame b
            Console.Write("Enter b: ");
            int b = Convert.ToInt32(Console.ReadLine()); // iny sposob konverzie, pozor - ak zadame nieco ine ako cislo, vyvola sa vynimka

            // Vypocitame
            for (int i = 2; i <= b; i++)
            {
                Console.WriteLine("{0}^{1} = {2}", a, i, Math.Pow(a, i)); // Na vypocet mocniny mozeme pouzit staticku metodu Pow() triedy Math z menneho priestoru System
            }


            Console.ReadLine();
        }
    }
}
