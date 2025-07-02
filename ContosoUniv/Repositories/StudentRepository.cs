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
}