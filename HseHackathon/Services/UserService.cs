using HseHackathon.Data;
using HseHackathon.Entities;
using Microsoft.EntityFrameworkCore;

namespace HseHackathon.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _context.Users
            .OrderBy(u => u.FullName)
            .ToListAsync();
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <summary>
    /// Get user by email
    /// </summary>
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
    {
        user.Id = Guid.NewGuid();
        user.CreatedDate = DateTime.UtcNow;
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        
        return user;
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
