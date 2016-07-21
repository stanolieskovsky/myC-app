using System;
using MyLibrary;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new DoublyLinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            // Pouzitie indexera zoznamu
            Console.WriteLine("Prechod pomocou for cyklu a pouzitie indexera:");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            
            // Vola sa GetEnumerator() a nasledne cyklicky metoda MoveNext() a vlastnost Current, 
            // ktorej hodnota sa kopiruje do premennej cyklu i
            Console.WriteLine("Prechod pomocou foreachu cyklu - dopredna iteracia:");
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }

            // Volame metodu, ktora vracia IEnumerable<>, ta obsahuje GetEnumerator() a uz to funguje :)
            Console.WriteLine("Prechod pomocou foreach cyklu - spatna iteracia:");
            foreach (var i in list.GetReverse())
            {
                Console.WriteLine(i);
            }
        }
    }
}
