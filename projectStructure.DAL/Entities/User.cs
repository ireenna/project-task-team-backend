using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.DAL
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? TeamId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(1)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(1)]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime RegisteredAt { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
    }
}
