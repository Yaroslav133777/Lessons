namespace LessonsProject;

public class Menu
{
    private List<MenuItem> MenuItems { get; } = new List<MenuItem>();
    public void AddItem(MenuItem item) => MenuItems.Add(item);

    public Menu(List<MenuItem> items)
    {
        foreach (var item in items)
            AddItem(item);
    }

    public void Run()
    {
        while (true)
        {
            Print();
            string command = Console.ReadLine().Trim();

            if (!int.TryParse(command, out int commandIndex))
            {
                ShowUnknownCommand();
                continue;
            }

            MenuItem? item = Execute(commandIndex);
            if (item == null)
            {
                ShowUnknownCommand();
                continue;
            }

            item.Run();
        }
    }

    private static void ShowUnknownCommand()
    {
        Console.WriteLine("Неизвестная команда");
        WaitForEnter();
    }

    private void Print()
    {
        Console.Clear();
        for (int i = 0; i < MenuItems.Count; i++)
        {
            Console.Write($"{i + 1}.");
            MenuItems[i].Print();
        }
    }

    private MenuItem? Execute(int commandIndex)
    {
        if (commandIndex < 1 || commandIndex > MenuItems.Count)
            return null;

        return MenuItems[commandIndex - 1];
    }

    private static void WaitForEnter()
    {
        Console.WriteLine("Enter - продолжить");
        Console.ReadLine();
    }
}
