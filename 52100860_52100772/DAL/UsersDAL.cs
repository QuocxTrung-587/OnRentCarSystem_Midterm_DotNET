using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class UsersDAL
    {
        private string connectionString = "Server=DESKTOP-T0CB33S;Database=OnRentCarSystemDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

        public bool InsertUser(UsersDTO userDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Users (PhoneNumber, Pass, UserFullName, UserDateOfBirth, UserGender, UserEmail, UserAddress, UserRole) " +
                               "VALUES (@PhoneNumber, @Pass, @UserFullName, @UserDateOfBirth, @UserGender, @UserEmail, @UserAddress, @UserRole)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", userDTO.PhoneNumber);
                command.Parameters.AddWithValue("@Pass", userDTO.Pass);
                command.Parameters.AddWithValue("@UserFullName", userDTO.UserFullName);
                command.Parameters.AddWithValue("@UserDateOfBirth", userDTO.UserDateOfBirth);
                command.Parameters.AddWithValue("@UserGender", userDTO.UserGender);
                command.Parameters.AddWithValue("@UserEmail", userDTO.UserEmail);
                command.Parameters.AddWithValue("@UserAddress", userDTO.UserAddress);
                command.Parameters.AddWithValue("@UserRole", userDTO.UserRole);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteUser(UsersDTO usersDTO)
        {
            using (SqlConnection connection =new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE PhoneNumber = @PhoneNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", usersDTO.PhoneNumber);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteUser(string userID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Users WHERE PhoneNumber = @PhoneNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", userID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool UpdateUser(UsersDTO userDTO)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Users SET Pass = @Pass, UserFullName = @UserFullName, UserDateOfBirth = @UserDateOfBirth, UserGender = @UserGender, UserEmail = @UserEmail, UserAddress = @UserAddress, UserRole = @UserRole WHERE PhoneNumber = @PhoneNumber";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", userDTO.PhoneNumber);
                command.Parameters.AddWithValue("@Pass", userDTO.Pass);
                command.Parameters.AddWithValue("@UserFullName", userDTO.UserFullName);
                command.Parameters.AddWithValue("@UserDateOfBirth", userDTO.UserDateOfBirth);
                command.Parameters.AddWithValue("@UserGender", userDTO.UserGender);
                command.Parameters.AddWithValue("@UserEmail", userDTO.UserEmail);
                command.Parameters.AddWithValue("@UserAddress", userDTO.UserAddress);
                command.Parameters.AddWithValue("UserRole", userDTO.UserRole);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
            
        }

        public List<UsersDTO> GetAllUsers(int userRole)
        {
            List<UsersDTO> users = new List<UsersDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE UserRole = @UserRole";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserRole", userRole);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UsersDTO user = new UsersDTO(
                        reader["PhoneNumber"].ToString(),
                        reader["Pass"].ToString(),
                        reader["UserFullName"].ToString(),
                        Convert.ToDateTime(reader["UserDateOfBirth"]),
                        Convert.ToInt32(reader["UserGender"]),
                        reader["UserEmail"].ToString(),
                        reader["UserAddress"].ToString(),
                        Convert.ToInt32(reader["UserRole"].ToString())
                    );

                    users.Add(user);
                }
                connection.Close();
            }
            
            return users;
        }

        public UsersDTO getUserByPhoneNumber(string phoneNumber)
        {
            List<UsersDTO> users = new List<UsersDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE PhoneNumber = @PhoneNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UsersDTO user = new UsersDTO(
                        reader["PhoneNumber"].ToString(),
                        reader["Pass"].ToString(),
                        reader["UserFullName"].ToString(),
                        Convert.ToDateTime(reader["UserDateOfBirth"]),
                        Convert.ToInt32(reader["UserGender"]),
                        reader["UserEmail"].ToString(),
                        reader["UserAddress"].ToString(),
                        Convert.ToInt32(reader["UserRole"].ToString())
                    );

                    users.Add(user);
                }
                connection.Close();
            }

            if (users.Count > 0) {
                return users[0];
            }

            return null;
        }

        public UsersDTO Login(string phonenumber, string password)
        {
            List<UsersDTO> users = new List<UsersDTO>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Users WHERE PhoneNumber = @PhoneNumber AND Pass = @Pass";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PhoneNumber", phonenumber);
                command.Parameters.AddWithValue("@Pass", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UsersDTO user = new UsersDTO(
                        reader["PhoneNumber"].ToString(),
                        reader["Pass"].ToString(),
                        reader["UserFullName"].ToString(),
                        DateTime.Now,
                        Convert.ToInt32(reader["UserGender"]),
                        reader["UserEmail"].ToString(),
                        reader["UserAddress"].ToString(),
                        Convert.ToInt32(reader["UserRole"].ToString())
                    );

                    users.Add(user);
                }
                connection.Close();
            }

            if (users.Count > 0)
            {
                return users[0];
            }

            return null;
        }
    }
}
