using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatHouse
{
    public class Flat
    {
        public int ApartmentNumber { get; private set; }
        public float FloorArea { get; private set; }
        public float Rent { get; private set; }
        public bool IsOccupied { get; private set; }
        public string ResidentName { get; private set; }
        public Flat(int apartmentNumber, float floorArea)
        {
            ApartmentNumber = apartmentNumber;
            FloorArea = floorArea;
            Rent = floorArea * 100f;
            IsOccupied = false;
        }
        public Flat(int apartmentNumber, float floorArea, string residentName) : this(apartmentNumber, floorArea)
        {
            if (residentName != string.Empty)
            {
                IsOccupied = true;
            }
            ResidentName = residentName;
        }
    }
}
