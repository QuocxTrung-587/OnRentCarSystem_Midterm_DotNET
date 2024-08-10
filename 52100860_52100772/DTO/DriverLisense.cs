using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DriverLisense
    {
        private string lisenseID;
        private string driverFullName;
        private DateTime driverDateOfBirth;
        private byte[] driverImage;
        private string phoneNumber;

        public DriverLisense(string lisenseID, string driverFullName, DateTime driverDateOfBirth, byte[] driverImage, string phoneNumber)
        {
            this.lisenseID = lisenseID;
            this.driverFullName = driverFullName;
            this.driverDateOfBirth = driverDateOfBirth;
            this.driverImage = driverImage;
            this.phoneNumber = phoneNumber;
        }

        public string LisenseID { get => lisenseID; set => lisenseID = value; }
        public string DriverFullName { get => driverFullName; set => driverFullName = value; }
        public DateTime DriverDateOfBirth { get => driverDateOfBirth; set => driverDateOfBirth = value; }
        public byte[] DriverImage { get => driverImage; set => driverImage = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
    }
}
