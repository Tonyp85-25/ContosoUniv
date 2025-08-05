namespace ContosoUniv.Models;

public class Student
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    
    // initial values compulsory to avoid modelState error if not provided
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}