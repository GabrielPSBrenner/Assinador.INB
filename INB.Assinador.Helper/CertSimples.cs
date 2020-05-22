using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Helper
{
    public class CertSimples
    {
        private string _Subject;

        public string SerialNumber { get; set; }

        public X509Certificate2 Certificado { get; set; }

        public string Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                string DadoOriginal = value;
                try
                {
                    string[] Dados = DadoOriginal.Split(',');
                    _Subject = Dados[0].Substring(3).Replace(":", " - CPF:");
                }
                catch
                {
                    _Subject = DadoOriginal;
                }

            }
        }

        public static List<CertSimples> ListaCertificado(X509Certificate2Collection Certificados)
        {
            List<CertSimples> oLista = new List<CertSimples>();
            for (int i = 0; i < Certificados.Count; i++)
            {
                CertSimples oCert = new CertSimples();
                oCert.SerialNumber = Certificados[i].SerialNumber;
                oCert.Subject = Certificados[i].Subject;
                oCert.Certificado = Certificados[i];
                oLista.Add(oCert);
            }
            return oLista;
        }

    }


}
