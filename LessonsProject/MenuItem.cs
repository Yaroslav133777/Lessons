namespace LessonsProject;

public record MenuItem(string Description, Action Action)
{
    public void Run()
    {
        Action();
    }

    public void Print()
    {
        Console.WriteLine(Description);
    }
}
