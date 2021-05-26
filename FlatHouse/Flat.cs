using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatHouse
{
    public class Flat
    {
        public int FlatNumber { get; private set; }
        public float FloorArea { get; private set; }
        public float Rent { get; private set; }
        public bool IsOccupied { get; private set; }
        public string ResidentName { get; private set; }

        private static int numberCounter = 1;

        public Flat(int flatNumber, float floorArea)
        {
            FlatNumber = flatNumber;
            FloorArea = floorArea;
            Rent = floorArea * 100f;
            IsOccupied = false;
        }
        public Flat(int flatNumber, float floorArea, string residentName) : this(flatNumber, floorArea)
        {
            IsOccupied = true;
            ResidentName = residentName;
        }
    }
}
