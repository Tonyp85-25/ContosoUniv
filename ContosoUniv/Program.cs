using System.Security.Claims;
using ContosoUniv.Data;
using ContosoUniv.Models;
using ContosoUniv.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders().AddConsole();
// Add services to the container.
builder.Services.AddControllersWithViews();

// via dotnet cli
var strBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
strBuilder.DataSource = builder.Configuration["DbHost"];
strBuilder.InitialCatalog = builder.Configuration["DbName"];
strBuilder.UserID = builder.Configuration["DbUser"];
strBuilder.Password = builder.Configuration["DbPassword"];

builder.Services.AddDbContext<SchoolContext>(options =>
{
    options.UseSqlServer(strBuilder.ConnectionString);
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddTransient<StudentRepository>();
builder.Services.AddTransient<CourseRepository>();
builder.Services.AddTransient<EnrollmentRepository>();
builder.Services.AddTransient<UserRepository>();

builder.Services.AddAuthentication().AddCookie("CookieScheme",options =>
{
    options.Cookie.HttpOnly= true;
    options.Cookie.Name = "CookieScheme";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Login/Login";
}) ;

builder.Services.AddAuthorization(options =>

    options.AddPolicy("EmployeeOnly", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, Role.Employee.ToString());
    })
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services, app.Configuration);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();