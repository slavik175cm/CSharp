using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    class Vehicle
    {
        public string serialNumber { get; private set; }
        public double weight { get; private set; }
        public double carryingCapacity { get; private set; }
        public double maxSpeed { get; private set; }
        public double cost { get; private set; }
        public DateTime manufacturingTime { get; private set; }
        public bool isBroken { get; private set; }
        public uint myid { get; private set; }
        private static uint id = 0;

        public Vehicle(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost, bool isBroken = false)
        {
            this.serialNumber = serialNumber;
            this.weight = weight;
            this.carryingCapacity = carryingCapacity;
            this.maxSpeed = maxSpeed;
            this.cost = cost;
            this.manufacturingTime = DateTime.Now;
            this.isBroken = isBroken;
            this.myid = ++id;
        }

        public Vehicle(string serialNumber, double weight, double carryingCapacity, double maxSpeed, double cost, DateTime manufacturingTime, bool isBroken = false)
        {
            this.serialNumber = serialNumber;
            this.weight = weight;
            this.carryingCapacity = carryingCapacity;
            this.maxSpeed = maxSpeed;
            this.cost = cost;
            this.manufacturingTime = manufacturingTime;
            this.isBroken = isBroken;
            this.myid = ++id;
        }

        public bool ChangeSerialNumber(string newSerialNumber)
        {
            foreach(var ch in newSerialNumber)
            {
                if ((ch < 'A' || ch > 'Z') && (ch < '0' || ch > '9'))
                {
                    return false;
                }
            }
            serialNumber = newSerialNumber;
            return true;
        }

        public int HowManyYears()
        {
            return DateTime.Now.Year - manufacturingTime.Year;
        }

        public void Repair()
        {
            isBroken = false;
        }

    }
}
