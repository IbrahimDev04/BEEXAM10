using System.ComponentModel.DataAnnotations;

namespace BEExam10.ViewModels.Account
{
    public class LoginVM
    {
        public string UsernameOrEmail { get; set; }


        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
