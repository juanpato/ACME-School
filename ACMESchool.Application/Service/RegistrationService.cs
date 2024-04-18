using ACMESchool.Application.IRepositories;
using ACMESchool.Application.IService;
using ACMESchool.Domain.Model;

namespace ACMESchool.Application.Service
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public RegistrationService(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public Student RegisterStudent(string name, int age)
        {
            if (age < 18)
            {
                throw new Exception("Only adults can register");
            }

            var student = new Student() { Id = Guid.NewGuid(), Name = name, Age = age };

            return _studentRepository.AddStudent(student);
        }

        public Course RegisterCourse(string name, decimal registrationFee, DateTime startDate, DateTime endDate)
        {
            var course = new Course()
            {
                Id = Guid.NewGuid(),
                Name = name,
                RegistrationFee = registrationFee,
                StartDate = startDate,
                EndDate = endDate
            };

            return _courseRepository.AddCourse(course);
        }
    }

}
