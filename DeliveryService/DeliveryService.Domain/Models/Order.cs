namespace DeliveryService;

public class Order
{
    public int Id { get; set; }
    public double Weight { get; set; }
    public string CityDistrict { get; set; }
    public DateTime DeliveryTime { get; set; }
}