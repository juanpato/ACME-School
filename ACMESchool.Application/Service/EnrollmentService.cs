using ACMESchool.Application.IRepositories;
using ACMESchool.Application.IService;
using ACMESchool.Domain.Model;

namespace ACMESchool.Application.Service
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public EnrollmentService(ICourseRepository courseRepository, IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public Course EnrollStudentInCourse(Guid studentId, Guid courseId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            var course = _courseRepository.GetCourseById(courseId);

            if (student == null)
            {
                throw new Exception("Student not found");
            }

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            if (course.EnrolledStudents.Any(s => s.Id == studentId))
            {
                throw new Exception("Student already registered");
            }

            course.EnrolledStudents.Add(student);
            var courseUpdated = _courseRepository.UpdateCourse(course);

            return courseUpdated;
        }

        public Dictionary<Student, List<Course>> GetEnrollmentsBetweenDates(DateTime startDate, DateTime endDate)
        {
            var students = _studentRepository.GetAllStudents();

            var enrollments = new Dictionary<Student, List<Course>>();

            foreach (var student in students)
            {
                var enrolledCourses = _courseRepository.GetAllCourses()
                    .Where(course => course.EnrolledStudents.Any(s => s.Id == student.Id) &&
                                     course.StartDate >= startDate &&
                                     course.EndDate <= endDate)
                    .ToList();

                enrollments.Add(student, enrolledCourses);
            }

            return enrollments;
        }
    }

}
