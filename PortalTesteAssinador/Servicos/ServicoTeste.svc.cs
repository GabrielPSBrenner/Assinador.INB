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
        public void EnviarArquivo(byte[] Arquivo, string Codigo, string Versao, string UsuarioAutenticado, string HashArquivoOriginal)
        {
            string path = HttpContext.Current.Server.MapPath("~/Anexos/");
            string pathCompleto = path + Codigo + "_" + Versao + "_assinado.pdf";

            string pathArquivoOriginal = path + Codigo + "_" + Versao + ".pdf";
            
            
            //Compara o hash do arquivo enviado com o do arquivo armazenado, os dois tem que bater, senão houve mudanças.
            byte[] ArquivoOriginal = INB.Assinador.Helper.FileHelper.ReadFile(pathArquivoOriginal);
            string HashOriginal = INB.Assinador.Integracao.DocumentHash.GetHashToString(ArquivoOriginal);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (comparer.Compare(HashOriginal, HashArquivoOriginal) == 0)
            {
                MemoryStream stream = new MemoryStream(Arquivo);
                stream.Flush();
                FileStream fs = new FileStream(pathCompleto, FileMode.OpenOrCreate);
                stream.CopyTo(fs);
                fs.Flush();
                fs.Close();
                stream.Close();
            }
            else
            {
                //o arquivo assinado não é mais o que está armazenado.
                throw new Exception("O arquivo assinado não corresponde ao arquivo que foi enviado para assinatura. Por favor, repetir o processo.");
            }
        }

        public byte[] ReceberArquivo(string Codigo, string Versao)
        {
            string path = HttpContext.Current.Server.MapPath("~/Anexos/");
            string pathCompleto = path + Codigo + "_" + Versao + ".pdf";
            return File.ReadAllBytes(pathCompleto);
        }
    }
}
