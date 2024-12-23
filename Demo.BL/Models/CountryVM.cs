using Demo.DAL.Entity;

namespace Demo.BL.Models
{
    public class CountryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<City>? City { get; set; }
    }
}
