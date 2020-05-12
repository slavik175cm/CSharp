using System;
using System.Collections.Generic;
namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            var vehicles = new Dictionary<int, Vehicle>()
            {
                [1] = new Vehicle("A012B12", 1000, 200, 150, 2500, 10000, new DateTime(2002, 10, 10), true),
                [2] = new BMWX5("AB1010DC", 100, 0),
                [3] = new LamborghiniAventador("DCS122", 100, new DateTime(2012, 1, 2), false, 80)
            };
            while (true)
            {
                Console.WriteLine("1 to show all the vehicles");
                Console.WriteLine("2 to add a new vehicle");
                Console.WriteLine("3 to delete vehicle by id");
                Console.WriteLine("4 to change some vehicle");
                Console.WriteLine("5 to view vehicles sorted by cost");
                Console.WriteLine("6 to quit the program");
                switch (Choice(1, 6))
                {
                    case 1:
                        Console.Clear();
                        ShowAll(vehicles);
                        break;
                    case 2:
                        Console.Clear();
                        vehicles[Vehicle.Id + 1] = AddNewVehicle(vehicles);
                        Console.WriteLine("\nCharacteristics of the new car\n");
                        Console.WriteLine(vehicles[Vehicle.Id].InfoToString());
                        Console.WriteLine("Vehicle has been added!\n");
                        break;
                    case 3:
                        DeleteVehicle(vehicles);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        ChangeSome(vehicles);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        ShowAllSortedByCost(vehicles);
                        break;
                    case 6:
                        return;
                }
            }
        }

        static void ShowAll(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("There is no vehicles");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("******************************************************");
            foreach (var a in vehicles)
            {
                Console.Write(a.Value.InfoToString());
                Console.Write("******************************************************");
                Console.ReadLine();
            }
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
            return new Vehicle(serialNumber, weight, carryingCapacity, maxSpeed, cost, ReadDouble("Mileage(m) - "), 
                GetManufacturingTime(), Choice(0, 1, "Is your vehicle broken(1/0)? ") == 1 ? true : false);
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
                    manufacturingTime = new DateTime(Choice(1, 9999, "Year - "), Choice(1, 12, "Mounth - "), Choice(1, 31, "Day - "));
                    if (manufacturingTime < DateTime.Now)
                    {
                        break;
                    }
                    Console.WriteLine("Is the vehicle from the future??");
                } catch
                {
                    Console.WriteLine("Such date doesn't exist!");
                }
            }
            return manufacturingTime;
        }

       static Vehicle AddNewVehicle(Dictionary<int, Vehicle> vehicles)
        {
            Console.WriteLine("1 to add a new vehicle");
            Console.WriteLine("2 to add a new car");
            Console.WriteLine("3 to add a new BMWX5");
            Console.WriteLine("4 to add a new Lamborghini Aventador");
            Console.WriteLine("5 to add a new Hyundai Aero City");
            Console.WriteLine("6 to clone some vehicle");
            switch (Choice(1, 6))
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
                        return new BMWX5(GetSerialNumber(), ReadDouble("Current fuel - "), ReadDouble("Current amount of energy - "));
                    }
                    return new BMWX5(GetSerialNumber(), ReadDouble("Mileage(m) - "), GetManufacturingTime(),
                        Choice(0, 1, "Is you car is broken?(1/0) ") == 0 ? false : true, ReadDouble("Current fuel(L) - "), ReadDouble("Current amount of energy - "));
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
                case 6:
                    Console.WriteLine("Input number of vehicle you want to clone");
                    return (Vehicle)vehicles[ChooseSpecific(vehicles)].Clone();
            }
            return new Vehicle();
        }

        static void DeleteVehicle(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("\nThere is no vehicles");
                return;
            }
            Console.WriteLine("\nChoose the id of the vehicle you want to delete");
            int index = ChooseSpecific(vehicles);
            vehicles.Remove(index);
            Console.WriteLine("It has been removed!");
        }

        static void ChangeSome(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("\nThere is no vehicles");
                return;
            }
            Console.WriteLine("\nChoose the id of the vehicle you want to change");
            int index = ChooseSpecific(vehicles);
            Console.WriteLine();
            Console.WriteLine("1 to change the serial number");
            Console.WriteLine("2 to run some kilometers");
            Console.WriteLine("3 to repair/break the vehicle");
            Console.WriteLine("4 to fuel up the vehicle");
            Console.WriteLine("5 to charge the vehicle");
            Console.WriteLine("6 to do nothing\n");
            switch (Choice(1, 6))
            {
                case 1:
                    vehicles[index].SerialNumber = GetSerialNumber();
                    Console.WriteLine("Serial number has been changed!");
                    break;
                case 2:
                    if (vehicles[index].IsBroken)
                    {
                        Console.WriteLine("You can not drive broken vehicle");
                        break;
                    }
                    Console.WriteLine("Input the distance you want to drive");
                    while (!vehicles[index].Run(ReadDouble("Length(m) = ")))
                    {
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
                    if (!(vehicles[index] is Car car))
                    {
                        Console.WriteLine("You can not fuel up this vehicle!");
                        break;
                    }
                    Console.WriteLine("Current amount of fuel - {0}", car.CurrentFuel);
                    Console.WriteLine("Maximum amount of fuel - {0}", car.MaxFuel);
                    if (car.CurrentFuel == car.MaxFuel)
                    {
                        Console.WriteLine("It's already full!");
                        break;
                    }
                    car.FuelUp(ReadDouble("Add fuel(L) - ", 0, car.MaxFuel - car.CurrentFuel));
                    Console.WriteLine("OK! Current amount of fuel - {0}", car.CurrentFuel);
                    break;
                case 5:
                    if (!(vehicles[index] is IElectricMotor vehicle))
                    {
                        Console.WriteLine("You can not charge up this vehicle!");
                        break;
                    }
                    Console.WriteLine("Current amount of energy - {0}", vehicle.CurrentEnergy);
                    Console.WriteLine("Maximum amount of energy - {0}", vehicle.BatteryCapacity);
                    if (vehicle.CurrentEnergy == vehicle.BatteryCapacity)
                    {
                        Console.WriteLine("It's already full!");
                        break;
                    }
                    vehicle.ChargeBatteryUp(ReadDouble("Add energy(kW) - ", 0, vehicle.BatteryCapacity - vehicle.CurrentEnergy));
                    Console.WriteLine("OK! Current amount of energy - {0}", vehicle.CurrentEnergy);
                    break;
                case 6:
                    Console.WriteLine("Nothing has been changed");
                    return;
            }
        }

        static void ShowAllSortedByCost(Dictionary<int, Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("There is no vehicles");
                Console.ReadLine();
                return;
            }
            List<Vehicle> list = new List<Vehicle>();
            foreach (var vehicle in vehicles)
            {
                list.Add(vehicle.Value);
            }
            list.Sort();
            Console.WriteLine("******************************************************");
            foreach (var vehicle in list)
            {
                Console.WriteLine(vehicle.InfoToString());
                Console.Write("******************************************************");
                Console.ReadLine();
            }
        }

        static int ChooseSpecific(Dictionary<int, Vehicle> vehicles)
        {
            Console.Write("Available vehicles: ");
            foreach (var a in vehicles)
            {
                Console.Write(a.Key + " ");
            }
            Console.WriteLine();
            while (true)
            {
                int choice = Choice();
                if (vehicles.ContainsKey(choice))
                {
                    return choice;
                }
                Console.WriteLine("There is no such vehicle. Try again");
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

    }
}