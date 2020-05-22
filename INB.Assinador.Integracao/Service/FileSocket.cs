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

namespace INB.Assinador.Integracao.Service
{
    public class FileSocket
    {
        public Thread t;
        private static Socket Servidor;
        private static IPEndPoint EndPointServer;
        public static bool ArquivoNovo = false;
        public static string ServerMsg;
        public static FileTransfer oFT;
        public static bool ParaServidor = false;

        public FileSocket(int Porta)
        {
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
            while (true)
            {
                counter += 1;
                TcpClient clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                clientSocket.ReceiveBufferSize = 10000;
                string str;
                using (NetworkStream stream = clientSocket.GetStream())
                {
                    byte[] data = new byte[1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int numBytesRead;
                        while ((numBytesRead = stream.Read(data, 0, data.Length)) > 0)
                        {
                            ms.Write(data, 0, numBytesRead);
                        }
                        str = Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length);
                    }

                    //FileStream FS = new FileStream("C:\\Lixo.txt", FileMode.OpenOrCreate);
                    //Byte[] info = ASCIIEncoding.ASCII.GetBytes(str);
                    //FS.Write(info, 0, info.Length);
                    //FS.Close();
                    oFT = (FileTransfer)ConverterObjeto.RetornaObjetoFT(str);
                    ServerMsg = "Arquivo recebido";
                    ArquivoNovo = true;
                }
                clientSocket.Close();
            }
            serverSocket.Stop();
        }

        public static bool EnviarArquivoViaSocket(string PathCompletoComArquivo, int Codigo, int Versao, string EnderecoIPEnvia, string EnderecoIPResposta, int PortaEnvia, int PortaResposta, bool RetornoAssinado, out string MsgCliente)
        {
            try
            {

                string caminhoArquivo = Path.GetDirectoryName(PathCompletoComArquivo) + "\\";
                string nomeArquivo = Path.GetFileName(PathCompletoComArquivo);
                string caminhoCompleto = caminhoArquivo + nomeArquivo;
                byte[] fileData = File.ReadAllBytes(caminhoCompleto);
                FileTransfer oFT = new FileTransfer();
                oFT.Codigo = Codigo;
                oFT.Versao = Versao;
                oFT.FileName = nomeArquivo;
                oFT.IPResposta = EnderecoIPResposta;
                oFT.PortaRespostaAssinado = PortaResposta;
                oFT.FileBase64 = Convert.ToBase64String(fileData);
                oFT.RetornoAssinado = RetornoAssinado;
                oFT.UsuarioAutenticado = "1495";
                byte[] clientData = ConverterObjeto.SerializaObjetoXMLByteArray(oFT);
                TcpClient clientSocket = new TcpClient();
                clientSocket.ReceiveBufferSize = 1000;
                clientSocket.SendBufferSize = 1000;
                clientSocket.Connect(EnderecoIPEnvia, PortaEnvia);
                NetworkStream serverStream = clientSocket.GetStream();
                serverStream.Write(clientData, 0, clientData.Length);
                serverStream.Flush();
                clientSocket.Close();
                MsgCliente = "Arquivo [" + caminhoCompleto + "] transferido.";
                return true;
            }
            catch (Exception ex)
            {
                MsgCliente = ex.Message + ".";
                return false;
            }
        }
    }
}

