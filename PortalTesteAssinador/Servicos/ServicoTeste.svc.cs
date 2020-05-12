using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.IO;
using INB.Assinador.Integracao;


namespace PortalTesteAssinador.Servicos
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicoTeste" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicoTeste.svc or ServicoTeste.svc.cs at the Solution Explorer and start debugging.
    public class ServicoTeste : IServicoTeste
    {
        public void EnviarArquivo(byte[] Arquivo, int Codigo, int Versao)
        {
            string path = HttpContext.Current.Server.MapPath("~/Anexos/");
            string pathCompleto = path + Codigo.ToString() + "_" + Versao.ToString() + "_assinado.pdf";
            MemoryStream stream = new MemoryStream(Arquivo);
            stream.Flush();
            FileStream fs = new FileStream(pathCompleto, FileMode.OpenOrCreate);
            stream.CopyTo(fs);
            fs.Flush();
            fs.Close();
            stream.Close();
        }

        public byte[] ReceberArquivo(int Codigo, int Versao)
        {
            string path = HttpContext.Current.Server.MapPath("~/Anexos/");
            string pathCompleto = path + Codigo.ToString() + "_" + Versao.ToString() + ".pdf";
            return File.ReadAllBytes(pathCompleto);
        }     
    }
}
