using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class CarsBUS
    {
        private CarsDAL carsDAL;

        public CarsBUS()
        {
            this.carsDAL = new CarsDAL();
        }

        public bool InsertCar(Cars car)
        {
            return this.carsDAL.InsertCar(car);
        }

        public bool DeleteCar(Cars car)
        {
            return this.carsDAL.DeleteCar(car);
        }

        public bool DeleteCar(string carID)
        {
            return this.carsDAL.DeleteCar(carID);
        }

        public bool UpdateCar(Cars car)
        {
            return this.carsDAL.UpdateCar(car);
        }

        public List<Cars> GetAllCars()
        {
            return this.carsDAL.GetAllCars();
        }

        public Cars getCarByID(string carID)
        {
            return this.carsDAL.getCarByID(carID);
        }

        public CarType getCarTypeByID(int carTypeID)
        {
            return this.carsDAL.getCarTypeByID(carTypeID);
        }

        public DataTable loadData()
        {
            return this.carsDAL.loadData();
        }

        public Fuel getFuelByID(int fuelID)
        {
            return this.carsDAL.getFuelByID(fuelID);
        }

        public List<CarType> GetAllCarTypes()
        {
            return this.carsDAL.GetAllCarTypes();
        }

        public List<Fuel> GetAllFuels()
        {
            return this.carsDAL.GetAllFuels();
        }

        public List<CarFeature> GetAllCarFeatures()
        {
            return this.carsDAL.GetAllCarFeatures();
        }

        public CarFeature GetCarFeatureByCarID(string carID)
        {
            return this.carsDAL.GetCarFeatureByCarID(carID);
        }

        public bool InsertCarFeature(CarFeature carFeature)
        {
            return this.carsDAL.InsertCarFeature(carFeature);
        }

        public DataTable LoadDataWithFilters(List<string> selectedFeatures, List<string> selectedFuelTypes)
        {
            return this.carsDAL.LoadDataWithFilters(selectedFeatures, selectedFuelTypes);
        }

        public bool UpdateCarFeature(CarFeature carFeature)
        {
            return this.carsDAL.UpdateCarFeature(carFeature);
        }

        public bool DeleteCarFeature(string carID)
        {
            return this.carsDAL.DeleteCarFeature(carID);
        }
    }
}
