using ContosoUniv.Controllers;
using ContosoUniv.Lib;
using ContosoUniv.Models;
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


    public async Task <IViewComponentResult> InvokeAsync(string? filter, int? pageNumber, SortDirection? nameOrder)
    {
        var students = await StudentRepository.GetStudents(filter, nameOrder);
        int pageSize = 10;
        var list = PaginatedList<Student>.Create(students, pageNumber ?? 1, pageSize);
        return View(list);
    }
}