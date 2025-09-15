using ContosoUniv.Controllers;
using ContosoUniv.Data;
using ContosoUniv.Lib;
using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Repositories;

public class StudentRepository : EfRepository
{
    public StudentRepository(SchoolContext context, ILogger<StudentRepository> logger) : base(context, logger)
    {
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

    public async Task<Result> Create(Student student)
    {
        _context.Add(student);
        return await Save();
    }

    public async Task<Result> Update(Student student)
    {
        _context.Update(student);
        return await Save();
    }

    public async Task<Result> Remove(Student student)
    {
        _context.Students.Remove(student);
        return await Save();
    }
    
    
}