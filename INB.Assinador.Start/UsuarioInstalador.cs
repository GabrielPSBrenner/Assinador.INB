using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Start
{
    public class UsuarioInstalador
    {
        public const string Login = "8888";
        public const string Dominio = "INB";
        public const string Senha = "H*@p47!Bs#";

        public void ExecutaAssinador()
        {
            string ArquivoTemp = "";            
            var Usuario = new UsuarioInstalador();
            var oImpersonate = new Impersonate();
            ArquivoTemp = System.AppDomain.CurrentDomain.BaseDirectory + "assinador.application";
            WebClient wc = new WebClient();
            MemoryStream f = new MemoryStream(wc.DownloadData("http://inbnet/instalacoes/Assinador/asssinador%20da%20Inb.application"));
            f.Flush();
            FileStream fs = new FileStream(ArquivoTemp, FileMode.OpenOrCreate);
            f.CopyTo(fs);
            fs.Flush();
            fs.Close();
            f.Close();

            var p = new ProcessStartInfo(ArquivoTemp);

            using (var exeProcess = Process.Start(p))
            {
                exeProcess.StartInfo.UseShellExecute = false;
                exeProcess.StartInfo.UserName = UsuarioInstalador.Login;
                exeProcess.StartInfo.Domain = UsuarioInstalador.Dominio;
                exeProcess.StartInfo.Password = new SecureString();
                foreach (char c in UsuarioInstalador.Senha)
                    exeProcess.StartInfo.Password.AppendChar(c);
                exeProcess.WaitForExit();
            }
            oImpersonate.undoImpersonation();
        }

    }
}
