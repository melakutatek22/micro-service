using MassTransit;
using ReportsService.Data;
using ReportsService.Models;
using Shared.Events;

namespace ReportsService.Consumers;

public class JobSeekerCreatedConsumer : IConsumer<JobSeekerCreated>
{
    private readonly AppDbContext _context;
    private readonly ILogger<JobSeekerCreatedConsumer> _logger;

    public JobSeekerCreatedConsumer(AppDbContext context, ILogger<JobSeekerCreatedConsumer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<JobSeekerCreated> context)
    {
        var message = context.Message;

        _context.JobSeekers.Add(new JobSeekerReport
        {
            Id = message.Id,
            FullName = message.FullName,
            Email = message.Email,
            Skills = message.Skills
        });

        await _context.SaveChangesAsync();
        _logger.LogInformation("📊 JobSeekerCreated event saved for {Email}", message.Email);
    }
}
