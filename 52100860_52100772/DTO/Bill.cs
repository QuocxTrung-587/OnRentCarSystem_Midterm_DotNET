using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Bill
    {
        private int billID;
        private string userPhoneNumber;
        private string carID;
        private DateTime dateCreate;
        private int isRent;
        private DateTime dateRent;
        private DateTime dateReturn;
        private TimeSpan timeRent;
        private float total;

        public Bill(int billID, string userPhoneNumber, string carID, DateTime dateCreate, int isRent, DateTime dateRent, DateTime dateReturn, TimeSpan timeRent, float total)
        {
            this.billID = billID;
            this.userPhoneNumber = userPhoneNumber;
            this.carID = carID;
            this.dateCreate = dateCreate;
            this.isRent = isRent;
            this.dateRent = dateRent;
            this.dateReturn = dateReturn;
            this.timeRent = timeRent;
            this.total = total;
        }

        public int BillID { get => billID; set => billID = value; }
        public string UserPhoneNumber { get => userPhoneNumber; set => userPhoneNumber = value; }
        public string CarID { get => carID; set => carID = value; }
        public DateTime DateCreate { get => dateCreate; set => dateCreate = value; }
        public TimeSpan TimeRent { get => timeRent; set => timeRent = value; }
        public float Total { get => total; set => total = value; }
        public int IsRent { get => isRent; set => isRent = value; }
        public DateTime DateRent { get => dateRent; set => dateRent = value; }
        public DateTime DateReturn { get => dateReturn; set => dateReturn = value; }
    }
}
