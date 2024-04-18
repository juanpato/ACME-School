namespace SchoolManagementTests
{
    using ACMESchool.Application.IRepositories;
    using ACMESchool.Application.IService;
    using ACMESchool.Application.Service;
    using ACMESchool.Domain.Model;
    using ACMESchool.Infrastructure;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class SchoolManagementTests
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IRegistrationService _registrationService;
        private readonly IEnrollmentService _enrollmentService;

        public SchoolManagementTests()
        {
            _studentRepository = new StudentRepository();
            _courseRepository = new CourseRepository();
            _registrationService = new RegistrationService(_studentRepository, _courseRepository);
            _enrollmentService = new EnrollmentService(_courseRepository, _studentRepository);
        }

        [Fact]
        public void RegisterStudent_Success()
        {
            // Arrange
            var name = "Test Student";
            var age = 25;

            // Act
            var registeredStudent = _registrationService.RegisterStudent(name, age);

            // Assert
            Assert.NotNull(registeredStudent);
            Assert.Equal(name, registeredStudent.Name);
            Assert.Equal(age, registeredStudent.Age);

            var retrievedStudent = _studentRepository.GetStudentById(registeredStudent.Id);
            Assert.NotNull(retrievedStudent);
            Assert.Equal(name, retrievedStudent.Name);
            Assert.Equal(age, retrievedStudent.Age);
        }


        [Fact]
        public void RegisterStudent_Fail_Underage()
        {
            // Arrange
            var name = "Underage Student";
            var age = 17;

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _registrationService.RegisterStudent(name, age));
            Assert.Equal("Only adults can register", exception.Message);
        }


        [Fact]
        public void RegisterCourse_Success()
        {
            // Arrange
            var name = "Mathematics";
            var registrationFee = 100.0m;
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(30);

            // Act
            var registeredCourse = _registrationService.RegisterCourse(name, registrationFee, startDate, endDate);

            // Assert
            Assert.NotNull(registeredCourse);
            Assert.Equal(name, registeredCourse.Name);
            Assert.Equal(registrationFee, registeredCourse.RegistrationFee);
            Assert.Equal(startDate, registeredCourse.StartDate);
            Assert.Equal(endDate, registeredCourse.EndDate);
        }


        [Fact]
        public void EnrollStudentInCourse_Success()
        {
            // Arrange
            SeedData();
            var studentId = _studentRepository.GetAllStudents().First().Id;
            var courseId = _courseRepository.GetAllCourses().First().Id;

            // Act
            var updatedCourse = _enrollmentService.EnrollStudentInCourse(studentId, courseId);

            // Assert
            Assert.NotNull(updatedCourse);

            var course = _courseRepository.GetCourseById(courseId);
            Assert.Contains(studentId, course.EnrolledStudents.Select(s => s.Id));

        }

        [Fact]
        public void GetEnrollmentsBetweenDates_Success()
        {
            // Arrange
            SeedData();
            var startDate = DateTime.Now.AddDays(-7);
            var endDate = DateTime.Now.AddDays(7);

            // Act
            var enrollments = _enrollmentService.GetEnrollmentsBetweenDates(startDate, endDate);

            // Assert
            Assert.NotNull(enrollments);
        }

        [Fact]
        public void GetCourseByName_ExistingCourse()
        {
            // Arrange
            var courseName = "Mathematics";
            SeedData();

            // Act
            var course = _courseRepository.GetCourseByName(courseName);

            // Assert
            Assert.NotNull(course);
            Assert.Equal(courseName, course.Name);
        }

        [Fact]
        public void GetCourseByName_NonExistingCourse()
        {
            // Arrange
            var courseName = "NonExistingCourse";

            // Act
            var course = _courseRepository.GetCourseByName(courseName);

            // Assert
            Assert.Null(course);
        }

        [Fact]
        public void UpdateCourse_Success()
        {
            // Arrange
            SeedData();
            var course = _courseRepository.GetAllCourses().First();
            var updatedName = "UpdatedName";

            // Act
            course.Name = updatedName;
            var updatedCourse = _courseRepository.UpdateCourse(course);

            // Assert
            Assert.NotNull(updatedCourse);
            Assert.Equal(updatedName, updatedCourse.Name);
        }

        [Fact]
        public void UpdateCourse_NonExistingCourse()
        {
            // Arrange
            var nonExistingCourse = new Course { Id = Guid.NewGuid(), Name = "NonExistingCourse", RegistrationFee = 0, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) };

            // Act & Assert
            Assert.Throws<Exception>(() => _courseRepository.UpdateCourse(nonExistingCourse));
        }


        private void SeedData()
        {
            _registrationService.RegisterStudent("Test Student", 25);
            _registrationService.RegisterStudent("Alice Smith", 22);

            _registrationService.RegisterCourse("Mathematics", 100.0m, DateTime.Now, DateTime.Now.AddDays(30));
            _registrationService.RegisterCourse("Physics", 120.0m, DateTime.Now, DateTime.Now.AddDays(30));
            if (_studentRepository.GetAllStudents().Count == 0)
            {
                var students = new List<Student>
                {
                    new() { Id = Guid.NewGuid(), Name = "Test Student", Age = 25 },
                    new() { Id = Guid.NewGuid(), Name = "Alice Smith", Age = 22 }
                };
                foreach (var student in students)
                {
                    _studentRepository.AddStudent(student);
                }
            }

            if (_courseRepository.GetAllCourses().Count == 0)
            {
                var courses = new List<Course>
                {
                    new() { Id = Guid.NewGuid(), Name = "Mathematics", RegistrationFee = 100.0m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) },
                    new() { Id = Guid.NewGuid(), Name = "Physics", RegistrationFee = 120.0m, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30) }
                };
                foreach (var course in courses)
                {
                    _courseRepository.AddCourse(course);
                }
            }
        }
    }

}