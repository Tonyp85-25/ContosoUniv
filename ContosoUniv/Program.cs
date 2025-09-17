using ContosoUniv.Data;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();