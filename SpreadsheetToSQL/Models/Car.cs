using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpreadsheetToSQL.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        [Display(Name = "Lot Number")]
        public int LotNumber { get; set; }

        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        public int Year { get; set; }

        public string Color { get; set; }
    }
}