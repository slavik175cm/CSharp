using System;
using System.Collections.Generic;
using System.Text;

namespace lab5
{
    class LamborghiniAventador : Car
    {
        public LamborghiniAventador(string serialNumber, double mileage, DateTime manufacturingTime, bool isBroken, double currentFuel)
               : this(serialNumber, currentFuel)
        {
            Mileage = mileage;
            ManufacturingTime = manufacturingTime;
            IsBroken = isBroken;
        }

        public LamborghiniAventador(string serialNumber, double currentFuel)
            : base(serialNumber, currentFuel)
        {
            Weight = 1575;
            CarryingCapacity = 200;
            MaxSpeed = 350;
            Cost = 500000;
            NumberOfSeats = 2;
            MaxFuel = 90;
            FuelConsumption = 17.2;
            MyType = Type.Sport;
            if (CurrentFuel > MaxFuel)
            {
                CurrentFuel = MaxFuel;
            }
        }
    }
}
