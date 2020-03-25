using System;
using System.Collections.Generic;
namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var vehicles = new Dictionary<int, Vehicle>();
            vehicles[1] = new Vehicle("A012B12", 100, 200, 150, 2500, 10000, new DateTime(2002, 10, 10), true);
            vehicles[2] = new Vehicle("CC8D091", 50, 100, 100, 1500);

            //Vehicle a = new Vehicle("A012B12", 100, 200, 150, 2500, 10000, new DateTime(2002, 10, 10), true);
            //Vehicle b = new Vehicle("CC8D091", 50, 100, 100, 1500);
            Console.WriteLine(vehicles[1].InfoToString());
            Console.WriteLine(vehicles[2].InfoToString());
            while (true)
            {
                Console.WriteLine("Input 1 to show all the vehicles");
                Console.WriteLine("Input 2 to add a new vehicle");
                Console.WriteLine("Input 3 to delete vehicle by id");
                Console.WriteLine("Input 4 to change some vehicle");
                Console.WriteLine("Input 5 to quit the program");
                switch(Choice(1, 5))
                {
                    case 1:
                        if (vehicles.Count == 0)
                        {
                            Console.WriteLine("There is no vehicles");
                        }
                        foreach (var a in vehicles)
                        {
                            Console.WriteLine(a.Value.InfoToString());
                        }
                        break;
                    case 2:

                        break;
                    case 3:
                        DeleteVehicle(vehicles);
                        break;
                    case 4:
                        break;
                    case 5:
                        return;
                }
                Console.ReadLine();
            }
        }

        static int Choice(int min = int.MinValue, int max = int.MaxValue)
        {
            int x;
            Console.Write("Choice: ");
            while (!int.TryParse(Console.ReadLine(), out x) || x < min || x > max)
            {
                Console.WriteLine("Error! Input a number from {0} to {1}", min, max);
                Console.Write("Choice: ");
            }
            Console.WriteLine();
            return x;
        }

        static void DeleteVehicle(Dictionary<int, Vehicle> vehicles)
        {
            Console.WriteLine("Choose the id of the vehicle you want to delete");
            int choice;
            while (true)
            {
                choice = Choice();
                if (vehicles.ContainsKey(choice))
                {
                    vehicles.Remove(choice);
                    Console.WriteLine("It has been removed!");
                    break;
                } 
                else
                {
                    Console.WriteLine("There is no vehicle with such id! Type 'y' to continue...");
                    if (Console.ReadLine().ToLower() != "y")
                    {
                        Console.WriteLine("Nothing has been deleted");
                        break;
                    }
                }
            }
        }


    }
}
