namespace DeliveryService;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrderForDeliveryAreaAsync(string cityDistrict, DateTime fromTime, DateTime toTime);
    Task AddOrderAsync(Order order);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
}