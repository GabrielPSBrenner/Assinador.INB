using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using iTextSharp.text;
using iTextSharp.text.log;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using INB.Assinador.Helper;

namespace INB.Assinador.Model
{
    public class AssinaComTokenITextSharp
    {
        public static object hashAlgorithm { get; private set; }

      
        private static void ConfiguraAparenciaAssinatura(PdfSignatureAppearance signatureAppearance, string Reason, string Contact, string Location, string Creator, Bitmap bmp, float Altura, float Largura, float X, float Y, int Rotation, int Pagina, PdfReader pdfReader)
        {
            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(bmp, System.Drawing.Imaging.ImageFormat.Jpeg);

            signatureAppearance.Reason = Reason;
            signatureAppearance.Contact = Contact;
            signatureAppearance.Location = "Indústrias Nucleares do Brasil S/A - INB";
            signatureAppearance.SignatureCreator = "Assinador da INB - Desenvolvimento Interno";
            signatureAppearance.SignatureGraphic = pic;
            signatureAppearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC;


            float Left, Top;
            float Width, Height;
            float LarguraAssinatura = Largura;
            float AlturaAssinatura = Altura;
            float X_Ajustado, Y_Ajustado;

            iTextSharp.text.Rectangle cropBox = pdfReader.GetCropBox(Pagina);
            iTextSharp.text.Rectangle oRetangulo;
            if (Rotation == 90 || Rotation == 270)
            {
                X_Ajustado = (float)X;
                Y_Ajustado = (float)Y ;

                if (Rotation ==270 )
                {
                    Y_Ajustado -= 5;
                    Top = cropBox.GetLeft(X_Ajustado);                                                         
                    Width = cropBox.GetLeft(X_Ajustado + Largura);
                    Left = cropBox.GetRight(Y_Ajustado);
                    Height = cropBox.GetRight(Y_Ajustado + Altura);
                    oRetangulo = new iTextSharp.text.Rectangle(Top, Left, Width, Height);
                 }
                else
                {
                    Y_Ajustado -= 5;
                    Top = cropBox.GetRight(X_Ajustado);
                    Width = cropBox.GetRight(X_Ajustado + Largura);
                    Left = cropBox.GetLeft(Y_Ajustado);
                    Height = cropBox.GetLeft(Y_Ajustado + Altura);
                    oRetangulo = new iTextSharp.text.Rectangle(Top, Left, Width, Height);
                }
            }
            else
            {
                X_Ajustado = (float)X;
                Y_Ajustado = (float)Y - 5;
                Left = cropBox.GetLeft(X_Ajustado);
                Top = cropBox.GetTop(Y_Ajustado);
                Width = cropBox.GetLeft(X_Ajustado + LarguraAssinatura);
                Height = cropBox.GetTop(Y_Ajustado + AlturaAssinatura);
                oRetangulo = new iTextSharp.text.Rectangle(Left, Height, Width, Top);
            }   
            signatureAppearance.SetVisibleSignature(oRetangulo, Pagina, "sig" + DateTime.Now.ToString("ddMMyyHHmmss"));
        }

        private static SignaturePolicyInfo PoliticaDaAssinatura()
        {
            string PolicyIdentifier = "2.16.76.1.7.1.2.2.3";
            string PolicyDigestAlgorithm = "SHA-256";
            string PolicyUriSource = "http://politicas.icpbrasil.gov.br/LPA_CAdES.der";           
            byte[] PolicyHash = null;
            Helper.MyPolicy MyPolicyBase = MontaPolitica.getHashPolitica(PolicyUriSource, PolicyIdentifier, PolicyDigestAlgorithm, "LPA_CAdES.der");
            List<string> MyPolicyAuth = MontaPolitica.getHashPoliticaEspecifica(MyPolicyBase.SubURLPolicy, PolicyIdentifier, PolicyDigestAlgorithm, "PA_AD_RT_v2_3.der");
            string Hash = MyPolicyAuth[2].Replace("#", "");
            PolicyHash = INB.Assinador.Helper.Funcoes.StringToByteArray(Hash);
            string strBase64 = Convert.ToBase64String(PolicyHash);
            SignaturePolicyInfo spi = new SignaturePolicyInfo(PolicyIdentifier, strBase64, PolicyDigestAlgorithm, MyPolicyBase.SubURLPolicy);
            return spi;
        }

