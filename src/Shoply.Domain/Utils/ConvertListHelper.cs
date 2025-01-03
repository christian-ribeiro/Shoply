using System.Collections;

namespace Shoply.Domain.Utils;

public static class ConvertListHelper
{
    public static List<TOutput> Cast<TOutput>(this IList? inputList) where TOutput : class
    {
        return inputList == null ? [] : (from i in inputList.OfType<dynamic>() select (TOutput)i).ToList();
    }
}