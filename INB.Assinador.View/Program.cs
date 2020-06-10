using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace INB.Assinador.View
{

    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetFocus(HandleRef hWnd);

        //public const string Login = "";
        //public const string Dominio = "INB";
        //public const string Senha = "";

        //const int LOGON32_PROVIDER_DEFAULT = 0;
        //const int LOGON32_LOGON_INTERACTIVE = 2;


        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]
        [STAThread]
        static void Main(string[] Args)
        {
            //Process curr = Process.GetCurrentProcess();


            ////if (Args.Count() == 0)
            ////{

            ////}
            ////else
            ////{
            ///  
            //IntPtr tokenHandle = new IntPtr(0);
            //IntPtr dupeTokenHandle = new IntPtr(0);
            //tokenHandle = IntPtr.Zero;
            //bool returnValue = Impersonate.LogonUser(Login, Dominio, Senha, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);
            //using (WindowsIdentity newId = new WindowsIdentity(tokenHandle))
            //{
            //    using (WindowsImpersonationContext impersonatedUser = newId.Impersonate())
            //    {
            Process oP = PriorProcess();
            if (oP == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmAssinador());
            }
            else
            {
                var handle = new HandleRef(null, oP.MainWindowHandle);
                SetFocus(handle);
            }
            //    }
            //}
        }


        public static Process PriorProcess()
        {
            Process curr = Process.GetCurrentProcess();
            Process[] procs = Process.GetProcessesByName(curr.ProcessName);
            foreach (Process p in procs)
            {
                if ((p.Id != curr.Id) &&
                    (p.MainModule.FileName == curr.MainModule.FileName))
                    return p;
            }
            return null;
        }
    }
}
