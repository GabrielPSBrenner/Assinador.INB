using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INB.Assinador.Helper;
using iText.Signatures;
using iText.Kernel.Pdf;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;
using Org.BouncyCastle.Security;
using System.Drawing;
using Org.BouncyCastle.Tsp;
using iText.Forms;
using Org.BouncyCastle.X509;
using System.Security.Cryptography;

namespace INB.Assinador.Model_IT
{
    public class AssinaComTokenIText7
    {
        private static readonly object data;

        public static X509Certificate2 getDadosCadeiaCertificadao(CertSimples oCert, out List<ICrlClient> crlList)
        {
            X509Store KeyStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            // Abre o Store
            KeyStore.Open(OpenFlags.ReadOnly);
            // Obtém a coleção dos certificados da Store
            X509Certificate2Collection Certificados = KeyStore.Certificates;
            X509Certificate2Collection certificates = new X509Certificate2Collection();
            certificates.Add(oCert.Certificado);

            List<X509Certificate> chain = new List<X509Certificate>();
            X509Certificate2 cert = null;

            if (certificates.Count > 0)
            {
                X509Certificate2Enumerator certificatesEn = certificates.GetEnumerator();
                certificatesEn.MoveNext();
                cert = certificatesEn.Current;
                X509Chain x509chain = new X509Chain();
                x509chain.Build(cert);
                foreach (X509ChainElement x509ChainElement in x509chain.ChainElements)
                {
                    chain.Add(DotNetUtilities.FromX509Certificate(x509ChainElement.Certificate));
                }
            }

            crlList = new List<ICrlClient>();
            crlList.Add(new CrlClientOnline(chain.ToArray()));
            return cert;
        }

