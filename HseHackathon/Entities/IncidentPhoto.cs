namespace HseHackathon.Entities;

public class IncidentPhoto
{
    public int Id { get; set; }
    public int IncidentId { get; set; }
    public Incident? Incident { get; set; }
    public string PhotoUrl { get; set; } = string.Empty;
    public DateTime UploadedDate { get; set; } = DateTime.UtcNow;
    public string FileName { get; set; } = string.Empty;
}
