using System;
using System.Numerics;
namespace lab2_1 {
    class Program {
        static void Main(string[] args) 
        {
            Console.WriteLine("Input the borders");
            ulong a, b;
            do
            {
                a = ReadUInt64("a = ");
                b = ReadUInt64("b = ");
                if (a > b)
                    Console.WriteLine("a should be smaller than b! Try again");
            } while (a > b);
            Console.WriteLine("Answer: {0}", FastSolution(a, b));
            //in order to check the answer
            Console.WriteLine("Press enter to quit the program, type 'brute' to brute force the answer");
            if (Console.ReadLine() == "brute")
                Console.WriteLine("Brute force answer: {0}", SlowSolution(a, b));
        }

        static ulong FastSolution(ulong a, ulong b)
        {
            ulong answer = 0;
            for (ulong i = 2; i <= b; i *= 2)
            {
                ulong left = a + (i - a % i) % i;
                ulong right = b - b % i;
                if (left <= right)
                    answer += (right - left) / i + 1;
            }
            return answer;
        }

        static ulong SlowSolution(ulong a, ulong b) 
        {
            BigInteger mult = 1;
            for (ulong i = a; i <= b; i++)
                mult *= i;
            ulong power = 0;
            while (mult % 2 == 0) 
            {
                mult /= 2;
                power++;
            }
            return power;
        }

        static ulong ReadUInt64(string s) 
        {
            ulong x;
            Console.Write(s);
            while (!ulong.TryParse(Console.ReadLine(), out x) || x == 0) 
            {
                Console.WriteLine("String is not a positive Int64 number! Try again");
                Console.Write(s);
            }
            return x;
        }

    }
}
