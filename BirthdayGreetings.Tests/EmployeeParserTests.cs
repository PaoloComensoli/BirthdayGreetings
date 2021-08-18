using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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

    public static class EmployeesTestsHelper
    {
        public static Employee John = new Employee("John", "Doe", new DateTime(1982, 10, 8), EmailAddress.Of("john.doe@foobar.com"));
        public static Employee Mary = new Employee("Mary", "Ann", new DateTime(1975, 9, 11), EmailAddress.Of("mary.ann@foobar.com"));
        public static List<Employee> TestEmployees = new List<Employee> { John };
    }

    public class Employee
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime BirthDate { get; init; }
        public EmailAddress EmailAddress { get; init; }

        public Employee(string FirstName, string LastName, DateTime BirthDate, EmailAddress EmailAddress)
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

    public class EmailAddress
    {
        private const string EmailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$";
        public string value { get; init; }

        private EmailAddress(string value)
        {
            this.value = value;
        }

        public static EmailAddress Of(string employeeEmail)
        {
            if (!Regex.IsMatch(employeeEmail, EmailRegex))
            {
                throw new ArgumentException($"{employeeEmail} is not in email format");
            }
            return new EmailAddress(employeeEmail);
        }
        public override string ToString()
        {
            return $"{nameof(value)}: {value}";
        }
        protected bool Equals(EmailAddress other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EmailAddress)obj);
        }

        public static bool operator ==(EmailAddress emailAddress1, EmailAddress emailAddress2)
        {
            if (emailAddress1 is null)
            {
                if (emailAddress2 is null) return true;
                return false;
            }
            return emailAddress1.Equals(emailAddress2);
        }

        public static bool operator !=(EmailAddress emailAddress1, EmailAddress emailAddress2) => !(emailAddress1 == emailAddress2);

        public override int GetHashCode()
        {
            return (value != null ? value.GetHashCode() : 0);
        }
    }
}
