using LessonsProject;
using Xunit;

namespace UnitTests;

public class LessonServiceTest
{
    private readonly LessonService _service = new();

    [Fact]
    public void GetLessonsInfo_EmptyList_ReturnsEmptyString()
    {
        Assert.Equal(string.Empty, _service.GetLessonsInfo([]));
    }

    [Fact]
    public void GetLessonsInfo_RegularLesson_ContainsAllFields()
    {
        RegularLesson lesson = new()
        {
            Date = new DateOnly(2024, 1, 15),
            AudienceName = "Ауд.301",
            ProfessorName = "Иванов"
        };

        string info = _service.GetLessonsInfo([lesson]);

        Assert.Contains("15.01.2024", info);
        Assert.Contains("Ауд.301", info);
        Assert.Contains("Иванов", info);
    }

    [Fact]
    public void GetLessonsInfo_Lecture_ContainsSpecificFields()
    {
        Lecture lecture = new()
        {
            Date = new DateOnly(2024, 1, 15),
            AudienceName = "Ауд.301",
            ProfessorName = "Иванов",
            MaxStudents = 100,
            IsRecorded = true
        };

        string info = _service.GetLessonsInfo([lecture]);

        Assert.Contains("100", info);
        Assert.Contains("True", info);
    }

    [Fact]
    public void GetLessonsInfo_Seminar_ContainsSpecificFields()
    {
        Seminar seminar = new()
        {
            Date = new DateOnly(2024, 1, 15),
            AudienceName = "Ауд.301",
            ProfessorName = "Иванов",
            HasHomework = true,
            HomeworkDeadLine = new DateOnly(2024, 1, 20)
        };

        string info = _service.GetLessonsInfo([seminar]);

        Assert.Contains("True", info);
        Assert.Contains("20.01.2024", info);
    }

    [Fact]
    public void GetLessonsInfo_MultipleLessons_AddsSeparatorPerLesson()
    {
        var lessons = new List<RegularLesson>
        {
            new() { Date = new DateOnly(2024, 1, 15), AudienceName = "Ауд.301", ProfessorName = "Иванов" },
            new() { Date = new DateOnly(2024, 1, 16), AudienceName = "Ауд.302", ProfessorName = "Петров" }
        };

        string info = _service.GetLessonsInfo(lessons);

        Assert.Equal(2, info.Split(_service.Separator).Length - 1);
    }
}