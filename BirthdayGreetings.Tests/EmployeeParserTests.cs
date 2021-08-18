using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentAssertions;

using Xunit;

namespace BirthdayGreetings.Tests
{
    public class EmployeeParserTests
    {
        [Fact]
        public void Can_parse_an_employee()
        {
            string employeeLine = "Doe, John, 1982/10/08, john.doe@foobar.com";
            Employee actualEmployee = EmployeeParser.ToEmployee(employeeLine);
            var pluto = EmployeesTestsHelper.John;
            actualEmployee.Should().Be(EmployeesTestsHelper.John);
        }
    }

    public class EmployeeParser { 
        public static Employee ToEmployee(string employeeLine)
        {
            string[] employeeItems = employeeLine.Split(",").Select(s => s.Trim()).ToArray();
            var firstName = employeeItems[1];
            var lastName = employeeItems[0];
            var birthDate = employeeItems[2];
            var emailAddress = employeeItems[3];
            return new Employee(firstName, lastName, birthDate, emailAddress);
        }
    }

    public static class EmployeesTestsHelper
    {
        public static Employee John = new Employee("John", "Doe", "1982/10/08", "john.doe@foobar.com");
        public static List<Employee> TestEmployees = new List<Employee> { John };
    }

    public class Employee
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string BirthDate { get; init; }
        public string EmailAddress { get; init; }

        public Employee(string FirstName, string LastName, string BirthDate, string EmailAddress)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.BirthDate = BirthDate;
            this.EmailAddress = EmailAddress;
        }

        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, " +
                $"{nameof(LastName)}: {LastName}, " +
                $"{nameof(BirthDate)}: {BirthDate}, " +
                $"{nameof(EmailAddress)}: {EmailAddress}";
        }

        protected bool Equals(Employee other)
        {
            return FirstName == other.FirstName && 
                LastName == other.LastName && 
                BirthDate == other.BirthDate &&    
                EmailAddress == other.EmailAddress;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Employee)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, BirthDate, EmailAddress);
        }
    }
}
