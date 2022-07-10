namespace SearchPhilosophyPageTest.Loggers;

public class FileLogger : ILogger
{
    private readonly StreamWriter _writer;
        
    public FileLogger(string fileName)
    {
        _writer = new StreamWriter(fileName);
        Console.WriteLine($"file: {fileName}");
    }

    public void Write(string message)
    {
        _writer.WriteLine($"{DateTime.Now.ToLongTimeString()}: {message}");
        _writer.Flush();
    }

    public void Close()
    {
        _writer.Close();
    }
}