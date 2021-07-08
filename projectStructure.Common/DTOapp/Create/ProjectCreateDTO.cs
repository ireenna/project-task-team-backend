using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.Common.DTOapp
{
    public class ProjectCreateDTO
    {
        [Required]
        public int AuthorId { get; set; }
        public int TeamId { get; set; }
        
        private string name;
        public string Description { get; set; }
        private DateTime deadline;
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length < 2 | value.Length > 20)
                    throw new ArgumentException();
                name = value;
            }
        }
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                if(value.Date < DateTime.Now)
                    throw new ArgumentException();
                deadline = value;
            }
        }
    }
}
