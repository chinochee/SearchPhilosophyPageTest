namespace SearchPhilosophyPageTest.Loggers;

public class LoggerSystem
{
    private const string PathFolder = "C:/Users/User/source/repos/SearchPhilosophyPageTest";
    private const string FileName = "AutotestSearch.log";
    
    private readonly List<ILogger> _loggers;
    
    public LoggerSystem()
    {
        _loggers = new List<ILogger>
        {
            new FileLogger(Path.Combine(PathFolder, FileName)),
            new ConsoleLogger()
        };
    }
    
    public void Write(string message)
    {
        foreach (var logger in _loggers)
        {
           logger.Write(message);
        }
    }

    public void Close()
    {
        foreach (var logger in _loggers)
        {
            logger.Close();
        }
    }
}