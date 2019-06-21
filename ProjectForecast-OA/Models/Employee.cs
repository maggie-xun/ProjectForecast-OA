using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectForecast_OA.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int CostRate { get; set; }

        public Employee(int id,string name,string type, int costRate)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.CostRate = costRate;
        }
    }
}