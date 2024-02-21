using System.Collections.Generic;

namespace Utils.Extensions
{
    public static class ListExtensions
    {
        public static void ClearList<T>(this List<T> list)
        {
            list.RemoveAll(x => x == null);
        } 
    }
}