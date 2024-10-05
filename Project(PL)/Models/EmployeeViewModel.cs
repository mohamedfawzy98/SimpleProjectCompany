using DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_PL_.Models
{
    public class EmployeeViewModel
    {
        public int ? Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public decimal Age { get; set; }
        public string? Address { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string phone { get; set; }
        [Required(ErrorMessage = "Salary is Required")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        public int? WorkForId { get; set; }
        public Department? WorkFor { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

    }
}
