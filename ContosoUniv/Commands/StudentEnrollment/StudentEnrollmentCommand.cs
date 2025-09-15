using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ContosoUniv.Models;
using Microsoft.Extensions.Primitives;

namespace ContosoUniv.Commands.StudentEnrollment;

public record StudentEnrollmentCommandRequest([Required]string StudentId, [Required]string[] CoursesIds)
{
 
}

public sealed record StudentEnrollmentCommand
{
    [Required]
    public StudentId StudentId { get; set; } 
    
    [Required]
    public required CourseId[] CourseIds { get; set; }

    public static StudentEnrollmentCommand FromRequest(StudentEnrollmentCommandRequest request)
    {
        var length = request.CoursesIds.Length;
        
        CourseId[] coursesIds = new CourseId[length];
        for (int i = 0; i < length; i++)
        {
            coursesIds[i] = new CourseId(Guid.Parse(request.CoursesIds[i]));
        }
        return new StudentEnrollmentCommand
        {
            CourseIds = coursesIds,
            StudentId = new StudentId(Guid.Parse(request.StudentId))
        };
    }
    
   
}