using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Integracao.Interfaces
{
    public interface IComunicacao
    {
        string Codigo { get; set; }
        string Versao { get; set; }
        string URLWS { get; set; }
        string UserID { get; set; }
        string Senha { get; set; }

        string UsuarioAutenticado { get; set; }
        
        string HashArquivoOriginal { get; set; }
    }
}
