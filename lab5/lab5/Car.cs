using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    class Car : Vehicle
    {
        public int NumberOfSeats { get; protected set; }
        public double CurrentFuel { get; protected set; }
        public double MaxFuel { get; protected set; }
        public double FuelConsumption { get; protected set; }
        public Type MyType { get; protected set; }

        public Car(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost, double mileage, DateTime manufacturingTime,
            bool isBroken, int numberOfSeats, double currentFuel, double maxFuel, double fuelConsumption, Type myType) 
            : base(serialNumber, weight, carryingCapacity, maxSpeed, cost, mileage, manufacturingTime, isBroken)
        {
            NumberOfSeats = numberOfSeats;
            CurrentFuel = currentFuel;
            MaxFuel = maxFuel;
            FuelConsumption  = fuelConsumption;
            MyType = myType;
        }

        public Car(Vehicle vehicle, int numberOfSeats, double currentFuel, double maxFuel, double fuelConsumption, Type myType)
            : base(vehicle.SerialNumber, vehicle.Weight, vehicle.CarryingCapacity, vehicle.MaxSpeed, vehicle.Cost,
                   vehicle.Mileage, vehicle.ManufacturingTime, vehicle.IsBroken)
        {
            MyId = --Id;
            NumberOfSeats = numberOfSeats;
            CurrentFuel = currentFuel;
            MaxFuel = maxFuel;
            FuelConsumption = fuelConsumption;
            MyType = myType;
        }
        
        public Car(string serialNumber, double currentFuel)
        {
            MyId = ++Id;
            SerialNumber = serialNumber;
            CurrentFuel = currentFuel;
            Mileage = 0;
            ManufacturingTime = DateTime.Now;
            IsBroken = false;
        }

        public enum Type { Sport = 1, City, Truck, Cabriolet, Van, Bus };

        public override string InfoToString()
        {
            StringBuilder info = new StringBuilder(base.InfoToString());
            info.AppendLine($"Numeber of seats: {NumberOfSeats}");
            info.AppendLine($"Current fuel: {CurrentFuel}L");
            info.AppendLine($"Max fuel: {MaxFuel}L");
            info.AppendLine($"Fuel consumption: {FuelConsumption}L/100Km");
            info.AppendLine($"{MyType.ToString()} Car");
            return info.ToString();
        }

        public void FuelUp()
        {
            CurrentFuel = MaxFuel;
        }

        public void FuelUp(double amount)
        {
            CurrentFuel += amount;
            if (CurrentFuel > MaxFuel)
            {
                CurrentFuel = MaxFuel;
            }
        }

        public override bool Run()
        {
            if (FuelConsumption / 1000 / 100 >= CurrentFuel)
            {
                return false;
            }
            CurrentFuel -= FuelConsumption / 1000 / 100;
            Mileage++;
            return true;
        }

        public override bool Run(double distance)
        {
            if (FuelConsumption * distance / 1000 / 100 >= CurrentFuel)
            {
                return false;
            }
            CurrentFuel -= FuelConsumption * distance / 1000 / 100;
            Mileage += distance;
            return true;
        }

    }
}
