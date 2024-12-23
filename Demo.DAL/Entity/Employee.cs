using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    [Table("Employee")]
    public class Employee
    {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public double Salary { get; set; }

        [Required]
        public string Email { get; set; }

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

        // Navigation Property
        public Department? Department { get; set; }


        public int DistrictId { get; set; }

        // Navigation Property
        public District? District { get; set; }


        //public ICollection<District> DistrictList { get; set; }


    }





    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
