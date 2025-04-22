namespace Common.Models;

public class MoodRequest
{
    public string UserId { get; set; }
    public string Mood { get; set; }
    public DateTime DetectedAt { get; set; } = DateTime.UtcNow;
}