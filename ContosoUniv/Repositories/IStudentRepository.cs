using ContosoUniv.Controllers;
using ContosoUniv.Lib;
using ContosoUniv.Models;

namespace ContosoUniv.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetStudents(string? filter, SortDirection? nameOrder);
    Task<Student?> GetStudentById(Guid id);
    Task<int> Create(Student student);
    
    Task<int> Update(Student student);
    Task<int> Remove(Student student);
}