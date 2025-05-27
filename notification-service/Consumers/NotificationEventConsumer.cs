using MassTransit;
using NotificationService.DTOs;

using NotificationService.Services;
using shared.Events;


namespace NotificationService.Consumers;

public class NotificationEventConsumer : IConsumer<NotificationEvent>
{
    private readonly NotifierService _notifier;

    public NotificationEventConsumer(NotifierService notifier)
    {
        _notifier = notifier;
    }

    public async Task Consume(ConsumeContext<NotificationEvent> context)
    {
        var dto = new NotificationRequestDto
        {
            To = context.Message.To,
            Subject = context.Message.Subject,
            Message = context.Message.Message,
            Type = context.Message.Type,
            SourceModule = context.Message.SourceModule
        };

        await _notifier.SendNotificationAsync(dto);
    }
}
