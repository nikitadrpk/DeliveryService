using System;
using System.Globalization;
using DeliveryService;
using DeliveryService.DeliveryService.BusinessLogicLayer.Interfaces;
using DeliveryService.DeliveryService.BusinessLogicLayer.Services;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        /*using (var context = new DeliveryServiceDbContext())
        {
            context.Database.EnsureCreated();
        }*/  // Создание тестовой базы данных.
        
        string? cityDistrict = args.FirstOrDefault(a => a.StartsWith("_cityDistrict="))?.Split('=')[1];
        string? deliveryLogPath = args.FirstOrDefault(a => a.StartsWith("_deliveryLog="))?.Split('=')[1];
        string? firstDeliveryDateTimeStr = args.FirstOrDefault(a => a.StartsWith("_firstDeliveryDateTime="))?.Split('=')[1];
        string? deliveryOrderPath = args.FirstOrDefault(a => a.StartsWith("_deliveryOrder="))?.Split('=')[1];
        
        if (cityDistrict == null || deliveryLogPath == null || firstDeliveryDateTimeStr == null || deliveryOrderPath == null)
        {
            Console.WriteLine("Недостаточно параметров. Требуются _cityDistrict, _deliveryLog и _firstDeliveryDateTime.");
            return;
        }

        if (!DateTime.TryParseExact(firstDeliveryDateTimeStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime firstDeliveryDateTime))
        {
            Console.WriteLine("Некорректный формат даты. Ожидается: yyyy-MM-dd HH:mm:ss");
            return;
        }
        
        var services = new ServiceCollection();
        ConfigureServices(services, deliveryLogPath);

        var serviceProvider = services.BuildServiceProvider();

        var orderService = serviceProvider.GetRequiredService<IOrderService>();
        
        var orderRepository = serviceProvider.GetService<IOrderRepository>();
        
        /*if (orderRepository is OrderRepository repo)
        {
            await repo.SeedDatabaseAsync();
            Console.WriteLine("Тестовые данные добавлены в базу данных.");
        }*/ // Добавление тестовых данных в бд
        
        try
        {
            var orders = await orderService.GetFilteredOrderAsync(cityDistrict, firstDeliveryDateTime);
            Console.WriteLine($"Найдено {orders.Count()} заказов для района {cityDistrict} на указанное время.");

            foreach (var order in orders)
            {
                Console.WriteLine($"Заказ #{order.Id}, вес: {order.Weight} кг, доставка: {order.DeliveryTime}");
            }
            SaveOrdersToFile(orders, deliveryOrderPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при выполнении программы: {ex.Message}");
        }
    }

    private static void ConfigureServices(IServiceCollection services, string logFilePath)
    {
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddDbContext<DeliveryServiceDbContext>();
        services.AddSingleton<ILogService>(new LogService(logFilePath));
        services.AddSingleton<IOrderService, OrderService>();
    }
    
    private static void SaveOrdersToFile(IEnumerable<Order> orders, string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Заказ, Вес, Район, Время доставки");
                foreach (var order in orders)
                {
                    writer.WriteLine($"{order.Id}, {order.Weight}, {order.CityDistrict}, {order.DeliveryTime:yyyy-MM-dd HH:mm:ss}");
                }
            }
            Console.WriteLine($"Результаты успешно записаны в файл: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
        }
    }
}
