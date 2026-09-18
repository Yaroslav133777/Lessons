namespace LessonsProject;
internal class Program
{
    public static List<List<int>> TripletsOfNums(List<int> numbers, int num)
    {
        List<List<int>> result = [];
        if (numbers.Count < 3)
            return result;
        int leftBound = 0;
        while (leftBound <= numbers.Count - 3)
        {
            if (result.Count == 3)
                break;
            var triplets = new List<int>();
            for (int i = leftBound; i < leftBound + 3; i++)
            {
                triplets.Add(numbers[i]);
            }
            
            if (triplets.Sum() == num)
            {
                result.Add(triplets);
            }
            leftBound++;
            
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
            Console.WriteLine("3.Выход");
            string choice = Console.ReadLine().Trim().ToLower();
            switch (choice)
            {
                case "1":
                {
                    Console.WriteLine("Введите: дату занятия (YYYY.MM.DD), название аудитории и имя преподавателя через запятую");
                    var lessonInfo = Console.ReadLine().Split(',');
                    DateOnly lessonDate = DateOnly.Parse(lessonInfo[0]);
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
                        Console.WriteLine($"Дата занятия: {lesson.Date}");
                        Console.WriteLine($"Аудитория: {lesson.AudienceName}");
                        Console.WriteLine($"Имя профессора: {lesson.ProfessorName}");
                    }
                    Console.WriteLine("Enter - продолжить");
                    Console.ReadLine();
                    break;
                }
                case "3":
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
                    break;
                }
                
            }
        }
        Console.Clear();
        Console.WriteLine("Итоговый список записей:");
        foreach (Lesson lesson in lessons)
        {
            Console.WriteLine(separator);
            Console.WriteLine($"Дата занятия: {lesson.Date}");
            Console.WriteLine($"Аудитория: {lesson.AudienceName}");
            Console.WriteLine($"Имя профессора: {lesson.ProfessorName}");
        }
        
    }
}