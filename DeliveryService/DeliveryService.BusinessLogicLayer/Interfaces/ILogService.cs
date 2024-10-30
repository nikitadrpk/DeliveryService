namespace DeliveryService.DeliveryService.BusinessLogicLayer.Interfaces;

public interface ILogService
{
    void LogInfo(string message);
    void LogError(string message);
}