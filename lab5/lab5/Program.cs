using System;
using System.Collections.Generic;
namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            var vehicles = new Dictionary<int, Vehicle>();
            vehicles[1] = new Vehicle("A012B12", 1000, 200, 150, 2500, 10000, new DateTime(2002, 10, 10), true);
            vehicles[2] = new BMWX5("AB1010DC", 100);
            vehicles[3] = new LamborghiniAventador("DCS122", 100, new DateTime(2012, 1, 2), false, 80);

            bool doClear = false;
            while (true)
            {
                Console.WriteLine("1 to show all the vehicles");
                Console.WriteLine("2 to add a new vehicle");
                Console.WriteLine("3 to delete vehicle by id");
                Console.WriteLine("4 to change some vehicle");
                Console.WriteLine("5 to quit the program");
                switch (Choice(1, 5))
                {
                    case 1:
                        Console.Clear();
                        ShowAll(vehicles);
                        doClear = false;
                        break;
                    case 2:
                        Console.Clear();
                        vehicles[Vehicle.Id + 1] = AddNewVehicle(vehicles);
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

        static int Choice(int min = 0, int max = int.MaxValue, string text = "Choice: ")
        {
            int x;
            Console.Write(text);
            while (!int.TryParse(Console.ReadLine(), out x) || x < min || x > max)
            {
                Console.WriteLine("Error! It should be an interger from {0} to {1}", min, max);
                Console.Write(text);
            }
            return x;
        }

        static double ReadDouble(string text, double min = 0, double max = double.MaxValue)
        {
            double x;
            Console.Write(text);
            while (!double.TryParse(Console.ReadLine().Replace('.', ','), out x) || x < min || x > max)
            {
                Console.WriteLine("Error! It should be a number from {0} to {1}", min, max);
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

        static string GetSerialNumber()
        {
            while (true)
            {
                Console.Write("Serial number - ");
                string serialNumber = Console.ReadLine();
                if (Vehicle.IsValidSerialNumber(serialNumber))
                {
                    return serialNumber;
                }
                Console.WriteLine("String should contain up to 20 symbols A-Z and 0-9");
            }
        }

        static DateTime GetManufacturingTime()
        {
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
            return manufacturingTime;
        }

        static Vehicle CreateNewVehicle()
        {
            string serialNumber = GetSerialNumber();
            double weight = ReadDouble("Weight(kg) - ");
            double carryingCapacity = ReadDouble("Carrying capacity(kg) - ");
            double maxSpeed = ReadDouble("MaxSpeed(m/s) - ");
            double cost = ReadDouble("Cost($) - ");
            if (Choice(0, 1, "Is your vehicle new(1/0)? ") == 1)
            {
                return new Vehicle(serialNumber, weight, carryingCapacity, maxSpeed, cost);
            }
            double mileage = ReadDouble("Mileage(m) - ");
            bool isBroken = false;
            if (Choice(0, 1, "Is your vehicle new(1/0)? ") == 1)
            {
                isBroken = true;
            }
            return new Vehicle(serialNumber, weight, carryingCapacity, maxSpeed, cost, mileage, GetManufacturingTime(), isBroken);
        }

        static Vehicle AddNewVehicle(Dictionary<int, Vehicle> vehicles)
        {
             Console.WriteLine();
             Console.WriteLine("1 to add a new vehicle");
             Console.WriteLine("2 to add a new car");
             Console.WriteLine("3 to add a new BMWX5");
             Console.WriteLine("4 to add a new Lamborghini Aventador");
             Console.WriteLine("5 to add a new Hyundai Aero City");
             switch(Choice(1, 5))
             {
                 case 1:
                     return CreateNewVehicle();
                 case 2:
                     return new Car(CreateNewVehicle(), Choice(0, 100, "Number of seats -"), ReadDouble("Current fuel(L) - "), ReadDouble("Max fuel - "), 
                         ReadDouble("Fuel consumption - "), (Car.Type)(Choice(1, 5, "Choose the type (1.Sport 2.City 3.Truck 4.Cabriolet 5.Van 6.Bus)\n")));
                 case 3:
                     Console.WriteLine("Is you car new?(1/0) ");
                     if (Choice(0, 1) == 1)
                     {
                         return new BMWX5(GetSerialNumber(), ReadDouble("Current fuel - "));
                     }
                     return new BMWX5(GetSerialNumber(),ReadDouble("Mileage(m) - "), GetManufacturingTime(),
                         Choice(0, 1, "Is you car is broken?(1/0) ") == 0 ? false : true,  ReadDouble("Current fuel(L) - "));
                 case 4:
                     Console.WriteLine("Is you car new?(1/0) ");
                     if (Choice(0, 1) == 1)
                     {
                         return new LamborghiniAventador(GetSerialNumber(), ReadDouble("Current fuel - "));
                     }
                     return new LamborghiniAventador(GetSerialNumber(), ReadDouble("Mileage(m) - "), GetManufacturingTime(),
                         Choice(0, 1, "Is you car is broken?(1/0) ") == 0 ? false : true, ReadDouble("Current fuel(L) - "));
                 case 5:
                     Console.WriteLine("Is you car new?(1/0) ");
                     if (Choice(0, 1) == 1)
                     {
                         return new HyundaiAeroCity(GetSerialNumber(), ReadDouble("Current fuel - "));
                     }
                     return new HyundaiAeroCity(GetSerialNumber(), ReadDouble("Mileage(m) - "), GetManufacturingTime(),
                         Choice(0, 1, "Is you car is broken?(1/0) ") == 0 ? false : true, ReadDouble("Current fuel(L) - "));
             }
            return new Vehicle();
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
            switch (Choice(1, 4))
            {
                case 1:
                    vehicles[index].ChangeSerialNumber(GetSerialNumber());
                    Console.WriteLine("Serial number has been changed!");
                    break;
                case 2:
                    if (vehicles[index].IsBroken)
                    {
                        Console.WriteLine("You can not drive broken vehicle");
                        break;
                    }
                    Console.WriteLine("Input the distance you want to drive");
                    while (!vehicles[index].Run(ReadDouble("Length(m) = "))) {
                        Console.WriteLine("You can not drive that far");
                    }
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