using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTransport
{
    public class Hex
    {
        public static string ToHexString(byte[] array)
        {
            return string.Concat(Array.ConvertAll(array, b => b.ToString("x2")));
        }

        public static byte[] FromHexString(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
