using System.IO;
using System.Windows.Input;

namespace LibraryApp
{
    public static class cursor
    {
        public static Cursor FromByteArray(byte[] array)
        {
            using (MemoryStream memoryStream = new MemoryStream(array))
            {
                return new Cursor(memoryStream);
            }
        }
    }
}