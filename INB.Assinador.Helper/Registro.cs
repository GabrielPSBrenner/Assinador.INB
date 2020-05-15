using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace INB.Assinador.Helper
{
    public class Registro
    {
        public static void SetStartup(bool StartUp, string AppName)
        {
             RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (StartUp)
                rk.SetValue(AppName, System.AppDomain.CurrentDomain.BaseDirectory + AppName + ".exe");
            else
                rk.DeleteValue(AppName, false);

        }
    }
}



   