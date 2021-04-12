using System.IO;
using System.Text;

namespace Web_Development.Utils
{
    /// <summary>
    ///     Class containing useful extension functions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     Reads all bytes from this stream and returns it as a string
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <returns>The output string</returns>
        public static string ReadString(this Stream stream)
        {
            // Create StringBuilder for storing chars
            var builder = new StringBuilder();

            // Read all bytes in the stream
            for (var i = stream.ReadByte(); i != -1; i = stream.ReadByte())
                // Convert to chars and store in StringBuilder
                builder.Append((char) i);

            return builder.ToString();
        }
    }
}