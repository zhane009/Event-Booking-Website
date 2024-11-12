using Microsoft.AspNetCore.Identity;

namespace soft20181_starter.Models
{
    public class User : IdentityUser
    {
        public string dateOfBirth { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string roleName { get; set;}
    }
}
