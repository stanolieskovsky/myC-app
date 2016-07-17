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
            var Stanoo = new Person("Stano", "Lieskovsky", new DateTime(2000, 12, 1));
            Person stano2 = new Person("Stano", "Lieskovsky", new DateTime(1990, 10, 1));
            Person maria = new Person("Maria", "Lieskovska", new DateTime(year: 1980, month: 10, day: 1));

            // Vytvorenie pomocou bezparametrickeho konstruktora, inicializacia vlastnosti explicitne pomocou vlastnosti 
            var StanooKlon = new Person();
            StanooKlon.FirstName = "Stano";
            StanooKlon.LastName = "Lieskovsky";
            StanooKlon.Birthday = DateTime.Parse("1.12.2000");

            // Vytvorenie pomocou bezparametrickeho konstruktora s objektovym inicializatorom - inicializacia vlastnosti objektu
            var fero = new Person { FirstName = "Frantisek", LastName = "Mrkvicka", Birthday = new DateTime(2000, 1, 1) };

            // Vypisanie, pouziva nas prekryty ToString()
            Console.WriteLine("Stanoo: " + Stanoo);
            Console.WriteLine("StanooKlon: " + StanooKlon);
            Console.WriteLine("fero: " + fero);
            Console.WriteLine();

            var compResult = Stanoo.CompareTo(fero);
            Console.WriteLine("Stanoo.CompareTo(fero): {0} (Stanoo {1} Fero)", compResult, compResult > 0 ? ">" : (compResult < 0 ? "<" : "==")); // 1
            Console.WriteLine("Stanoo.Equals(fero): {0}", Stanoo.Equals(fero)); // False
            Console.WriteLine("Stanoo.Equals(StanooKlon): {0}", Stanoo.Equals(StanooKlon)); // True. Keby nemame vlastnu implementaciu, vracalo by to False (pretoze by sa porovnavali odkazy).
            Console.WriteLine("stano2{0}", stano2);
            Console.WriteLine();


            Console.WriteLine("Stanoo[0]: {0}", Stanoo[0]);
            Console.WriteLine("Stanoo[2]: {0}", Stanoo[2]);
            //Console.WriteLine("Stanoo[3]: {0}", Stanoo[3]); // Vyhodi vynimku
            Console.WriteLine("Stanoo[\"Age\"]: {0}", Stanoo["Age"]);
            Console.WriteLine("Stanoo  pred zmenou na baklazan");
            Console.WriteLine(" ");//kontrola medzery

            Stanoo[1] = "Baklazan";
            Console.WriteLine("Stanoo[1] = {0}", Stanoo[1]);
            Console.WriteLine("Stanoo: {0}", Stanoo);
            Console.WriteLine();

            // Vygenerujeme 10 osob
            var firstNames = new[] { "Stanoo", "Juraj", "Zuzka" };
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
