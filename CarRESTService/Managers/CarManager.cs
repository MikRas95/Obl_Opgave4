using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Obl_Opgave1;

namespace CarRESTService.Managers
{
    public class CarManager
    {
        private static int _nextId = 1;
        private static readonly List<Car> Data = new List<Car>()
        {
            new Car{Id = _nextId++, Model = "BMW A8", Price = 100, LicensePlate = "WLT 740"},
            new Car{Id = _nextId++, Model = "Audi B10", Price = 200,  LicensePlate = "DAF 320"},
            new Car{Id = _nextId++, Model = "Toyota F34", Price = 300,  LicensePlate = "FDG 281"},
            new Car{Id = _nextId++, Model = "Ford A18", Price = 50,  LicensePlate = "UYG 484"}
        };
        /// <summary>
        /// Returns list of cars if maximumPrice is 0, otherwise it returns a filtered list of cars
        /// </summary>
        /// <param name="maximumPrice"></param>
        /// <returns></returns>
        public List<Car> GetAllCars(int maximumPrice = 0)
        {
            List<Car> result = new List<Car>(Data);
            if (maximumPrice != 0)
            {
                result = Data.FindAll(c => c.Price <= maximumPrice);
            }
            return result;
        }
        /// <summary>
        /// Gets a single car from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Car GetCarsById(int id)
        {
            return Data.Find(c => c.Id == id);
        }
        /// <summary>
        /// Creates a new car object and adds it to list of cars
        /// </summary>
        /// <param name="newCar"></param>
        /// <returns></returns>
        public Car AddCar(Car newCar)
        {
            newCar.Id = _nextId++;
            Data.Add(newCar);
            return newCar;
        }
        /// <summary>
        /// Removes a car from list & returns the removed car
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Car DeleteCar(int id)
        {
            Car car = GetCarsById(id);
            if (car == null) return null;
            Data.Remove(car);
            return car;
        }
    }
}
