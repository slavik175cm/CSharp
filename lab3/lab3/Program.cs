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
            bool doClear = false;
            while (true)
            {
                Console.WriteLine("1 to show all the vehicles");
                Console.WriteLine("2 to add a new vehicle");
                Console.WriteLine("3 to delete vehicle by id");
                Console.WriteLine("4 to change some vehicle");
                Console.WriteLine("5 to quit the program");
                switch(Choice(1, 5))
                {
                    case 1:
                        Console.Clear();
                        ShowAll(vehicles);
                        doClear = false;
                        break;
                    case 2:
                        Console.Clear();
                        vehicles[Vehicle.Id + 1] = GetNewVehicle();
                        Console.WriteLine("Vehicle has been added!");
                        doClear = true;
                        break;
                    case 3:
                        DeleteVehicle(vehicles);
                        doClear = true;
                        break;
                    case 4:
                        Console.Clear();
                        ChangeSome(vehicles);
                        doClear = true;
                        break;
                    case 5:
                        return;
                }
                Console.ReadLine();
                if (doClear)
                {
                    Console.Clear();
                }
            }
        }

        static int Choice(int min = int.MinValue, int max = int.MaxValue, string text = "Choice: ")
        {
            int x;
            Console.Write(text);
            while (!int.TryParse(Console.ReadLine(), out x) || x < min || x > max)
            {
                Console.WriteLine("Error! Input a number from {0} to {1}", min, max);
                Console.Write(text);
            }
            Console.WriteLine();
            return x;
        }

        static double ReadDouble(string text)
        {
            double x;
            Console.Write(text);
            while (!double.TryParse(Console.ReadLine().Replace('.', ','), out x))
            {
                Console.WriteLine("String is not a number! Try again");
                Console.Write(text);
            }
            return x;
        }

        static void ShowAll(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("There is no vehicles");
                return;
            } 
            foreach (var a in vehicles)
            {
                Console.Write(a.Value.InfoToString());
                Console.ReadLine();
            }
        }

        static Vehicle GetNewVehicle()
        {
            string serialNumber;
            while (true)
            {
                bool ok = true;
                Console.Write("Serial number - ");
                serialNumber = Console.ReadLine();
                if (serialNumber.Length > 20)
                {
                    ok = false;
                }
                foreach(var ch in serialNumber)
                {
                    if ((ch < 'A' || ch > 'Z') && (ch < '0' || ch > '9'))
                        ok = false;
                }
                if (ok)
                {
                    break;
                }
                Console.WriteLine("String should contain up to 20 symbols A-Z and 0-9");
            }
            double weight = ReadDouble("Weight(kg) - ");
            double carryingCapacity = ReadDouble("Carrying capacity(kg) - ");
            double maxSpeed = ReadDouble("MaxSpeed(m/s) - ");
            double cost = ReadDouble("Cost($) - ");
            while (true)
            {
                Console.Write("Is your vehicle new(y/n)? "); 
                string input = Console.ReadLine().ToLower();
                if (input == "y")
                {
                    return new Vehicle(serialNumber, weight, carryingCapacity, maxSpeed, cost);
                }
                if (input == "n")
                {
                    break;
                }
            }
            double mileage = ReadDouble("Mileage(m) - ");
            bool isBroken;
            while (true)
            {
                Console.Write("Is your vehicle broken(y/n)? ");
                string input = Console.ReadLine().ToLower();
                if (input == "y")
                {
                    isBroken = true;
                    break;
                }
                if (input == "n")
                {
                    isBroken = false;
                    break;
                }
            }
            Console.WriteLine("Manufacturing time");
            DateTime manufacturingTime;
            while (true)
            {
                try
                {
                    manufacturingTime = new DateTime(Choice(1, 9999, "Year - "), Choice(1, 12, "Mounth - "), Choice(1, 30, "Day - "));
                    if (manufacturingTime < DateTime.Now)
                    {
                        break;
                    }
                    Console.WriteLine("Is the vehicle from the future??");
                } catch
                {
                    Console.WriteLine("Such date is not exist!");
                }
            }
            return new Vehicle(serialNumber, weight, carryingCapacity, maxSpeed, cost, mileage, manufacturingTime, isBroken);
        }

        static int ChooseSpecific(Dictionary<int, Vehicle> vehicles)
        {
            int choice;
            while (true)
            {
                choice = Choice(0);
                if (vehicles.ContainsKey(choice))
                {
                    return choice;
                } 
                Console.WriteLine("There is no vehicle with such id! Type 'y' to continue...");
                if (Console.ReadLine().ToLower() != "y")
                {
                    return -1;
                }
            }
        }

        static void DeleteVehicle(Dictionary<int, Vehicle> vehicles)
        {
            Console.WriteLine("Choose the id of the vehicle you want to delete");
            int index = ChooseSpecific(vehicles);
            if (index == -1)
            {
                Console.WriteLine("Nothing has been deleted");
                return;
            }
            vehicles.Remove(index);
            Console.WriteLine("It has been removed!");
        }

        static void ChangeSome(Dictionary<int, Vehicle> vehicles)
        {
            Console.WriteLine("Choose the id of the vehicle you want to change");
            int index = ChooseSpecific(vehicles);
            if (index == -1)
            {
                Console.WriteLine("Nothing has been changed");
                return;
            }
            Console.WriteLine("1 to change the serial number");
            Console.WriteLine("2 to run some kilometers");
            Console.WriteLine("3 to repair/break the vehicle");
            Console.WriteLine("4 to do nothing");
            switch(Choice(1, 4))
            {
                case 1:
                    Console.Write("New serial number - ");
                    while (!vehicles[index].ChangeSerialNumber(Console.ReadLine()))
                    {
                        Console.WriteLine("String should contain up to 20 symbols A-Z and 0-9");
                        Console.Write("New serial number - ");
                    }
                    Console.WriteLine("Serial number has been changed");
                    break;
                case 2:
                    Console.WriteLine("Input the distance you want to drive");
                    vehicles[index].Run(ReadDouble("Length(m) = "));
                    Console.WriteLine("OK! Current mileage is {0}", vehicles[index].Mileage);
                    break;
                case 3:
                    Console.WriteLine("1 to repair");
                    Console.WriteLine("2 to break it");
                    int choice = Choice(1, 2);
                    if (choice == 1 && vehicles[index].IsBroken == false)
                    {
                        Console.WriteLine("It's already not broken");
                        break;
                    }
                    if (choice == 2 && vehicles[index].IsBroken == true)
                    {
                        Console.WriteLine("It's already broken");
                        break;
                    }
                    if (choice == 1)
                    {
                        vehicles[index].Repair();
                        Console.WriteLine("it has been repaired");
                        break;
                    }
                    vehicles[index].Break();
                    Console.WriteLine("it has been broken");
                    break;
                case 4:
                    Console.WriteLine("Nothing has been changed");
                    return;
            }
        }

    }
}
