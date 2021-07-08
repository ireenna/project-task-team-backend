using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace projectStructure.Common.DTOapp.Update
{
    public class UserUpdateDTO
    {
        public int? TeamId { get; set; }
        private string firstName;
        private string lastName;
        private string email;
        private DateTime birthDay;
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
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
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
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
        [Required]
        [EmailAddress]
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

        [Required]
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
