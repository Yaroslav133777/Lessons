namespace LessonsProject;

public class Seminar: Lesson
{
    public bool HasHomework  { get; set; }
    public DateOnly HomeworkDeadLine { get; set; }
    public List<string> DiscussionQuestions { get; set; } = [];
}