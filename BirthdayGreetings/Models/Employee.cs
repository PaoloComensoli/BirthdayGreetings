using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGreetings.Models
{
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
}
