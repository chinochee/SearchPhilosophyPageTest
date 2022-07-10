namespace SearchPhilosophyPageTest.Loggers;

public class ConsoleLogger : ILogger
{
    public void Write(string message)
    {
        Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: {message}");
    }

    public void Close()
    {
        
    }
}