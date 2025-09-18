using ContosoUniv.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniv.ViewModels;

public sealed record EnrollmentViewModel
{
    public EnrollmentViewModel(StudentId studentId, List<Course> courses, List<Enrollment> enrollments)
    {
        StudentId = studentId;
        Courses = courses;
        Enrollments = enrollments;
        Courses.ForEach((c) =>
        {
            var enrollment =enrollments.FirstOrDefault(e => e.CourseId == c.Id);
            if (enrollment != null) return;
            var item = new SelectListItem
            {
               
                Value = c.publicId.Value.ToString(),
                Text = c.Title,
              
            };
          
           
            CoursesItems.Add(item);
        });
    }

    public StudentId StudentId { get; }
    
    public IEnumerable<string> CoursesIds { get; set; }

    private List<Course> Courses { get; }

    public List<SelectListItem> CoursesItems { get; } = new List<SelectListItem>();
    
    public List<Enrollment> Enrollments { get; }
    
}