        public static void AssinaComToken(Stream File, out byte[] SignFile, CertSimples cert, float X, float Y, int Pagina, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-1", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {
            int Largura = 155;
            int Altura = 63;
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.Certificado.RawData) };
            IExternalSignature externalSignature = new X509Certificate2Signature(cert.Certificado, MyDigestAlgorithm);
            PdfReader pdfReader = new PdfReader(File);
            MemoryStream signedPdf = new MemoryStream();
            //cria a assinatura
            //PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0', "temp" + signedPdf, true);

            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\";

            PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0', path + DateTime.Now.ToString("hhMMddHHmmss") + ".pdf", true);

            Bitmap bmp = Graphic.ConfiguraBMP(cert, out Altura, Tipo);
            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;

            ConfiguraAparenciaAssinatura(signatureAppearance, Reason, Contact, Location, Creator, bmp, Altura, Largura, X, Y, Rotation, Pagina, pdfReader);   

            TSAClientBouncyCastle tsaClient = null;
            if (AddTimeStamper)
            {                           
                tsaClient = new TSAClientBouncyCastle(urlTimeStamper, timeStampUser, timeStampPass, TSAClientBouncyCastle.DEFAULTTOKENSIZE, MyDigestAlgorithm);
            }

            IOcspClient ocspClient = new OcspClientBouncyCastle();
            List<ICrlClient> crlList = new List<ICrlClient>();
            crlList.Add(new CrlClientOnline(chain));



            

