using System;
namespace lab2_2
{
    class Program
    {
        static int[] pow26 = {1, 26, 26 * 26, (int)Math.Pow(26, 3), (int)Math.Pow(26, 4) };

        static void Main(string[] args)
        {

            Random rnd = new Random();
            Console.CursorVisible = false;  
            do
            {
                int number = rnd.Next(pow26[1] + pow26[2] + pow26[3] + pow26[4]) + 1;
                Console.Write("string {0}: ", number);
                WriteNthString(number);
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        static void WriteNthString(int number)
        {
            int length = 1;

            for (int i = 1; i <= 4; i++) 
            { 
                if (pow26[i] < number)
                {
                    number -= pow26[i];
                    length = i + 1;
                }
            }

            for (int i = 0; i < length; i++)
            {
                Console.Write((char)('a' + number / pow26[length - i - 1]));
                number %= pow26[length - i - 1];
            }

            Console.Write("\n");
        }
    }
}
