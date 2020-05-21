using System;
using System.Collections.Generic;
using System.Text;

namespace lab8
{
    class BMWX5 : Car, IElectricMotor
    {
        public double BatteryCapacity { get; protected set; }
        public double CurrentEnergy { get; protected set; }
        public double EnergyConsumption { get; protected set; }

        public BMWX5(string serialNumber, double mileage, DateTime manufacturingTime, bool isBroken, double currentFuel, double currentEnergy)
            : this(serialNumber, currentFuel, currentEnergy)
        {
            Mileage = mileage;
            ManufacturingTime = manufacturingTime;
            IsBroken = isBroken;
        }

        public BMWX5(string serialNumber, double currentFuel, double currentEnergy)
            : base(serialNumber, currentFuel)
        {
            Weight = 2190;
            CarryingCapacity = 710;
            MaxSpeed = 250;
            Cost = 60000;
            NumberOfSeats = 5;
            MaxFuel = 85;
            FuelConsumption = 7.6;
            MyType = Type.City;
            BatteryCapacity = 100;
            EnergyConsumption = 2.5;
            if (CurrentFuel > MaxFuel)
            {
                CurrentFuel = MaxFuel;
            }
            CurrentEnergy = currentEnergy;
            if (CurrentEnergy > BatteryCapacity)
            {
                CurrentEnergy = BatteryCapacity;
            }
        }

        public void ChargeBatteryUp()
        {
            CurrentEnergy = BatteryCapacity;
        }

        public void ChargeBatteryUp(double amount)
        {
            CurrentEnergy += amount;
            if (CurrentEnergy > BatteryCapacity)
            {
                CurrentEnergy = BatteryCapacity;
            }
        }

        public bool RunOnElectricMotor(double meters)
        { 
            if (meters * EnergyConsumption / 1000 > CurrentEnergy)
            {
                return false;
            }
            Mileage += meters;
            CurrentEnergy -= meters * EnergyConsumption / 1000;
            return true;
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

            double howFarOnGas = CurrentFuel / FuelConsumption * 1000 * 100;
            double howFarOnEnergy = CurrentEnergy / EnergyConsumption * 1000;
            if (howFarOnGas + howFarOnEnergy < meters)
            {
                return false;
            }
            if (base.Run(meters))
            {
                return true;
            }

            CurrentFuel = 0;
            Mileage += howFarOnGas;
            RunOnElectricMotor(meters - howFarOnGas);

            if (Mileage > 2000000000)
            {
                OnGoneOldEventHandler(this);
            }
            return true;
        }

        public override string InfoToString()
        {
            StringBuilder info = new StringBuilder(base.InfoToString());
            info.AppendLine("Has electric motor");
            info.AppendLine($"Current energy: {CurrentEnergy}kW");
            info.AppendLine($"Battery capacity: {BatteryCapacity}kW");
            info.AppendLine($"Energy consumption: {EnergyConsumption}kW/Km");
            return info.ToString();
        }
    }
}
