using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BillBUS
    {
        private BillDAL billDAL;

        public BillBUS()
        {
            this.billDAL = new BillDAL();
        }

        public bool InsertBill(Bill bill)
        {
            return this.billDAL.InsertBill(bill);
        }

        public bool DeleteBill(Bill bill)
        {
            return this.billDAL.DeleteBill(bill);
        }

        public bool DeleteBill(int billID, string phoneNumber)
        {
            return this.billDAL.DeleteBill(billID, phoneNumber);
        }

        public bool UpdateBill(Bill bill)
        {
            return this.billDAL.UpdateBill(bill);
        }

        public List<Bill> GetAllBills()
        {
            return this.billDAL.GetAllBills();
        }

        public Bill getBillByPhoneNumberAndBillID(int billID, string phoneNumber)
        {
            return this.billDAL.getBillByPhoneNumberAndBillID(billID, phoneNumber);
        }

        public List<Bill> GetAllBillsByPhoneNumber(string phoneNumber)
        {
            return this.billDAL.GetAllBillsByPhoneNumber(phoneNumber);
        }

        public List<Bill> GetBillsByDateRange(DateTime startDate, DateTime endDate)
        {
            return this.billDAL.GetBillsByDateRange(startDate, endDate);
        }
    }
}
