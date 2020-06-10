using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INB.Assinador.Integracao;
using INB.Assinador.Integracao.Service;
using INB.Assinador.Helper;
using System.IO;
using System.Net;
using iText.Signatures;
using Org.BouncyCastle.Tsp;
using System.Security.Policy;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using Org.BouncyCastle.Math;

namespace INB.Assinador.TesteClientTCP
{
    public partial class FrmTeste : Form
    {
        public FrmTeste()
        {
            InitializeComponent();
        }

        private void BtnEnviaMSG_Click(object sender, EventArgs e)
        {
            Comunicacao oCom = new Comunicacao();
            oCom.Codigo = "1";
            oCom.Versao = "1";
            oCom.UserID = "admcomp";
            oCom.Senha = "TESTE";
            oCom.URLWS = "http://RES700850/WSTESTE/WS.xxx";
            var retorno = INB.Assinador.Integracao.Service.Cliente.EnviaDados(oCom, "127.0.0.1", 27525);
            MessageBox.Show(retorno);
        }

        private void BtnIniciaServidorArquivo_Click(object sender, EventArgs e)
        {
            BtnIniciaServidorArquivo.Enabled = false;
            BtnIniciaServidorArquivo.Text = "Servidor 27526 Iniciado...";
            FileSocket oFS = new FileSocket(27526);
        }

        private void BtnServidorArquivo2_Click(object sender, EventArgs e)
        {
            BtnServidorArquivo2.Enabled = false;
            BtnServidorArquivo2.Text = "Servidor 27527 Iniciado...";
            FileSocket oFS = new FileSocket(27527);
        }

