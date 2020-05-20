using System;
using System.Collections.Generic;
using System.Globalization;
namespace lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Fraction a = new Fraction(1, 2);
            Fraction b = new Fraction(-3, 4);
            Fraction c = new Fraction(10, 3);

            Console.WriteLine("a = " + a.ToString());
            Console.WriteLine("b = " + b.ToString());
            Console.WriteLine("c = " + c.ToString());
            Console.WriteLine();

            var list = new List<Fraction>{ a, b, c};
            list.Sort();
            Console.WriteLine("Sorted in in ascending order:");
            foreach(var fraction in list)
            {
                Console.WriteLine(fraction.ToString());
            }
            Console.WriteLine();

            Console.WriteLine("a + b = {0}", a + b);
            Console.WriteLine("a - b = {0}", a - b);
            Console.WriteLine("a * -c = {0}", a * -c);
            Console.WriteLine("b / c = {0}", b / c);
            Console.WriteLine();

            Console.WriteLine("b {0} c", b > c ? ">" : "<=");
            Console.WriteLine("a {0} -c", a >= -c ? ">=" : "<");
            Console.WriteLine("a {0} b", a >= b ? ">=" : "<");
            Console.WriteLine("b {0} c", b == c ? "=" : "!=");
            Console.WriteLine();

            Console.WriteLine("a = {0}", a.ToString("DecimalLike"));
            Console.WriteLine("b = {0}", b.ToString("DoubleLike"));
            Console.WriteLine();


            string number = "1,25";
            Fraction.TryParse(number, out Fraction result);
            Console.WriteLine(number + " = " + result.ToString());

            number = "43/5";
            Fraction.TryParse(number, out result);
            Console.WriteLine(number + " = " + result.ToString("DoubleLike"));

            number = "2,750000e+001";
            Fraction.TryParse(number, out result);
            Console.WriteLine(number + " = " + result.ToString("DecimalLike"));

            number = "200";
            Fraction.TryParse(number, out result);
            Console.WriteLine(number + " = " + result.ToString());

            number = "4,6.";
            if (Fraction.TryParse(number, out result))
            {
                Console.WriteLine(number + " = " + result.ToString());
            }
            else
            {
                Console.WriteLine("Can't parse " + number);
            }
            Console.WriteLine();

            Console.WriteLine("(byte)a, (int)a, (double)a, (decimal)a :");
            Console.WriteLine($"{(byte)a}, {(int)a}, {(double)a}, {(decimal)a}");
            a = (Fraction)c.Clone();
            Console.WriteLine("\na clones c\n");
            Console.WriteLine("(byte)a, (int)a, (double)a, (decimal)a :");
            Console.WriteLine($"{(byte)a}, {(int)a}, {(double)a}, {(decimal)a}");



        }
    }
}