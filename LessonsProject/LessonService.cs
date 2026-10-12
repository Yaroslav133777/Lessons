namespace LessonsProject;

public class LessonService
{
    public string Separator { get; } = "----------------------";
    public string GetLessonsInfo(IEnumerable<RegularLesson> lessons)
    {
        string result = string.Empty;
        foreach (var lesson in lessons)
        {
            result += lesson.GetInfo() + "\n";
            result += Separator + "\n";
        }
        return result;
    }
}