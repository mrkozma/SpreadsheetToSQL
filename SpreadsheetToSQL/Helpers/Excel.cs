using EPPlus.DataExtractor;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using SpreadsheetToSQL.Models;

namespace SpreadsheetToSQL.Helpers
{
    public static class Excel
    {

        public static List<Car> ExtractCarData(string uploadPath, bool isHeader)
        {
            FileInfo filePath = new FileInfo(uploadPath);

            //Implement check whether the table data is in the right format and order (LotNumber, Make, Model, Year)

            try
            {
                // Use EPPlus DataExtractor library https://github.com/ipvalverde/EPPlus.DataExtractor
                using (var excelPackage = new ExcelPackage(filePath))
                {
                    int fromRow = isHeader ? 2 : 1; //sets the start of the extraction depending on whether there is a header or not
                    int toRow = fromRow; //only extract one row at a time so we can check for end of data each time

                    List<Car> listOfCars = new List<Car>();
                    List<Car> carLineItem = new List<Car>();

                    while (true) 
                    {
                        carLineItem = excelPackage.Workbook.Worksheets["Sheet1"] //TODO:implement worksheet as variable
                            .Extract<Car>()
                            .WithProperty(c => c.LotNumber, "A")
                            .WithProperty(c => c.Make, "B")
                            .WithProperty(c => c.Model, "C")
                            .WithProperty(c => c.Year, "D")
                            .WithProperty(c => c.Color, "E")
                            .GetData(fromRow, toRow)
                            .ToList();

                        if (string.IsNullOrEmpty(carLineItem.LastOrDefault().Make)) //check for empty row if yes don't go further just return list
                        {
                            try
                            {
                                InventoryManager.AddToDb(listOfCars);
                                return listOfCars;
                            }
                            catch (Exception ex)
                            {

                                throw ex;
                            }
                        }

                        listOfCars.AddRange(carLineItem); //adding one line at a time

                        fromRow++;
                        toRow = fromRow;
                    } 
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}