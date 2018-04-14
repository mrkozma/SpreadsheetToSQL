using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace SpreadsheetToSQL.Helpers
{
    public static class FileHandler
    {
        public static string Upload( HttpPostedFileBase file )
        {
            string uploadDir = @"~\App_Data\uploads\";
            string fileName = Path.GetFileName(file.FileName); //Path.GetFileName which returns ONLY the file name

            string uploadPath = Path.Combine(HostingEnvironment.MapPath(uploadDir), fileName);

            file.SaveAs(uploadPath);

            return uploadPath;
        }
    }
}

