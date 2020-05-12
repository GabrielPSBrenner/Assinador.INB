using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Helper
{
    public class FileHelper
    {
        public static byte[] ReadFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }

        public static FileStream FSReadFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return f;
        }

        public static Stream MSReadFileURL(string URL)
        {
            WebClient wc = new WebClient();
            MemoryStream f = new MemoryStream(wc.DownloadData(URL));
            return f;
        }

        public static byte[] ReadFileURL(string URL)
        {
            WebClient wc = new WebClient();
            MemoryStream f = new MemoryStream(wc.DownloadData(URL));
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }

    }
}
