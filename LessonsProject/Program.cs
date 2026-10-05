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
        LessonService service = new LessonService();
        LessonParser parser = new LessonParser();
        TextFileStorage storage = new TextFileStorage();
        bool start = true;
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
                    try
                    {
                        lessons.Add(parser.ParseLessonFromString(Console.ReadLine()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    break;
                }
                case "2":
                {
                    Console.WriteLine(service.GetLessonsInfo(lessons));
                    Console.WriteLine("Enter - продолжить");
                    Console.ReadLine();
                    break;
                }
                case "3":
                {
                    List<Lesson> readLessons; 
                    Console.WriteLine("Путь до файла:");
                    string path = Console.ReadLine().Trim('\'').Trim('\"');
                    try
                    {
                        readLessons = storage.ReadAllLessons(path);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Enter - продолжить");
                        Console.ReadLine();
                        break;
                    }

                    foreach (Lesson lesson in readLessons)
                    {
                        lessons.Add(lesson);
                    }
                    break;
                }
                case "4":
                {
                    Console.WriteLine("Путь к файлу сохранения:");
                    string path = Console.ReadLine().Trim('\'').Trim('\"');
                    storage.WriteAllLessons(path, lessons);
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
        Console.WriteLine(service.GetLessonsInfo(lessons));

    }
}