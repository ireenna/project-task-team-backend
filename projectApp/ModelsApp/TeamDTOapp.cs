using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructureApp.ModelsDTOapp
{
    public class TeamDTOapp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public override string ToString()
        {
            return $"{Id}. Name: {Name}\nCreatedAt: {CreatedAt}\n";
        }
    }
}
