using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class DriverLisenseBUS
    {
        private DriverLisenseDAL driverLisenseDAL;

        public DriverLisenseBUS()
        {
            this.driverLisenseDAL = new DriverLisenseDAL();
        }

        public bool InsertLisense(DriverLisense driverLisense)
        {
            return this.driverLisenseDAL.InsertLisense(driverLisense);
        }

        public bool DeleteDriverLisense(DriverLisense driverLisense)
        {
            return this.driverLisenseDAL.DeleteDriverLisense(driverLisense);
        }

        public bool DeleteDriverLisense(string lisenseID, string phoneNumber)
        {
            return this.driverLisenseDAL.DeleteDriverLisense(lisenseID, phoneNumber);
        }

        public bool UpdateDriverLisense(DriverLisense driverLisense)
        {
            return this.driverLisenseDAL.UpdateDriverLisense(driverLisense);
        }

        public List<DriverLisense> GetAllDriverLisenses()
        {
            return this.driverLisenseDAL.GetAllDriverLisenses();
        }

        public DriverLisense getDriverLisenseByPhoneNumber(string phoneNumber)
        {
            return this.driverLisenseDAL.getDriverLisenseByPhoneNumber(phoneNumber);
        }
    }
}
