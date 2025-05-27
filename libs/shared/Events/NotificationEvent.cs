namespace shared.Events;

public class NotificationEvent
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string Type { get; set; } = "Email"; // Email, SMS, InApp
    public string SourceModule { get; set; } = "JobSeeker";
}
