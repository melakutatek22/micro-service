namespace NotificationService.DTOs;

public class NotificationRequestDto
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Type { get; set; } = "Email"; // or "System", "SMS"
    public string SourceModule { get; set; }
}
