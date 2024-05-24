namespace BEExam10.Models
{
    public class Job : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Employees> Employees { get; set; }
    }
}
