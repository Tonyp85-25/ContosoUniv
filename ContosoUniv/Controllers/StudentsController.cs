using ContosoUniv.Lib;
using ContosoUniv.Models;
using ContosoUniv.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Controllers;


public class StudentsController : Controller
{
    private IStudentRepository StudentRepository { get; init; }

    public StudentsController(IStudentRepository studentRepository)
    {
        StudentRepository = studentRepository;
    }

    // GET
    public IActionResult Index([FromQuery] int? pageNumber, [FromQuery] SortDirection? nameOrder)
    {
        if (pageNumber.HasValue)
        {
            ViewData["pageNumber"] = pageNumber.Value;
        }

       
            ViewData["nameOrder"] = nameOrder??SortDirection.Ascending;
        
        return View();
    }

    [Route("/student-list/")]
    public IActionResult SearchStudents([FromQuery]string? filter)
    {
        return ViewComponent("StudentList", new { filter });
    }

    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var student = await StudentRepository.GetStudentById(id.Value);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        Student student)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await StudentRepository.Create(student);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (DbUpdateException ex)
        {
            ModelState.AddModelError("", "Unable to save changes");
        }

        return View(student);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var student = await StudentRepository.GetStudentById(id.Value);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await StudentRepository.Update(student);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists, " +
                                             "see your system administrator.");
            }

            return RedirectToAction(nameof(Index));
        }

        return View(student);
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await StudentRepository.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
        }

        try
        {
            await StudentRepository.Remove(student);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists, " +
                                         "see your system administrator.");
        }


        return ViewComponent("StudentList");
    }
}