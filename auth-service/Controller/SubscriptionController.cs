using auth_service.Data;
using Microsoft.AspNetCore.Mvc;

namespace auth_service.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubscriptionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{orgId}")]
        public async Task<IActionResult> GetPlan(int orgId)
        {
            var org = await _context.Organizations.FindAsync(orgId);
            return org == null ? NotFound() : Ok(org);
        }

        [HttpPut("{orgId}")]
        public async Task<IActionResult> UpdatePlan(int orgId, [FromBody] string newPlan)
        {
            var org = await _context.Organizations.FindAsync(orgId);
            if (org == null) return NotFound();

            org.Plan = newPlan;
            await _context.SaveChangesAsync();
            return Ok(org);
        }
    }

}
