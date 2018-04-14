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
        //InventoryContext db = new InventoryContext();

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Index( HttpPostedFileBase file )
        {
            ViewBag.Message = Path.GetFileName(file.FileName);
            
            try
            {
                //UPLOAD FILE:
                //string fileName = file.FileName;//msdn doc says that this returns the name with the directory path, but I only got the file name. why?
                string fileName = Path.GetFileName(file.FileName); //in any case I will use the Path.GetFileName which supposed to return ONLY the file name
                string uploadDir = @"~\App_Data\uploads\";

                string uploadPath = Path.Combine(Server.MapPath(uploadDir), fileName);

                file.SaveAs(uploadPath);
                ViewBag.Message = $"The {fileName} file was successfully uploaded.";

                //TODO: enable manually associatiation of columns to properties and 

                //EXTRACT DATA FROM FILE:
                FileInfo filePath = new FileInfo(uploadPath);   
                List<Car> cars = Excel.ExtraxtCarData(filePath, isHeader: true); //Now it only works with Car Type //TODO: implement interactive isHeader

                //List<Car> dbCars = db.Cars.ToList();
                
                //RETURN TO VIEW: 
                return View("Index", cars);

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }

    }
}