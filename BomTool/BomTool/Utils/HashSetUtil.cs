using NPOI.SS.Formula.Functions;

namespace BomTool.Utils
{
    internal class HashSetUtil
    {
        public static HashSet<T> GetIntersection<T>(HashSet<T> set1, HashSet<T> set2)
        {
            var result = new HashSet<T>(set1);
            result.IntersectWith(set2);
            return result;
        }

        public static string HashSetToString<T>(HashSet<T>? set)
        {
            if (set is null)
                return string.Empty;
            return string.Join(",", set);
        }

        public static string EnumerableToString<T>(IEnumerable<T>? enumerable)
        {
            if (enumerable is null)
                return string.Empty;
            return string.Join(",", enumerable);
        }
    }
}
