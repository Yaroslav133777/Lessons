namespace LessonsProject;

public class Seminar: Lesson
{
    public bool HasHomework  { get; set; }
    public DateOnly HomeworkDeadLine { get; set; }
    public List<string> DiscussionQuestions { get; set; } = [];
    public override string PrintInfo()
    {
        string duscussionQuestions = string.Join("\n", DiscussionQuestions);
        string info = base.PrintInfo() + "\n" + $"Наличие домашней работы - {HasHomework}\n"  +
                      $"Дедлайн работы - {HomeworkDeadLine}\n" + $"{duscussionQuestions}";
        return info;
    }
}