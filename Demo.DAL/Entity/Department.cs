using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    [Table("Department")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(50)]
        public string Code { get; set; }

        public ICollection<Employee> Employee { get; set; }

    }
}
