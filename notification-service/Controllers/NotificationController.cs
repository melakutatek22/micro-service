using Microsoft.AspNetCore.Mvc;
using NotificationService.DTOs;
using NotificationService.Services;

namespace NotificationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly NotifierService _notifierService;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(NotifierService notifierService, ILogger<NotificationController> logger)
    {
        _notifierService = notifierService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequestDto request)
    {
        _logger.LogInformation("📨 Sending {Type} notification to {To}", request.Type, request.To);

        try
        {
            var success = await _notifierService.SendNotificationAsync(request);
            if (!success)
                return StatusCode(500, "Failed to send notification");

            return Ok(new { message = $"{request.Type} sent to {request.To}" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error sending notification");
            return StatusCode(500, "Error sending notification");
        }
    }
}
