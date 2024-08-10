using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BillDAL
    {
        private string connectionString = "Server=DESKTOP-T0CB33S;Database=OnRentCarSystemDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

        public bool InsertBill(Bill bill)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                /*string query = "INSERT INTO Bill VALUES (@BillID, @UserPhoneNumber, @CarID, @DateCreate" +
                    ", @IsRent, @DateRent, @DateReturn, @TimeRent, @Total)";*/

                string query = "INSERT INTO Bill VALUES (@UserPhoneNumber, @CarID, @DateCreate" +
                    ", @IsRent, @DateRent, @DateReturn, @TimeRent, @Total)";

                SqlCommand command = new SqlCommand(query, connection);
                /*command.Parameters.AddWithValue("@BillID", bill.BillID);*/
                command.Parameters.AddWithValue("@UserPhoneNumber", bill.UserPhoneNumber);
                command.Parameters.AddWithValue("@CarID", bill.CarID);
                command.Parameters.AddWithValue("@DateCreate", bill.DateCreate);
                command.Parameters.AddWithValue("@IsRent", bill.IsRent);
                command.Parameters.AddWithValue("@DateRent", bill.DateRent);
                command.Parameters.AddWithValue("@DateReturn", bill.DateReturn);
                command.Parameters.AddWithValue("@TimeRent", bill.TimeRent);
                command.Parameters.AddWithValue("@Total", bill.Total);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteBill(Bill bill)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Bill WHERE BillID = @BillID AND UserPhoneNumber = @UserPhoneNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BillID", bill.BillID);
                command.Parameters.AddWithValue("@UserPhoneNumber", bill.UserPhoneNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteBill(int billID, string phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Bill WHERE BillID = @BillID AND UserPhoneNumber = @UserPhoneNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BillID", billID);
                command.Parameters.AddWithValue("@UserPhoneNumber", phoneNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool UpdateBill(Bill bill)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Bill SET CarID = @CarID, DateCreate = @DateCreate, IsRent = @IsRent, DateRent = @DateRent" +
                    ", DateReturn = @DateReturn, TimeRent = @TimeRent, Total = @Total WHERE UserPhoneNumber = @UserPhoneNumber AND BillID = @BillID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BillID", bill.BillID);
                command.Parameters.AddWithValue("@UserPhoneNumber", bill.UserPhoneNumber);
                command.Parameters.AddWithValue("@CarID", bill.CarID);
                command.Parameters.AddWithValue("@DateCreate", bill.DateCreate);
                command.Parameters.AddWithValue("@IsRent", bill.IsRent);
                command.Parameters.AddWithValue("@DateRent", bill.DateRent);
                command.Parameters.AddWithValue("@DateReturn", bill.DateReturn);
                command.Parameters.AddWithValue("@TimeRent", bill.TimeRent);
                command.Parameters.AddWithValue("@Total", bill.Total);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }

        }

        public List<Bill> GetAllBills()
        {
            List<Bill> bills = new List<Bill>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Bill";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Bill bill = new Bill(
                        Convert.ToInt32(reader["BillID"].ToString()),
                        reader["UserPhoneNumber"].ToString(),
                        reader["CarID"].ToString(),
                        Convert.ToDateTime(reader["DateCreate"].ToString()),
                        Convert.ToInt32(reader["IsRent"].ToString()),
                        Convert.ToDateTime(reader["DateRent"].ToString()),
                        Convert.ToDateTime(reader["DateReturn"].ToString()),
                        TimeSpan.Parse(reader["TimeRent"].ToString()),
                        float.Parse(reader["Total"].ToString())
                    );
                    

                    bills.Add(bill);
                }
                connection.Close();
            }

            return bills;
        }

        public Bill getBillByPhoneNumberAndBillID(int billID, string phoneNumber)
        {
            List<Bill> bills = new List<Bill>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Bill WHERE UserPhoneNumber = @UserPhoneNumber AND BillID = @BillID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserPhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@BillID", billID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Bill bill = new Bill(
                        Convert.ToInt32(reader["BillID"].ToString()),
                        reader["UserPhoneNumber"].ToString(),
                        reader["CarID"].ToString(),
                        Convert.ToDateTime(reader["DateCreate"].ToString()),
                        Convert.ToInt32(reader["IsRent"].ToString()),
                        Convert.ToDateTime(reader["DateRent"].ToString()),
                        Convert.ToDateTime(reader["DateReturn"].ToString()),
                        TimeSpan.Parse(reader["TimeRent"].ToString()),
                        float.Parse(reader["Total"].ToString())
                    );

                    bills.Add(bill);
                }
                connection.Close();
            }

            if (bills.Count > 0)
            {
                return bills[0];
            }

            return null;
        }

        public List<Bill> GetAllBillsByPhoneNumber(string phoneNumber)
        {
            List<Bill> bills = new List<Bill>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Bill WHERE UserPhoneNumber = @UserPhoneNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserPhoneNumber", phoneNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Bill bill = new Bill(
                        Convert.ToInt32(reader["BillID"].ToString()),
                        reader["UserPhoneNumber"].ToString(),
                        reader["CarID"].ToString(),
                        Convert.ToDateTime(reader["DateCreate"].ToString()),
                        Convert.ToInt32(reader["IsRent"].ToString()),
                        Convert.ToDateTime(reader["DateRent"].ToString()),
                        Convert.ToDateTime(reader["DateReturn"].ToString()),
                        TimeSpan.Parse(reader["TimeRent"].ToString()),
                        float.Parse(reader["Total"].ToString())
                    );


                    bills.Add(bill);
                }
                connection.Close();
            }

            return bills;
        }

        public List<Bill> GetBillsByDateRange(DateTime startDate, DateTime endDate)
        {
            List<Bill> bills = new List<Bill>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Bill WHERE DateCreate >= @StartDate AND DateCreate < @EndDate";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bill bill = new Bill
                            (
                                Convert.ToInt32(reader["BillID"]),
                                reader["UserPhoneNumber"].ToString(),
                                reader["CarID"].ToString(),
                                Convert.ToDateTime(reader["DateCreate"]),
                                Convert.ToInt32(reader["IsRent"]),
                                Convert.ToDateTime(reader["DateRent"]),
                                Convert.ToDateTime(reader["DateReturn"]),
                                TimeSpan.Parse(reader["TimeRent"].ToString()),
                                Convert.ToSingle(reader["Total"])
                            );

                            bills.Add(bill);
                        }
                    }
                }
            }

            return bills;
        }
    }
}
