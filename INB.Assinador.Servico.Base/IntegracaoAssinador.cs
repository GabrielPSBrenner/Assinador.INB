using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using INB.Assinador.Integracao;

namespace INB.Assinador.Servico.Base
{
    public class IntegracaoAssinador : IDisposable
    {
        public Thread t;
        private static Socket Servidor;
        private static IPEndPoint EndPointServer;
        public static bool ArquivoNovo = false;
        public static string ServerMsg;
        public static FileTransfer oFT;
        public static bool ParaServidor = false;
        ServiceBase oServico;

        public IntegracaoAssinador(int Porta = 27520)
        {
            oServico = new ServiceBase();
            t = new Thread(() => IniciarServidor(Porta));
            t.Start();
        }

        private void IniciarServidor(int Porta)
        {
            byte[] bytesFrom = new byte[10000];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string MeuIP = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            if (MeuIP == "::1")
            {
                MeuIP = Dns.GetHostAddresses(Dns.GetHostName())[1].ToString();
            }
            TcpListener serverSocket = new TcpListener(IPAddress.Parse(MeuIP), Porta);
            serverSocket.Start();
            int counter = 0;

            while (!ParaServidor)
            {
                counter += 1;
                TcpClient clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                clientSocket.ReceiveBufferSize = 10000;

                while(!ParaServidor)
                {
                    if (clientSocket.Connected)
                    {
                        if (oServico.NovaMensagemS)
                        {
                            string ObjetoSerializado = ConverterObjeto.SerializaObjeto(oServico.MensagemS);
                            NetworkStream serverStream = clientSocket.GetStream();
                            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(ObjetoSerializado);
                            serverStream.Write(outStream, 0, outStream.Length);
                            serverStream.Flush();
                            byte[] inStream = new byte[1000];
                            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                            returndata = returndata.Substring(0, returndata.IndexOf("\0"));
                        }
                        if (oServico.NovaMensagemWS)
                        {

                        }
                    }
                    else
                    {
                        break;
                    }
                    //FileStream FS = new FileStream("C:\\Lixo.txt", FileMode.OpenOrCreate);
                    //Byte[] info = ASCIIEncoding.ASCII.GetBytes(str);
                    //FS.Write(info, 0, info.Length);
                    //FS.Close();
                }

                try
                {
                    clientSocket.Close();
                }
                catch (Exception ex) { }
            }
            serverSocket.Stop();
        }


        public void Dispose()
        {
            ParaServidor = true;
            try
            {
                
            }
            catch (Exception e) { }

            try
            {
                Servidor.Close();
            }
            catch (Exception e) { }
            try
            {
                t.Abort();
            }
            catch (Exception e) { }
        }
    }
}
