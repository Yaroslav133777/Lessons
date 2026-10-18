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
        while (lessonApplication.IsRunning)
        {
            lessonApplication.PrintMenu();
            string choice = lessonApplication.ReadCommand();
            switch (choice)
            {
                case "1": lessonApplication.AddLesson(); break;
                case "2": lessonApplication.ShowLessons(); break;
                case "3": lessonApplication.ReadFromFile(); break;
                case "4": lessonApplication.SaveToFile(); break;
                case "5": lessonApplication.Exit(); break;
                default:
                    lessonApplication.UnknownCommandMessage();
                    break;
            }
        }
    }
}