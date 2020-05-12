using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using INB.Assinador.Helper;
using System.Reflection;

namespace INB.Assinador.Model_IT
{
    public class MontaPolitica
    {    

        public static MyPolicy getHashPolitica(string PolicyUriSource, string PolicyIdentifier = "2.16.76.1.7.1.2.2.3", string PolicyDigestAlgorithm = "SHA-256", string FileName= "LPA_CAdES.der")
        {
            MyPolicy Retorno = new MyPolicy();
            Retorno.PolicyIdentifier = PolicyIdentifier;
            Retorno.PolicyDigestAlgorithm = PolicyDigestAlgorithm;
            Retorno.URLPolicy = PolicyUriSource;

            Stream St;
            try
            {
    
                St = Helper.FileHelper.MSReadFileURL(PolicyUriSource);
            }
            catch (Exception ex)
            {
                //FileStream File = Helper.FileHelper.FSReadFile(System.AppDomain.CurrentDomain.BaseDirectory + FileName);
                //St = File;
                St = new MemoryStream(Properties.Resources.LPA_CAdES);
            }
            DerSequence privKeyObj = (DerSequence)Asn1Object.FromStream(St);

           var objCollection = privKeyObj.GetEnumerator();
            objCollection.MoveNext();


            Org.BouncyCastle.Asn1.Asn1Sequence objPrincipal = (Org.BouncyCastle.Asn1.Asn1Sequence)objCollection.Current;
            var Politicas = objPrincipal.GetObjects();

           

            while (Politicas.MoveNext())
            {
                Org.BouncyCastle.Asn1.Asn1Sequence Politica = (Org.BouncyCastle.Asn1.Asn1Sequence)Politicas.Current;
                var Itens = Politica.GetObjects();
                Itens.MoveNext();
                string item1 = Itens.Current.ToString();
                Itens.MoveNext();
                string item2 = Itens.Current.ToString();
                if (item2.Contains(PolicyIdentifier))
                {
                    Itens.MoveNext();
                    string item3 = Itens.Current.ToString();
                    Retorno.SubURLPolicy = item3.Replace("[","").Replace("]","") ;

                    Itens.MoveNext();
                    Org.BouncyCastle.Asn1.Asn1Sequence item4 = (Org.BouncyCastle.Asn1.Asn1Sequence)Itens.Current;

                    var Item4d = item4.GetObjects();
                    Item4d.MoveNext();
                    Retorno.SubPolicyIdentifier = Item4d.Current.ToString().Replace("[", "").Replace("]", "");


                    Item4d.MoveNext();
                    Retorno.Hash = Item4d.Current.ToString();
                }
            }
            St.Close();
            return Retorno;

        }

        public static List<string> getHashPoliticaEspecifica(string PolicyUriSource, string PolicyIdentifier , string PolicyDigestAlgorithm , string FileName)
        {
             Stream St;
            try
            {

                St = Helper.FileHelper.MSReadFileURL(PolicyUriSource);
            }
            catch (Exception ex)
            {
                // FileStream File = Helper.FileHelper.FSReadFile(System.AppDomain.CurrentDomain.BaseDirectory + FileName);
                // St = File;
                St = new MemoryStream(Properties.Resources.PA_AD_RT_v2_3); 
            }
            DerSequence privKeyObj = (DerSequence)Asn1Object.FromStream(St);

            var objCollection = privKeyObj.GetEnumerator();
            List<String> oRetorno = new List<string>();
            while (objCollection.MoveNext())
            {
                string texto = objCollection.Current.ToString();
                oRetorno.Add(texto);
            }
            St.Close();
            return oRetorno;

           

        }
    }
}
