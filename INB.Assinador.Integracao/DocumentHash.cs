using System;
using System.Security.Cryptography;
using System.Text;


namespace INB.Assinador.Integracao
{
    public class DocumentHash
    {

        public static byte[] GetHash(byte[] ArrayHash)
        {
            HashAlgorithm hashAlgorithm = SHA256.Create();
            byte[] data = hashAlgorithm.ComputeHash(ArrayHash);
            return data;
        }

        public static String GetHashToString(byte[] ArrayHash)
        {
            HashAlgorithm hashAlgorithm = SHA256.Create();
            byte[] data = hashAlgorithm.ComputeHash(ArrayHash);
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string GetStringHash(string input)
        {
            HashAlgorithm hashAlgorithm  = SHA256.Create();            
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));          
            var sBuilder = new StringBuilder();           
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }         
         
            return sBuilder.ToString();
        }

        
        public static bool VerifyStringHash(string input, string hash)
        {            
            var hashOfInput = GetStringHash(input);         
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
