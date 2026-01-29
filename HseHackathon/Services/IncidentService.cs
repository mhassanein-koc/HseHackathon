using HseHackathon.Data;
using HseHackathon.Entities;
using Microsoft.EntityFrameworkCore;

namespace HseHackathon.Services;

public class IncidentService
{
    private readonly AppDbContext _context;

    public IncidentService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all incidents with related data
    /// </summary>
    public async Task<List<Incident>> GetAllIncidentsAsync()
    {
        return await _context.Incidents
            .Include(i => i.IncidentType)
            .OrderByDescending(i => i.CreatedDate)
            .ToListAsync();
    }

    /// <summary>
    /// Get incident by ID
    /// </summary>
    public async Task<Incident?> GetIncidentByIdAsync(int id)
    {
        return await _context.Incidents
            .Include(i => i.IncidentType)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    /// <summary>
    /// Create a new incident
    /// </summary>
    public async Task<Incident> CreateIncidentAsync(Incident incident)
    {
        incident.CreatedDate = DateTime.UtcNow;
        incident.Status = IncidentStatus.Draft;
        
        _context.Incidents.Add(incident);
        await _context.SaveChangesAsync();
        
        return incident;
    }

    /// <summary>
    /// Update an existing incident
    /// </summary>
    public async Task<Incident> UpdateIncidentAsync(Incident incident)
    {
        incident.UpdatedDate = DateTime.UtcNow;
        
        _context.Incidents.Update(incident);
        await _context.SaveChangesAsync();
        
        return incident;
    }

    /// <summary>
    /// Submit an incident (change status from Draft to Submitted)
    /// </summary>
    public async Task<Incident> SubmitIncidentAsync(int id)
    {
        var incident = await _context.Incidents.FindAsync(id);
        if (incident == null)
            throw new InvalidOperationException("Incident not found");

        incident.Status = IncidentStatus.Submitted;
        incident.UpdatedDate = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return incident;
    }

    /// <summary>
    /// Delete an incident
    /// </summary>
    public async Task<bool> DeleteIncidentAsync(int id)
    {
        var incident = await _context.Incidents.FindAsync(id);
        if (incident == null)
            return false;

        _context.Incidents.Remove(incident);
        await _context.SaveChangesAsync();
        
        return true;
    }

    /// <summary>
    /// Get incidents filtered by status
    /// </summary>
    public async Task<List<Incident>> GetIncidentsByStatusAsync(IncidentStatus status)
    {
        return await _context.Incidents
            .Include(i => i.IncidentType)
            .Where(i => i.Status == status)
            .OrderByDescending(i => i.CreatedDate)
            .ToListAsync();
    }

    /// <summary>
    /// Get incidents reported by a specific user
    /// </summary>
    // Removed GetIncidentsByUserAsync — incidents are no longer linked to users
}
