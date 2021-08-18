using System.Globalization;
using BirthdayGreetings.Models;

namespace BirthdayGreetings
{
    public class EmployeeParser
    {
        public static Employee ToEmployee(string employeeLine)
        {
            string[] employeeItems = employeeLine.Split(",").Select(s => s.Trim()).ToArray();
            var firstName = employeeItems[1];
            var lastName = employeeItems[0];
            var birthDate = ParseDatetimeExact(employeeItems[2]);
            var emailAddress = ParseEmailAddress(employeeItems[3]);
            return new Employee(firstName, lastName, birthDate, emailAddress);
        }

        private static EmailAddress ParseEmailAddress(string employeeItem)
        {
            try
            {
                return EmailAddress.Of(employeeItem);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static DateTime ParseDatetimeExact(string employeeItem)
        {
            try
            {
                return DateTime.ParseExact(employeeItem, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
