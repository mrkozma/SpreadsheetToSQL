using SpreadsheetToSQL.Models;
using SpreadsheetToSQL.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpreadsheetToSQL.Controllers
{
    public class HomeController : Controller
    {
        InventoryContext db = new InventoryContext();

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Index( HttpPostedFileBase file )
        {
            try
            {
                //UPLOAD FILE and GET THE PATH:
                string filePathOnServer = FileHandler.Upload(file);

                //TODO: enable manual associatiation of columns to properties and 
                //TODO: implement interactive isHeader

                //EXTRACT DATA FROM FILE and ADD TO DB:
                Excel.ExtractCarData(filePathOnServer, isHeader: true); 

                //GET LIST FROM DB:
                List<Car> dbCars = db.Cars.ToList();

                //SEND LIST TO VIEW: 
                ViewBag.Message = $"{Path.GetFileName(file.FileName)} file has been successfully processed.";
                return View("Index", dbCars);

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }

        }

    }
}