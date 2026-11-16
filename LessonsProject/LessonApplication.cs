using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;

namespace LessonsProject;

public class LessonApplication : IDisposable
{
    private readonly List<RegularLesson> _lessons = [];
    private readonly LessonService _service = new LessonService();
    private readonly LessonFileService _lessonFileService = new LessonFileService();
    private readonly ILogger<LessonApplication> _logger;
    private readonly ILoggerFactory? _loggerFactory;

    public LessonApplication(bool isLogging)
    {
        if (isLogging)
        {
            _loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Trace);
                builder.AddSimpleConsole(options =>
                {
                    options.ColorBehavior = LoggerColorBehavior.Disabled;
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                });
            });
            _logger = _loggerFactory.CreateLogger<LessonApplication>();
        }
        else
        {
            _logger = NullLogger<LessonApplication>.Instance;
        }
    }
    private LessonType? SelectLessonType()
    {
        Console.WriteLine("Выберите тип записи:");
        Console.WriteLine("1.Занятие");
        Console.WriteLine("2.Лекция");
        Console.WriteLine("3.Семинар");
        string choice = Console.ReadLine().Trim().ToLower();
        switch (choice)
        {
            case "1": return LessonType.RegularLesson;
            case "2": return LessonType.Lecture;
            case "3": return LessonType.Seminar;
            default:
                _logger.LogError("Неизвестный тип занятия");
                WaitForEnter();
                return null;
        }
    }

    private RegularLesson? ParseLesson(LessonType? type, string lessonInfo)
    {
        switch (type)
        {
            case LessonType.RegularLesson: return new RegularLessonParser().ParseFromString(lessonInfo);
            case LessonType.Lecture: return new LectureParser().ParseFromString(lessonInfo);
            case LessonType.Seminar: return new SeminarParser().ParseFromString(lessonInfo);
        }
        return null;
    }

    public void Dispose()
    {
        _loggerFactory?.Dispose();
    }
    
    public void AddLesson()
    {
        LessonType? type = SelectLessonType();
        if (type == null)
        {
            _logger.LogError("Неверно выбран тип занятия");
            return;
        }
        Console.WriteLine(GetLessonPrompt(type.Value));
        RegularLesson? lesson = ParseLesson(type, Console.ReadLine());
        if (lesson == null)
        {
            _logger.LogError("Не удалось распознать запись");
        }
        else
        {
            _lessons.Add(lesson);
        }
        WaitForEnter();
    }
    
    public void ShowLessons()
    {
        if (_lessons.Count == 0)
        {
            _logger.LogError("Список занятий пуст");
        }
        Console.WriteLine(_service.GetLessonsInfo(_lessons));
        WaitForEnter();
    }

    public void ReadFromFile()
    {
        LessonType? type = SelectLessonType();
        if (type == null)
            return;
        Console.WriteLine("Путь до файла:");
        string path = Console.ReadLine().Trim('\'').Trim('"');
        try
        {
            foreach (RegularLesson? lesson in ReadLessonsFromFile(type.Value, path))
            {
                if (lesson != null)
                {
                    _lessons.Add(lesson);
                }
                else
                    _logger.LogError("Неверный формат записи");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка чтения из файла");
        }
        WaitForEnter();
    }

    public void SaveToFile()
    {
        LessonType? type = SelectLessonType();
        if (type == null)
            return;
        Console.WriteLine("Путь к файлу сохранения:");
        string path = Console.ReadLine().Trim('\'').Trim('"').Trim();
        if (!File.Exists(path))
        {
            _logger.LogError("Ошибка: несуществующий файл");
            WaitForEnter();
            return;
        }
        if (_lessons.Count == 0)
        {
            _logger.LogError("Нет занятий для сохранения");
        }
        try
        {
            WriteLessonsToFile(type.Value, path);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка сохранения записи");
        }
        WaitForEnter();
    }
    

    public void Exit()
    {
        Console.WriteLine("Завершить программу? [y/n]");
        string answer = Console.ReadLine().Trim().ToLower();
        if (answer == "y")
        {
            Environment.Exit(0);
        }
    }

    public void WaitForEnter()
    {
        Console.WriteLine("Enter - продолжить");
        Console.ReadLine();
    }
    private IEnumerable<RegularLesson?> ReadLessonsFromFile(LessonType type, string path)
    {
        switch (type)
        {
            case LessonType.Lecture: return _lessonFileService.ReadLessons(path, new LectureParser());
            case LessonType.Seminar: return _lessonFileService.ReadLessons(path, new SeminarParser());
            default: return _lessonFileService.ReadLessons(path, new RegularLessonParser());
        }
    }

    private void WriteLessonsToFile(LessonType type, string path)
    {
        switch (type)
        {
            case LessonType.Lecture: _lessonFileService.WriteLessons(path, _lessons.OfType<Lecture>(), new LectureParser()); break;
            case LessonType.Seminar: _lessonFileService.WriteLessons(path, _lessons.OfType<Seminar>(), new SeminarParser()); break;
            default: _lessonFileService.WriteLessons(path, _lessons, new RegularLessonParser()); break;
        }
    }
    private string GetLessonPrompt(LessonType type)
    {
        switch (type)
        {
            case LessonType.Lecture:
                return "Введите: дату занятия (YYYY.MM.DD), название аудитории, имя преподавателя, " +
                       "максимальное количество студентов и наличие записи (true/false) через запятую";
            case LessonType.Seminar:
                return "Введите: дату занятия (YYYY.MM.DD), название аудитории, имя преподавателя, " +
                       "наличие домашней работы (true/false) и дедлайн (YYYY.MM.DD) через запятую";
            default:
                return "Введите: дату занятия (YYYY.MM.DD), название аудитории и имя преподавателя через запятую";
        }
    }
}