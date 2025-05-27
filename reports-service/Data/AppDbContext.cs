using Microsoft.EntityFrameworkCore;
using ReportsService.Models;

namespace ReportsService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<JobSeekerReport> JobSeekers { get; set; }
}