        private void BtnEnviaArquivo_Click(object sender, EventArgs e)
        {
            int Codigo, Versao;
            string MeuIP = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            if (MeuIP == "::1")
            {
                MeuIP = Dns.GetHostAddresses(Dns.GetHostName())[1].ToString();
            }
            string IPDestino = MeuIP; //Nesse exemplo é o mesmo IP, no exemplo real, seria o IP da máquina do assinador
            Codigo = int.Parse(TxtArquivo.Text);
            Versao = int.Parse(TxtVersao.Text);
            string Arquivo = TxtArquivo.Text + "_" + TxtVersao.Text + ".pdf";
            string PathRecebido = System.AppDomain.CurrentDomain.BaseDirectory + "ArquivoReceber\\";
            string PathEnvio = System.AppDomain.CurrentDomain.BaseDirectory + "ArquivoEnviar\\";
            if (!Directory.Exists(PathRecebido))
            {
                Directory.CreateDirectory(PathRecebido);
            }
            if (!File.Exists(PathEnvio + Arquivo))
            {
                MessageBox.Show("Arquivo não encontrato", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string MsgCliente;
            FileSocket.EnviarArquivoViaSocket(PathEnvio + Arquivo.ToString(), Codigo, Versao, MeuIP, IPDestino, 27526, 27527, false, out MsgCliente);
            MessageBox.Show(MsgCliente, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string PathRecebido = System.AppDomain.CurrentDomain.BaseDirectory + "ArquivoReceber\\";
            string PathEnvio = System.AppDomain.CurrentDomain.BaseDirectory + "ArquivoEnviar\\";
            if (FileSocket.ArquivoNovo)
            {
                timer1.Enabled = false;
                FileTransfer oFT = FileSocket.oFT;
                byte[] Arquivo = Convert.FromBase64String(oFT.FileBase64);
                if (!oFT.RetornoAssinado)
                {
                    string pathCompleto = PathRecebido + oFT.FileName + ".pdf";
                    FileStream fs = new FileStream(pathCompleto, FileMode.OpenOrCreate);
                    MemoryStream stream = new MemoryStream(Arquivo);
                    stream.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                    stream.Close();
                }
                else
                {
                    string pathCompleto = PathRecebido + oFT.FileName + "assinado.pdf";

                    FileStream fs = new FileStream(pathCompleto, FileMode.OpenOrCreate);
                    MemoryStream stream = new MemoryStream(Arquivo);
                    stream.CopyTo(fs);
                    fs.Flush();
                    fs.Close();
                    stream.Close();
                }
                FileSocket.ArquivoNovo = false;
                FileSocket.oFT = null;
                timer1.Enabled = true;
            }
        }
        private bool TestaTimeStamp(string URL)
        {
            SHA1 sha1 = SHA1CryptoServiceProvider.Create();
            byte[] hash = sha1.ComputeHash(Encoding.ASCII.GetBytes("TESTE DE SELO"));

            TimeStampRequestGenerator reqGen = new TimeStampRequestGenerator();
            reqGen.SetCertReq(true);

            TimeStampRequest tsReq = reqGen.Generate(TspAlgorithms.Sha1, hash, BigInteger.ValueOf(100)); byte[] tsData = tsReq.GetEncoded();

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            req.Method = "POST";
            req.ContentType = "application/timestamp-query";
            req.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("9024:")));
            req.ContentLength = tsData.Length;

            Stream reqStream = req.GetRequestStream();
            reqStream.Write(tsData, 0, tsData.Length);
            reqStream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            if (res == null) { return false; }
            else
            {
                Stream resStream = new BufferedStream(res.GetResponseStream());

                TimeStampResponse tsRes = new TimeStampResponse(resStream); resStream.Close();

                try
                {
                    tsRes.Validate(tsReq);
                    resStream.Close();
                    return true;
                }
                catch (TspException e)
                {
                    resStream.Close();
                    return false;
                }
                //saveresponse
            }

        }
        private TimeStampResponse getTimeStamp(string URL)
        {
            //RFC3161 compliant Time Stamp Authority (TSA) server
            TimeStampResponse response = null;
            byte[] sha1Digest = new byte [20];
            TimeStampRequestGenerator reqGen = new TimeStampRequestGenerator();
            //SecureRandom randomGenerator = SecureRandom.GetInstance("SHA1PRNG");
            //long nonce = randomGenerator.NextLong();


            Org.BouncyCastle.Asn1.DerObjectIdentifier oDR = new Org.BouncyCastle.Asn1.DerObjectIdentifier("1.3.6.1.4.1.311.3.2.1");
            // request with digestAlgorithmOID, byte[] digest, java.math.BigInteger nonc
            //byte[] digest = TSToken.TimeStampInfo.GetMessageImprintDigest();

            TimeStampRequest request = reqGen.Generate(oDR, sha1Digest, Org.BouncyCastle.Math.BigInteger.ValueOf(100));
            byte[] reqData = request.GetEncoded();
            WebRequest conn = WebRequest.Create(URL);
            Stream st = conn.GetRequestStream();
            st.Write(reqData, 0, reqData.Length);
            st.Flush();
            st.Close();
            var resposta = conn.GetResponse();
            Stream stReponse = resposta.GetResponseStream();
            response = new TimeStampResponse(stReponse);
            stReponse.Close();
            return response;
        }

        private void BtnEncerrarServidor_Click(object sender, EventArgs e)
        {
            FileSocket.ParaServidor = true;
            BtnIniciaServidorArquivo.Enabled = true;
            BtnIniciaServidorArquivo.Text = "Servidor Arquivo 1";
        }

        private void BtnTestaTimeStamp_Click(object sender, EventArgs e)
        {
            List<string> oListaServer = new List<string>();
            oListaServer.Add("https://freetsa.org/tsr");
            oListaServer.Add("http://timestamp.globalsign.com/scripts/timstamp.dll");
            //oListaServer.Add("https://timestamp.geotrust.com/tsa");
            oListaServer.Add("http://timestamp.comodoca.com/rfc3161");
            //oListaServer.Add("http://timestamp.wosign.com");
            oListaServer.Add("http://tsa.startssl.com/rfc3161");
            oListaServer.Add("http://time.certum.pl");
            oListaServer.Add("http://timestamp.digicert.com");            
            //oListaServer.Add("http://dse200.ncipher.com/TSS/HttpTspServer");
            //oListaServer.Add("http://tsa.safecreative.org");
            oListaServer.Add("http://zeitstempel.dfn.de");
            oListaServer.Add("https://ca.signfiles.com/tsa/get.aspx");
            //oListaServer.Add("http://services.globaltrustfinder.com/adss/tsa");
            //oListaServer.Add("https://tsp.iaik.tugraz.at/tsp/TspRequest");
            oListaServer.Add("http://timestamp.apple.com/ts01");
            //oListaServer.Add("http://timestamp.entrust.net/TSS/RFC3161sha2TS");

            TSAClientBouncyCastle tsaClient = null;
            foreach (string URL in oListaServer)
            {
                try
                {
                    bool teste = TestaTimeStamp(URL);
                    if (teste)
                    {
                        MessageBox.Show("O servidor: " + URL + " respondeu o timestamp corretamente", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("O servidor: " + URL + " não respondeu o timestamp corretamente", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Um erro ocorreu ao tentar acessar o servidor: " + URL + ". Erro: " + getMSGErro(ex), ProductName, MessageBoxButtons.OK,  MessageBoxIcon.Error);
                }

            }

        }

        private string getMSGErro(Exception ex)
        {
            string msgErro;
            if (ex.InnerException != null)
            {
                msgErro = ex.Message + " || " + getMSGErro(ex.InnerException);
            }
            else
            {
                msgErro = ex.Message;
            }
            return msgErro;
        }

        private void FrmTeste_Load(object sender, EventArgs e)
        {

        }
    }
}
