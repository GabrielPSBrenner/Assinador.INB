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

        public static X509Certificate2Collection ListaCertificadosValidosSimplificado()
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

        public static X509Certificate2 RetornaCertificadoArquivo(string file, TipoCertificado Tipo, string senha = "")
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

        public static X509Certificate2 CertificadoArquivo(string certificatePath, string certificatePassword)
        {
            try
            {

                X509Certificate2 cert = new X509Certificate2(certificatePath, certificatePassword);
                return cert;
            }
            catch (Exception ex)
            {
                throw new Exception("Certificado não encontrado.", ex);
            }
        }


        public static void InstallCertificate(string certificatePath, string certificatePassword)
        {
            try
            {
                var serviceRuntimeUserCertificateStore = new X509Store(StoreName.My);
                serviceRuntimeUserCertificateStore.Open(OpenFlags.ReadWrite);
                X509Certificate2 cert = CertificadoArquivo(certificatePath, certificatePassword);

                //VERIFICA SE JÁ ESTÁ INSTALADO
                X509Certificate2Collection certificados = serviceRuntimeUserCertificateStore.Certificates;
                bool achou = false;
                foreach (X509Certificate2 verCert in certificados)
                {
                    if (verCert.SerialNumber == cert.SerialNumber)
                    {
                        achou = true;
                        break;
                    }
                }

                if (!achou)
                {
                    serviceRuntimeUserCertificateStore.Add(cert);
                }                
                serviceRuntimeUserCertificateStore.Close();                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void InstalaCertificadoTimeStamp(bool fromURL = false)
        {
            try
            {
                var serviceRuntimeUserCertificateStore = new X509Store(StoreName.My);
                serviceRuntimeUserCertificateStore.Open(OpenFlags.ReadWrite);
                X509Certificate2 cert;
                               
                if (fromURL)
                {
                    Stream St = Helper.FileHelper.MSReadFileURL("https://freetsa.org/files/tsa.crt");
                    BinaryReader br = new BinaryReader(St);
                    byte[] Certificado;
                    Certificado = br.ReadBytes((int)St.Length);
                    St.Close();
                    cert = new X509Certificate2(Certificado, "");
                }
                else
                {
                    cert = new X509Certificate2(Properties.Resources.tsa, "");
                }

                //BUSCA TODOS OS CERTIFICADOS DA COLEÇÃO DE CERTIFICADOS INSTALADOS
                X509Certificate2Collection certificados = serviceRuntimeUserCertificateStore.Certificates;
                bool achou = false;
                

                //VERIFICA SE JÁ ESTÁ INSTALADO
                foreach(X509Certificate2 verCert in certificados)
                {
                    if (verCert.SerialNumber == cert.SerialNumber)
                        {
                        achou = true;
                        break;
                    }
                }

                if (!achou)
                { 
                    serviceRuntimeUserCertificateStore.Add(cert);
                }
                serviceRuntimeUserCertificateStore.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
