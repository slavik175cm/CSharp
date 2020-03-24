using System;

namespace lab2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input a number");
            string number;
            do
            {
                number = Console.ReadLine();
            } while (!Valid(number));

            decimal a = ParseToDouble(number);
            Console.WriteLine(a);
        }

        static bool Valid(string number)
        {
            if (number == "")
            {
                Console.WriteLine("The string is empty");
                return false;
            }
            if (number[0] == '.')
            {
                Console.WriteLine("The string can't start with point");
                return false;
            }
            int commas = 0;  
            for (int i = 0; i < number.Length; i++)
            {

                if (number[i] == '-' && i != 0)
                {
                    Console.WriteLine("symbol '-' in the middle of the string");
                    return false;
                }

                if (number[i] != '.' && number[i] != ',' && (number[i] < '0' || number[i] > '9') && number[i] != '-')
                {
                    Console.WriteLine("String contains restricted symbol(s)");
                    return false;
                }

                if (number[i] == '.' || number[i] == ',')
                {
                    commas++;
                    if (commas == 2)
                    {
                        Console.WriteLine("String contains more than one decimal points");
                        return false;
                    }
                }
            }
            return true;
        }

        static decimal ParseToDouble(string number)
        {
            decimal parsed = 0, pow10 = 1;
            bool wascomma = false;
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == '-') continue;
                if (number[i] == '.' || number[i] == ',')
                {
                    wascomma = true;
                    continue;
                }
                if (!wascomma)
                {
                    parsed *= 10;
                    parsed += number[i] - '0';
                }
                else
                {
                    pow10 *= 10;
                    parsed += (number[i] - '0') / pow10;
                }
            }
            if (number[0] == '-')
            {
                parsed *= -1;
            }
            return parsed;
        }

    }
}
