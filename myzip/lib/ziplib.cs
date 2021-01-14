using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace myziplib
{
    class myziplib
    {
        public List<Dictionary<string, string>> getZipFileLists(string zipFilePath)
        {
            //列出zip所有檔案
            List<Dictionary<string, string>> output = new List<Dictionary<string, string>>();
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        //Console.WriteLine(entry.FullName);
                        var d = new Dictionary<string, string>();
                        d["FullName"] = entry.FullName;
                        d["BaseName"] = basename(d["FullName"]);
                        d["SubName"] = subname(d["FullName"]);
                        //d["Name"] = entry.Name;
                        d["isDir"] = (entry.Name == "") ? "1" : "0";
                        d["CompressedLength"] = entry.CompressedLength.ToString();
                        d["FileSize"] = entry.Length.ToString();
                        d["LastWriteTime"] = entry.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                        output.Add(d);
                    }
                }
                return output;
            }
            catch
            {
                return null;
            }
        }
        public byte[] getZipFileData(string zipFilePath, string fullName)
        {
            //取得某zip裡的資料
            byte[] output = null;
            using (ZipArchive archive = ZipFile.OpenRead(zipFilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (fullName == entry.FullName)
                    {
                        using (var stream = entry.Open())
                        {
                            output = ReadStream(stream, 8192);
                        }
                    }
                }
            }
            return output;
        }
        private string basename(string path)
        {
            return Path.GetFileName(path);
        }
        private string mainname(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        private string subname(string path)
        {
            return Path.GetExtension(path).TrimStart('.');
        }
        
        private byte[] ReadStream(Stream stream, int initialLength)
        {
            if (initialLength < 1)
            {
                initialLength = 32768;
            }
            byte[] buffer = new byte[initialLength];
            int read = 0;
            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            byte[] bytes = new byte[read];
            Array.Copy(buffer, bytes, read);
            return bytes;
        }
    }
}
