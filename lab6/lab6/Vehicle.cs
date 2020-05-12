using System;
using System.Collections.Generic;
using System.Text;

namespace lab6
{
    class Vehicle : IComparable<Vehicle>, ICloneable
    {
        private string serialNumber;
        public string SerialNumber
        {
            get => serialNumber;
            set
            {
                if (IsValidSerialNumber(value))
                {
                    serialNumber = value;
                }
            }
        }
        public double Weight { get; protected set; }
        public double CarryingCapacity { get; protected set; }
        public double MaxSpeed { get; protected set; }
        public double Cost { get; protected set; }
        public double Mileage { get; protected set; }
        public DateTime ManufacturingTime { get; protected set; }
        public bool IsBroken { get; protected set; }
        public int MyId { get; protected set; }
        public static int Id { get; set; } = 0;

        public Vehicle()
        {

        }

        public Vehicle(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost)
        {
            SerialNumber = serialNumber;
            Weight = weight;
            CarryingCapacity = carryingCapacity;
            MaxSpeed = maxSpeed;
            Cost = cost;
            Mileage = 0;
            ManufacturingTime = DateTime.Now;
            IsBroken = false;
            MyId = ++Id;
        }

        public Vehicle(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost, double mileage, DateTime manufacturingTime, bool isBroken = false)
            : this(serialNumber, weight, carryingCapacity, maxSpeed, cost)
        {
            ManufacturingTime = manufacturingTime;
            Mileage = mileage;
            IsBroken = isBroken;
        }

        static public bool IsValidSerialNumber(string serialNumber)
        {
            if (serialNumber.Length > 20)
            {
                return false;
            }
            foreach (var character in serialNumber)
            {
                if ((character < 'A' || character > 'Z') && (character < '0' || character > '9'))
                {
                    return false;
                }
            }
            return true;
        }

        public virtual string InfoToString()
        {
            StringBuilder info = new StringBuilder();
            info.AppendLine($"Id: {MyId}");
            info.AppendLine($"Serial number: {SerialNumber}");
            info.AppendLine($"Weight: { Weight}kg");
            info.AppendLine($"Carrying capacity: {CarryingCapacity}kg");
            info.AppendLine($"Max speed: {MaxSpeed}m/s");
            info.AppendLine($"Mileage: {Mileage}m");
            info.AppendLine($"Cost: {Cost}$");
            info.AppendLine($"Manufacturing time: { ManufacturingTime.ToShortDateString()}, {GetHowManyYears().ToString()} year(s)");
            info.AppendLine(IsBroken ? "Vehicle is broken!" : "Vehicle is not broken");
            return info.ToString();
        }

        public int GetHowManyYears()
        {
            return DateTime.Now.Year - ManufacturingTime.Year;
        }

        public virtual bool Run()
        {
            return Run(1);
        }

        public virtual bool Run(double distance)
        {
            if (IsBroken) return false;
            Mileage += distance;
            return true;
        }   

        public void Repair()
        {
            IsBroken = false;
        }

        public void Break()
        {
            IsBroken = true;
        }

        public int CompareTo(Vehicle vehicle)
        {
            if (Cost < vehicle.Cost)
                return 1;
            else if (Cost > vehicle.Cost)
                return -1;
            else 
                return 0;
        }

        public Object Clone()
        {
            Vehicle clonedVehicle = (Vehicle)MemberwiseClone();
            clonedVehicle.MyId = ++Id;
            return clonedVehicle;
        }
    }
}
