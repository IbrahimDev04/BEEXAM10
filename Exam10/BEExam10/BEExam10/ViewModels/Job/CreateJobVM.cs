using System.ComponentModel.DataAnnotations;

namespace BEExam10.ViewModels.Job
{
    public class CreateJobVM
    {
        [Required, MaxLength(32, ErrorMessage = "Lenght Error")]
        public string Name { get; set; }
    }
}