        public static void AssinaPDF(string FileName, string SignFileName, CertSimples oCert, int Pagina, int X, int Y, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string Contact = "", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {
            X509Certificate2 cert;
            List<ICrlClient>  crlList;
            cert = getDadosCadeiaCertificadao(oCert, out crlList);
            AssinaComCertificado(crlList, FileName, SignFileName, oCert, X, Y, Pagina, Rotation, AddTimeStamper, urlTimeStamper, timeStampUser, timeStampPass, Reason, AplicaPolitica, "SHA-256", Contact, null, null, Tipo, Cargo, CREACRM);
        }

        public static void AssinaPDF(byte[] File, out byte[] SignFile, CertSimples oCert, int Pagina, int X, int Y, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string Contact = "", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {
            List<ICrlClient> crlList;
            X509Certificate2 cert;
            cert = getDadosCadeiaCertificadao(oCert, out crlList);
            AssinaComCertificado(crlList, File, out SignFile, oCert, X, Y, Pagina, Rotation, AddTimeStamper, urlTimeStamper, timeStampUser, timeStampPass, Reason, AplicaPolitica, null, null, null, null, Tipo, Cargo, CREACRM);
        }

        private static void ConfiguraAparencia(PdfSigner objStamper, CertSimples cert, int X, int Y, int Largura, int Altura, int Pagina, int Rotation, string Contact = "", string Reason = "Assinatura Digital", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {
            PdfDocument pdfDoc = objStamper.GetDocument();
            PdfPage oPage = pdfDoc.GetPage(Pagina);
            int LarguraAssinatura = Largura;
            int AlturaAssinatura = Altura;
            int X_Ajustado, Y_Ajustado;
            X_Ajustado = X;
            Y_Ajustado = Y;
            var crop = oPage.GetCropBox();
            float Left, Top, Width, Height;
            Bitmap bmp = Graphic.ConfiguraBMP(cert, out Altura, Tipo);

            if (Rotation == 270)
            {
                Left = (int)crop.GetLeft() + X_Ajustado;
                Top = (int)crop.GetRight() - (Y_Ajustado + AlturaAssinatura + 5);
            }
            else if (Rotation == 90)
            {
                Left = (int)crop.GetRight() + X_Ajustado;
                Top = (int)crop.GetLeft() - (Y_Ajustado + AlturaAssinatura + 5);
            }
            else
            {
                Left = (int)crop.GetLeft() + X_Ajustado;
                Top = (int)crop.GetTop() - (Y_Ajustado + AlturaAssinatura + 5);
            }
            Width = LarguraAssinatura;
            Height = AlturaAssinatura;
            iText.Kernel.Geom.Rectangle oRetangulo = new iText.Kernel.Geom.Rectangle(Left, Top, Width, Height);
            var pageSize = pdfDoc.GetPage(Pagina).GetMediaBox();
            var signaturePosition = new iText.Kernel.Geom.Rectangle(pageSize.GetLeft(), pageSize.GetBottom(), pageSize.GetWidth(), pageSize.GetHeight());
            PdfSignatureAppearance signatureAppearance = objStamper.GetSignatureAppearance();
            var memoryStream = new MemoryStream();
            bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            iText.IO.Image.ImageData pic = iText.IO.Image.ImageDataFactory.Create(memoryStream.ToArray());
            signatureAppearance.SetReason(Reason);
            signatureAppearance.SetLocation(Location);
            signatureAppearance.SetSignatureCreator(Creator);
            signatureAppearance.SetSignatureGraphic(pic);
            signatureAppearance.SetRenderingMode(PdfSignatureAppearance.RenderingMode.GRAPHIC);
            signatureAppearance.SetPageRect(oRetangulo);
            signatureAppearance.SetPageNumber(Pagina);
        }

        private static SignaturePolicyInfo getPolitica()
        {
            string PolicyIdentifier = "2.16.76.1.7.1.2.2.3";
            string PolicyDigestAlgorithm = "SHA-256";
            string PolicyUriSource = "http://politicas.icpbrasil.gov.br/LPA_CAdES.der";
            string PolicyUri = "";
            byte[] PolicyHash = null;
            Helper.MyPolicy MyPolicyBase = MontaPolitica.getHashPolitica(PolicyUriSource, PolicyIdentifier, PolicyDigestAlgorithm, "LPA_CAdES.der");
            List<string> MyPolicyAuth = MontaPolitica.getHashPoliticaEspecifica(MyPolicyBase.SubURLPolicy, PolicyIdentifier, PolicyDigestAlgorithm, "PA_AD_RT_v2_3.der");
            string Hash = MyPolicyAuth[2].Replace("#", "");
            PolicyHash = INB.Assinador.Helper.Funcoes.StringToByteArray(Hash);
            string strBase64 = Convert.ToBase64String(PolicyHash);
            SignaturePolicyInfo spi = new SignaturePolicyInfo(PolicyIdentifier, strBase64, PolicyDigestAlgorithm, MyPolicyBase.SubURLPolicy);
            return spi;
        }

        public static void AssinaComCertificado(List<ICrlClient> crlList, byte[] File, out byte[] SignFile, CertSimples cert, int X, int Y, int Pagina, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-256", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {

            int Largura = 140;
            int Altura = 63;
            MemoryStream ArquivoOrigem = new MemoryStream(File);
            PdfReader pdfReader = new PdfReader(ArquivoOrigem);
            MemoryStream signedPdf = new MemoryStream();
            StampingProperties osp = new StampingProperties();
            osp.UseAppendMode();
            PdfSigner objStamper = new PdfSigner(pdfReader, signedPdf, osp);
            ITSAClient tsaClient = null;
            IOcspClient ocspClient = null;
            ConfiguraAparencia(objStamper, cert, X, Y, Largura, Altura, Pagina, Rotation, Contact, Reason, Location, Creator, Tipo, Cargo, CREACRM);

            Org.BouncyCastle.X509.X509Certificate vert = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(cert.Certificado);
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] Arraychain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.Certificado.RawData) };
            X509CertificateParser objCP = new X509CertificateParser();

            RSACryptoServiceProvider rsa;
            RSACryptoServiceProvider Provider;
            IExternalSignature externalSignature;
            if (cert.Certificado.PrivateKey is RSACryptoServiceProvider)
            {
                rsa = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);
            }
            else
            {
                RSA rsaTeste = cert.Certificado.GetRSAPrivateKey();

                rsa = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);
            }

