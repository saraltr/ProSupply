using CSE_325_group_project.Data;
using CSE_325_group_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// This class is to manage order-related services
public class OrderService
{
    private readonly IServiceScopeFactory _scopeFactory; // used to create a scope for a new DbContext instance
    public event Action? OnChange; // event to notify when order count changes
    private int _orderCount; // holds the current order count for the user

    // constructor that accepts an scopeFactory to create a scope for dependency injection
    public OrderService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory; // initialize the scope factory to be used later for creating scoped services
    }

    // Property get the current order count
    public int OrderCount => _orderCount;

    // load the count of orders for a particular user asynchronously
    public async Task LoadOrderCount(int userId)
    {
        // create a new scope to resolve services like DbContext for database access
        using (var scope = _scopeFactory.CreateScope())
        {
            // retrieve an instance of the AppDbContext from the scope (which represents the database context)
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // count the active orders
            var orderCount = await dbContext.NewOrder
                .Where(o => o.UserId == userId && (o.OrderStatus == 1 || o.OrderStatus == 2 || o.OrderStatus == 3)) // filter active orders by status
                .CountAsync();

            // count the active quotes
            var quoteCount = await dbContext.Quotes
                .Where(q => q.UserId == userId && (q.QuoteStatus == 1 || q.QuoteStatus == 2 || q.QuoteStatus == 3)) // filter active quotes by status
                .CountAsync();

            // sum both order and quote counts
            _orderCount = orderCount + quoteCount;
        }


        // if there are any subscribers to the OnChange event change the UI
        OnChange?.Invoke();
    }
    // fetch all orders for a specific supplier by supplier_id
    public async Task<List<NewOrder>> GetOrdersBySupplierIdAsync(int supplierId)
    {
        // create a new scope to resolve services like DbContext for database access
        using var scope = _scopeFactory.CreateScope();
        // retrieve an instance of the AppDbContext from the scope (which represents the database context)
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // get all orders for the supplier with the given supplier_id
        var orders = await dbContext.NewOrder
            .Where(o => o.SupplierId == supplierId)
            .Include(o => o.User)
            .ToListAsync();

        return orders;
    }

    public async Task<bool> UpdateOrderStatus(int orderId, int newStatus)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var order = await dbContext.NewOrder.FindAsync(orderId);
            if (order == null)
            {
                return false; // Order not found
            }

            // Update the order status
            order.OrderStatus = newStatus;

            try
            {
                // Save changes to the database
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false; // Handle any errors
            }
        }
    }

}
