using Microsoft.AspNetCore.Identity;

namespace Demo.DAL.Extend
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            CreateOn = DateTime.Now;
        }

        public bool IsAgree { get; set; }
        public DateTime CreateOn { get; set; }
        public string? CreateBy { get; set; }

    }
}
