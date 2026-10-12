using LessonsProject;
using Xunit;

namespace UnitTests;

public class LessonFileServiceTest
{
    private readonly LessonFileService _service = new();

    [Fact]
    public void WriteAllLessons_And_ReadAllLessons_RoundTrip_RegularLesson()
    {
        string path = MakeTempFile();

        try
        {
            RegularLesson lesson = new()
            {
                Date = new DateOnly(2024, 1, 15),
                AudienceName = "Ауд.301",
                ProfessorName = "Иванов"
            };

            _service.WriteLessons(path, new[] { lesson }, new RegularLessonParser());
            RegularLesson read = _service.ReadLessons(path, new RegularLessonParser()).Single()!;

            Assert.Equal(lesson.Date, read.Date);
            Assert.Equal(lesson.AudienceName, read.AudienceName);
            Assert.Equal(lesson.ProfessorName, read.ProfessorName);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void ReadAllLessons_YieldsNullForInvalidLines()
    {
        string path = MakeTempFile();

        try
        {
            File.WriteAllText(path, "мусор;\n2024.01.15, Ауд.301, Иванов;");

            List<RegularLesson?> read = _service.ReadLessons(path, new RegularLessonParser()).ToList();

            Assert.Equal(2, read.Count);
            Assert.Null(read[0]);
            Assert.Equal("Иванов", read[1]!.ProfessorName);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void ReadAllLessons_NonexistentFile_Throws()
    {
        Assert.ThrowsAny<Exception>(() => _service.ReadLessons("nonexistent/path/file.txt", new RegularLessonParser()).ToList());
    }

    [Fact]
    public void WriteAllLessons_And_ReadAllLessons_RoundTrip_Lecture()
    {
        string path = MakeTempFile();

        try
        {
            Lecture lecture = new()
            {
                Date = new DateOnly(2024, 1, 15),
                AudienceName = "Ауд.301",
                ProfessorName = "Иванов",
                MaxStudents = 100,
                IsRecorded = true
            };

            _service.WriteLessons(path, new[] { lecture }, new LectureParser());
            Lecture read = _service.ReadLessons(path, new LectureParser()).Single()!;

            Assert.Equal(lecture.Date, read.Date);
            Assert.Equal(lecture.MaxStudents, read.MaxStudents);
            Assert.Equal(lecture.IsRecorded, read.IsRecorded);
        }
        finally
        {
            File.Delete(path);
        }
    }

    private static string MakeTempFile()
    {
        return Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.txt");
    }
}