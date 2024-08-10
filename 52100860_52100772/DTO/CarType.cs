using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CarType
    {
        private int carTypeID;
        private string carTypeName;

        public CarType(int carTypeID, string carTypeName)
        {
            this.carTypeID = carTypeID;
            this.carTypeName = carTypeName;
        }

        public int CarTypeID { get => carTypeID; set => carTypeID = value; }
        public string CarTypeName { get => carTypeName; set => carTypeName = value; }
    }
}
