// Needed for accessing database

using Microsoft.EntityFrameworkCore;


namespace UniversityRegistrar.Models 
{
  public class UniversityRegistrarContext : DbContext 
  {
    public DbSet<Course> Courses { get; set; }  
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public UniversityRegistrarContext(DbContextOptions options) : base(options) { } 
  }
}