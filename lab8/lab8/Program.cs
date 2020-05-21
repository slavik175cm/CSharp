using System;
using System.Collections.Generic;
namespace lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            var vehicles = new Dictionary<int, Vehicle>();

            Vehicle.GoneOld += delegate (Vehicle oldVehicle)
            {
                if (vehicles.ContainsKey(oldVehicle.MyId))
                {
                    vehicles.Remove(oldVehicle.MyId);
                    Console.WriteLine($"Vehicle {oldVehicle.MyId} gone old. It has been removed from the collection");
                }
            };

            Vehicle.NewAppeared += x => vehicles[x.MyId] = x;

            new Vehicle("A012B12", 1000, 200, 150, 250000, 10000, new DateTime(2002, 10, 10), true);
            new BMWX5("AB1010DC", 100, 0);
            new LamborghiniAventador("DCS122", 100, new DateTime(2012, 1, 2), false, 80);

            while (true)
            {
                Console.WriteLine("1 to show vehicles sorted by Id");
                Console.WriteLine("2 to show vehicles sorted by cost");
                Console.WriteLine("3 to add a new vehicle");
                Console.WriteLine("4 to delete vehicle by id");
                Console.WriteLine("5 to change some vehicle");
                Console.WriteLine("6 to quit the program");
                switch (Choice(1, 6))
                {
                    case 1:
                        Console.Clear();
                        ShowAll(vehicles, Vehicle.CompareById);
                        break;
                    case 2:
                        Console.Clear();
                        ShowAll(vehicles, Vehicle.CompareByCost);
                        break;
                    case 3:
                        Console.Clear();
                        AddNewVehicle(vehicles);
                        Console.WriteLine("\nCharacteristics of the new car\n");
                        Console.WriteLine(vehicles[Vehicle.Id].InfoToString());
                        Console.WriteLine("Vehicle has been added!\n");
                        break;
                    case 4:
                        DeleteVehicle(vehicles);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        ChangeSome(vehicles);
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 6:
                        return;
                }
            }
        }

        static void ShowAll(Dictionary<int, Vehicle> vehicles, Vehicle.Comparator comparator)
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

            list.Sort((x, y) => comparator(x, y));

            Console.WriteLine("******************************************************");
            foreach (var vehicle in list)
            {
                Console.WriteLine(vehicle.InfoToString());
                Console.Write("******************************************************");
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
                    Console.WriteLine("Is you vehicle new?(1/0) ");
                    if (Choice(0, 1) == 1)
                    {
                        return new Vehicle(GetSerialNumber(), ReadDouble("Weight(kg) - "), ReadDouble("Carrying capacity(kg) - "), ReadDouble("MaxSpeed(m/s) - "), ReadDouble("Cost($) - "));
                    }
                    return new Vehicle(GetSerialNumber(), ReadDouble("Weight(kg) - "), ReadDouble("Carrying capacity(kg) - "), ReadDouble("MaxSpeed(m/s) - "), ReadDouble("Cost($) - "),
                        ReadDouble("Mileage(m) - "), GetManufacturingTime(), Choice(0, 1, "Is your vehicle broken(1/0)? ") == 1 ? true : false);
                case 2:
                    Console.WriteLine("Is you car new?(1/0) ");
                    if (Choice(0, 1) == 1)
                    {
                        return new Car(GetSerialNumber(), ReadDouble("Weight(kg) - "), ReadDouble("Carrying capacity(kg) - "), ReadDouble("MaxSpeed(m/s) - "), ReadDouble("Cost($) - "),
                            Choice(0, 100, "Number of seats -"), ReadDouble("Current fuel(L) - "), ReadDouble("Max fuel - "), ReadDouble("Fuel consumption - "),
                            (Car.Type)(Choice(1, 5, "Choose the type (1.Sport 2.City 3.Truck 4.Cabriolet 5.Van 6.Bus)\n")));
                    }
                    return new Car(GetSerialNumber(), ReadDouble("Weight(kg) - "), ReadDouble("Carrying capacity(kg) - "), ReadDouble("MaxSpeed(m/s) - "), ReadDouble("Cost($) - "), 
                        ReadDouble("Mileage(m) - "), GetManufacturingTime(), Choice(0, 1, "Is your vehicle broken(1/0)? ") == 1 ? true : false, 
                        Choice(0, 100, "Number of seats -"), ReadDouble("Current fuel(L) - "), ReadDouble("Max fuel - "), ReadDouble("Fuel consumption - "),
                        (Car.Type)(Choice(1, 5, "Choose the type (1.Sport 2.City 3.Truck 4.Cabriolet 5.Van 6.Bus)\n")));
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
                    Console.WriteLine("Input id of vehicle you want to clone");
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
            Console.WriteLine("4 to fuel the vehicle");
            Console.WriteLine("5 to charge the vehicle");
            Console.WriteLine("6 to do nothing\n");
            switch (Choice(1, 6))
            {
                case 1:
                    vehicles[index].SerialNumber = GetSerialNumber();
                    Console.WriteLine("Serial number has been changed!");
                    break;
                case 2:
                    try
                    {
                        if (vehicles[index].Run(ReadDouble("Length(m) = ")))
                        {
                            Console.WriteLine("OK! Current mileage is {0}", vehicles[index].Mileage);
                        }
                        else
                        {
                            Console.WriteLine("You can't drive that far");
                        }
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 3:
                    try
                    {
                        if (Choice(1, 2, "1 to repair\n2 to break it\n") == 1)
                        {
                            vehicles[index].Repair();
                        }
                        else
                        {
                            vehicles[index].Break();
                        }
                        Console.WriteLine("Done");
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case 4:
                    if (!(vehicles[index] is Car car))
                    {
                        Console.WriteLine("You can not fuel this vehicle!");
                        break;
                    }
                    Console.WriteLine("Current amount of fuel - {0}", car.CurrentFuel);
                    Console.WriteLine("Maximum amount of fuel - {0}", car.MaxFuel);

                    car.FuelUp(ReadDouble("Add fuel(L) - ", 0, car.MaxFuel - car.CurrentFuel));
                    Console.WriteLine("OK! Current amount of fuel - {0}", car.CurrentFuel);
                    break;
                case 5:
                    if (!(vehicles[index] is IElectricMotor vehicle))
                    {
                        Console.WriteLine("You can not charge this vehicle!");
                        break;
                    }
                    Console.WriteLine("Current amount of energy - {0}", vehicle.CurrentEnergy);
                    Console.WriteLine("Maximum amount of energy - {0}", vehicle.BatteryCapacity);

                    vehicle.ChargeBatteryUp(ReadDouble("Add energy(kW) - ", 0, vehicle.BatteryCapacity - vehicle.CurrentEnergy));
                    Console.WriteLine("OK! Current amount of energy - {0}", vehicle.CurrentEnergy);
                    break;
                case 6:
                    Console.WriteLine("Nothing has been changed");
                    return;
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