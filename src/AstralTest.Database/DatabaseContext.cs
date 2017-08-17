using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AstralTest.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AstralTest.Database
{
    //Контекст базы данных
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> opt):base(opt)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TasksContainer> TasksContainers { get; set; }
        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ActionLog> Actions { get; set; }
        public DbSet<InfoAboutAction> InfoAboutActions { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(x => x.UserName)
                .IsUnique();
        }
    }
}
