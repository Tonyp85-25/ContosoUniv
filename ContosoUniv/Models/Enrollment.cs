using System.ComponentModel.DataAnnotations;

namespace ContosoUniv.Models;

public enum Grade
{
    A,B,C,D,F
}

public readonly record struct EnrollmentId(Guid Value);
public class Enrollment
{
    public int Id { get; set; }
    
    public EnrollmentId publicId { get; private set; }
    
    public int CourseId { get; set; }
    
    public int StudentId { get; set; }
    public Grade? Grade { get; set; }
    
    public Course Course { get; set; }
    public Student Student { get; set; }
}