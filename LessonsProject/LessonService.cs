namespace LessonsProject;

public class LessonService
{
    public string GetLessonsInfo(IEnumerable<RegularLesson> lessons)
    {
        string separator = "----------------------";
        string result = string.Empty;
        foreach (var lesson in lessons)
        {
            result += lesson.GetInfo() + "\n";
            result += separator + "\n";
        }
        return result;
    }
}