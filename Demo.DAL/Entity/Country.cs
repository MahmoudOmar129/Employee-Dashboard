using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    [Table("Country")]
    public class Country
    {

        //public Country()
        //{
        //    City = new HashSet<City>();
        //}


        public int Id { get; set; }
        public string Name { get; set; }

        //public ICollection<City>? City { get; set; }

    }
}
