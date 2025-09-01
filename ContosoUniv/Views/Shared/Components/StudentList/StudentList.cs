using ContosoUniv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniv.Views.Shared.Components.StudentList;

public class StudentList: ViewComponent
{
    public IStudentRepository StudentRepository { get; }

    public StudentList(IStudentRepository studentRepository)
    {
        StudentRepository = studentRepository;
    }


    public async Task <IViewComponentResult> InvokeAsync(string? filter)
    {
        var students = await StudentRepository.GetStudents(filter);
        return View(students);
    }
}