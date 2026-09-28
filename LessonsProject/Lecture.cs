namespace LessonsProject;

public class Lecture: Lesson
{
    public int MaxStudents { get; set; }
    public bool IsRecorded { get; set; }

    public override string GetInfo()
    {
        string info = base.GetInfo() + "\n" + $"Макс студентов в аудитории {MaxStudents}\n"
                + $"Наличие записи лекции:{IsRecorded}";
        return "";
    }
}