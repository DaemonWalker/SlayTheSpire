using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlayTheSpire.Sever.Extenssions
{
    public static class SaveUtils
    {
        private static readonly byte[] KEY;
        private static readonly int KEYLENGTH;
        static SaveUtils()
        {
            KEY = Encoding.UTF8.GetBytes("key");
            KEYLENGTH = KEY.Length;
        }
        public static string Decode(this string save)
        {
            var json = new string(Xor(Convert.FromBase64String(save)).Select(p => (char)p).ToArray())
                .Replace("\n", "")
                .Replace("\r", "");
            return json;
        }
        public static string Encode(this string json)
        {
            return Convert.ToBase64String(Xor(json.Select(p => (byte)p).ToArray()));
        }
        private static byte[] Xor(byte[] input)
        {
            var bytes = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                bytes[i] = (byte)(input[i] ^ KEY[i % KEYLENGTH]);
            }
            return bytes;
        }
    }
}
