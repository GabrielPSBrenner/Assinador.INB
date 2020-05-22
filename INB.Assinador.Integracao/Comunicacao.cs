using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INB.Assinador.Integracao.Interfaces;

namespace INB.Assinador.Integracao
{
    public class Comunicacao : IComunicacao
    {
        public int Codigo { get; set; }
        public int Versao { get; set; }
        public string URLWS { get; set; }
        public string UserID { get; set; }
        public string Senha { get; set; }

        public string UsuarioAutenticado { get; set; }
    }
}
