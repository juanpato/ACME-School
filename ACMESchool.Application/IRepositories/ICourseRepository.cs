using ACMESchool.Domain.Model;

namespace ACMESchool.Application.IRepositories
{
    public interface ICourseRepository
    {
        Course AddCourse(Course course);
        List<Course> GetAllCourses();
        Course GetCourseById(Guid courseId);
        Course GetCourseByName(string name);
        Course UpdateCourse(Course course);
    }
}
