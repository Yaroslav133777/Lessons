namespace LessonsProject;

public class Program
{
    public static List<List<int>> FindTriplets(List<int> numbers, int targetSum)
    {
        List<List<int>> result = [];
        if (numbers.Count < 3)
            return result;
        for (int i = 0; i < numbers.Count - 2; i++)
        {
            if (numbers[i] + numbers[i + 1] + numbers[i + 2] == targetSum && result.Count < 3)
            {
                result.Add([numbers[i], numbers[i + 1], numbers[i + 2]]);
            }
        }
        return result;
    }
    static void Main(string[] args)
    {
        using LessonApplication lessonApplication = new LessonApplication(args.Contains("--log"));
        Menu menu = new Menu();
        menu.AddItem(new ("Добавить запись",lessonApplication.AddLesson));
        menu.AddItem(new("Показать список записей", lessonApplication.ShowLessons));
        menu.AddItem(new("Считать и добавить данные с файлы", lessonApplication.ReadFromFile));
        menu.AddItem(new("Сохранить в файл", lessonApplication.SaveToFile));
        menu.AddItem(new ("Выход", lessonApplication.Exit));
        while (lessonApplication.IsRunning)
        {
            lessonApplication.PrintMenu(menu);
            string choice = lessonApplication.ReadCommand();
            lessonApplication.ExecuteCommand(menu, choice);
        }
    }
}