using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatHouse
{
    public class Flat
    {
        public float FloorArea { get; private set; }
        public float Rent { get; private set; }
        public bool IsOccupied { get; private set; }
        public string ResidentName { get; private set; }

        public Flat(float floorArea)
        {
            FloorArea = floorArea;
            Rent = floorArea * 100f;
            IsOccupied = false;
        }
        public Flat(float floorArea, string residentName) : this(floorArea)
        {
            IsOccupied = true;
            ResidentName = residentName;
        }
    }
}
