using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common;
using projectStructure.Common.DTO;

namespace projectStructureApp.ModelsDTOapp
{
    public class ProjectDTOapp
    {
        public int Id { get; set; }
        public UserDTOapp Author { get; set; }
        public TeamDTOapp Team { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<TasksDTOapp> Tasks { get; set; }
        public override string ToString()
        {
            string strTasks = null;
            Tasks.ForEach(t => strTasks += $"{t.Id}. {t.Name} Performer: {t.Performer.FirstName} {t.Performer.LastName}\n");
            return $"{Id}. Name: {Name}\nAuthor: {Author.FirstName} {Author.LastName}\n" +
                $"Descriprion: {Description}\nTeam: {Team.Name}.\n" +
                $"Deadline: {Deadline}\nTasks:\n{strTasks ?? "No tasks."}\n";
        }
    }
}
