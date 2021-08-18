using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BirthdayGreetings.Helpers;
using BirthdayGreetings.Models;
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
}
