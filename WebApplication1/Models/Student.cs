namespace WebApplication1.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string phone_number { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
    }
}
