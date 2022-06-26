using System;
using System.Collections.Generic;
using System.IO;

namespace Task1Server
{
    internal class FileSaver
    {
        public FileSaver(string path, string fileName)
        {
            this.path = path;
            this.fileName = fileName;

        }

        private readonly string path;
        private readonly string fileName;

        public void Save(IEnumerable<byte[]> bytes)
        {
            var fullPath = path + $"\\{fileName}";
            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
            using (var stream = File.Open(fullPath, FileMode.Append, FileAccess.Write))
            {
                using (var bw = new BinaryWriter(stream))
                {
                    foreach (var elment in bytes)
                        bw.Write(elment);
                }
            }
        }

    }
}
