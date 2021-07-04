using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace projectStructure.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var teams = new List<Team>()
            {
                new Team{CreatedAt = DateTime.Now, Name="DreamTeam", Id = 1},
                new Team{CreatedAt = DateTime.Now, Name="SuperTeam", Id = 2},
                new Team{CreatedAt = DateTime.Now, Name="Hackers", Id = 3}
            };
            var users = new List<User>()
            {
                new User{Id=1, RegisteredAt = DateTime.Now, BirthDay = new DateTime(2002,5,18), Email = "somemail@gmail.com", FirstName="Irina", LastName="K", TeamId = 1},
                new User{Id=2, RegisteredAt = DateTime.Now, BirthDay = new DateTime(1975,2,2), Email = "lalala@gmail.com", FirstName="Leo", LastName="Di Caprio", TeamId = 1},
                new User{Id=3, RegisteredAt = DateTime.Now, BirthDay = new DateTime(1970,3,3), Email = "tesla@gmail.com", FirstName="Ilon", LastName="Musk", TeamId = 2},
                new User{Id=4,RegisteredAt = DateTime.Now, BirthDay = new DateTime(2000,1,1), Email = "superemail@gmail.com", FirstName="Super", LastName="User", TeamId = 2}
            };
            var projects = new List<Project>()
            {
                new Project{Id=1, AuthorId = 1, CreatedAt = DateTime.Now, Deadline = new DateTime(2021,07,07), Name = "Super cool project", Description="unbelievable project", TeamId = 1}
            };
            var tasks = new List<Tasks>()
            {
                new Tasks{Id=1, CreatedAt = DateTime.Now, Description = "to do something", Name = "First task", PerformerId = 1, ProjectId = 1, State = TaskState.ToDo},
                new Tasks{Id=2, CreatedAt = DateTime.Now, Description = "to hack", Name = "Secong task", PerformerId = 2, ProjectId = 1, State = TaskState.ToDo},
                new Tasks{Id=3, CreatedAt = DateTime.Now, Description = "to do refactoring", Name = "Third task", PerformerId = 1, ProjectId = 1, State = TaskState.ToDo},
                new Tasks{Id=4, CreatedAt = DateTime.Now, Description = "", Name = "Final task", PerformerId = 1, ProjectId = 1, State = TaskState.ToDo}
            };

            modelBuilder.Entity<Team>().HasData(teams);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<Tasks>().HasData(tasks);
            

        }
    }
}
