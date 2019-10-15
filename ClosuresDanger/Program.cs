using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClosuresDanger
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example1();
            //Example2();

            foreach (int i in new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })
            {
                Task.Factory.StartNew(() => Console.WriteLine(i));
            }
            Console.ReadLine();


            Console.ReadKey();
        }

        private static void Example1()
        {
            for (int i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(() => Console.WriteLine(i));
            }
        }

        private static void Example2()
        {
            for (int i = 0; i < 10; i++)
            {
                int localInteger = i;
                Task.Factory.StartNew(() => Console.WriteLine(localInteger));
            }
        }
    }
}
