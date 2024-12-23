using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    [Table("City")]
    public class City
    {

        //public City()
        //{
        //    District = new HashSet<District>();
        //}


        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country? Country { get; set; }


        //public ICollection<District>? District { get; set; }

    }
}
