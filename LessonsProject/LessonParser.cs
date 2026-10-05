namespace LessonsProject;

public class LessonParser
{
    public Lesson ParseLessonFromString(string lessonInfo)
    {
        string[] lessonParts = lessonInfo.Split(' ');
        if (lessonParts.Length < 3 || lessonParts.Length > 3)
        {
            throw new Exception("Неверный формат строки");
        }
        bool tryDate = DateOnly.TryParse(lessonParts[0], out DateOnly date);
        string audienceName = lessonParts[1];
        string professorName = lessonParts[2];
        if (!tryDate)
            throw new Exception("Неверный формат даты");
        return new Lesson() {Date = date, AudienceName = audienceName, ProfessorName = professorName};
    }
}