namespace BEExam10.Models
{
    public class Employees : BaseEntity
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedInUrl { get; set; } 
        public string ImageUrl { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
    }
}
