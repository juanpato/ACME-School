using ACMESchool.Application.IRepositories;
using ACMESchool.Domain.Model;

namespace ACMESchool.Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        private List<Student> students = new();

        public void SeedData()
        {
            students = new()
            {
                new() { Id = Guid.NewGuid(), Name = "John", Age = 20 },
                new() { Id = Guid.NewGuid(), Name = "Alice", Age = 22 },
                new() { Id = Guid.NewGuid(), Name = "Bob", Age = 21 }
            };
        }
        public Student AddStudent(Student student)
        {
            students.Add(student);
            return student;
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }

        public Student GetStudentByName(string name)
        {
            var result = students.Where(s => s.Name == name).FirstOrDefault();

            return result;
        }

        public Student GetStudentById(Guid studentId)
        {
            var studentSearched = students.FirstOrDefault(s => s.Id == studentId);

            return studentSearched;
        }
    }

}
