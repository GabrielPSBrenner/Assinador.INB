using INB.Assinador.Integracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Servico.Base
{
    public class ServiceBase : IDisposable
    {
        private static bool ContinuaServico = true;
        private static TcpListener serverSocket;
        private WebSocketServer oWS;


        public ServiceBase(int PortaSocket = 27525, int PortaWebSocket = 27524)
        {
            OpenServer(PortaSocket);
            OpenServerWS(PortaWebSocket.ToString());
        }

        public bool NovaMensagemS
        {
            get
            {
                try
                {
                    return INB.Assinador.Integracao.Service.SocketWS.MensagemNova;
                }
                catch
                {
                    return false;
                }

            }
        }

        public bool NovaMensagemWS
        {
            get
            {
                try
                {
                    return oWS.MensagemNova;
                }
                catch
                {
                    return false;
                }
            }
        }

        public string MensagemS
        {
            get
            {
                try
                {
                    return INB.Assinador.Integracao.Service.SocketWS.Mensagem;
                }
                catch
                {
                    return "";
                }
            }
        }

        public string MensagemWS
        {
            get
            {
                try
                {
                    return oWS.Mensagem;
                }
                catch
                {
                    return "";
                }
            }
        }
        
        public async void OpenServerWS(string Porta = "27524")
        {
            oWS = new WebSocketServer();
            oWS.Start("http://localhost:" + Porta + "/Assinador/");
        }

        public async void OpenServer(int Porta = 27525)
        {

            serverSocket = new TcpListener(Porta);
            serverSocket.Start();
            int counter = 0;

            while (ContinuaServico)
            {
                counter += 1;
                TcpClient clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                clientSocket.ReceiveBufferSize = 10000;
                INB.Assinador.Integracao.Service.SocketWS client = new INB.Assinador.Integracao.Service.SocketWS();
                client.startClient(clientSocket, Convert.ToString(counter));
            }
            serverSocket.Stop();
        }

        public void Dispose()
        {
            try
            {
                ContinuaServico = false;
                try
                {
                    serverSocket.Stop();                    
                }
                catch (Exception ex) { }
                try
                {
                    oWS.Close();                    
                }
                catch (Exception ex) { }
            }
            catch (Exception ex) { }
        }
    }
}
