using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using INB.Assinador.Integracao.Interfaces;

namespace INB.Assinador.Integracao.Service
{
    public class Cliente
    {
        public static string EnviaDados(IComunicacao Mensagem, string IP, int Porta = 27525)
        {
            TcpClient clientSocket = new TcpClient();
            clientSocket.ReceiveBufferSize = 1000;           
            clientSocket.Connect(IP, Porta);

            string ObjetoSerializado = ConverterObjeto.SerializaObjeto(Mensagem);
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(ObjetoSerializado);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            byte[] inStream = new byte[1000];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            returndata = returndata.Substring(0, returndata.IndexOf("\0"));
            clientSocket.Close();
            return returndata;
        }
    }
}
