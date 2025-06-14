using Microsoft.AspNetCore.Identity;

namespace Project01.AppData
{
    public class AppUser: IdentityUser
    {
        public String ? FirstName {  get; set; }
        public String? LastName { get; set; }
        public String? Address {  get; set; }
    }
}
