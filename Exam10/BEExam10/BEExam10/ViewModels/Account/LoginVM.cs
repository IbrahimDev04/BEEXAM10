using System.ComponentModel.DataAnnotations;

namespace BEExam10.ViewModels.Account
{
    public class LoginVM
    {

        [Required, MinLength(3, ErrorMessage = "Lenght Error"), MaxLength(12, ErrorMessage = "Lenght Error")]
        public string UsernameOrEmail { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
