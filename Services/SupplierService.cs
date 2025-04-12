using Microsoft.AspNetCore.Components.Authorization;
using CSE_325_group_project.Models;
using CSE_325_group_project.Data;
using Microsoft.EntityFrameworkCore;

namespace CSE_325_group_project.Services
{
    public interface ISupplierService
    {
        Task<Supplier?> GetSupplierByUserIdAsync(int userId);
    }

    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _dbContext;

        public SupplierService(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<Supplier?> GetSupplierByUserIdAsync(int userId)
        {
            return await _dbContext.Suppliers
                .Where(s => s.UserId == userId)
                .Include(s => s.Category)
                .Include(s => s.Services)
                .FirstOrDefaultAsync();
        }
    }

}