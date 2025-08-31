using System.ComponentModel.DataAnnotations;

namespace ContosoUniv.Models;

public class Student
{
    public int Id { get; set; }
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string FirstMidName { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }
    
    // initial values compulsory to avoid modelState error if not provided
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}