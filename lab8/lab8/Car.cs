using System;
using System.Collections.Generic;
using System.Text;

namespace lab8
{
    class Car : Vehicle
    {
        public int NumberOfSeats { get; protected set; }
        public double CurrentFuel { get; protected set; }
        public double MaxFuel { get; protected set; }
        public double FuelConsumption { get; protected set; }
        public Type MyType { get; protected set; }
        public enum Type { Sport = 1, City, Truck, Cabriolet, Van, Bus };

        public Car(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost, double mileage, DateTime manufacturingTime,
            bool isBroken, int numberOfSeats, double currentFuel, double maxFuel, double fuelConsumption, Type myType)
            : base(serialNumber, weight, carryingCapacity, maxSpeed, cost, mileage, manufacturingTime, isBroken)
        {
            NumberOfSeats = numberOfSeats;
            CurrentFuel = currentFuel;
            MaxFuel = maxFuel;
            FuelConsumption = fuelConsumption;
            MyType = myType;
        }

        public Car(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost,
            int numberOfSeats, double currentFuel, double maxFuel, double fuelConsumption, Type myType)
            : base(serialNumber, weight, carryingCapacity, maxSpeed, cost)
        {
            NumberOfSeats = numberOfSeats;
            CurrentFuel = currentFuel;
            MaxFuel = maxFuel;
            FuelConsumption = fuelConsumption;
            MyType = myType;
        }

        protected Car(string serialNumber, double currentFuel)
        {
            MyId = ++Id;
            SerialNumber = serialNumber;
            CurrentFuel = currentFuel;
            Mileage = 0;
            ManufacturingTime = DateTime.Now;
            IsBroken = false;
            OnNewVehicleAppearedEventHandler(this);
        }


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
            return Run(1);
        }

        public override bool Run(double meters)
        {
            if (IsBroken)
            {
                throw new Exception("Can't run broken vehicle");
            }
            if (meters < 0)
            {
                throw new Exception("Can't run negative distance");
            }

            if (FuelConsumption * meters / 1000 / 100 >= CurrentFuel)
            {
                return false;
            }
            CurrentFuel -= FuelConsumption * meters / 1000 / 100;
            Mileage += meters;

            if (Mileage > 2000000000)
            {
                OnGoneOldEventHandler(this);
            }
            return true;
        }

    }
}
