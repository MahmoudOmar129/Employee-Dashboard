using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Database
{
    public partial class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> opt) : base(opt)
        {

        }
    }

    public partial class ApplicationContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }

    }
}
