using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myziplib;
using utility;
namespace myzip
{
    class Program
    {
        static myinclude my = new myinclude();
        static void Main(string[] args)
        {
            //列出所有檔案
            myziplib.myziplib myzip = new myziplib.myziplib();
            string zipFilePath = pwd() + "\\test.zip";
            var p = myzip.getZipFileLists(zipFilePath);
            var output = my.json_format_utf8(my.json_encode(p));
            Console.WriteLine(output);

            //取得資料
            byte[] obyte = myzip.getZipFileData(zipFilePath, "test/folder1/images.jpg");
            string output_file = pwd() + "\\output.jpg";
            my.file_put_contents(output_file, obyte);
            Array.Clear(obyte,0, obyte.Length);
            obyte = null;

        }
        static public string pwd()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}
