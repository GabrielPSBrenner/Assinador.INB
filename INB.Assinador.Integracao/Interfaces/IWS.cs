using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;

namespace INB.Assinador.Integracao.Interfaces
{
   [ServiceContract]
    public interface IWS
    {
        [OperationContract]
        byte[] ReceberArquivo(int Codigo, int Versao);

        [OperationContract]
        void EnviarArquivo(byte[] Arquivo, int Codigo, int Versao);
    }
}
