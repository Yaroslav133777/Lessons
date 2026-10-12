namespace LessonsProject;

public class LessonFileService
{
    public void WriteLessons<T>(string path, IEnumerable<T> lessons, IParser<T> parser) where T : RegularLesson
    {
        using StreamWriter sw = new StreamWriter(path);
        foreach (var lesson in lessons)
        {
            sw.WriteLine($"{parser.FormatToString(lesson)};");
        }
    }

    public IEnumerable<RegularLesson> ReadLessons<T>(string path, IParser<T> parser) where T : RegularLesson
    {
        List<RegularLesson> readLessons = new List<RegularLesson>();
        foreach (var info in File.ReadAllText(path).Split(';'))
        {
            T? lesson = parser.ParseFromString(info);
            if (lesson != null)
                readLessons.Add(lesson);
        }
        return readLessons;
    }
}