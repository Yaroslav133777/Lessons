namespace LessonsProject;

public class Lecture: Lesson
{
    public int MaxStudents { get; set; }
    public bool IsRecorded { get; set; }

    public override string PrintInfo()
    {
        string info = base.PrintInfo() + "\n" + $"Макс студентов в аудитории {MaxStudents}\n"
                + $"Наличие записи лекции:{IsRecorded}";
        return "";
    }
}