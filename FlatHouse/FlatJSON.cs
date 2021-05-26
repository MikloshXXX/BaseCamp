using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatHouse
{
    public class FlatJSON
    {
        public int FlatNumber { get; set; }
        public float FloorArea { get; set; }
        public string ResidentName { get; set; }
        public FlatJSON(int flatNumber, float floorArea, string residentName)
        {
            FlatNumber = flatNumber;
            FloorArea = floorArea;
            ResidentName = residentName;
        }
    }
}
