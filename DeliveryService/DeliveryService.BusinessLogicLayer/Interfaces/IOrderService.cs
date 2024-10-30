namespace DeliveryService.DeliveryService.BusinessLogicLayer.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetFilteredOrderAsync(string cityDistrict, DateTime firstDeliveryTime);
}