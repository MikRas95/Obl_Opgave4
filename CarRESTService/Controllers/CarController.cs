using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Obl_Opgave1;
using CarRESTService.Managers;

namespace CarRESTService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly CarManager _carManager = new CarManager();
        /// <summary>
        /// Finds all cars - can filter by maximumPrice
        /// </summary>
        /// <param name="maximumPrice"></param>
        /// <returns></returns>
        /// <response code="200">Everything was OK</response>
        /// <response code="404">List is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<List<Car>> Get([FromQuery] int maximumPrice)
        {
            List<Car> cars = _carManager.GetAllCars(maximumPrice);
            if (!cars.Any()) return NotFound("No cars found");

            return Ok(cars);
        }

        /// <summary>
        /// Finds a car by an Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Everything was OK</response>
        /// <response code="404">Error: List is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Car> GetCarsById(int id)
        {
            Car car = _carManager.GetCarsById(id);
            if (car == null) return NotFound("No such car found, id: " + id);
            return Ok(car);
        }

        /// <summary>
        /// Creates a new car object
        /// </summary>
        /// <param name="newCar"></param>
        /// <returns></returns>
        /// <response code="201">Item was created</response>
        /// <response code="400">Error 400: Are you missing a parameter?</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car newCar)
        {
            Car car = new Car();
            if (newCar.Model == null || newCar.Price <= 0 || newCar.LicensePlate == null)
            {
                return BadRequest(newCar);
            }

            car = _carManager.AddCar(newCar);
            return Created("api/car/" + car.Id, car);
        }

        /// <summary>
        /// Deletes an item found by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Everything was OK</response>
        /// <response code="404">Error: List is not found</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            Car car = _carManager.GetCarsById(id);
            if (car == null) return NotFound("Car not found, id: " + id);
            return Ok(_carManager.DeleteCar(id));
        }
    }
}
