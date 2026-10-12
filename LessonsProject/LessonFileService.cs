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

    public IEnumerable<T?> ReadLessons<T>(string path, IParser<T> parser) where T : RegularLesson
    {
        List<T?> readLessons = new List<T?>();
        foreach (var info in File.ReadAllText(path).Split(';'))
        {
            if (string.IsNullOrWhiteSpace(info))
                continue;
            readLessons.Add(parser.ParseFromString(info));
        }
        return readLessons;
    }
}