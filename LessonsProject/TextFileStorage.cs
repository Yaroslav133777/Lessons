namespace LessonsProject;

public class TextFileStorage
{
    public bool WriteAllLessons (string path, IEnumerable<Lesson> lessons)
    {
        if (!File.Exists(path))
            return false;
        using StreamWriter sw = new StreamWriter(path);
        foreach (var lesson in lessons)
        {
            sw.WriteLine($"{lesson.Date}, {lesson.AudienceName}, {lesson.ProfessorName};");
        }
        return true;
    }

    public List<Lesson> ReadAllLessons(string path)
    {
        if (!File.Exists(path))
            throw new Exception("Файл не найден");
        List<Lesson> readLessons = new List<Lesson>();
        LessonParser parser = new LessonParser();
        foreach (var info in File.ReadAllText(path).Split(';'))
        {
            try
            {
                readLessons.Add(parser.ParseLessonFromString(info));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                            
        }
        return readLessons;
    }
}