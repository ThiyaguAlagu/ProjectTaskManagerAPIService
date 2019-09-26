using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BusinessEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataAccessLayer
{
    public class ProjectManagerContext : DbContext
    {
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> options) : base(options)
        {
        }

        public virtual DbSet<ParentTask> ParentTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder != null)
            {
                modelBuilder.Entity<ParentTask>(entity =>
                {
                    entity.HasKey(e => e.ParentId);
                    entity.ToTable("ParentTask");
                    
                });

                modelBuilder.Entity<Task>(entity =>
                {
                    entity.HasKey(e => e.TaskId);
                    entity.ToTable("Task");

                });
                modelBuilder.Entity<Task>().HasOne(t => t.Parent);
                modelBuilder.Entity<Task>().HasOne(t => t.User).WithOne().HasForeignKey<Task>(t => t.UserId);
                modelBuilder.Entity<Task>().HasOne(t => t.Project).WithOne().HasForeignKey<Task>(t => t.ProjectId); ;

                modelBuilder.Entity<Project>(entity =>
                {
                    entity.HasKey(e => e.ProjectId);
                    entity.ToTable("Project");

                });
                modelBuilder.Entity<Project>().HasOne(t => t.ProjectManager).WithOne().HasForeignKey<Project>(p => p.ManagerId);

                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(e => e.UserId);
                    entity.ToTable("User");

                });
                modelBuilder.Entity<User>().HasOne(t => t.UserProject).WithOne().HasForeignKey<User>(u=>u.ProjectId);
                modelBuilder.Entity<User>().HasOne(t => t.UserTask).WithOne().HasForeignKey<User>(u=>u.TaskId);
            }
        }
    }
}

