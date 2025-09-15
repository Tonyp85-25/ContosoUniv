using ContosoUniv.Data;
using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Repositories;

public class CourseRepository:EfRepository
{
    public CourseRepository(SchoolContext context, ILogger<CourseRepository> logger) : base(context, logger)
    {
    }

    public async Task<Course?> GetCourseById(Guid id)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(m => m.publicId == new CourseId(id));
    }

    public async Task<List<Course>> GetAll()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<List<int>> GetSelectedIds(CourseId[] ids)
    {
        var courseIds = _context.Courses.Where(c => ids.Contains(c.publicId)).Select((c => c.Id));

        return await courseIds.ToListAsync();
    }
}