using Demo.DAL.Entity;
using System.ComponentModel.DataAnnotations;

namespace Demo.BL.Models
{
    public class EmployeeVM
    {

        public EmployeeVM()
        {
            CreatedOn = DateTime.Now;
            IsDeleted = false;
            IsActive = true;
        }


        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MaxLength(50, ErrorMessage = "Max Len 50")]
        [MinLength(3, ErrorMessage = "Min Len 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Salary Required")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Enter valid mail")]
        public string Email { get; set; }

        [Range(20, 65, ErrorMessage = "Age Out Of Range")]
        public int? Age { get; set; }

        public string? Notes { get; set; }

        public DateTime? HireDate { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsUpdated { get; set; }

        public bool IsActive { get; set; }

        public int DepartmentId { get; set; }

        public int? DptId { get; set; }

        public string? DepartmentName { get; set; }

        public string? DepartmentCode { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }
        public ICollection<District> DistrictList { get; set; }

    }
}
