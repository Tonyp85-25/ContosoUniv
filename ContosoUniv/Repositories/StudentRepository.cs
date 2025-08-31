using ContosoUniv.Data;
using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Repositories;

public  class StudentRepository : IStudentRepository
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
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> Create(Student student)
    {
        _context.Add(student);
       return  await _context.SaveChangesAsync();
    }

    public async Task<int> Update(Student student)
    {
        _context.Update(student);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> Remove(Student student)
    {
        _context.Students.Remove(student);
        return await _context.SaveChangesAsync();
    }
}