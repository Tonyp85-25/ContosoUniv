using ContosoUniv.Data;
using ContosoUniv.Lib;
using ContosoUniv.Lib.Errors;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Repositories;

public abstract class EfRepository
{
    protected readonly SchoolContext _context;
    protected readonly ILogger _logger;

    public EfRepository(SchoolContext context, ILogger<EfRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    protected async Task<Result> Save()
    {
        try
        {
            var row = await _context.SaveChangesAsync();
            if (row > 0)
            {
                _logger.LogInformation("A new record was saved by {type}", this.GetType().ToString());
                return Result.Success();
            }
            return Result.Failure(new Error(ErrorType.DbUpdate, "Enrollment Creation failed"));
        }
        catch (Exception e ) when(e is DbUpdateException )
        {
            _logger.LogError("DatabaseError of type {type}: {message}",e.GetType().ToString(), e.Message);
            return Result.Failure(new Error(ErrorType.DbUpdate, "Enrollment Creation failed"));
        }
    }
    
    
}