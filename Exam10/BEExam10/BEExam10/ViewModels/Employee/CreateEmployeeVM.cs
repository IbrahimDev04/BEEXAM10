using System.ComponentModel.DataAnnotations;

namespace BEExam10.ViewModels.Employee
{
    public class CreateEmployeeVM
    {
        [Required,MaxLength(32 , ErrorMessage = "Lenght Error")]
        public string FullName { get; set; }

        [Required, MaxLength(600, ErrorMessage = "Lenght Error")]
        public string Description { get; set; }

        [Required]
        public IFormFile ImageFile { get; set; }

        [Required]
        public string TwitterUrl { get; set; }

        [Required]
        public string FacebookUrl { get; set; }

        [Required]
        public string InstagramUrl { get; set; }

        [Required]
        public string LinkedInUrl { get; set; }

        [Required]
        public int JobId { get; set; }
    }
}
