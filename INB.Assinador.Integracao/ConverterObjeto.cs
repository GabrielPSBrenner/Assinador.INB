using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using INB.Assinador.Integracao.Interfaces;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace INB.Assinador.Integracao
{
    public class ConverterObjeto
    {
        public static string SerializaObjeto(object objeto)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializador = new System.Web.Script.Serialization.JavaScriptSerializer();
            string retorno = serializador.Serialize(objeto);
            return retorno;
        }

        public static string SerializaObjetoXML(IFileTransfer Objeto)
        {
            XmlSerializer x = new XmlSerializer(Objeto.GetType());
            MemoryStream St = new MemoryStream();
            x.Serialize(St, Objeto);
            St.Flush();            
            byte[] ArrayByte = St.ToArray();
            string retorno = System.Text.Encoding.ASCII.GetString(ArrayByte);
            return retorno;
        }

        public static byte[] SerializaObjetoXMLByteArray(IFileTransfer Objeto)
        {
            XmlSerializer x = new XmlSerializer(Objeto.GetType());
            MemoryStream St = new MemoryStream();
            x.Serialize(St, Objeto);
            St.Flush();
            byte[] ArrayByte = St.ToArray();           
            return ArrayByte;
        }


        public static IComunicacao RetornaObjeto(string objeto)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializador = new System.Web.Script.Serialization.JavaScriptSerializer();
            Comunicacao oCom = serializador.Deserialize<Comunicacao>(objeto);
            return oCom;
        }

        public static IFileTransfer RetornaObjetoFT(string objeto)
        {
            Type Tipo = typeof(FileTransfer);
            XmlSerializer xml = new XmlSerializer(Tipo);
            StringReader SR = new StringReader(objeto);
            IFileTransfer oFT = (IFileTransfer)xml.Deserialize(SR);
            SR.Close();
            SR.Dispose();
            return oFT;
        }
    }
}
