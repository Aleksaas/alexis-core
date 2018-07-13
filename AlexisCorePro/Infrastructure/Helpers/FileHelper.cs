using System.IO;

namespace AlexisCorePro.Infrastructure.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// Converts stream to byte array.
        /// </summary>
        /// <param name="input">Input stream.</param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}

