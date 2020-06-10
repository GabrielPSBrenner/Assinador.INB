using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Start
{
    class Program
    {
        static void Main(string[] args)
        {
            UsuarioInstalador usuarioInstalador = new UsuarioInstalador();
            usuarioInstalador.ExecutaAssinador();
        }
    }
}
