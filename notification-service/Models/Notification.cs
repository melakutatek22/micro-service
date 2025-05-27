namespace NotificationService.Models;

public class Notification
{
    public int Id { get; set; }
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Type { get; set; } = "Email"; // Email, SMS, etc.
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public bool Success { get; set; } = true;
    public string SourceModule { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
