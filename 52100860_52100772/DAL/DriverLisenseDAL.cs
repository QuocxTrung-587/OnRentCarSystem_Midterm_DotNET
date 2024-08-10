using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DriverLisenseDAL
    {
        private string connectionString = "Server=DESKTOP-T0CB33S;Database=OnRentCarSystemDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

        public bool InsertLisense(DriverLisense driverLisense)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO DriverLisense " +
                               "VALUES (@LisenseID, @DriverFullName, @DriverDateOfBirth, @DriverImage, @PhoneNumber)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LisenseID", driverLisense.LisenseID);
                command.Parameters.AddWithValue("@DriverFullName", driverLisense.DriverFullName);
                command.Parameters.AddWithValue("@DriverDateOfBirth", driverLisense.DriverDateOfBirth);
                command.Parameters.AddWithValue("@DriverImage", driverLisense.DriverImage);
                command.Parameters.AddWithValue("@PhoneNumber", driverLisense.PhoneNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteDriverLisense(DriverLisense driverLisense)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM DriverLisense WHERE PhoneNumber = @PhoneNumber AND LisenseID = @LisenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", driverLisense.PhoneNumber);
                command.Parameters.AddWithValue("@LisenseID", driverLisense.LisenseID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteDriverLisense(string lisenseID, string phoneNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM DriverLisense WHERE PhoneNumber = @PhoneNumber AND LisenseID = @LisenseID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                command.Parameters.AddWithValue("@LisenseID", lisenseID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool UpdateDriverLisense(DriverLisense driverLisense)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE DriverLisense SET DriverFullName = @DriverFullName, DriverDateOfBirth = @DriverDateOfBirth, DriverImage = @DriverImage WHERE PhoneNumber = @PhoneNumber AND LisenseID = @LisenseID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LisenseID", driverLisense.LisenseID);
                command.Parameters.AddWithValue("@DriverFullName", driverLisense.DriverFullName);
                command.Parameters.AddWithValue("@DriverDateOfBirth", driverLisense.DriverDateOfBirth);
                command.Parameters.AddWithValue("@DriverImage", driverLisense.DriverImage);
                command.Parameters.AddWithValue("@PhoneNumber", driverLisense.PhoneNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }

        }

        public List<DriverLisense> GetAllDriverLisenses()
        {
            List<DriverLisense> driverLisenses = new List<DriverLisense>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM DriverLisense";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DriverLisense driver = new DriverLisense(
                        reader["LisenseID"].ToString(),
                        reader["DriverFullName"].ToString(),
                        (DateTime)reader["DriverDateOfBirth"],
                        (byte[])reader["DriverImage"],
                        reader["PhoneNumber"].ToString()
                    );

                    driverLisenses.Add(driver);
                }
                connection.Close();
            }

            return driverLisenses;
        }

        public DriverLisense getDriverLisenseByPhoneNumber(string phoneNumber)
        {
            List<DriverLisense> driverLisenses = new List<DriverLisense>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM DriverLisense WHERE PhoneNumber = @PhoneNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DriverLisense driver = new DriverLisense(
                        reader["LisenseID"].ToString(),
                        reader["DriverFullName"].ToString(),
                        (DateTime)reader["DriverDateOfBirth"],
                        (byte[])reader["DriverImage"],
                        reader["PhoneNumber"].ToString()
                    );

                    driverLisenses.Add(driver);
                }
                connection.Close();
            }

            if (driverLisenses.Count > 0)
            {
                return driverLisenses[0];
            }

            return null;
        }
    }
}
