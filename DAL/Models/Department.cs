using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Department : BasicClass
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
