namespace LessonsProject;

public class Seminar: RegularLesson
{
    public bool HasHomework { get; set; }
    public DateOnly HomeworkDeadLine { get; set; }

    public override string GetInfo()
    {
        return base.GetInfo() + "\n" + $"Наличие домашней работы - {HasHomework}\n"  +
                      $"Дедлайн работы - {HomeworkDeadLine}\n"; ;
    }
}