namespace ACMESchool.Domain.Model
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Student> EnrolledStudents { get; set; } = new List<Student>();
    }
}
