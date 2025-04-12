using Microsoft.AspNetCore.Components.Authorization;
using CSE_325_group_project.Models;
using CSE_325_group_project.Data;
using Microsoft.EntityFrameworkCore;

namespace CSE_325_group_project.Services
{
    public interface ICompanyService
    {
        Task<Company?> GetCompanyByUserIdAsync(int userId);
    }

    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _dbContext;

        public CompanyService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Company?> GetCompanyByUserIdAsync(int userId)
        {
            return await _dbContext.Companies
                .Where(c => c.User_id == userId)
                .FirstOrDefaultAsync();
        }
    }

}