using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    class HyundaiAeroCity : Car
    {
        public HyundaiAeroCity(string serialNumber, double mileage, DateTime manufacturingTime, bool isBroken, double currentFuel)
            : this(serialNumber, currentFuel)
        {
            Mileage = mileage;
            ManufacturingTime = manufacturingTime;
            IsBroken = isBroken;
        }

        public HyundaiAeroCity(string serialNumber, double currentFuel)
            : base(serialNumber, currentFuel)
        {
            Weight = 10500;
            CarryingCapacity = 5000;
            MaxSpeed = 120;
            Cost = 44390;
            NumberOfSeats = 24;
            MaxFuel = 200;
            FuelConsumption = 22;
            MyType = Type.Bus;
            if (CurrentFuel > MaxFuel)
            {
                CurrentFuel = MaxFuel;
            }
        }
    }
}
