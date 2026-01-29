using HseHackathon.Data;
using HseHackathon.Entities;
using Microsoft.EntityFrameworkCore;

namespace HseHackathon.Services;

public class IncidentTypeService
{
    private readonly AppDbContext _context;

    public IncidentTypeService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all incident types
    /// </summary>
    public async Task<List<IncidentType>> GetAllIncidentTypesAsync()
    {
        return await _context.IncidentTypes
            .OrderBy(it => it.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Get incident type by ID
    /// </summary>
    public async Task<IncidentType?> GetIncidentTypeByIdAsync(int id)
    {
        return await _context.IncidentTypes
            .FirstOrDefaultAsync(it => it.Id == id);
    }

    /// <summary>
    /// Create a new incident type
    /// </summary>
    public async Task<IncidentType> CreateIncidentTypeAsync(IncidentType incidentType)
    {
        _context.IncidentTypes.Add(incidentType);
        await _context.SaveChangesAsync();
        
        return incidentType;
    }

    /// <summary>
    /// Update an existing incident type
    /// </summary>
    public async Task<IncidentType> UpdateIncidentTypeAsync(IncidentType incidentType)
    {
        _context.IncidentTypes.Update(incidentType);
        await _context.SaveChangesAsync();
        
        return incidentType;
    }

    /// <summary>
    /// Delete an incident type
    /// </summary>
    public async Task<bool> DeleteIncidentTypeAsync(int id)
    {
        var incidentType = await _context.IncidentTypes.FindAsync(id);
        if (incidentType == null)
            return false;

        _context.IncidentTypes.Remove(incidentType);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
