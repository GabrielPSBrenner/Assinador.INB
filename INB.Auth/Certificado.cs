﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace INB.Auth
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
            //var teste = ListaCertificados_todos();
            X509Certificate2Collection certificados = new X509Certificate2Collection();
            X509Store stores = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                // Abre o Store
                stores.Open(OpenFlags.ReadOnly);
                // Obtém a coleção dos certificados da Store
                certificados.AddRange(stores.Certificates);

            }
            finally
            {
                stores.Close();
            }         
            return certificados;            
        }

        public static X509Certificate2Collection ListaCertificados_todos()
        {
            X509Certificate2Collection certificados = new X509Certificate2Collection();
            foreach (StoreLocation storeLocation in (StoreLocation[])Enum.GetValues(typeof(StoreLocation)))
            {
                foreach (StoreName storeName in (StoreName[])Enum.GetValues(typeof(StoreName)))
                {
                    if (storeName.ToString() == "AddressBook" || storeName.ToString() == StoreName.My.ToString())
                    {
                        X509Store store = new X509Store(storeName, storeLocation);

                        try
                        {
                            store.Open(OpenFlags.OpenExistingOnly);
                            certificados.AddRange(store.Certificates);
                        }
                        catch (CryptographicException Ex)
                        {
                            throw Ex;
                        }
                    }
                }
            }
            return certificados;
        }

        public static X509Certificate2Collection ListaCertificadosValidos()
        {
            X509Certificate2Collection certificados = ListaCertificados();
            certificados = certificados.Find(X509FindType.FindByTimeValid, DateTime.Now, false);



            X509Certificate2Collection retorno = new X509Certificate2Collection();

            foreach (X509Certificate2 cert in certificados)
            {
                //if ((cert.NotAfter > DateTime.Now && cert.NotBefore < DateTime.Now) && (cert.Issuer.Contains("ICP-Brasil")))
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
    }
}
