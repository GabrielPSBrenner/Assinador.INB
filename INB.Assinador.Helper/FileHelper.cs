using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using INB.Assinador.Integracao;


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


        public static string FileToBase64(string fileName)
        {
            byte[] Arquivo = ReadFile(fileName);
            return Convert.ToBase64String(Arquivo);
        }

        public static MemoryStream Base64toMemoryStream(string file)
        {
            byte[] Arquivo = Convert.FromBase64String(file);
            MemoryStream Ms = new MemoryStream(Arquivo);
            return Ms;
        }
        public static FileStream Base64toFileStream(string file, string Path, string FileName )
        {
            byte[] Arquivo = Convert.FromBase64String(file);
            FileStream Ms = new FileStream(Path + FileName, FileMode.OpenOrCreate);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(Ms, Arquivo);
            Ms.Flush();           
            return Ms;
        }


       
    }
}
