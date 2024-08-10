using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Fuel
    {
        private int fuelID;
        private string fuelName;

        public Fuel(int fuelID, string fuelName)
        {
            this.fuelID = fuelID;
            this.fuelName = fuelName;
        }

        public int FuelID { get => fuelID; set => fuelID = value; }
        public string FuelName { get => fuelName; set => fuelName = value; }
    }
}
