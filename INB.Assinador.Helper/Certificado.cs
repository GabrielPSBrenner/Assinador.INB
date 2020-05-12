using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Helper
{
    public class Certificado
    {
        public enum TipoCertificado
        {
            file = 1,
            url = 2
        }
        public static X509Certificate2Collection ListaCertificados()
        {
            X509Store stores = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                // Abre o Store
                stores.Open(OpenFlags.ReadOnly);

                // Obtém a coleção dos certificados da Store
                X509Certificate2Collection certificados = stores.Certificates;
                return certificados;
            }
            finally
            {
                stores.Close();

            }
        }

        public static X509Certificate2Collection ListaCertificadosValidos()
        {
            X509Certificate2Collection certificados = ListaCertificados();
            certificados = certificados.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection retorno = new X509Certificate2Collection();

            foreach (X509Certificate2 cert in certificados)
            {
                if ((cert.HasPrivateKey && cert.NotAfter > DateTime.Now && cert.NotBefore < DateTime.Now) && (cert.Issuer.Contains("ICP-Brasil")))
                {
                    retorno.Add(cert);
                }
            }

            return retorno;
        }        

        public static X509Certificate2 RetornaCertificadoArquivo(string file, TipoCertificado Tipo, string senha="")
        {
           
            //Create X509Certificate2 object from .cer file.
            byte[] rawData;
            if (Tipo == TipoCertificado.file)
            {
                rawData = FileHelper.ReadFile(file);
            }
            else
            {
                rawData = FileHelper.ReadFileURL(file);
            }
            X509Certificate2 x509 = new X509Certificate2(rawData, senha);                        
            return x509;
        }

       
    }
}
