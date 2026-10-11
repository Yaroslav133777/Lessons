using LessonsProject;
using Xunit;

namespace Tests;

public class LessonFileServiceTest
{
    [Fact]
    public void WriteAllLessons_And_ReadAllLessons_RoundTrip()
    {
        string path = Path.GetTempFileName();
        try
        {
            List<RegularLesson> lessons = [new RegularLesson()
            {
                Date = DateOnly.Parse("2024.01.15"),
                AudienceName = "Ауд.301",
                ProfessorName = "Иванов"
            }];
            LessonFileService service = new LessonFileService();
            RegularLessonParser parser = new RegularLessonParser();

            service.WriteAllLessons(path, lessons, parser);
            List<RegularLesson> readLessons = service.ReadAllLessons(path, parser).ToList();

            Assert.Single(readLessons);
            Assert.Equal(lessons[0].Date, readLessons[0].Date);
            Assert.Equal(lessons[0].AudienceName, readLessons[0].AudienceName);
            Assert.Equal(lessons[0].ProfessorName, readLessons[0].ProfessorName);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void ReadAllLessons_NonexistentFile_Throws()
    {
        LessonFileService service = new LessonFileService();
        RegularLessonParser parser = new RegularLessonParser();

        Assert.ThrowsAny<Exception>(() => service.ReadAllLessons("nonexistent/path/file.txt", parser).ToList());
    }
}