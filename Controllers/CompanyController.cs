using CSE_325_group_project.Data;  // Your DbContext namespace
using CSE_325_group_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSE_325_group_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CompanyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] Company company)
        {
            if (company == null)
            {
                return BadRequest("Invalid company data.");
            }

            try
            {
                _context.Companies.Add(company);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Company added successfully", companyId = company.Company_id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}