using ContosoUniv.Data;
using ContosoUniv.Lib;
using ContosoUniv.Models;


namespace ContosoUniv.Repositories;

public class EnrollmentRepository :EfRepository
{
    public EnrollmentRepository(SchoolContext context, ILogger<EnrollmentRepository> logger) : base(context, logger)
    {
    }

    public async Task<Result> Create(Student student, List<int> courses)
    {
        foreach (var c in courses)
        {
            var enrollment = new Enrollment
            {
                CourseId = c,
                StudentId = student.Id,
            };
            _context.Add(enrollment);
        }
        return await Save();
    }
}