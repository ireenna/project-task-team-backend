using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructureApp.ModelsDTOapp
{
    public class TasksDTOapp
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStateDTOapp State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public override string ToString()
        {
            return $"{Id}. Name: {Name}\nProjectId: {ProjectId}\n" +
                $"PerformerId: {PerformerId}\nDescription: {Description}.\n" +
                $"State: {State}\nCreated at: {CreatedAt}\nFinishedAt:{FinishedAt}\n";
        }
    }
}
