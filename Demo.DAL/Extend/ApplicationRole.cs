using Microsoft.AspNetCore.Identity;

namespace Demo.DAL.Extend
{
    public class ApplicationRole : IdentityRole
    {

        public ApplicationRole()
        {
            CreateOn = DateTime.Now;
        }
        public DateTime CreateOn { get; set; }
    }
}
