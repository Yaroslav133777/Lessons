namespace LessonsProject;

public class LessonApplication
{
    private readonly List<RegularLesson> _lessons = [];
    private readonly LessonService _service = new LessonService();
    private readonly LessonFileService _lessonFileService = new LessonFileService();
    public bool IsRunning { get; private set; } = true;
    public void PrintMenu()
    {
        Console.Clear();
        Console.WriteLine("1.Добавить запись");
        Console.WriteLine("2.Показать список записей");
        Console.WriteLine("3.Считать и добавить данные с файлы");
        Console.WriteLine("4.Сохранить в файл");
        Console.WriteLine("5.Выход");
    }
    public string ReadCommand()
    {
        return Console.ReadLine().Trim().ToLower();
    }

    public void AddLesson()
    {
        LessonType? type = SelectLessonType();
        if (type == null)
            return;
        Console.WriteLine(GetLessonPrompt(type.Value));
        RegularLesson? lesson = ParseLesson(type, Console.ReadLine());
        if (lesson == null)
        {
            Console.WriteLine("Ошибка: не удалось распознать запись");
        }
        else
        {
            _lessons.Add(lesson);
        }
        WaitForEnter();
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
                Console.WriteLine("Неизвестный тип записи");
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

    public void ShowLessons()
    {
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
            foreach (RegularLesson lesson in ReadLessonsFromFile(type.Value, path))
            {
                _lessons.Add(lesson);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        WaitForEnter();
    }

    public void SaveToFile()
    {
        LessonType? type = SelectLessonType();
        if (type == null)
            return;
        Console.WriteLine("Путь к файлу сохранения:");
        string path = Console.ReadLine().Trim('\'').Trim('"');
        try
        {
            WriteLessonsToFile(type.Value, path);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        WaitForEnter();
    }
    

    public void Exit()
    {
        Console.WriteLine("Завершить программу? [y/n]");
        string answer = Console.ReadLine().Trim().ToLower();
        if (answer == "y")
        {
            IsRunning = false;
        }
    }

    public void UnknownCommandMessage()
    {
        Console.WriteLine("Неизвестная команда");
        WaitForEnter();
    }
    public void WaitForEnter()
    {
        Console.WriteLine("Enter - продолжить");
        Console.ReadLine();
    }
    private IEnumerable<RegularLesson> ReadLessonsFromFile(LessonType type, string path)
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