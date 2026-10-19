namespace LessonsProject;

public class Menu
{
    private List<MenuItem> MenuItems { get; } = new List<MenuItem>();
    public void AddItem(MenuItem item) => MenuItems.Add(item);

    public void Run()
    {
        for (int i = 0; i < MenuItems.Count; i++)
        {
            Console.Write($"{i + 1}.");
            MenuItems[i].Print();
        }
    }

    public MenuItem? Execute(int commandIndex)
    {
        return MenuItems.FirstOrDefault(item => MenuItems.IndexOf(item) + 1 == commandIndex);
    }
}