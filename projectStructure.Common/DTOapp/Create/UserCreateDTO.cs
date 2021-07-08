using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace projectStructure.Common.DTOapp.Create
{
    public class UserCreateDTO
    {
        public int? TeamId { get; set; }
        private string firstName;
        private string lastName;
        private string email;
        private DateTime birthDay;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (value.Length < 2 | value.Length > 20)
                    throw new ArgumentException();
                firstName = value;
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (value.Length < 2 | value.Length > 20)
                    throw new ArgumentException();
                lastName = value;
            }
        }
        public string Email
        {
            get => email;
            set
            {
                if (value is null || !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
                    throw new ArgumentException();
                email = value;
            }
        }
        public DateTime BirthDay
        {
            get => birthDay;
            set
            {
                if (value.Date > DateTime.Now)
                    throw new ArgumentException();
                birthDay = value;
            }
        }
    }
}
