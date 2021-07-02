using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructureApp.ModelsDTOapp.Create
{
    public class ProjectCreateApp
    {
        //id CreatedAt tasks
        public int AuthorId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}
