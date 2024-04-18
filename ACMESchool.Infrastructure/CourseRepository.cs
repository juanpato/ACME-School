using ACMESchool.Application.IRepositories;
using ACMESchool.Domain.Model;

namespace ACMESchool.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        public List<Course> courses = new();

        public void SeedData()
        {
            courses = new(){
                new()
                {
                    Name = "Mathematics",
                    RegistrationFee = 100,
                    StartDate = new DateTime(2024, 4, 1),
                    EndDate = new DateTime(2024, 6, 30),
                    EnrolledStudents = new List<Student>
                    {
                        new() { Name = "John", Age = 20 },
                        new() { Name = "Alice", Age = 22 },
                        new() { Name = "Bob", Age = 21 }
                    }
                },
                new()
                {
                    Name = "Physics",
                    RegistrationFee = 120,
                    StartDate = new DateTime(2024, 5, 1),
                    EndDate = new DateTime(2024, 7, 31),
                    EnrolledStudents = new List<Student>
                    {
                        new() { Name = "Sarah", Age = 19 },
                        new() { Name = "David", Age = 23 }
                    }
                }
            };
        }

        public Course AddCourse(Course course)
        {
            courses.Add(course);
            return course;
        }

        public List<Course> GetAllCourses()
        {
            return courses;
        }

        public Course GetCourseById(Guid courseId)
        {
            var result = courses.Where(c => c.Id == courseId).FirstOrDefault();

            return result;
        }

        public Course GetCourseByName(string name)
        {
            var result = courses.Where(c => c.Name == name).FirstOrDefault();

            return result;
        }

        public Course UpdateCourse(Course course)
        {
            var courseSearched = courses.Where(course => course.Id == course.Id).FirstOrDefault();
            if (courseSearched == null)
            {
                throw new Exception("Course not found");
            }
            courseSearched = course;

            return courseSearched;
        }
    }
}
