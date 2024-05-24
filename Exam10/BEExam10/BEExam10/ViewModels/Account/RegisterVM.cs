using System.ComponentModel.DataAnnotations;

namespace BEExam10.ViewModels.Account
{
    public class RegisterVM
    {
        [Required, MinLength(3, ErrorMessage = "Lenght Error"), MaxLength(12, ErrorMessage = "Lenght Error")]
        public string Name { get; set; }

        [Required, MinLength(3, ErrorMessage = "Lenght Error"), MaxLength(12, ErrorMessage = "Lenght Error")]
        public string Surname { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MinLength(3, ErrorMessage = "Lenght Error"), MaxLength(12, ErrorMessage = "Lenght Error")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password")]
        public string RepidePassword { get; set; }

    }
}
