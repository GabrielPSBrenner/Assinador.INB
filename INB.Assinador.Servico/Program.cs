using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Servico
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Assinador()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
