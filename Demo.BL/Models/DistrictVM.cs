using Demo.DAL.Entity;

namespace Demo.BL.Models
{
    public class DistrictVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
