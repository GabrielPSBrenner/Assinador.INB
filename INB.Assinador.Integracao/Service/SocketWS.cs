﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace INB.Assinador.Integracao.Service
{
    //Class to handle each client request separatly
    public class SocketWS
    {
        public static string Mensagem;
        public static bool MensagemNova = false;

        TcpClient clientSocket;
        string clNo;

        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;            
            RecebeMensagem();
            inClientSocket.Close();
        }

        private void RecebeMensagem()
        {
            byte[] bytesFrom = new byte[10000];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;           
            NetworkStream networkStream = clientSocket.GetStream();
            networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
            dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("\0"));
            SocketWS.Mensagem = dataFromClient;
            SocketWS.MensagemNova = true;
            serverResponse = "Ok";
            sendBytes = Encoding.ASCII.GetBytes(serverResponse);
            networkStream.Write(sendBytes, 0, sendBytes.Length);
            networkStream.Flush();
            networkStream.Close();           
        }
    }
}