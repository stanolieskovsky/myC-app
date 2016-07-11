using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Priklad1
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Zadanie c. 1.0
            //foreach (string arg in args)
            //{
            //    Console.WriteLine(arg);
            //}
            //
            //Console.ReadLine();


            //// Zadanie c. 1.1
            //Array.Sort(args);
            //foreach (string arg in args)
            //{
            //    Console.WriteLine(arg);
            //}
            //
            //Console.ReadLine();


            //// Zadanie c. 1.2
            //int idx = 1;
            //foreach (string arg in args)
            //{
            //   // Console.WriteLine(idx + ". " + arg);
            //    Console.WriteLine("{0}{1}{2}", idx, arg, arg);
            //    idx++;
            //}

            //Console.ReadLine();


            //// Zadanie c. 1.3
            //string sss = string.Empty;
            //string str = string.Empty;
            //foreach (string arg in args)
            //{
            //    str += arg;
            //}
            //Console.WriteLine(str);

            //Console.ReadLine();


            //// Zadanie c. 1.3 - cez StringBuilder
            StringBuilder sb = new StringBuilder();
            foreach (string arg in args)
            {
                sb.Append(arg);
            }
            Console.WriteLine(sb);

            Console.ReadLine();


            // Zadanie c. 1.4
            //foreach (string arg in args)
            //{
            //    Console.WriteLine(arg.Length + " " + arg);
            //    /* alebo Console.WriteLine(
            //               "{0} {1}", arg.Length, arg); */
            //}

            //Console.ReadLine();
        }
    }
}
