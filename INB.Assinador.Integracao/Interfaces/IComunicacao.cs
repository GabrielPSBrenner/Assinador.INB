using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Integracao.Interfaces
{
    public interface IComunicacao
    {
        int Codigo { get; set; }
        int Versao { get; set; }
        string URLWS { get; set; }
        string UserID { get; set; }
        string Senha { get; set; }

        string UsuarioAutenticado { get; set; }
    }
}
