using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BirthdayGreetings.Models
{
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
