using System;
using System.Collections.Generic;
using System.Globalization;

namespace Task1
{
    class Program
    {
        static void Main()
        {
            // Vytvorenie pomocou parametrickeho konstruktora
            var jano = new Person("Jan", "Mrkvicka", new DateTime(2000, 12, 1));

            // Vytvorenie pomocou bezparametrickeho konstruktora, inicializacia vlastnosti explicitne pomocou vlastnosti 
            var janoKlon = new Person();
            janoKlon.FirstName = "Jan";
            janoKlon.LastName = "Mrkvicka";
            janoKlon.Birthday = DateTime.Parse("1.12.2000", new CultureInfo("sk"));

            // Vytvorenie pomocou bezparametrickeho konstruktora s objektovym inicializatorom - inicializacia vlastnosti objektu
            var fero = new Person { FirstName = "Frantisek", LastName = "Mrkvicka", Birthday = new DateTime(2000, 1, 1) };

            // Vypisanie, pouziva nas prekryty ToString()
            Console.WriteLine("jano: " + jano);
            Console.WriteLine("janoKlon: " + janoKlon);
            Console.WriteLine("fero: " + fero);
            Console.WriteLine();

            var compResult = jano.CompareTo(fero);
            Console.WriteLine("jano.CompareTo(fero): {0} (Jano {1} Fero)", compResult, compResult > 0 ? ">" : (compResult < 0 ? "<" : "==")); // 1
            Console.WriteLine("jano.Equals(fero): {0}", jano.Equals(fero)); // False
            Console.WriteLine("jano.Equals(janoKlon): {0}", jano.Equals(janoKlon)); // True. Keby nemame vlastnu implementaciu, vracalo by to False (pretoze by sa porovnavali odkazy).
            Console.WriteLine();

            Console.WriteLine("jano[0]: {0}", jano[0]);
            Console.WriteLine("jano[2]: {0}", jano[2]);
            //Console.WriteLine("jano[3]: {0}", jano[3]); // Vyhodi vynimku
            Console.WriteLine("jano[\"Age\"]: {0}", jano["Age"]);

            jano[1] = "Baklazan";
            Console.WriteLine("jano[1] = {0}", jano[1]);
            Console.WriteLine("jano: {0}", jano);
            Console.WriteLine();

            // Vygenerujeme 10 osob
            var firstNames = new[] { "Jano", "Juraj", "Zuzka" };
            var lastNames = new[] { "Mrkvicka", "Parametricky", "Bezparametricky" };
            var persons = new List<Person>();
            var random = new Random();
            for (int i = 0; i < 10; i++)
            {
                var person = new Person(firstNames[random.Next(firstNames.Length)], lastNames[random.Next(lastNames.Length)],
                    new DateTime(random.Next(1990, 2000), 1, 1));

                persons.Add(person);
            }

            // Vypiseme vygenerovane osoby
            Console.WriteLine("Vygenerovane osoby:");
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
            Console.WriteLine();

            // Utriedime a vypiseme
            persons.Sort();
            Console.WriteLine("Zotriedene osoby:");
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }

            Console.ReadLine();
        }
    }
}
