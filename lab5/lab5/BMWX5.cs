using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    class BMWX5 : Car
    {
        public BMWX5(string serialNumber, double mileage, DateTime manufacturingTime, bool isBroken, double currentFuel)
            :this(serialNumber, currentFuel)
        {
            Mileage = mileage;
            ManufacturingTime = manufacturingTime;
            IsBroken = isBroken;
        }

        public BMWX5(string serialNumber, double currentFuel)
            :base(serialNumber, currentFuel)
        {
            Weight = 2190;
            CarryingCapacity = 710;
            MaxSpeed = 250;
            Cost = 60000;
            NumberOfSeats = 5;
            MaxFuel = 85;
            FuelConsumption = 7.6;
            MyType = Type.City;
            if (CurrentFuel > MaxFuel)
            {
                CurrentFuel = MaxFuel;
            }
        }
    }
}
