using Microsoft.EntityFrameworkCore;
using HseHackathon.Entities;

namespace HseHackathon.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<IncidentType> IncidentTypes => Set<IncidentType>();
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<IncidentPhoto> IncidentPhotos => Set<IncidentPhoto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Incident>()
            .HasOne(i => i.IncidentType)
            .WithMany(it => it.Incidents)
            .HasForeignKey(i => i.IncidentTypeId);

        modelBuilder.Entity<Incident>()
            .HasOne(i => i.ReportedByUser)
            .WithMany(u => u.Reports)
            .HasForeignKey(i => i.ReportedByUserId);

        modelBuilder.Entity<IncidentPhoto>()
            .HasOne(ip => ip.Incident)
            .WithMany()
            .HasForeignKey(ip => ip.IncidentId);

        // Seed incident types
        modelBuilder.Entity<IncidentType>().HasData(
            new IncidentType { Id = 1, Name = "Near Miss", Description = "A near miss incident that could have resulted in injury or damage" },
            new IncidentType { Id = 2, Name = "Injury", Description = "An actual injury incident" },
            new IncidentType { Id = 3, Name = "Environmental", Description = "Environmental hazard or spillage" },
            new IncidentType { Id = 4, Name = "Property Damage", Description = "Damage to property or equipment" },
            new IncidentType { Id = 5, Name = "Other", Description = "Other HSE-related incidents" }
        );
    }
}

public class WeatherForecast
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = string.Empty;
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
