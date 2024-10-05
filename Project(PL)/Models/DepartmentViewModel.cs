using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_PL_.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        public List<Employee>? Employees { get; set; }

    }
}
