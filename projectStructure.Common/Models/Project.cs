using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common;
using projectStructure.Common.DTO;

namespace projectStructure.Common.Models
{
    public class Project
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public Team Team { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Tasks> Tasks { get; set; }
        
        public Project(ProjectDTO p, IEnumerable<Tasks> ts, User u, Team t)
        {
            Id = p.Id;
            Author = u;
            Team = t;
            Name = p.Name;
            Description = p.Description;
            Deadline = p.Deadline;
            CreatedAt = p.CreatedAt;
            Tasks = ts.ToList();
        }
    }
}
