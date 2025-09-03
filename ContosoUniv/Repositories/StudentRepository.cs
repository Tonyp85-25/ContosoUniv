using ContosoUniv.Controllers;
using ContosoUniv.Data;
using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly SchoolContext _context;

    public StudentRepository(SchoolContext context)
    {
        _context = context;
    }


    public async Task<List<Student>> GetStudents(string? filter, SortDirection? nameOrder)
    {
        var students = _context.Students.Select(s => s);
        if (nameOrder is SortDirection.Descending)
        {
            students = students.OrderByDescending(s => s.LastName);
        }

        if (string.IsNullOrEmpty(filter) || string.IsNullOrWhiteSpace(filter))
        {
            return await students.ToListAsync();
        }

        students =
            students.Where(s => s.LastName.Contains(filter) || s.FirstMidName.Contains(filter));

        return await students.AsNoTracking().ToListAsync();
    }

    public async Task<Student?> GetStudentById(Guid id)
    {
        return await _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.publicId == new StudentId(id));
    }

    public async Task<int> Create(Student student)
    {
        _context.Add(student);
        return await _context.SaveChangesAsync();
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