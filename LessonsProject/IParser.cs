namespace LessonsProject;

public interface IParser <T>
{
    public T? ParseFromString(string itemInfo);
    public string FormatToString(T item);
}