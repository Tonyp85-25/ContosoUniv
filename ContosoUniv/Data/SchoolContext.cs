using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
    {
        
    }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("Course").Property(x=>x.publicId).HasConversion(id => id.Value,value=>new CourseId(value)).IsRequired();
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment").Property(x=>x.PublicId).HasConversion(id => id.Value,value=>new EnrollmentId(value)).IsRequired();
        modelBuilder.Entity<Student>().ToTable("Student").Property(x=>x.publicId).HasConversion(id => id.Value,value=>new StudentId(value)).IsRequired();
    }
}