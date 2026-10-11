using LessonsProject;
using Xunit;

namespace Tests;

public class RegularLessonParserTest
{
    [Fact]
    public void TryParse_ValidInput_ReturnsLesson()
    {
        RegularLessonParser parser = new RegularLessonParser();

        RegularLesson? lesson = parser.ParseFromString("2024.01.15 Ауд.301 Иванов");

        Assert.NotNull(lesson);
        Assert.Equal(DateOnly.Parse("2024.01.15"), lesson.Date);
        Assert.Equal("Ауд.301", lesson.AudienceName);
        Assert.Equal("Иванов", lesson.ProfessorName);
    }

    [Fact]
    public void TryParse_InvalidDateFormat_ReturnsNull()
    {
        RegularLessonParser parser = new RegularLessonParser();

        RegularLesson? lesson = parser.ParseFromString("недата Ауд.301 Иванов");

        Assert.Null(lesson);
    }

    [Fact]
    public void TryParse_WrongPartsCount_ReturnsNull()
    {
        RegularLessonParser parser = new RegularLessonParser();

        RegularLesson? lesson = parser.ParseFromString("2024.01.15 Ауд.301");

        Assert.Null(lesson);
    }

    [Fact]
    public void TryParse_EmptyString_ReturnsNull()
    {
        RegularLessonParser parser = new RegularLessonParser();

        RegularLesson? lesson = parser.ParseFromString("");

        Assert.Null(lesson);
    }
}