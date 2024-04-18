using ACMESchool.Domain.Model;

namespace ACMESchool.Application.IRepositories
{
    public interface IStudentRepository
    {
        Student AddStudent(Student student);
        List<Student> GetAllStudents();
        Student GetStudentById(Guid studentId);
        Student GetStudentByName(string name);
    }
}
