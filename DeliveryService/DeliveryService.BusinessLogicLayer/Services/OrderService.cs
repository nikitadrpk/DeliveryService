using DeliveryService.DeliveryService.BusinessLogicLayer.Interfaces;

namespace DeliveryService.DeliveryService.BusinessLogicLayer.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogService _logService;

    public OrderService(IOrderRepository orderRepository, ILogService logService)
    {
        _orderRepository = orderRepository;
        _logService = logService;
    }
    
    public async Task<IEnumerable<Order>> GetFilteredOrderAsync(string cityDistrict, DateTime firstDeliveryTime)
    {
        try
        {
            _logService.LogInfo($"Фильтрация заказов по району: {cityDistrict}, time: {firstDeliveryTime}");
            DateTime endTime = firstDeliveryTime.AddMinutes(30);

            var orders = await _orderRepository.GetOrderForDeliveryAreaAsync(cityDistrict, firstDeliveryTime, endTime);
            
            _logService.LogInfo($"Отфильтровано {orders.Count()} заказов по району.");
            return orders;
        }
        catch (Exception e)
        {
            _logService.LogError($"Ошибка фильтрации заказов по району: {e.Message}");
            throw;
        }
    }
}