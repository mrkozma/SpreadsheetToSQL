using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpreadsheetToSQL.Models
{
    public static class InventoryManager
    {
        public static void AddToDb(List<Car> listOfCars)
        {
            using (var db = new InventoryContext())
            {
                foreach (var carLineItem in listOfCars)
                {
                    var car = new Car
                    {
                        CarId = Guid.NewGuid(),
                        LotNumber = carLineItem.LotNumber,
                        Make = carLineItem.Make,
                        Model = carLineItem.Model,
                        Year = carLineItem.Year,
                        Color = carLineItem.Color
                    };

                    db.Cars.Add(car);
                }

                db.SaveChanges();
            }

        }

    }
}