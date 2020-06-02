using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INB.Assinador.Integracao;
using System.Web.Services;
using System.ServiceModel;
using System.IO;
using INB.Assinador.Integracao.Interfaces;

namespace INB.Assinador.View.Service
{
    public class WSFile
    {
        public static MemoryStream getFileMS(Comunicacao oCom)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferSize = 2147483647;
            binding.MaxBufferPoolSize = 2147483647;
            EndpointAddress epAddr = new EndpointAddress(oCom.URLWS);           
            var channelHelper = ChannelFactory<IRequestChannelFile>.CreateChannel(binding, epAddr);            
            var clientProxy = (System.ServiceModel.Channels.IRequestChannel)channelHelper;            
            clientProxy.Open();
            var oService = (IWS)channelHelper;
            byte[] file = oService.ReceberArquivo(oCom.Codigo, oCom.Versao);            
            clientProxy.Close();
            MemoryStream stream = new MemoryStream(file);           
            stream.Flush();           
            return stream;
        }

        public static byte[] getFile(Comunicacao oCom)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferSize = 2147483647;
            binding.MaxBufferPoolSize = 2147483647;
            EndpointAddress epAddr = new EndpointAddress(oCom.URLWS);
            var channelHelper = ChannelFactory<IRequestChannelFile>.CreateChannel(binding, epAddr);
            var clientProxy = (System.ServiceModel.Channels.IRequestChannel)channelHelper;
            clientProxy.Open();
            var oService = (IWS)channelHelper;
            byte[] file = oService.ReceberArquivo(oCom.Codigo, oCom.Versao);
            clientProxy.Close();            
            return file;
        }

        public static void setFile(Comunicacao oCom , byte[] Arquivo)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferSize = 2147483647;
            binding.MaxBufferPoolSize = 2147483647;  
            EndpointAddress epAddr = new EndpointAddress(oCom.URLWS);
            var channelHelper = ChannelFactory<IRequestChannelFile>.CreateChannel(binding, epAddr);
            var clientProxy = (System.ServiceModel.Channels.IRequestChannel)channelHelper;
            clientProxy.Open();
            var oService = (IWS)channelHelper;          
            oService.EnviarArquivo(Arquivo, oCom.Codigo, oCom.Versao, oCom.UsuarioAutenticado, oCom.HashArquivoOriginal);           
            clientProxy.Close();
            return;
        }


        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

    }
}
