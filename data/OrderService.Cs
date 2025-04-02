using CSE_325_group_project.Data;
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

            // query the database asynchronously to count the number of orders that belong to the given user
            _orderCount = await dbContext.NewOrder
                .Where(o => o.UserId == userId) // filter orders by the user's ID
                .CountAsync();
        }


        // if there are any subscribers to the OnChange event change the UI
        OnChange?.Invoke();
    }
}
