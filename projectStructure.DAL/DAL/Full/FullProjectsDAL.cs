using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.DAL.DAL
{
    public class FullProjectsDAL
    {
        public int Id { get; set; }
        public UserDAL Author { get; set; }
        public FullTeamDAL Team { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<FullTasksDAL> Tasks { get; set; }
    }
}
