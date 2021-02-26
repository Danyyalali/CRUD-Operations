using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web;

namespace CRUD_OPERATION
{
    public class ManagementContext : DbContext
    {
        public DbSet<Student> student { get; set; }
        public DbSet<StudentAddress> address { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=Management;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
            .Property(a => a.TeacherId)
            .HasDefaultValue(0);


            modelBuilder.Entity<StuCourse>().HasKey(cs => new { cs.StudentId, cs.CourseId });
            //modelBuilder.Entity<User>().Property(user => user.Role).HasDefaultValue("Player");
        }
    }
}