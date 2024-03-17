using DB.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class ApplicationDbContext : DbContext
    {
         public ApplicationDbContext(DbContextOptions options) : base(options)
 {
 }
        
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ProjectTask> Tasks { get; set; }
        public virtual DbSet<TaskCategory> TaskCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<UserTaskLog> UserTaskLogs { get; set; }

        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
    {

        var connectionString = "server=(localdb)\\MSSQLLocalDB;database=ProjectManagement;Trusted_Connection=true";
        object p = optionsBuilder.UseSqlServer(connectionString);
    }

    }
    }
}
