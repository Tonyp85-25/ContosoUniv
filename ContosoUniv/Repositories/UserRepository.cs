using ContosoUniv.Data;
using ContosoUniv.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniv.Repositories;

public class UserRepository: EfRepository
{
    public UserRepository(SchoolContext context, ILogger<EfRepository> logger) : base(context, logger)
    {
    }

    public async Task<User?> GetByEmail(string email)
    {
        var user =await _context.Users.FirstOrDefaultAsync(u => u.EmailAddress == email);

        return user;
    }
}