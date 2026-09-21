namespace LessonsProject;

public class Lesson
{
    public DateOnly Date { get; set; }
    public string AudienceName { get; set; }
    public string ProfessorName { get; set; }

    public virtual string PrintInfo()
    {
        string info = $"Дата занятия - {Date}\n" +
                      $"Аудитория - {AudienceName}\n" +
                      $"Имя преподавателя - {ProfessorName}\n";
        return info;
    }
    
}