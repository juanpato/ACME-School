using ACMESchool.Domain.Model;

namespace ACMESchool.Application.IService
{
    public interface IRegistrationService
    {
        Student RegisterStudent(string name, int age);
        Course RegisterCourse(string name, decimal registrationFee, DateTime startDate, DateTime endDate);

    }
}