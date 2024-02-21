using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class ListExtensions
    {
        public static void ClearListFromNulls<T>(this List<T> list)
        {
            list.RemoveAll(x => x == null);
        }

        public static List<T> CleanedListFromNulls<T>(this List<T> list)
        {
            return list.Where(x => x != null).ToList();
        }
    }
}