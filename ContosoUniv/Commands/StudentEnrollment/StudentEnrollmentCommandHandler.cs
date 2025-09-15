using ContosoUniv.Lib;
using ContosoUniv.Lib.Errors;
using ContosoUniv.Repositories;

namespace ContosoUniv.Commands.StudentEnrollment;

public class StudentEnrollmentCommandHandler(StudentRepository studentRepository,CourseRepository courseRepository,EnrollmentRepository enrollmentRepository )
{
    public async Task<Result> Handle(StudentEnrollmentCommand command )
    {
        var student = await studentRepository.GetStudentById(command.StudentId.Value);
        if (student is null )
        {
            
            return Result.Failure(new Error(ErrorType.NotFound, "Student not Found"));
        }
      

        var courseIds = await courseRepository.GetSelectedIds(command.CourseIds);

        foreach (var id in courseIds )
        {
            if (student.HasEnrollment(id))
            {
                return Result.Failure(new Error(ErrorType.AlreadyExists,"Enrollment already exists"));
            }
        }
        
        return await enrollmentRepository.Create(student, courseIds);
        
     
       
    }
}

