using System;
using System.IO;


namespace Services
{
    public class FileExtensionService
    {
        public static bool HasExtension(string filename)
        {
            // add other possible extensions here
            return Path.GetExtension(filename).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(filename).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase)
                || Path.GetExtension(filename).Equals(".png", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
