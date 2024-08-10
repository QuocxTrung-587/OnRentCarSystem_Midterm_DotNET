using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CarsDAL
    {
        private string connectionString = "Server=DESKTOP-T0CB33S;Database=OnRentCarSystemDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;";

        public bool InsertCar(Cars car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Cars " +
                               "VALUES (@CarID, @CarTypeID, @FuelID, @IsRent, @CarPrice)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", car.CarsID);
                command.Parameters.AddWithValue("@CarTypeID", car.CarTypeID);
                command.Parameters.AddWithValue("@FuelID", car.FuelID);
                command.Parameters.AddWithValue("@IsRent", car.IsRent);
                command.Parameters.AddWithValue("@CarPrice", car.CarPrice);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteCar(Cars car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cars WHERE CarID = @CarID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", car.CarsID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteCar(string carID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Cars WHERE CarID = @CarID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool UpdateCar(Cars car)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Cars SET IsRent = @IsRent, FuelID = @FuelID, CarTypeID = @CarTypeID, @CarPrice = CarPrice WHERE CarID = @CarID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", car.CarsID);
                command.Parameters.AddWithValue("@CarTypeID", car.CarTypeID);
                command.Parameters.AddWithValue("@FuelID", car.FuelID);
                command.Parameters.AddWithValue("@IsRent", car.IsRent);
                command.Parameters.AddWithValue("@CarPrice", car.CarPrice);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }

        }

        public List<Cars> GetAllCars()
        {
            List<Cars> cars = new List<Cars>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Cars car = new Cars(
                        reader["CarID"].ToString(),
                        Convert.ToInt32(reader["CarTypeID"].ToString()),
                        Convert.ToInt32(reader["FuelID"].ToString()),
                        Convert.ToInt32(reader["IsRent"].ToString()),
                        Convert.ToInt32(reader["CarPrice"].ToString())
                    );

                    cars.Add(car);
                }
                connection.Close();
            }

            return cars;
        }

        public DataTable loadData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Cars.CarID, Cars.CarTypeID, CarType.CarTypeName, Cars.FuelID, Fuel.FuelName, Cars.IsRent, Cars.CarPrice FROM Cars JOIN CarType ON Cars.CarTypeID = CarType.CarTypeID JOIN Fuel ON Cars.FuelID = Fuel.FuelID;";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
                connection.Close();
                return dataTable;
            }
        }
       

        public Cars getCarByID(string carID)
        {
            List<Cars> cars = new List<Cars>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Cars WHERE CarID = @CarID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Cars car = new Cars(
                        reader["CarID"].ToString(),
                        Convert.ToInt32(reader["CarTypeID"].ToString()),
                        Convert.ToInt32(reader["FuelID"].ToString()),
                        Convert.ToInt32(reader["IsRent"].ToString()),
                        Convert.ToInt32(reader["CarPrice"].ToString())
                    );

                    cars.Add(car);
                }
                connection.Close();
            }

            if (cars.Count > 0)
            {
                return cars[0];
            }

            return null;
        }

        public CarType getCarTypeByID(int carTypeID)
        {
            List<CarType> carTypes = new List<CarType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarType WHERE CarTypeID = @CarTypeID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarTypeID", carTypeID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarType carType = new CarType(
                        Convert.ToInt32(reader["CarTypeID"].ToString()),
                        reader["CarTypeName"].ToString()
                    );

                    carTypes.Add(carType);
                }
                connection.Close();
            }

            if (carTypes.Count > 0)
            {
                return carTypes[0];
            }

            return null;
        }

        public List<CarType> GetAllCarTypes()
        {
            List<CarType> carTypes = new List<CarType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarType";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarType carType = new CarType(
                        Convert.ToInt32(reader["CarTypeID"].ToString()),
                        reader["CarTypeName"].ToString()
                    );

                    carTypes.Add(carType);
                }
                connection.Close();
            }

            return carTypes;
        }

        public Fuel getFuelByID(int fuelID)
        {
            List<Fuel> fuels = new List<Fuel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Fuel WHERE FuelID = @FuelID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FuelID", fuelID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Fuel fuel = new Fuel(
                        Convert.ToInt32(reader["FuelID"].ToString()),
                        reader["FuelName"].ToString()
                    );

                    fuels.Add(fuel);
                }
                connection.Close();
            }

            if (fuels.Count > 0)
            {
                return fuels[0];
            }

            return null;
        }

        public List<Fuel> GetAllFuels()
        {
            List<Fuel> fuels = new List<Fuel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Fuel";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Fuel fuel = new Fuel(
                        Convert.ToInt32(reader["FuelID"].ToString()),
                        reader["FuelName"].ToString()
                    );

                    fuels.Add(fuel);
                }
                connection.Close();
            }

            return fuels;
        }

        public List<CarFeature> GetAllCarFeatures()
        {
            List<CarFeature> carFeatures = new List<CarFeature>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarFeature";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarFeature carFeature = new CarFeature(
                        reader["CarID"].ToString(),
                        Convert.ToInt32(reader["CarMap"].ToString()),
                        Convert.ToInt32(reader["CarBluetooth"].ToString()),
                        Convert.ToInt32(reader["CarBackwardCam"].ToString()),
                        Convert.ToInt32(reader["CarCurbsideCam"].ToString()),
                        Convert.ToInt32(reader["CarJourneyCam"].ToString()),
                        Convert.ToInt32(reader["CarSpeedWarning"].ToString()),
                        Convert.ToInt32(reader["CarTireSensor"].ToString()),
                        Convert.ToInt32(reader["CarCollisionSensor"].ToString()),
                        Convert.ToInt32(reader["CarSunroof"].ToString()),
                        Convert.ToInt32(reader["CarGPS"].ToString()),
                        Convert.ToInt32(reader["CarUSB"].ToString()),
                        Convert.ToInt32(reader["CarSubTire"].ToString()),
                        Convert.ToInt32(reader["CarTrunkLid"].ToString()),
                        Convert.ToInt32(reader["CarCam360"].ToString())
                    );

                    carFeatures.Add(carFeature);
                }
                connection.Close();
            }

            return carFeatures;
        }

        public CarFeature GetCarFeatureByCarID(string carID)
        {
            List<CarFeature> carFeatures = new List<CarFeature>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM CarFeature WHERE CarID = @CarID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CarFeature carFeature = new CarFeature(
                        reader["CarID"].ToString(),
                        Convert.ToInt32(reader["CarMap"].ToString()),
                        Convert.ToInt32(reader["CarBluetooth"].ToString()),
                        Convert.ToInt32(reader["CarBackwardCam"].ToString()),
                        Convert.ToInt32(reader["CarCurbsideCam"].ToString()),
                        Convert.ToInt32(reader["CarJourneyCam"].ToString()),
                        Convert.ToInt32(reader["CarSpeedWarning"].ToString()),
                        Convert.ToInt32(reader["CarTireSensor"].ToString()),
                        Convert.ToInt32(reader["CarCollisionSensor"].ToString()),
                        Convert.ToInt32(reader["CarSunroof"].ToString()),
                        Convert.ToInt32(reader["CarGPS"].ToString()),
                        Convert.ToInt32(reader["CarUSB"].ToString()),
                        Convert.ToInt32(reader["CarSubTire"].ToString()),
                        Convert.ToInt32(reader["CarTrunkLid"].ToString()),
                        Convert.ToInt32(reader["CarCam360"].ToString())
                    );

                    carFeatures.Add(carFeature);
                }
                connection.Close();
            }

            if (carFeatures.Count > 0)
            {
                return carFeatures[0];
            }

            return null;
        }

        public bool InsertCarFeature(CarFeature carFeature)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO CarFeature " +
                               "VALUES (@CarID, @CarMap, @CarBluetooth, @CarBackwardCam, @CarCurbsideCam, @CarJourneyCam, " +
                               "@CarSpeedWarning, @CarTireSensor, @CarCollisionSensor, @CarSunroof, @CarGPS, @CarUSB, @CarSubTire, " +
                               "@CarTrunkLid, @CarCam360)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carFeature.CarID);
                command.Parameters.AddWithValue("@CarMap", carFeature.CarMap);
                command.Parameters.AddWithValue("@CarBluetooth", carFeature.CarBluetooth);
                command.Parameters.AddWithValue("@CarBackwardCam", carFeature.CarBackwardCam);
                command.Parameters.AddWithValue("@CarCurbsideCam", carFeature.CarCurbsideCam);
                command.Parameters.AddWithValue("@CarJourneyCam", carFeature.CarJourneyCam);
                command.Parameters.AddWithValue("@CarSpeedWarning", carFeature.CarSpeedWarning);
                command.Parameters.AddWithValue("@CarTireSensor", carFeature.CarTireSensor);
                command.Parameters.AddWithValue("@CarCollisionSensor", carFeature.CarCollisionSensor);
                command.Parameters.AddWithValue("@CarSunroof", carFeature.CarSunroof);
                command.Parameters.AddWithValue("@CarGPS", carFeature.CarGPS);
                command.Parameters.AddWithValue("@CarUSB", carFeature.CarUSB);
                command.Parameters.AddWithValue("@CarSubTire", carFeature.CarSubTire);
                command.Parameters.AddWithValue("@CarTrunkLid", carFeature.CarTrunkLid);
                command.Parameters.AddWithValue("@CarCam360", carFeature.CarCam360);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool UpdateCarFeature(CarFeature carFeature)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE CarFeature SET " +
                               "CarMap = @CarMap, CarBluetooth = @CarBluetooth, CarBackwardCam = @CarBackwardCam, " +
                               "CarCurbsideCam = @CarCurbsideCam, CarJourneyCam = @CarJourneyCam, " +
                               "CarSpeedWarning = @CarSpeedWarning, CarTireSensor = @CarTireSensor, " +
                               "CarCollisionSensor = @CarCollisionSensor, CarSunroof = @CarSunroof, CarGPS = @CarGPS, " +
                               "CarUSB = @CarUSB, CarSubTire = @CarSubTire, " +
                               "CarTrunkLid = @CarTrunkLid, CarCam360 = @CarCam360 WHERE @CarID = @CarID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carFeature.CarID);
                command.Parameters.AddWithValue("@CarMap", carFeature.CarMap);
                command.Parameters.AddWithValue("@CarBluetooth", carFeature.CarBluetooth);
                command.Parameters.AddWithValue("@CarBackwardCam", carFeature.CarBackwardCam);
                command.Parameters.AddWithValue("@CarCurbsideCam", carFeature.CarCurbsideCam);
                command.Parameters.AddWithValue("@CarJourneyCam", carFeature.CarJourneyCam);
                command.Parameters.AddWithValue("@CarSpeedWarning", carFeature.CarSpeedWarning);
                command.Parameters.AddWithValue("@CarTireSensor", carFeature.CarTireSensor);
                command.Parameters.AddWithValue("@CarCollisionSensor", carFeature.CarCollisionSensor);
                command.Parameters.AddWithValue("@CarSunroof", carFeature.CarSunroof);
                command.Parameters.AddWithValue("@CarGPS", carFeature.CarGPS);
                command.Parameters.AddWithValue("@CarUSB", carFeature.CarUSB);
                command.Parameters.AddWithValue("@CarSubTire", carFeature.CarSubTire);
                command.Parameters.AddWithValue("@CarTrunkLid", carFeature.CarTrunkLid);
                command.Parameters.AddWithValue("@CarCam360", carFeature.CarCam360);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public bool DeleteCarFeature(string carID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM CarFeature WHERE CarID = @CarID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarID", carID);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();
                return rowsAffected > 0;
            }
        }

        public DataTable LoadDataWithFilters(List<string> selectedFeatures, List<string> selectedFuelTypes)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Cars.CarID, Cars.CarTypeID, CarType.CarTypeName, Cars.FuelID, Fuel.FuelName, Cars.IsRent, Cars.CarPrice FROM Cars " +
                                   "JOIN CarType ON Cars.CarTypeID = CarType.CarTypeID " +
                                   "JOIN Fuel ON Cars.FuelID = Fuel.FuelID ";

                    // Add conditions for selected features
                    if (selectedFeatures.Count > 0)
                    {
                        query += "WHERE Cars.CarID IN (SELECT CarID FROM CarFeature WHERE ";

                        foreach (string featureName in selectedFeatures)
                        {
                            query += $"{featureName} = 1 AND ";
                        }

                        // Remove the trailing "AND"
                        query = query.TrimEnd(' ', 'A', 'N', 'D', ' ');

                        query += ") ";
                    }

                    // Add conditions for selected fuel types
                    if (selectedFuelTypes.Count > 0)
                    {
                        if (selectedFeatures.Count > 0)
                        {
                            query += "AND ";
                        }
                        else
                        {
                            query += "WHERE ";
                        }

                        // Use parameters for Fuel.FuelName
                        query += $"Fuel.FuelName IN ({string.Join(", ", selectedFuelTypes.Select((_, index) => $"@param{index}"))}) ";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters for Fuel.FuelName
                        for (int i = 0; i < selectedFuelTypes.Count; i++)
                        {
                            command.Parameters.AddWithValue($"@param{i}", selectedFuelTypes[i]);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions according to your application's requirements
                Console.WriteLine("Error in LoadDataWithFilters: " + ex.Message);
            }

            return dataTable;
        }


    }
}
