using ContosoUniv.Models;

namespace ContosoUniv.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetStudents(string? filter);
    Task<Student?> GetStudentById(int? id);
    Task<int> Create(Student student);
    
    Task<int> Update(Student student);
    Task<int> Remove(Student student);
}