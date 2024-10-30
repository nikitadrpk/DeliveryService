using DeliveryService.DeliveryService.BusinessLogicLayer.Interfaces;

namespace DeliveryService.DeliveryService.BusinessLogicLayer.Services;

public class LogService : ILogService
{
    private readonly string _logFilePath;

    public LogService(string logFilePath)
    {
        _logFilePath = logFilePath;
    }
    
    public void LogInfo(string message)
    {
        Log($"INFO: {message}");
    }

    public void LogError(string message)
    { 
        Log($"ERROR: {message}");
    }

    private void Log(string message)
    {
        try
        {
            File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Не удалось записать в файл логирования: {e.Message}");
        }
    }
}