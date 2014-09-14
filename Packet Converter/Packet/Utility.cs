using System.Collections.Generic;

namespace Packet_Utility.Packet
{
    internal static class Utility
    {
        public static IEnumerable<string> SplitInGroups(this string original, int size)
        {
            int p = 0;
            int l = original.Length;
            while (l - p > size)
            {
                yield return original.Substring(p, size);
                p += size;
            }
            yield return original.Substring(p);
        }
    }
}