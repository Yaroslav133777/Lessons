using LessonsProject;
using Xunit;

namespace Tests;

public class LessonServiceTest
{
    [Fact]
    public void GetLessonsInfo_EmptyList_ReturnsEmptyString()
    {
        LessonService service = new LessonService();

        string info = service.GetLessonsInfo([]);

        Assert.Equal(string.Empty, info);
    }

    [Fact]
    public void GetLessonsInfo_SingleLesson_ContainsLessonInfo()
    {
        RegularLesson lesson = new RegularLesson()
        {
            Date = DateOnly.Parse("2024.01.15"),
            AudienceName = "Ауд.301",
            ProfessorName = "Иванов"
        };
        LessonService service = new LessonService();

        string info = service.GetLessonsInfo([lesson]);

        Assert.Contains("15.01.2024", info);
        Assert.Contains("Ауд.301", info);
        Assert.Contains("Иванов", info);
    }

    [Fact]
    public void GetLessonsInfo_MultipleLessons_UsesSeparator()
    {
        RegularLesson lesson1 = new RegularLesson()
        {
            Date = DateOnly.Parse("2024.01.15"),
            AudienceName = "Ауд.301",
            ProfessorName = "Иванов"
        };
        RegularLesson lesson2 = new RegularLesson()
        {
            Date = DateOnly.Parse("2024.01.16"),
            AudienceName = "Ауд.302",
            ProfessorName = "Петров"
        };
        LessonService service = new LessonService();

        string info = service.GetLessonsInfo([lesson1, lesson2]);

        Assert.Equal(2, info.Split("----------------------").Length - 1);
    }
}