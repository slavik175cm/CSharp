using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
namespace ConsoleApp1
{
    class Program
    {
        const string pathToDLL = "D:\\C#\\lab4_2\\HashTableDLL\\x64\\Debug\\HashTableDLL.dll";

        [DllImport(pathToDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void insert(string key, int value);

        [DllImport(pathToDLL, CallingConvention = CallingConvention.StdCall)]
        public static extern int get(string key);

        [DllImport(pathToDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void change(string key, int new_value);

        [DllImport(pathToDLL, CallingConvention = CallingConvention.StdCall)]
        public static extern void extract(string key);

        [DllImport(pathToDLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool key_exist(string key);

        static void Main(string[] args)
        {
            while (true)
            {
                string key;
                Console.WriteLine("1 to add new item");
                Console.WriteLine("2 to get value by key");
                Console.WriteLine("3 to change value by key");
                Console.WriteLine("4 to extract value by key");
                Console.WriteLine("5 to exit");
                switch (ReadInt("Choice: ", 1, 5))
                {
                    case 1:
                        Console.Write("Input key: ");
                        key = Console.ReadLine();
                        insert(key, ReadInt("Input value: "));
                        Console.WriteLine("Item has been added");
                        break;
                    case 2:
                        if (InputKey(out key))
                        {
                            Console.WriteLine("Value = " + get(key));
                        }
                        break;
                    case 3:
                        if (InputKey(out key))
                        {
                            change(key, ReadInt("Input new value: "));
                            Console.WriteLine("Value has been changed ");
                        }
                        break;
                    case 4:
                        if (InputKey(out key))
                        {
                            extract(key);
                            Console.WriteLine("Item has been deleted ");
                        }
                        break;
                    case 5:
                        return;
                }
                Console.ReadLine();
            }
        }

        static bool InputKey(out string key)
        {
            Console.Write("Input key:");
            key = Console.ReadLine();
            if (key_exist(key))
            {
                return true;
            }
            Console.WriteLine("There is no such key");
            return false;
        }

        static int ReadInt(string text, int min = int.MinValue, int max = int.MaxValue)
        {
            int x;
            Console.Write(text);
            while (!int.TryParse(Console.ReadLine(), out x) || x < min || x > max)
            {
                Console.WriteLine("Error! Input a number from {0} to {1}", min, max);
                Console.Write(text);
            }
            return x;
        }
    }
}
