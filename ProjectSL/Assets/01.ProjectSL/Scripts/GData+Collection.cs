using System.Collections;
using System.Collections.Generic;
using System.Linq;
public static partial class GData
{
    /// <summary>
    /// Collection이 유효한지 확인하는 함수 
    /// </summary>
    /// <param name="colleciton">Collection</param>
    /// <typeparam name="T">Generic</typeparam>
    /// <returns>
    /// 유효 : True, 유효하지 않으면 : False
    /// </returns>
    public static bool IsValidCollection<T>(this IEnumerable<T> colleciton)
    {
        if (colleciton == null || !colleciton.Any())
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 해당 콜렉션이 유효한지 value 요소가 존재하는지 확인하는 함수
    /// </summary>
    /// <param name="collection">Collection</param>
    /// <param name="value">값</param>
    /// <typeparam name="T">Generic</typeparam>
    /// <returns>
    /// 유효 : True, 유효하지 않으면 : False
    /// </returns>
    public static bool IsValidCollectionElement<T>(this IEnumerable<T> collection, T value)
    {
        if (collection == null || !collection.Any())
        {
            return false;
        }

        return collection.Contains(value);
    }
}
