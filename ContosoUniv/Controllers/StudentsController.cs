using ContosoUniv.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ContosoUniv.Controllers;

public class StudentsController : Controller
{
    private  StudentRepository StudentRepository { get; init; }

    public StudentsController(StudentRepository studentRepository)
    {
        StudentRepository = studentRepository;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var students = await StudentRepository.GetStudents();
        return View(students);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student =  await StudentRepository.GetStudentById(id);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }
}