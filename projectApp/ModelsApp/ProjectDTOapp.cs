using System;

namespace projectStructureApp.ModelsDTOapp
{
    public class ProjectDTOapp
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"{Id}. Name: {Name}\nAuthor: {AuthorId}\n" +
                $"Descriprion: {Description}\nTeam: {TeamId}.\n" +
                $"Deadline: {Deadline}\nCreated at: {CreatedAt}\n";
        }
    }
}
