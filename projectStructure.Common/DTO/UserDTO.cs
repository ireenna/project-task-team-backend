using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.Common.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int? TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
