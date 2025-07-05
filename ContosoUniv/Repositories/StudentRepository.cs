using ContosoUniv.Data;
using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Repositories;

public  class StudentRepository
{
    private  readonly SchoolContext _context;

    public StudentRepository(SchoolContext context)
    {
        _context = context;
    }


    public  async Task<List<Student>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student?> GetStudentById(int? id)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
    }
}