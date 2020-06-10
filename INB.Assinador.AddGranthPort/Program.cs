using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.AddGranthPort
{
    class Program
    {
        public const string Login = ""; //usuário com poder de admin local para dar a permissão
        public const string Dominio = "";
        public const string Senha = "";
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_LOGON_INTERACTIVE = 2;

        static void Main(string[] args)
        {
            IntPtr tokenHandle = new IntPtr(0);
            IntPtr dupeTokenHandle = new IntPtr(0);
            tokenHandle = IntPtr.Zero;
            //bool returnValue = Impersonate.LogonUser(Login, Dominio, Senha, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);
            System.Net.NetworkCredential oNC = new System.Net.NetworkCredential(Login, Senha, Dominio);
            System.Security.SecureString oSenha = oNC.SecurePassword;
            List<string> Comando = new List<string>();
            Comando.Add("netsh http delete urlacl url = http://+:27524/MyUri ");
            Comando.Add("netsh http delete urlacl url = http://+:27525/MyUri");
            Comando.Add("netsh http add urlacl url=http://+:27524/MyUri user=Todos");
            Comando.Add("netsh http add urlacl url=http://+:27525/MyUri user=Todos");
            Comando.Add("netsh http add urlacl url=http://+:27525/MyUri user=Everyone");
            Comando.Add("netsh http add urlacl url=http://+:27525/MyUri user=Everyone");
            Process proc = new Process();
            proc.StartInfo.UserName = Login;
            proc.StartInfo.Password = oSenha;
            proc.StartInfo.Domain = Dominio;
            proc.StartInfo.FileName = "CMD.exe";
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            foreach (string strCmd in Comando)
            {
                proc.StandardInput.WriteLine(strCmd);
                proc.StandardInput.Flush();
                //proc.StartInfo.Arguments = strCmd;
            }
            proc.WaitForExit();
            proc.Close();            
        }
    }
}
