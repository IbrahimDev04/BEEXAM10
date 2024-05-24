using Microsoft.AspNetCore.Identity;

namespace BEExam10.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
