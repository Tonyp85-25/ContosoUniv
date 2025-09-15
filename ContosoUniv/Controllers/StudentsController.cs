using ContosoUniv.Commands.StudentEnrollment;
using ContosoUniv.Lib;
using ContosoUniv.Models;
using ContosoUniv.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Controllers;


public class StudentsController(StudentRepository studentRepository, CourseRepository courseRepository,EnrollmentRepository enrollmentRepository)
    : Controller
{
    private StudentRepository StudentRepository { get;  } = studentRepository;
    private CourseRepository CourseRepository { get;  } = courseRepository;
    private EnrollmentRepository EnrollmentRepository { get; } = enrollmentRepository;

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
        if (!ModelState.IsValid) return View(student);
        var result=  await StudentRepository.Create(student);
        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }
        ModelState.AddModelError("", "Unable to save changes");
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

        var courses = await CourseRepository.GetAll();
        var enrollments = student.Enrollments.ToList();
        ViewBag.EnrollmentViewModel = new EnrollmentViewModel(student.publicId, courses, enrollments) as EnrollmentViewModel;

        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Student student)
    {
        if (!ModelState.IsValid) return View(student);
        var result =   await StudentRepository.Update(student);

        if (result.IsFailure)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists, " +
                                         "see your system administrator.");
        }
            
        return RedirectToAction(nameof(Index));

    }

    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await StudentRepository.GetStudentById(id);
        if (student == null)
        {
            return NotFound();
        }
        
        var result = await StudentRepository.Remove(student);

        if (result.IsFailure)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists, " +
                                         "see your system administrator.");
        }
            
        


        return ViewComponent("StudentList");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Enroll(StudentEnrollmentCommandRequest request )
    {
       
        if(!ModelState.IsValid) return RedirectToAction(nameof(Edit),new RouteValueDictionary{{"id",request.StudentId}});
        var handler = new StudentEnrollmentCommandHandler(StudentRepository, CourseRepository,EnrollmentRepository );
        var command =StudentEnrollmentCommand.FromRequest(request);
        var result = await handler.Handle(command);

        if (result.IsFailure)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                         "Try again, and if the problem persists, " +
                                         "see your system administrator.");
        }

        return RedirectToAction(nameof(Edit),new RouteValueDictionary{{"id",request.StudentId}});
    }
    
}