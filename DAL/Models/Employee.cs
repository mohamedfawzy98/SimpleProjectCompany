using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee : BasicClass
    {
        public string Name { get; set; }
        public decimal Age { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        public int? WorkForId { get; set; }
        public Department? WorkFor { get; set; }

        public string? ImageName { get; set; }
    }
}
