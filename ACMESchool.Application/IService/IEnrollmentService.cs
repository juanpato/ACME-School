using ACMESchool.Domain.Model;

namespace ACMESchool.Application.IService
{
    public interface IEnrollmentService
    {
        Course EnrollStudentInCourse(Guid studentId, Guid courseId);
        Dictionary<Student, List<Course>> GetEnrollmentsBetweenDates(DateTime startDate, DateTime endDate);
    }
}