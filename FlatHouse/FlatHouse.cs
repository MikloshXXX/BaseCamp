using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlatHouse
{
    public class FlatHouse
    {
        private static FlatHouse _instance;
        public static FlatHouse Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FlatHouse();
                }
                return _instance;
            }
        }
        public List<Flat> Flats { get; private set; }
    }
}
