using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Helper
{
    public class Funcoes
    {
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        //public static byte[] ObjToByteArray(object stream)
        //{
        //    ((Stream)stream).Position = 0;
        //    byte[] buffer = new byte[((Stream)stream).Length];
        //    for (int totalBytesCopied = 0; totalBytesCopied < ((Stream)stream).Length;)
        //        totalBytesCopied += ((Stream)stream).Read(buffer, totalBytesCopied, Convert.ToInt32(((Stream)stream).Length) - totalBytesCopied);
        //    return buffer;
        //}

        public static byte[] ToByteArray(Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }



        
    }
}
