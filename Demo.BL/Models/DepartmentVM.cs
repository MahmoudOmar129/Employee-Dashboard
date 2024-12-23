using Demo.DAL.Entity;
using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "name required")]
        [MinLength(3, ErrorMessage = "min len 3 characters")]
        [MaxLength(50, ErrorMessage = "max len 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code required")]
        [Range(1, 5000, ErrorMessage = "Code must between 1 to 5000")]
        public string Code { get; set; }
        public ICollection<Employee>? Employee { get; set; }

    }
}
