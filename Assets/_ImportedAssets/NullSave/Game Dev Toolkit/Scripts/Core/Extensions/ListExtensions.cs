//
// Game Developers Toolkit © 2023 by Thomas W Holtquist is licensed under CC BY-SA 4.0 
// https://www.nullsave.com
//

using System.Collections.Generic;

namespace NullSave.GDTK
{
    [AutoDocLocation("extensions")]
    [AutoDoc("This class contains methods that extends lists.")]
    public static class ListExtensions
    {

        #region Public Methods

        [AutoDoc("Swaps items at indexes")]
        [AutoDocParameter("List")]
        [AutoDocParameter("First index to swap")]
        [AutoDocParameter("Second index to swap")]
        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }
        #endregion

    }
}
