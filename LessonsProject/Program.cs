namespace LessonsProject;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите: дату занятия (YYYY.MM.DD), название аудитории и имя преподавателя через запятую");
        
        var lessonInfo = Console.ReadLine().Split(',');
        DateOnly lessonDate = DateOnly.Parse(lessonInfo[0]);
        string audienceName = lessonInfo[1];
        string professorName = lessonInfo[2];
        
        Lesson lesson = new Lesson() {LessonDate = lessonDate, AudienceName = audienceName, ProfessorName = professorName};
        Console.WriteLine($"Дата занятия: {lesson.LessonDate}");
        Console.WriteLine($"Имя преподавателя: {lesson.ProfessorName}");
        Console.WriteLine($"Название аудитории: {lesson.AudienceName}");
    }
}