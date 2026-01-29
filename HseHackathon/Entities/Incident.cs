namespace HseHackathon.Entities;

public class Incident
{
    public int Id { get; set; }
    public DateTime DateTimeReported { get; set; }
    public string Location { get; set; } = string.Empty;
    public int? IncidentTypeId { get; set; }
    public IncidentType? IncidentType { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public IncidentStatus Status { get; set; } = IncidentStatus.Draft;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
}

public enum IncidentStatus
{
    Draft,
    Submitted,
    Reviewed,
    Closed
}
