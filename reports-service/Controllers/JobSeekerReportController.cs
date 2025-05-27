using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsService.Data;

namespace ReportsService.Controllers;

[ApiController]
[Route("api/reports/jobseekers")]
public class JobSeekerReportController : ControllerBase
{
    private readonly AppDbContext _context;

    public JobSeekerReportController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await _context.JobSeekers
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(results);
    }
}
