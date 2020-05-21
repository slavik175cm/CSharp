using System;
using System.Collections.Generic;
using System.Text;

namespace lab8
{
    class Vehicle : IComparable<Vehicle>, ICloneable
    {
        public delegate void GoneOldEventHandler(Vehicle oldVehicle);
        public static event GoneOldEventHandler GoneOld;

        public delegate void NewVehicleAppearedEventHandler(Vehicle newVehicle);
        public static event NewVehicleAppearedEventHandler NewAppeared;

        public delegate int Comparator(Vehicle x, Vehicle y);
        
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
        public static int Id { get; protected set; } = 0;

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
            OnNewVehicleAppearedEventHandler(this);
        }

        public Vehicle(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost, double mileage, DateTime manufacturingTime, bool isBroken = false)
            : this(serialNumber, weight, carryingCapacity, maxSpeed, cost)
        {
            ManufacturingTime = manufacturingTime;
            Mileage = mileage;
            IsBroken = isBroken;
        }

        protected virtual void OnNewVehicleAppearedEventHandler(Vehicle newVehicle)
        {
            NewAppeared?.Invoke(newVehicle);
        }

        protected virtual void OnGoneOldEventHandler(Vehicle oldVehicle)
        {
            GoneOld?.Invoke(oldVehicle);
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

        public virtual bool Run(double meters)
        {
            if (IsBroken)
            {
                throw new Exception("Can't run broken vehicle");
            }
            if (meters < 0)
            {
                throw new Exception("Can't run negative distance");
            }

            Mileage += meters;
            
            if (Mileage > 2000000000)
            {
                OnGoneOldEventHandler(this);
            }
            return true;
        }   

        public void Repair()
        {
            if (IsBroken == false)
            {
                throw new Exception("It's not broken!");
            }
            IsBroken = false;
        }

        public void Break()
        {
            if (IsBroken == true)
            {
                throw new Exception("It's already broken!");
            }
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

        public static int CompareByCost(Vehicle x, Vehicle y)
        {
            return x.CompareTo(y);
        }

        public static int CompareById(Vehicle x, Vehicle y)
        {
            return x.MyId > y.MyId ? 1 : -1;
        }

        public Object Clone()
        {
            Vehicle clonedVehicle = (Vehicle)MemberwiseClone();
            clonedVehicle.MyId = ++Id;
            OnNewVehicleAppearedEventHandler(clonedVehicle);
            return clonedVehicle;
        }

    }
}
