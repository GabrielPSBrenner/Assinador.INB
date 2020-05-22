using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INB.Assinador.Integracao.Interfaces;


namespace INB.Assinador.Integracao
{
    public class FileTransfer : IFileTransfer
    {        
        public string FileName { get; set; }
        public string FileBase64 { get; set; }
        //public byte[] File { get; set; }
        public int Codigo { get; set; }
        public int Versao { get; set; }
        public string IPResposta { get; set; }
        public int PortaRespostaAssinado { get; set; }
        public bool RetornoAssinado { get; set; }
        public string UsuarioAutenticado { get; set; }
    }
}
