using NotificationService.Data;
using NotificationService.DTOs;
using NotificationService.Models;


namespace NotificationService.Services;

public class NotifierService
{
    private readonly AppDbContext _context;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<NotifierService> _logger;

    public NotifierService(AppDbContext context, IEmailSender emailSender, ILogger<NotifierService> logger)
    {
        _context = context;
        _emailSender = emailSender;
        _logger = logger;
    }

    public async Task<bool> SendNotificationAsync(NotificationRequestDto dto)
    {
        var entity = new Notification
        {
            To = dto.To,
            Subject = dto.Subject,
            Message = dto.Message,
            Type = dto.Type,
            SourceModule = dto.SourceModule,
            Status="Pending"
            
        };

        _context.Notifications.Add(entity);
        await _context.SaveChangesAsync(); // Save initial record

        try
        {
            var sent = await _emailSender.SendAsync(dto.To, dto.Subject, dto.Message); // send mail

            entity.Status = sent ? "Sent" : "Failed";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Failed to send notification to {To}", dto.To);
            entity.Status = "Failed";
        }

        await _context.SaveChangesAsync(); // update status
        return entity.Status == "Sent";
    }
}
