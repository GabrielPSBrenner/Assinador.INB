
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

        public enum TipoAssinatura
        {
            Normal = 1,
            Cargo = 2,
            CRM = 3,
            CREA = 4,
            CargoCrea = 5,
            CargoCRM = 6
        }

        public static void AssinaPDF(string FileName, string SignFileName, string SerialNumber, int Pagina, int X, int Y, double Escala, bool SeloCargo = false, bool SeloCREA = false, bool SeloCRM = false, string Cargo = "", string CREACRM = "", bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string Contact = "")
        {

            X509Store KeyStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            // Abre o Store
            KeyStore.Open(OpenFlags.ReadOnly);
            // Obtém a coleção dos certificados da Store
            X509Certificate2Collection Certificados = KeyStore.Certificates;
            X509Certificate2Collection certificates = new X509Certificate2Collection();

            foreach (X509Certificate2 iCert in Certificados)
            {
                if (iCert.SerialNumber == SerialNumber)
                {
                    certificates.Add(iCert);
                }
            }

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

            List<ICrlClient> crlList = new List<ICrlClient>();
            crlList.Add(new CrlClientOnline(chain.ToArray()));
            SignWithThisCert(crlList, FileName, SignFileName, cert, X, Y, Pagina, Escala, SeloCargo, SeloCREA, SeloCRM, Cargo, CREACRM, AddTimeStamper, urlTimeStamper, timeStampUser, timeStampPass, Reason, AplicaPolitica);


        }

        public static void SignWithThisCert(List<ICrlClient> crlList, string FileName, string SignFileName, X509Certificate2 cert, int X, int Y, int Pagina, double Escala, bool SeloCargo = false, bool SeloCREA = false, bool SeloCRM = false, string Cargo = "", string CREACRM = "", bool AddTimeStamper = true, string urlTimeStamper = "https://freetsa.org/tsr", string timeStampUser = "", string timeStampPass = "", string Reason = "Assinatura Digital", bool AplicaPolitica = false, string MyDigestAlgorithm = "SHA-256", string Contact = "")
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





            PdfDocument pdfDoc = objStamper.GetDocument();

            PdfPage oPage = pdfDoc.GetPage(Pagina);

            int LarguraAssinatura = Largura;
            int AlturaAssinatura = Altura;
            int X_Ajustado, Y_Ajustado;
            X_Ajustado = X;
            Y_Ajustado = Y;


            var crop = oPage.GetCropBox();



            float Left, Top, Width, Height;

            Bitmap bmp = INB.Assinador.Helper.Graphic.ConfiguraBMP(cert, SeloCargo, SeloCREA, SeloCRM, Cargo, CREACRM, out Altura);

            Left = (int)crop.GetLeft() + X_Ajustado;
            Top = (int)crop.GetTop() - (Y_Ajustado + AlturaAssinatura + 5);
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
            signatureAppearance.SetLocation("Indústrias Nucleares do Brasil S/A - INB");
            signatureAppearance.SetSignatureCreator("Assinador da INB - Desenvolvimento Interno");
            signatureAppearance.SetSignatureGraphic(pic);
            signatureAppearance.SetRenderingMode(PdfSignatureAppearance.RenderingMode.GRAPHIC);
            signatureAppearance.SetPageRect(oRetangulo);
            signatureAppearance.SetPageNumber(Pagina);





            Org.BouncyCastle.X509.X509Certificate vert = Org.BouncyCastle.Security.DotNetUtilities.FromX509Certificate(cert);
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] Arraychain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
            X509CertificateParser objCP = new X509CertificateParser();

            RSACryptoServiceProvider rsa;
            RSACryptoServiceProvider Provider;
            IExternalSignature externalSignature;
            if (cert.PrivateKey is RSACryptoServiceProvider)
            {
                //Token externo, tem que capturar como assymetric key
                rsa = (RSACryptoServiceProvider)cert.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);
            }
            else
            {
                //RETIRAR ESSA PARTE PARA IMPLEMENTAR OS DEMAIS MÉTODOS, OLHANDO OUTROS TIPOS DE CERTIFICADO
                rsa = (RSACryptoServiceProvider)cert.PrivateKey;
                Provider = (RSACryptoServiceProvider)cert.PrivateKey;
                externalSignature = new AsymmetricAlgorithmSignature(Provider, MyDigestAlgorithm);
            }


            if (AddTimeStamper)
            {
                //https://freetsa.org/tsr
                tsaClient = new TSAClientBouncyCastle(urlTimeStamper, timeStampUser, timeStampPass);

               // var teste = tsaClient.GetMessageDigest();
            }
            //IOcspClient ocspClient = new OcspClientBouncyCastle();

            OCSPVerifier ocspVerifier = new OCSPVerifier(null, null);
            ocspClient = new OcspClientBouncyCastle(ocspVerifier);

            if (AplicaPolitica)
            {
                //2.16.76.1.7.1.1.2.3
                string PolicyIdentifier = "2.16.76.1.7.1.2.2.3";
                string PolicyDigestAlgorithm = "SHA-256";
                string PolicyUriSource = "http://politicas.icpbrasil.gov.br/LPA_CAdES.der";
                string PolicyUri = "";
                byte[] PolicyHash = null;
                Helper.MyPolicy MyPolicyBase = MontaPolitica.getHashPolitica(PolicyUriSource, PolicyIdentifier, PolicyDigestAlgorithm, "LPA_CAdES.der");
                // subPolicyUri = "http://politicas.icpbrasil.gov.br/PA_AD_RT_v2_3.der";
                //string subPolicy = "2.16.840.1.101.3.4.2.1";
                // string subPolicyFile = "PA_AD_RT_v2_3.der";
                List<string> MyPolicyAuth = MontaPolitica.getHashPoliticaEspecifica(MyPolicyBase.SubURLPolicy, PolicyIdentifier, PolicyDigestAlgorithm, "PA_AD_RT_v2_3.der");
                string Hash = MyPolicyAuth[2].Replace("#", "");
                // Hash = "2b53082af649097717587f266b5aa0d2cff6cf0f12b29dafdfaa33d47bd094ef";
                ////hash presente no segundo arquivo de política (#80dfe81e2a8ae762cd360253722922332ee10164d992156d847c47c8fb879cd2)
                PolicyHash = INB.Assinador.Helper.Funcoes.StringToByteArray(Hash);
                string strBase64 = Convert.ToBase64String(PolicyHash);
                //Org.BouncyCastle.Asn1.Asn1OctetString.FromByteArray(PolicyHash);
                SignaturePolicyInfo spi = new SignaturePolicyInfo(PolicyIdentifier, strBase64, PolicyDigestAlgorithm, MyPolicyBase.SubURLPolicy);

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
