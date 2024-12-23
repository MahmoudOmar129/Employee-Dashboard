using Demo.DAL.Entity;

namespace Demo.BL.Models
{
    public class CityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<District>? District { get; set; }
    }
}
