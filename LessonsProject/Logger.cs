namespace LessonsProject;

public class Logger
{
    private readonly string _filePath = AppContext.BaseDirectory + "LessonsProject_logger.txt";

    public Logger()
    {
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close();
        }
    }
    public void LogError(string errorMessage)
    {
        Log(errorMessage);
    }
    private void Log( string message)
    {
        string logData = $"[{DateTime.Now}]\n{message}";
        Console.WriteLine(logData);
        if (File.Exists(_filePath))
        {
            File.AppendAllText(_filePath, logData + Environment.NewLine);
        }
    }
}
