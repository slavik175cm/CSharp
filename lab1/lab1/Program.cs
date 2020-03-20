using System;
using System.Collections.Generic;
namespace lab1 {
    class Program {
        static void Main(string[] args) {
            uint numberOfItems = ReadUInt("Number of items = ");
            uint weightOfBackpack = ReadUInt("Maximum weight of your backpack = ");
            uint[] weight = new uint[numberOfItems];
            double[] value = new double[numberOfItems];
            for (int i = 0; i < numberOfItems; i++) {
                Console.WriteLine("\nEnter item" + (i + 1));
                weight[i] = ReadUInt("Weight = ");
                value[i] = ReadDouble("Value = ");
            }
            double[,] dp = new double[numberOfItems + 1, weightOfBackpack + 1];
            double best;

            CalculateDP(dp, numberOfItems, weightOfBackpack, weight, value);
            List<uint> ListOfItems = RestoreAnswer(dp, numberOfItems, weightOfBackpack, weight, out best);

            Console.WriteLine("\nThe best total value you can get is {0}", best);
            if (ListOfItems.Count == 0) 
            {
                Console.WriteLine("You don't have to pick anything");
            } 
            else 
            {
                Console.Write("You need to pick items : ");
                foreach(var item in ListOfItems)
                    Console.Write(item + " ");
            }
        }

        static void CalculateDP(double [,] dp, uint numberOfItems, uint weightOfBackpack, uint[] weight, double[] value)
        {
            //Dynamic programming 
            //dp[i][j] - maximum total value that we can get from weight j considered only first i items

            for (int i = 0; i <= numberOfItems; i++)
                for (int j = 0; j <= weightOfBackpack; j++)
                    dp[i, j] = double.MinValue;
            dp[0, 0] = 0;

            for (int i = 0; i < numberOfItems; i++)
                for (int j = 0; j <= weightOfBackpack; j++)
                {
                    if (dp[i, j] == double.MinValue) continue;
                    if (j + weight[i] <= weightOfBackpack)
                        dp[i + 1, j + weight[i]] = Math.Max(dp[i + 1, j + weight[i]], dp[i, j] + value[i]);
                    dp[i + 1, j] = Math.Max(dp[i + 1, j], dp[i, j]);
                }
        }

        static List<uint> RestoreAnswer(double[,] dp, uint numberOfItems, uint weightOfBackpack, uint[] weight, out double best)
        {
            uint currentweight = 0;
            best = double.MinValue;
            List<uint> ListOfItems = new List<uint>();

            for (uint j = 0; j <= weightOfBackpack; j++)
                if (dp[numberOfItems, j] > best)
                {
                    currentweight = j;
                    best = dp[numberOfItems, j];
                }

            for (uint i = numberOfItems; i > 0; i--)
                if (dp[i, currentweight] != dp[i - 1, currentweight])
                {
                    ListOfItems.Add(i);
                    currentweight -= weight[i - 1];
                }

            ListOfItems.Reverse();
            return ListOfItems;
        }

        static uint ReadUInt(string s) 
        {
            uint x;
            Console.Write(s);
            while (!UInt32.TryParse(Console.ReadLine(), out x)) 
            {
                Console.WriteLine("String is not a non-negative integer! Try again");
                Console.Write(s);
            }
            return x;
        }   

        static double ReadDouble(string s) 
        {
            double x;
            Console.Write(s);
            while (!double.TryParse(Console.ReadLine().Replace('.',','), out x)) 
            {
                Console.WriteLine("String is not a number! Try again");
                Console.Write(s);
            }
            return x;
        }

    }
}