            if (AplicaPolitica)
            {     
                        
                SignaturePolicyInfo spi = PoliticaDaAssinatura();
                MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, crlList, ocspClient, tsaClient, 0, CryptoStandard.CADES, spi);
            }
            else
            {
                MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, crlList, ocspClient, tsaClient, 0, CryptoStandard.CADES);
            }

            try
            {
                SignFile = signedPdf.ToArray();
                //SignFile = INB.Assinador.Helper.Funcoes.ToByteArray(teste);
                
                //MemoryStream teste = (MemoryStream)signatureAppearance.TempFile;
               
                //signedPdf.Flush();
                //SignFile
               // SignFile = new MemoryStream(ArquivoAssinado);
                // signedPdf.CopyTo();
                try
                {
                    signedPdf.Close();
                    signedPdf.Dispose();
                }
                catch { }
            }
            catch(Exception ex)
            {
                SignFile = null;
                throw ex;
            }
            try
            {
                signedPdf.Close();
            }
            catch(Exception ex) {}
            pdfReader.Close();
            try
            {
                pdfReader.Dispose();
            }
            catch { }
        }


        //public static void AssinaComToken_OLD(string FileName, string SignFileName, X509Certificate2 cert, float X, float Y, int Pagina, double Escala, bool SeloCargo = false, bool SeloCREA = false, bool SeloCRM = false, string Cargo = "", string CREACRM = "", bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-1", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", bool SeloCertifico = false)
        //{
        //    string SourcePdfFileName = FileName;
        //    string DestPdfFileName = SignFileName;
        //    int Largura = 155;
        //    int Altura = 63;
        //    Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
        //    Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
        //    IExternalSignature externalSignature = new X509Certificate2Signature(cert, MyDigestAlgorithm);
        //    PdfReader pdfReader = new PdfReader(SourcePdfFileName);
        //    FileStream signedPdf = new FileStream(DestPdfFileName, FileMode.Create, FileAccess.ReadWrite);  //the output pdf file
        //                                                                                                    //cria a assinatura
        //    PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0', "temp" + signedPdf, true);
        //    PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
        //    Bitmap bmp = INB.Assinador.Helper.Graphic.ConfiguraBMP(cert, SeloCargo, SeloCREA, SeloCRM, Cargo, CREACRM, out Altura, SeloCertifico);

        //    //CONFIGURA A APARÊNCIA DO SELO DA ASSINATURA.
        //    ConfiguraAparenciaAssinatura(signatureAppearance, Reason, Contact, Location, Creator, bmp, Altura, Largura, X, Y, Escala, Pagina, pdfReader);            
            
        //    //ADICIONA O CARIMBO DO TEMPO.
        //   TSAClientBouncyCastle tsaClient = null;
        //    if (AddTimeStamper)
        //    {
        //        //urlTimeStamper = http://timestamp.globalsign.com/scripts/timestamp.dll
        //        //urlTimeStamper = "http://timestamp.apple.com/ts01";
        //        tsaClient = new TSAClientBouncyCastle(urlTimeStamper, timeStampUser, timeStampPass, TSAClientBouncyCastle.DEFAULTTOKENSIZE, MyDigestAlgorithm);
        //    }
        //    IOcspClient ocspClient = new OcspClientBouncyCastle();
        //    List<ICrlClient> crlList = new List<ICrlClient>();
        //    crlList.Add(new CrlClientOnline(chain));

        //    //Nota 2: O hash da política de assinatura no atributo id-aa-ets-sigPolicyId da assinatura deve ser o hash interno que está na própria PA e não o hash da PA que se encontra publicada na LPA.
        //    if (AplicaPolitica)
        //    {
        //            SignaturePolicyInfo spi = PoliticaDaAssinatura();
        //        MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, crlList, ocspClient, tsaClient, 0, CryptoStandard.CADES, spi);
        //    }
        //    else
        //    {
        //        MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, crlList, ocspClient, tsaClient, 0, CryptoStandard.CADES);
        //    }
        //    try { signedPdf.Flush(); }
        //    catch { }
        //    try { signedPdf.Close(); } catch { };
        //    pdfReader.Close();
        //    try { 
        //    pdfReader.Dispose();
        //    }
        //    catch { }
        //}

        public static void AssinaComToken(string FileName, string SignFileName, CertSimples cert, float X, float Y, int Pagina, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-1", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {
            string SourcePdfFileName = FileName;
            string DestPdfFileName = SignFileName;
            int Largura = 155;
            int Altura = 63;
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();

            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.Certificado.RawData) };


            //IExternalSignature externalSignature = new X509Certificate2Signature(cert.Certificado, MyDigestAlgorithm);

            RSACryptoServiceProvider rsa;
            RSACryptoServiceProvider Provider;
            IExternalSignature externalSignature = null;


            if (cert.Certificado.PrivateKey is RSACryptoServiceProvider)
            {
                rsa = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);
            }
            else 
            {
                rsa = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);               
            }

            PdfReader pdfReader = new PdfReader(SourcePdfFileName);
            FileStream signedPdf = new FileStream(DestPdfFileName, FileMode.Create, FileAccess.ReadWrite);  //the output pdf file

            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\";//cria a assinatura
            PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, signedPdf, '\0', path + DateTime.Now.ToString("yyyyMMddHHmmss") +".pdf", true);

            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
            Bitmap bmp = INB.Assinador.Model.Graphic.ConfiguraBMP(cert, out Altura, Tipo);

            //CONFIGURA A APARÊNCIA DO SELO DA ASSINATURA.
            ConfiguraAparenciaAssinatura(signatureAppearance, Reason, Contact, Location, Creator, bmp, Altura, Largura, X, Y, Rotation, Pagina, pdfReader);

            //ADICIONA O CARIMBO DO TEMPO.
            TSAClientBouncyCastle tsaClient = null;
            if (AddTimeStamper)
            {
                //urlTimeStamper = http://timestamp.globalsign.com/scripts/timestamp.dll
                //urlTimeStamper = "http://timestamp.apple.com/ts01";
                tsaClient = new TSAClientBouncyCastle(urlTimeStamper, timeStampUser, timeStampPass, TSAClientBouncyCastle.DEFAULTTOKENSIZE, MyDigestAlgorithm);
            }
            IOcspClient ocspClient = new OcspClientBouncyCastle();
            List<ICrlClient> crlList = new List<ICrlClient>();
            crlList.Add(new CrlClientOnline(chain));

            //Nota 2: O hash da política de assinatura no atributo id-aa-ets-sigPolicyId da assinatura deve ser o hash interno que está na própria PA e não o hash da PA que se encontra publicada na LPA.
            if (AplicaPolitica)
            {
                SignaturePolicyInfo spi = PoliticaDaAssinatura();
                MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, crlList, ocspClient, tsaClient, 0, CryptoStandard.CADES, spi);
            }
            else
            {
                MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, crlList, ocspClient, tsaClient, 0, CryptoStandard.CADES);
            }
            try { signedPdf.Flush(); }
            catch { }
            try { signedPdf.Close(); } catch { };
            pdfReader.Close();
            try
            {
                pdfReader.Dispose();
            }
            catch { }
        }


        public static void AssinaPDF(Stream File, out byte[] SignFile, CertSimples cert, int Pagina, float X, float Y, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-1", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM="")
        {
            LoggerFactory.GetInstance().SetLogger(new SysoLogger());
            X509Store x509Store = new X509Store("My");
            x509Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificates = new X509Certificate2Collection();
            X509Certificate2Collection Certificados = x509Store.Certificates;
            //foreach (X509Certificate2 cert in Certificados)
            //{
            //    if (cert.SerialNumber == SerialNumber)
            //    {
                    certificates.Add(cert.Certificado);
            //    }
            //}
            IList<X509Certificate> chain = new List<X509Certificate>();
            X509Certificate2 pk = null;
            if (certificates.Count > 0)
            {
                X509Certificate2Enumerator certificatesEn = certificates.GetEnumerator();
                certificatesEn.MoveNext();
                pk = certificatesEn.Current;
                X509Chain x509chain = new X509Chain();
                x509chain.Build(pk);
                foreach (X509ChainElement x509ChainElement in x509chain.ChainElements)
                {
                    chain.Add(DotNetUtilities.FromX509Certificate(x509ChainElement.Certificate));
                }
            }
            //remover
            AssinaComToken(File, out SignFile, cert, X, Y, Pagina, Rotation, AddTimeStamper, urlTimeStamper, timeStampUser, timeStampPass, Reason, AplicaPolitica, MyDigestAlgorithm, Contact,Location, Creator, Tipo, Cargo, CREACRM);
            return;
        }

        public static void AssinaPDF(string FileName, string SignFileName, CertSimples oCertificado, int Pagina, float X, float Y, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-1", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {
            LoggerFactory.GetInstance().SetLogger(new SysoLogger());
            X509Store x509Store = new X509Store("My");
            x509Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certificates = new X509Certificate2Collection();
            //X509Certificate2Collection Certificados = x509Store.Certificates;
            //foreach (X509Certificate2 cert in Certificados)
            //{
            //    if (cert.SerialNumber == SerialNumber)
            //    {
                    certificates.Add(oCertificado.Certificado);
            //    }
            //}
            IList<X509Certificate> chain = new List<X509Certificate>();
            X509Certificate2 pk = null;
            if (certificates.Count > 0)
            {
                X509Certificate2Enumerator certificatesEn = certificates.GetEnumerator();
                certificatesEn.MoveNext();
                pk = certificatesEn.Current;
                X509Chain x509chain = new X509Chain();
                x509chain.Build(pk);
                foreach (X509ChainElement x509ChainElement in x509chain.ChainElements)
                {
                    chain.Add(DotNetUtilities.FromX509Certificate(x509ChainElement.Certificate));
                }
            }

            AssinaComToken(FileName, SignFileName, oCertificado, X, Y, Pagina, Rotation, AddTimeStamper, urlTimeStamper, timeStampUser, timeStampPass, Reason, AplicaPolitica, MyDigestAlgorithm, Contact, Location, Creator, Tipo, Cargo, CREACRM);

            return;
        }


        //public static void AssinaPDF_OLD(string FileName, string SignFileName, string SerialNumber, int Pagina, float X, float Y, double Escala, bool SeloCargo = false, bool SeloCREA = false, bool SeloCRM = false, string Cargo = "", string CREACRM = "", bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-1", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", bool SeloCertifico = false)
        //{
        //    LoggerFactory.GetInstance().SetLogger(new SysoLogger());
        //    X509Store x509Store = new X509Store("My");
        //    x509Store.Open(OpenFlags.ReadOnly);
        //    X509Certificate2Collection certificates = new X509Certificate2Collection();
        //    X509Certificate2Collection Certificados = x509Store.Certificates;
        //    foreach (X509Certificate2 cert in Certificados)
        //    {
        //        if (cert.SerialNumber == SerialNumber)
        //        {
        //            certificates.Add(cert);
        //        }
        //    }
        //    IList<X509Certificate> chain = new List<X509Certificate>();
        //    X509Certificate2 pk = null;
        //    if (certificates.Count > 0)
        //    {
        //        X509Certificate2Enumerator certificatesEn = certificates.GetEnumerator();
        //        certificatesEn.MoveNext();
        //        pk = certificatesEn.Current;
        //        X509Chain x509chain = new X509Chain();
        //        x509chain.Build(pk);
        //        foreach (X509ChainElement x509ChainElement in x509chain.ChainElements)
        //        {
        //            chain.Add(DotNetUtilities.FromX509Certificate(x509ChainElement.Certificate));
        //        }
        //    }

        //    AssinaComToken(FileName, SignFileName, pk, X, Y, Pagina, Escala, SeloCargo, SeloCREA, SeloCRM, Cargo, CREACRM, AddTimeStamper, urlTimeStamper, timeStampUser, timeStampPass, Reason, AplicaPolitica, MyDigestAlgorithm, Contact, Location, Creator, SeloCertifico);

        //    return;
        //}
    }
}