            if (AddTimeStamper)
            {
                tsaClient = new TSAClientBouncyCastle(urlTimeStamper, timeStampUser, timeStampPass);
            }
            OCSPVerifier ocspVerifier = new OCSPVerifier(null, null);
            ocspClient = new OcspClientBouncyCastle(ocspVerifier);
            if (AplicaPolitica)
            {
                SignaturePolicyInfo spi = getPolitica();
                objStamper.SignDetached(externalSignature, Arraychain, crlList, ocspClient, tsaClient, 0, PdfSigner.CryptoStandard.CADES, spi);
            }
            else
            {
                objStamper.SignDetached(externalSignature, Arraychain, crlList, ocspClient, tsaClient, 0, PdfSigner.CryptoStandard.CADES);
            }

            try
            {
                SignFile = signedPdf.ToArray();
                try
                {
                    signedPdf.Close();
                    signedPdf.Dispose();
                }
                catch { }
            }
            catch (Exception ex)
            {
                SignFile = null;
                throw ex;
            }
            try
            {
                signedPdf.Close();
            }
            catch (Exception ex) { }
            pdfReader.Close();
        }

        public static void AssinaComCertificado(List<ICrlClient> crlList, string FileName, string SignFileName, CertSimples cert, int X, int Y, int Pagina, int Rotation, bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-256", string Contact = "", string Location = "Indústrias Nucleares do Brasil S/A - INB", string Creator = "Assinador da INB", TipoAssinatura Tipo = TipoAssinatura.Normal, string Cargo = "", string CREACRM = "")
        {
            string SourcePdfFileName = FileName;
            string DestPdfFileName = SignFileName;
            int Largura = 140;
            int Altura = 63;
            PdfReader pdfReader = new PdfReader(SourcePdfFileName);
            FileStream signedPdf = new FileStream(DestPdfFileName, FileMode.Create, FileAccess.ReadWrite);
            StampingProperties osp = new StampingProperties();
            osp.UseAppendMode();
            PdfSigner objStamper = new PdfSigner(pdfReader, signedPdf, osp);
            ITSAClient tsaClient = null;
            IOcspClient ocspClient = null;

            ConfiguraAparencia(objStamper, cert, X, Y, Largura, Altura, Pagina, Rotation, Contact, Reason, Location, Creator, Tipo);

            Org.BouncyCastle.X509.X509Certificate vert = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(cert.Certificado);
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] Arraychain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.Certificado.RawData) };
            X509CertificateParser objCP = new X509CertificateParser();

            RSACryptoServiceProvider rsa;
            RSACryptoServiceProvider Provider;
            IExternalSignature externalSignature;
            if (cert.Certificado.PrivateKey is RSACryptoServiceProvider)
            {
                rsa = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);
            }
            else
            {
                //RETIRAR ESSA PARTE PARA IMPLEMENTAR OS DEMAIS MÉTODOS, OLHANDO OUTROS TIPOS DE CERTIFICADO
                rsa = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.Certificado.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);
            }
            if (AddTimeStamper)
            {
                tsaClient = new TSAClientBouncyCastle(urlTimeStamper, timeStampUser, timeStampPass);
            }
            OCSPVerifier ocspVerifier = new OCSPVerifier(null, null);
            ocspClient = new OcspClientBouncyCastle(ocspVerifier);
            if (AplicaPolitica)
            {
                SignaturePolicyInfo spi = getPolitica();
                objStamper.SignDetached(externalSignature, Arraychain, crlList, ocspClient, tsaClient, 0, PdfSigner.CryptoStandard.CADES, spi);
            }
            else
            {
                objStamper.SignDetached(externalSignature, Arraychain, crlList, ocspClient, tsaClient, 0, PdfSigner.CryptoStandard.CADES);
            }
            try { signedPdf.Flush(); }
            catch { }
            try { signedPdf.Close(); } catch { };
            pdfReader.Close();
        }

        private void addVerificationInfo(IOcspClient ocspClient, LtvVerification verification, CrlClientOnline crl, String name)
        {
            verification.AddVerification(name, ocspClient, crl, LtvVerification.CertificateOption.WHOLE_CHAIN, LtvVerification.Level.OCSP_CRL, LtvVerification.CertificateInclusion.YES);
        }

    }
}
