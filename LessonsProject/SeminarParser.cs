namespace LessonsProject;

public class SeminarParser : IParser<Seminar>
{
    private DateOnly? TryParseDate(string value)
    {
        bool ok = DateOnly.TryParse(value, out DateOnly date);
        return ok ? date : null;
    }
    public Seminar? ParseFromString(string itemInfo)
    {
        string[] parts = itemInfo.Split(',');
        if (parts.Length != 5)
            return null;
        DateOnly? date = TryParseDate(parts[0].Trim());
        if (date == null)
            return null;
        bool hasHomework = parts[3].Trim().ToLower() == "true";
        DateOnly? deadline = TryParseDate(parts[4].Trim());
        if (deadline == null)
            return null;
        return new Seminar()
        {   Date = (DateOnly)date,
            AudienceName = parts[1].Trim(),
            ProfessorName = parts[2].Trim(),
            HasHomework = hasHomework,
            HomeworkDeadLine = (DateOnly)deadline,
        };
    }

    public string FormatToString(Seminar item)
    {
        return $"{item.Date}, {item.AudienceName}, {item.ProfessorName}, {item.HasHomework}, {item.HomeworkDeadLine}";
    }
}