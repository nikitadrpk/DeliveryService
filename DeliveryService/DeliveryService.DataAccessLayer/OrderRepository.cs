using Microsoft.EntityFrameworkCore;

namespace DeliveryService;

public class OrderRepository : IOrderRepository
{
    private readonly DeliveryServiceDbContext _context;
    public OrderRepository(DeliveryServiceDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Order>> GetOrderForDeliveryAreaAsync(string cityDistrict, DateTime fromTime, DateTime toTime)
    {
        return await _context.Orders
            .Where(o => o.CityDistrict == cityDistrict && o.DeliveryTime >= fromTime && o.DeliveryTime <= toTime)
            .ToListAsync();
    }

    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync() => await _context.Orders.ToListAsync();
    
    // создание записей в бд для тестирования 
    
    /*public async Task SeedDatabaseAsync()
    {
        if (await _context.Orders.AnyAsync()) return;

        var orders = new List<Order>
        {
            new Order { Id = 1, Weight = 2.5, CityDistrict = "Central", DeliveryTime = DateTime.Now.AddMinutes(-10) },
            new Order { Id = 2, Weight = 1.2, CityDistrict = "Central", DeliveryTime = DateTime.Now.AddMinutes(-5) },
            new Order { Id = 3, Weight = 3.0, CityDistrict = "North", DeliveryTime = DateTime.Now.AddMinutes(-15) },
            new Order { Id = 4, Weight = 4.5, CityDistrict = "East", DeliveryTime = DateTime.Now.AddMinutes(-30) },
            new Order { Id = 5, Weight = 2.0, CityDistrict = "Central", DeliveryTime = DateTime.Now.AddMinutes(-20) },
            new Order { Id = 6, Weight = 3.0, CityDistrict = "Central", DeliveryTime = DateTime.Now.AddMinutes(-21) },
            new Order { Id = 7, Weight = 2.0, CityDistrict = "Central", DeliveryTime = DateTime.Now.AddMinutes(-22) },
            new Order { Id = 8, Weight = 2.0, CityDistrict = "Central", DeliveryTime = DateTime.Now.AddMinutes(-23) },
            new Order { Id = 9, Weight = 2.0, CityDistrict = "Central", DeliveryTime = DateTime.Now.AddMinutes(-24) },
        };

        await _context.Orders.AddRangeAsync(orders);
        await _context.SaveChangesAsync();
    }*/
}