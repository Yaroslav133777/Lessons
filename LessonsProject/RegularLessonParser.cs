namespace LessonsProject;

public class RegularLessonParser : IParser<RegularLesson>
{
    public RegularLesson? ParseFromString(string lessonInfo)
    {
        string[] lessonParts = lessonInfo.Split(',');
        if (lessonParts.Length != 3)
            return null;
        bool tryDate = DateOnly.TryParse(lessonParts[0].Trim(), out DateOnly date);
        if (!tryDate)
            return null;
        string audienceName = lessonParts[1].Trim();
        string professorName = lessonParts[2].Trim();
        return new RegularLesson() {Date = date, AudienceName = audienceName, ProfessorName = professorName};
    }

    public string FormatToString(RegularLesson item)
    {
        return $"{item.Date}, {item.AudienceName}, {item.ProfessorName}";
    }
}