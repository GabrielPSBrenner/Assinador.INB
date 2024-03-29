﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.IO;
using System.Security.Principal;

//http://marcoluglio.github.io/tutorialwebsockets/


namespace INB.Assinador.Integracao
{
    public class WebSocketServer
    {
        public const string Login = "";
        public const string Dominio = "INB";
        public const string Senha = "";
        const int LOGON32_PROVIDER_DEFAULT = 0;
        const int LOGON32_LOGON_INTERACTIVE = 2;
        IntPtr tokenHandle = new IntPtr(0);
        IntPtr dupeTokenHandle = new IntPtr(0);


        private enum eTipoMenssagem
        {
            Texto = 0,
            Binario = 1
        }

        private eTipoMenssagem TipoMensagem;
        private int count = 0;
        public bool Ligado = true;
        public bool MensagemNova = false;
        public string Mensagem = "";
        public byte[] ByteMensagem;

        private HttpListener listener;
        public async void Start(string listenerPrefix)
        {
            tokenHandle = IntPtr.Zero;

            listener = new HttpListener();
            listener.Prefixes.Add(listenerPrefix);
            listener.Start();

            while (Ligado)
            {
                HttpListenerContext listenerContext = await listener.GetContextAsync();
                if (listenerContext.Request.IsWebSocketRequest)
                {
                    ProcessRequest(listenerContext);
                }
                else
                {
                    listenerContext.Response.StatusCode = 400;
                    listenerContext.Response.Close();
                }
            }

        }

        public async void Close()
        {
            Ligado = false;
            try
            {
                listener.Abort();
            }
            catch { }
            try
            {
                listener.Close();
            }
            catch { }

        }
        private async void ProcessRequest(HttpListenerContext listenerContext)
        {

            WebSocketContext webSocketContext = null;
            try
            {
                // When calling `AcceptWebSocketAsync` the negotiated subprotocol must be specified. This sample assumes that no subprotocol 
                // was requested. 
                webSocketContext = await listenerContext.AcceptWebSocketAsync(subProtocol: null);
                Interlocked.Increment(ref count);
                //Console.WriteLine("Processed: {0}", count);
            }
            catch (Exception e)
            {
                // The upgrade process failed somehow. For simplicity lets assume it was a failure on the part of the server and indicate this using 500.
                listenerContext.Response.StatusCode = 500;
                listenerContext.Response.Close();
                //Console.WriteLine("Exception: {0}", e);
                return;
            }

            WebSocket webSocket = webSocketContext.WebSocket;

            MemoryStream oMS = new MemoryStream();
            try
            {
                byte[] receiveBuffer = new byte[1024];
                // While the WebSocket connection remains open run a simple loop that receives data and sends it back.
                while (webSocket.State == WebSocketState.Open)
                {
                    // The first step is to begin a receive operation on the WebSocket. `ReceiveAsync` takes two parameters:
                    //
                    // * An `ArraySegment` to write the received data to. 
                    // * A cancellation token. In this example we are not using any timeouts so we use `CancellationToken.None`.
                    //
                    // `ReceiveAsync` returns a `Task<WebSocketReceiveResult>`. The `WebSocketReceiveResult` provides information on the receive operation that was just 
                    // completed, such as:                
                    //
                    // * `WebSocketReceiveResult.MessageType` - What type of data was received and written to the provided buffer. Was it binary, utf8, or a close message?                
                    // * `WebSocketReceiveResult.Count` - How many bytes were read?                
                    // * `WebSocketReceiveResult.EndOfMessage` - Have we finished reading the data for this message or is there more coming?
                    WebSocketReceiveResult receiveResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);


                    // The WebSocket protocol defines a close handshake that allows a party to send a close frame when they wish to gracefully shut down the connection.
                    // The party on the other end can complete the close handshake by sending back a close frame.
                    //
                    // If we received a close frame then lets participate in the handshake by sending a close frame back. This is achieved by calling `CloseAsync`. 
                    // `CloseAsync` will also terminate the underlying TCP connection once the close handshake is complete.
                    //
                    // The WebSocket protocol defines different status codes that can be sent as part of a close frame and also allows a close message to be sent. 
                    // If we are just responding to the client's request to close we can just use `WebSocketCloseStatus.NormalClosure` and omit the close message.
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    }
                    // This echo server can't handle text frames so if we receive any we close the connection with an appropriate status code and message.
                    else if (receiveResult.MessageType == WebSocketMessageType.Text)
                    {
                        await webSocket.SendAsync(new ArraySegment<byte>(receiveBuffer, 0, receiveResult.Count), WebSocketMessageType.Text, receiveResult.EndOfMessage, CancellationToken.None);
                        TipoMensagem = eTipoMenssagem.Texto;
                        oMS.Write(receiveBuffer, 0, receiveResult.Count);
                    }
                    // Otherwise we must have received binary data. Send it back by calling `SendAsync`. Note the use of the `EndOfMessage` flag on the receive result. This
                    // means that if this echo server is sent one continuous stream of binary data (with EndOfMessage always false) it will just stream back the same thing.
                    // If binary messages are received then the same binary messages are sent back.
                    else
                    {
                        await webSocket.SendAsync(new ArraySegment<byte>(receiveBuffer, 0, receiveResult.Count), WebSocketMessageType.Binary, receiveResult.EndOfMessage, CancellationToken.None);
                        TipoMensagem = eTipoMenssagem.Binario;
                        oMS.Write(receiveBuffer, 0, receiveResult.Count);
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (webSocket != null)
                    webSocket.Dispose();
            }

            if (oMS.Length > 0)
            {
                if (TipoMensagem == eTipoMenssagem.Texto)
                {
                    Mensagem = Encoding.UTF8.GetString(oMS.ToArray());
                    ByteMensagem = oMS.ToArray();
                    MensagemNova = true;
                }
                else
                {
                    Mensagem = "";
                    ByteMensagem = oMS.ToArray();
                    MensagemNova = true;
                }
            }
        }
    }

    public static class HelperExtensions
    {
        public static Task GetContextAsync(this HttpListener listener)
        {
            return Task.Factory.FromAsync<HttpListenerContext>(listener.BeginGetContext, listener.EndGetContext, TaskCreationOptions.None);
        }
    }
}
