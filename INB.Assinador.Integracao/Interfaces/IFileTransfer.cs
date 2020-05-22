using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Integracao.Interfaces
{
    public interface IFileTransfer
    {
        int Codigo { get; set; }
        int Versao { get; set;}
        string IPResposta { get; set; }
        int PortaRespostaAssinado { get; set; }
        string FileName { get; set; }
        string FileBase64 { get; set; }
        bool RetornoAssinado { get; set; }

        string UsuarioAutenticado { get; set; }
    }

    
}
