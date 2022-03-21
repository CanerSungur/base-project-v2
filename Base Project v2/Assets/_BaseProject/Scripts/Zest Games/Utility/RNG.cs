using System.Collections.Generic;
using UnityEngine;

namespace ZestGames.Utility
{
    public static class RNG
    {
        /// <summary>
        /// Rolls dice and returns true according to chance you enter.
        /// </summary>
        /// <param name="chance">Enter between 0 - 100</param>
        public static bool RollDice(int chance)
        {
            int dice = Random.Range(1, 101);

            if (dice <= chance)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void ShuffleList<T>(this List<T> ts)
        {
            var count = ts.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = Random.Range(i, count);
                var tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }
}

