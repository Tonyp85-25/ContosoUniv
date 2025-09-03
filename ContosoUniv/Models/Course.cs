using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniv.Models;
public readonly record struct CourseId(Guid Value);
public class Course
{
    public int Id { get; set; }
    
    public CourseId publicId{ get; private set; } = new(Guid.NewGuid());
    
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string Title { get; set; }
    
    [Required]
    [Range(0,100)]
    public int Credits { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
}