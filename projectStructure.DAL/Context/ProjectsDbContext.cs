using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectStructure.DAL.Context;

namespace projectStructure.DAL
{
    public class ProjectsDbContext: DbContext
    {
        public DbSet<Project> Projects { get; private set; }
        public DbSet<Tasks> Tasks { get; private set; }
        public DbSet<User> Users { get; private set; }
        public DbSet<Team> Teams { get; private set; }

        public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Project>()
            //    .HasMany(p => p.Tasks)
            //    .WithOne(t => t.Project);

            //modelBuilder.Entity<Project>()
            //    .HasOne(p => p.Team)
            //    .WithMany(t => t.Projects);

            //modelBuilder.Entity<Project>()
            //    .HasOne(p => p.Author)
            //    .WithMany(a => a.Projects);

            //modelBuilder.Entity<Tasks>()
            //    .HasOne(t => t.Performer)
            //    .WithMany(p => p.Tasks);

            //modelBuilder.Entity<Team>()
            //    .HasMany(t => t.Participants)
            //    .WithOne(p => p.Team);

            //modelBuilder.Entity<Team>()
            //    .HasMany(t => t.Participants)
            //    .WithOne(p => p.Team);

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }
    }
}
