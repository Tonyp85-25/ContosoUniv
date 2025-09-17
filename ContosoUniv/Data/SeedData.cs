using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider, IConfiguration config)
    {
        using (var context =new SchoolContext(serviceProvider.GetRequiredService<DbContextOptions<SchoolContext>>()))
        {
            context.Database.EnsureCreated();
            
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }
            
            context.Students.AddRange(
                new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
                new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
                new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
        
                );
            context.Courses.AddRange(
                new Course{Title="Chemistry",Credits=3},
                new Course{Title="Microeconomics",Credits=3},
                new Course{Title="Macroeconomics",Credits=3},
                new Course{Title="Calculus",Credits=4},
                new Course{Title="Trigonometry",Credits=4},
                new Course{Title="Composition",Credits=3},
                new Course{Title="Literature",Credits=4}
                );
            
            context.Users.AddRange(
                new User{EmailAddress = "user@test.com",Password = config["UserSecret"], Role = Role.Employee},
                new User{ EmailAddress = "admin@test.com", Password = config["AdminSecret"],Role= Role.Administrator}
                );
            
            context.SaveChanges();
            
            
            
        }
    }
}