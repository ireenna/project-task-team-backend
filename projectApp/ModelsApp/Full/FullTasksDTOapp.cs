using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructureApp.ModelsDTOapp
{
    public class FullTasksDTOapp
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public UserDTOapp Performer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStateDTOapp State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
