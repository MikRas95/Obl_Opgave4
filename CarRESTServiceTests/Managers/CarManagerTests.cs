using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRESTService.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obl_Opgave1;

namespace CarRESTService.Managers.Tests
{
    [TestClass()]
    public class CarManagerTests
    {
        private CarManager _carManager;
        [TestInitialize]
        public void Inti()
        {
            _carManager = new CarManager();
        }
        [TestMethod()]
        public void GetAllCarsTest()
        {
            //Checking that there is 4 objects in the collection
            Assert.IsTrue(_carManager.GetAllCars().Count == 4);
            // Testing maximumPrice filter
            Assert.IsNotNull(_carManager.GetAllCars(250));
            //Testing that maximumPrice filter includes objects where, the objects and the filter maximumPrice are equal
            Assert.IsTrue(_carManager.GetAllCars(200).Count == 3);
            Assert.IsTrue(_carManager.GetAllCars(199).Count == 2);

            Assert.IsTrue(_carManager.GetAllCars(10).Count == 0);
            Assert.IsTrue(_carManager.GetAllCars(0).Count == 4);
        }

        [TestMethod()]
        public void GetCarsByIdTest()
        {
            Assert.IsNotNull(_carManager.GetCarsById(1));
            Assert.IsNull(_carManager.GetCarsById(0));
            string _expectedName = "BMW A8";
            Assert.AreEqual(_expectedName, _carManager.GetCarsById(1).Model);
            int _expectedPrice;
            Assert.AreEqual(_expectedPrice = 100, _carManager.GetCarsById(1).Price);

            //Id should not exist
            Assert.IsNull(_carManager.GetCarsById(589));
        }

        //Testing Add and Delete in one Method,
        //so that the new test object is cleaned out (removed)
        //If there are other tests, that could be affected if we don't remove it
        [TestMethod]
        public void TestAddAndDelete()
        {
            //Count before new object is added
            int beforeAddCount = _carManager.GetAllCars().Count;
            //Getting an Id to compare with the new id, that should override this id when object it created
            int defaultId = 0;
            //Creates an test object
            Car newCar = new Car(defaultId, "Ford X", 10000, "ABC123");
            //Adding to list
            Car addedCar = _carManager.AddCar(newCar);
            //getting theAdded objects id
            int newId = addedCar.Id;
            //Comparing Ids to see if the object was giving a new id
            Assert.AreNotEqual(defaultId, newId);
            //Checking if the list is now bigger
            Assert.AreEqual(beforeAddCount + 1, _carManager.GetAllCars().Count);

            //Delete - using the new object we just created
            Car carToBeDeleted = _carManager.DeleteCar(newId);
            //Checking that count is now back to before we added an object
            Assert.AreEqual(beforeAddCount, _carManager.GetAllCars().Count);
            //Checking the deleting an object that does not exist return null
            Assert.IsNull(_carManager.DeleteCar(349));

        }
    }
}