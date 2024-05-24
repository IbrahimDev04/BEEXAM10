using System.ComponentModel.DataAnnotations;

namespace BEExam10.ViewModels.Job
{
    public class UpdateJobVM
    {
        [Required, MaxLength(32, ErrorMessage = "Lenght Error")]
        public string Name { get; set; }
    }
}
