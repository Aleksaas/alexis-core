using System.Text;

namespace AlexisCorePro.Infrastructure.Helpers
{
    public class TextHelper
    {
        public static byte[] Encode(string text, int encoding)
        {
            return Encoding.GetEncoding(encoding).GetBytes(text);
        }

        public static string Decode(byte[] data, int encoding)
        {
            return Encoding.GetEncoding(encoding).GetString(data);
        }
    }
}
