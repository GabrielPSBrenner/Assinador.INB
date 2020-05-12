using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using INB.Assinador.Integracao.Interfaces;
using System.Xml;

namespace INB.Assinador.Integracao
{
    public class ConverterObjeto
    {
        public static string  SerializaObjeto(IComunicacao objeto)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializador = new System.Web.Script.Serialization.JavaScriptSerializer();
            string retorno = serializador.Serialize(objeto);
            return retorno;
        }


        public static IComunicacao RetornaObjeto(string objeto)
        {
            Comunicacao oRetorno = new Comunicacao();
            System.Web.Script.Serialization.JavaScriptSerializer serializador = new System.Web.Script.Serialization.JavaScriptSerializer();
            Comunicacao oCom =   serializador.Deserialize<Comunicacao>(objeto);
            return oCom;
        }
    }
}
