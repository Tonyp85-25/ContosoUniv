using ContosoUniv.Models;

namespace ContosoUniv.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetStudents();
    Task<Student?> GetStudentById(int? id);
    Task<int> Create(Student student);
}