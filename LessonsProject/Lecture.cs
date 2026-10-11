namespace LessonsProject;

public class Lecture : RegularLesson
{
    public int MaxStudents { get; set; }
    public bool IsRecorded { get; set; }

    public override string GetInfo()
    {
        return base.GetInfo() + "\n" +
               $"Макс студентов в аудитории - {MaxStudents}\n" +
               $"Наличие записи лекции - {IsRecorded}";
    }
}