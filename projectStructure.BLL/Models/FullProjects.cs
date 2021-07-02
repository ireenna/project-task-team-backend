using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.BLL.Models
{
    public class FullProjects
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public Team Team { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}
