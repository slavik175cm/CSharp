using System;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle a = new Vehicle("A012B12", 100, 200, 150, 2500, new DateTime(2002, 10, 10), true);
            Vehicle b = new Vehicle("AAAAAAA", 50, 100, 100, 1500, false);
            Console.WriteLine(a.manufacturingTime);
            Console.WriteLine(b.manufacturingTime);



        }
    }
}
