namespace ACMESchool.Domain.Model
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> EnrolledCourses { get; set; } = new List<Course>();
    }
}
