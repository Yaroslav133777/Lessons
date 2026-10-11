namespace LessonsProject;

public class LectureParser : IParser<Lecture>
{
    private DateOnly? TryParseDate(string value)
    {
        bool ok = DateOnly.TryParse(value, out DateOnly date);
        return ok ? date : null;
    }
    public Lecture? ParseFromString(string itemInfo)
    {
        string[] parts = itemInfo.Split(',');
        if (parts.Length != 5)
            return null;
        DateOnly? date = TryParseDate(parts[0].Trim());
        if (date == null)
            return null;
        bool maxStudentsOk = int.TryParse(parts[3].Trim(), out int maxStudentsValue);
        if (!maxStudentsOk)
            return null;
        bool isRecorded = parts[4].Trim().ToLower() == "true";
        return new Lecture()
        {
            Date = (DateOnly)date,
            AudienceName = parts[1].Trim(),
            ProfessorName = parts[2].Trim(),
            MaxStudents = maxStudentsValue,
            IsRecorded = isRecorded
        };
    }

    public string FormatToString(Lecture item)
    {
        return $"{item.Date}, {item.AudienceName}, {item.ProfessorName}, {item.MaxStudents}, {item.IsRecorded}";
    }
}