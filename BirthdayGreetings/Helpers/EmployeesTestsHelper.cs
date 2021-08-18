using BirthdayGreetings.Models;
namespace BirthdayGreetings.Helpers
{
    public static class EmployeesTestsHelper
    {
        public static Employee John = new Employee("John", "Doe", new DateTime(1982, 10, 8), EmailAddress.Of("john.doe@foobar.com"));
        public static Employee Mary = new Employee("Mary", "Ann", new DateTime(1975, 9, 11), EmailAddress.Of("mary.ann@foobar.com"));
        public static List<Employee> TestEmployees = new List<Employee> { John, Mary };
    }
}
