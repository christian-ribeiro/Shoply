namespace Shoply.Arguments.Extensions;

public static class ListExtension
{
    public static List<T> GetDuplicateItem<T>(this List<T> source, T currentItem, Func<T, object> criteria)
    {
        return [.. source.Where(other => !ReferenceEquals(currentItem, other) && Equals(criteria(currentItem), criteria(other)))];
    }
}