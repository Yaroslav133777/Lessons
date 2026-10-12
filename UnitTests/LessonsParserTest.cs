using LessonsProject;
using Xunit;

namespace UnitTests;

public class RegularLessonParserTest
{
    private readonly RegularLessonParser _parser = new();

    [Fact]
    public void Parse_ValidInput_ReturnsLesson()
    {
        RegularLesson? lesson = _parser.ParseFromString("2024.01.15, Ауд.301, Иванов");

        Assert.NotNull(lesson);
        Assert.Equal(new DateOnly(2024, 1, 15), lesson.Date);
        Assert.Equal("Ауд.301", lesson.AudienceName);
        Assert.Equal("Иванов", lesson.ProfessorName);
    }

    [Theory]
    [InlineData("2024.01.15 Ауд.301")]
    [InlineData("")]
    [InlineData("недата, Ауд.301, Иванов")]
    public void Parse_InvalidInput_ReturnsNull(string input)
    {
        Assert.Null(_parser.ParseFromString(input));
    }

    [Fact]
    public void Format_RoundTrips()
    {
        RegularLesson lesson = new()
        {
            Date = new DateOnly(2024, 1, 15),
            AudienceName = "Ауд.301",
            ProfessorName = "Иванов"
        };

        Assert.Equal("15.01.2024, Ауд.301, Иванов", _parser.FormatToString(lesson));
    }
}

public class LectureParserTest
{
    private readonly LectureParser _parser = new();

    [Fact]
    public void Parse_ValidInput_ReturnsLecture()
    {
        Lecture? lecture = _parser.ParseFromString("2024.01.15, Ауд.301, Иванов, 100, true");

        Assert.NotNull(lecture);
        Assert.Equal(new DateOnly(2024, 1, 15), lecture.Date);
        Assert.Equal("Ауд.301", lecture.AudienceName);
        Assert.Equal("Иванов", lecture.ProfessorName);
        Assert.Equal(100, lecture.MaxStudents);
        Assert.True(lecture.IsRecorded);
    }

    [Fact]
    public void Parse_InvalidMaxStudents_ReturnsNull()
    {
        Assert.Null(_parser.ParseFromString("2024.01.15, Ауд.301, Иванов, абв, true"));
    }

    [Fact]
    public void Parse_WrongPartsCount_ReturnsNull()
    {
        Assert.Null(_parser.ParseFromString("2024.01.15, Ауд.301, Иванов, 100"));
    }
}

public class SeminarParserTest
{
    private readonly SeminarParser _parser = new();

    [Fact]
    public void Parse_ValidInput_ReturnsSeminar()
    {
        Seminar? seminar = _parser.ParseFromString("2024.01.15, Ауд.301, Иванов, true, 2024.01.20");

        Assert.NotNull(seminar);
        Assert.Equal(new DateOnly(2024, 1, 15), seminar.Date);
        Assert.Equal("Ауд.301", seminar.AudienceName);
        Assert.Equal("Иванов", seminar.ProfessorName);
        Assert.True(seminar.HasHomework);
        Assert.Equal(new DateOnly(2024, 1, 20), seminar.HomeworkDeadLine);
    }

    [Fact]
    public void Parse_InvalidDeadline_ReturnsNull()
    {
        Assert.Null(_parser.ParseFromString("2024.01.15, Ауд.301, Иванов, true, недата"));
    }

    [Fact]
    public void Parse_WrongPartsCount_ReturnsNull()
    {
        Assert.Null(_parser.ParseFromString("2024.01.15, Ауд.301, Иванов"));
    }

    [Fact]
    public void Format_RoundTrips()
    {
        Seminar seminar = new()
        {
            Date = new DateOnly(2024, 1, 15),
            AudienceName = "Ауд.301",
            ProfessorName = "Иванов",
            HasHomework = true,
            HomeworkDeadLine = new DateOnly(2024, 1, 20)
        };

        Assert.Equal("15.01.2024, Ауд.301, Иванов, True, 20.01.2024", _parser.FormatToString(seminar));
    }
}