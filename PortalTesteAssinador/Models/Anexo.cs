using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTesteAssinador.Models
{
    public class Anexo
    {
       public  int Codigo { get; set; }
        public int Versao { get; set;  }

        public static List<Anexo> ListaAnexos()
        {
            List<Anexo> oRetorno = new List<Anexo>();
            for (int i=1; i<10; i++)
            {
                Anexo oAnx = new Anexo();
                oAnx.Codigo = i;
                oAnx.Versao = 1;
                oRetorno.Add(oAnx);
            }
            return oRetorno;
        }
    }
}