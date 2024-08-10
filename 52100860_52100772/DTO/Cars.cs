using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Cars
    {
        private string carsID;
        private int carTypeID;
        private int fuelID;
        private int isRent;
        private int carPrice;

        public Cars(string carsID, int carTypeID, int fuelID, int isRent, int carPrice)
        {
            this.carsID = carsID;
            this.carTypeID = carTypeID;
            this.fuelID = fuelID;
            this.isRent = isRent;
            this.carPrice = carPrice;
        }

        public string CarsID { get => carsID; set => carsID = value; }
        public int CarTypeID { get => carTypeID; set => carTypeID = value; }
        public int FuelID { get => fuelID; set => fuelID = value; }
        public int IsRent { get => isRent; set => isRent = value; }
        public int CarPrice { get => carPrice; set => carPrice = value; }
    }
}
