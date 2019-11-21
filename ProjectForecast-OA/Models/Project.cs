using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Project
    {
        [Key]
        //public int Id { get; set; }
       
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public int CountryId { get; set; }
        //public virtual Country Country { get; set; }
        public string Type { get; set; }
        public int Consultant_Id { get; set; }
        //[ForeignKey("Consultant_Id")]
        //public virtual Consultant Consultant { get; set; }
        public int CustomerId { get; set; }
        //public virtual Customer Customer { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string CloseDate { get; set; }


    }
}