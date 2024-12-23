using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    [Table("District")]
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City? City { get; set; }
    }
}
