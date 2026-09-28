namespace LessonsProject;
public class Program
{
    public static List<List<int>> TripletsOfNums(List<int> numbers, int num)
    {
        List<List<int>> result = [];
        if (numbers.Count < 3)
            return result;
        for (int i = 0; i < numbers.Count - 2; i++)
        {
            if (numbers[i] + numbers[i + 1] + numbers[i + 2] == num && result.Count < 3)
            {
                result.Add([numbers[i], numbers[i + 1], numbers[i + 2]]);
            }
        }
        return result;
        
    }
    static void Main()
    {
        List<Lesson> lessons = [];
        bool start = true;
        string separator = "----------------------";
        while (start)
        {
            Console.Clear();
            Console.WriteLine("1.Добавить запись");
            Console.WriteLine("2.Показать список записей");
            Console.WriteLine("3.Считать и добавить данные с файлы");
            Console.WriteLine("4.Сохранить в файл");
            Console.WriteLine("5.Выход");
            
            string choice = Console.ReadLine().Trim().ToLower();
            switch (choice)
            {
                case "1":
                {
                    Console.WriteLine("Введите: дату занятия (YYYY.MM.DD), название аудитории и имя преподавателя через запятую");
                    var lessonInfo = Console.ReadLine().Split(',');
                    while (lessonInfo.Length < 3 || lessonInfo.Length > 3)
                    {
                        Console.WriteLine("Неверный формат!");
                        lessonInfo = Console.ReadLine().Split(',');
                    }
                    bool tryLessonDate = DateOnly.TryParse(lessonInfo[0], out DateOnly lessonDate);
                    while (tryLessonDate == false)
                    {
                        Console.WriteLine("Неверный формат даты!");
                        tryLessonDate = DateOnly.TryParse(lessonInfo[0], out lessonDate);
                    }
                    string audienceName = lessonInfo[1].Trim();
                    string professorName = lessonInfo[2].Trim();
                    Lesson lesson = new Lesson() {Date = lessonDate, AudienceName = audienceName, ProfessorName = professorName};
                    lessons.Add(lesson);
                    break;
                }
                case "2":
                {
                    foreach (Lesson lesson in lessons)
                    {
                        Console.WriteLine(separator);
                        Console.WriteLine(lesson.GetInfo());
                    }
                    Console.WriteLine("Enter - продолжить");
                    Console.ReadLine();
                    break;
                }
                case "3":
                {
                    Console.WriteLine("Путь до файла:");
                    string path = Console.ReadLine().Trim('\'').Trim('\"');
                    if (!Path.Exists(path))
                    {
                        Console.WriteLine("Путь до файла не существует!");
                        Console.WriteLine("Enter - продолжить");
                        Console.ReadLine();
                        break;
                    }

                    string[] lessonsInfo;
                    try
                    {
                        lessonsInfo = File.ReadAllText(path).Split(';');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка - " + ex.Message);
                        Console.WriteLine("Enter - продолжить");
                        Console.ReadLine();
                        break;
                    }
                    Console.WriteLine("Добавить данные в список?[y/n]");
                    string answer = Console.ReadLine().Trim().ToLower();
                    if (answer == "y")
                    {
                        foreach (var info in lessonsInfo)
                        {
                            var lessonInfo = info.Split(',');
                            if (lessonInfo.Length < 3 || lessonInfo.Length > 3)
                            {
                               Console.WriteLine("Неверный формат объекта");
                               continue;
                            }
                            bool tryLessonDate = DateOnly.TryParse(lessonInfo[0], out DateOnly lessonDate);
                            if (tryLessonDate == false)
                            {
                                Console.WriteLine("Неверный формат даты");
                                continue;
                            }
                            string audienceName = lessonInfo[1].Trim();
                            string professorName = lessonInfo[2].Trim();
                            Lesson lesson = new Lesson() {Date = lessonDate, AudienceName = audienceName, ProfessorName = professorName};
                            lessons.Add(lesson);
                            
                        }
                    }
                    break;
                }
                case "4":
                {
                    Console.WriteLine("Путь к файлу сохранения:");
                    string path = Console.ReadLine();
                    if (!Path.Exists(path))
                    {
                        Console.WriteLine("Путь не существует");
                        break;
                    }
                    using StreamWriter sw = new StreamWriter(path);
                    foreach (var lesson in lessons)
                    {
                        string lessonInfo = $"{lesson.Date}, {lesson.AudienceName}, {lesson.ProfessorName};";
                        try
                        {
                            sw.WriteLine(lessonInfo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Ошибка" + ex.Message);
                        }
                    }
                    break;
                }
                case "5":
                {
                    Console.WriteLine("Завершить программу? [y/n]");
                    string answer = Console.ReadLine().Trim().ToLower();
                    if (answer == "y")
                    {
                        start = false;
                    }
                    break;
                }
                default:
                {
                    Console.WriteLine("Неизвестная команда");
                    Console.WriteLine("Enter - продолжить");
                    Console.ReadLine();
                    break;
                }
                
            }
        }
        Console.Clear();
        Console.WriteLine("Итоговый список записей:");
        foreach (Lesson lesson in lessons)
        {
            Console.WriteLine(separator);
            Console.WriteLine(lesson.GetInfo());
        }
        
    }
}