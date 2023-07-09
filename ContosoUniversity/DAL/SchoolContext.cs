using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// To use   DbContext   for inhiritance
using System.Data.Entity;

// To use Base model Classes
using ContosoUniversity.Models;

// To use (PluralizingTableNameConvention)    inside     OnModelCreating
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    // Represent code based Database 
    public class SchoolContext : DbContext
    {
        // Connection String name     inhiritance from  Dbcontext     
        public SchoolContext() : base("SchoolContext")
        {
        }


        // Represent code based Tables     properties (Dbset) type    it name is plural
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }




        // To Stop plural  in the naming of the database    to be   (Singular)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors).WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID")
                    .MapRightKey("InstructorID")
                    .ToTable("CourseInstructor"));
        }

    }
}