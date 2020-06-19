
using INB.Assinador.View.Service;
using System;
using System.Configuration;
using System.Drawing;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;
using INB.Assinador.Integracao;
using System.IO;
using System.Diagnostics;
using INB.Assinador.Helper;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Collections;
using System.Collections.Generic;

namespace INB.Assinador.View
{



    public partial class FrmAssinador : Form
    {


        private INB.PDF.FrmPreview oFrm;

        private static bool ContinuaServico = true;

        private static TcpListener serverSocket;
        private WebSocketServer oWS;

        Point _Position;
        int _Pagina;
        int _Largura;
        int _Altura;
        double _Escala;
        bool fecha = false;

        private Thread t;

        //public const string Login = "";
        //public const string Dominio = "INB";
        //public const string Senha = "";
        //const int LOGON32_PROVIDER_DEFAULT = 0;
        //const int LOGON32_LOGON_INTERACTIVE = 2;
        //IntPtr tokenHandle = new IntPtr(0);
        //IntPtr dupeTokenHandle = new IntPtr(0);


        private enum eTipoMensagem
        {
            Socket = 1,
            WebSocket = 2
        }


        private bool Ignorar(string Mensagem)
        {
            List<string> Msg = new List<string>();
            Msg.Add("OPTIONS / HTTP/1.0");
            Msg.Add("OPTIONS / RTSP/1.0");
            Msg.Add("GET / HTTP/1.0");
            Msg.Add("?");
            Msg.Add("GIOP");
            Msg.Add("HELP");
            Msg.Add("default");
            Msg.Add("DmdT");          
            foreach (string obj in Msg)
            {
                if (obj.Trim() == Mensagem.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        public FrmAssinador()
        {
            InitializeComponent();
        }

        private void PosicaoSelo(Point Position, int Pagina, int Largura, int Altura, double Escala)
        {
            _Position = Position;
            _Pagina = Pagina;
            _Largura = Largura;
            _Altura = Altura;
            _Escala = Escala;
        }

        private void CarregaCertificado()
        {
            CboCertificados.DataSource = null;
            CboCertificados.ValueMember = "SerialNumber";
            CboCertificados.DisplayMember = "Subject";
            CboCertificados.DataSource = CertSimples.ListaCertificado(INB.Assinador.Helper.Certificado.ListaCertificadosValidos());
        }
        private void FrmAssinador_Load(object sender, EventArgs e)
        {
            if (ChkIgnora.Checked)
            {
                this.Height = 155;
                ChkCargo.Checked = false;
                ChkCREA.Checked = false;
                ChkCRM.Checked = false;
                ChkCargo.Visible = false;
                ChkCREA.Visible = false;
                ChkCRM.Visible = false;
                TxtCargo.Visible = false;
                TxtCRMCREA.Visible = false;
                AtualizaSettings();
            }
            else
            {
                this.Height = 180;
            }


            CboDigestAlgorithm.SelectedIndex = 2;
            CarregaCertificado();
            ReadSettings();

            try
            {
                INB.Assinador.Helper.Registro.SetStartup(true, ProductName);
            }
            catch { }


            //tokenHandle = IntPtr.Zero;
            //bool returnValue = Impersonate.LogonUser(Login, Dominio, Senha, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);
            try
            {
                //SERVIDOR QUE RECEBE O JSON COM OS DADOS DO SERVIDOR.
                t = new Thread(() => OpenServer());
                t.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir o servidor 27525. A Integração com os Sistemas Internos não funcionará, acione o Help-Desk.: " + ex.Message + ".", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                //SERVIDOR Web-Socket, que permite a conexão por script.
                oWS = new WebSocketServer();
                oWS.Start("http://localhost:27524/Assinador/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir o servidor WebSocket. A Integração com o Alfresco não funcionará, acione o Help-Desk.: " + ex.Message + ".", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenServer(int Porta = 27525)
        {
            //tokenHandle = IntPtr.Zero;
            //bool returnValue = Impersonate.LogonUser(Login, Dominio, Senha, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

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

        private void AtualizaSettings()
        {
            UpdateSetting("ChkCREA", ChkCREA.Checked.ToString());
            UpdateSetting("ChkCRM", ChkCRM.Checked.ToString());
            UpdateSetting("ChkCargo", ChkCargo.Checked.ToString());
            UpdateSetting("TxtCargo", TxtCargo.Text);
            UpdateSetting("TxtCRMCREA", TxtCRMCREA.Text);
        }

        private void ReadSettings()
        {
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\"))
            {
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\");
            }

            Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            try
            {
                ChkCREA.Checked = bool.Parse(configuration.AppSettings.Settings["ChkCREA"].Value);
                ChkCRM.Checked = bool.Parse(configuration.AppSettings.Settings["ChkCRM"].Value);
                ChkCargo.Checked = bool.Parse(configuration.AppSettings.Settings["ChkCargo"].Value);
                TxtCargo.Text = configuration.AppSettings.Settings["TxtCargo"].Value;
                TxtCRMCREA.Text = configuration.AppSettings.Settings["TxtCRMCREA"].Value;
            }
            catch { }
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private string getFileName(string path, string file, string complemento = "_assinado")
        {
            string NewFileName = file + complemento;
            if (File.Exists(path + NewFileName + ".pdf"))
            {
                NewFileName = getFileName(path, NewFileName, "_assinado");
            }
            else
            {
                NewFileName += ".pdf";
            }
            return NewFileName;
        }
        private PDF.FrmPreview.eTipoSelo SeloUtilizado()
        {
            PDF.FrmPreview.eTipoSelo TipoSelo;
            if (OptAsPadrao.Checked)
            {
                if (ChkCargo.Checked == false && ChkCRM.Checked == false && ChkCREA.Checked == false)
                {
                    TipoSelo = PDF.FrmPreview.eTipoSelo.Selo;
                }
                else if (ChkCargo.Checked == true && ChkCRM.Checked == false && ChkCREA.Checked == false)
                {
                    TipoSelo = PDF.FrmPreview.eTipoSelo.SeloCargo;
                }
                else if (ChkCargo.Checked == true && ChkCRM.Checked == true && ChkCREA.Checked == false)
                {
                    TipoSelo = PDF.FrmPreview.eTipoSelo.SeloCargoCRM;
                }
                else if (ChkCargo.Checked == true && ChkCRM.Checked == false && ChkCREA.Checked == true)
                {
                    TipoSelo = PDF.FrmPreview.eTipoSelo.SeloCargoCREA;
                }
                else if (ChkCargo.Checked == false && ChkCRM.Checked == false && ChkCREA.Checked == true)
                {
                    TipoSelo = PDF.FrmPreview.eTipoSelo.SeloCREA;
                }
                else if (ChkCargo.Checked == false && ChkCRM.Checked == true && ChkCREA.Checked == false)
                {
                    TipoSelo = PDF.FrmPreview.eTipoSelo.SeloCRM;
                }
                else
                {
                    TipoSelo = PDF.FrmPreview.eTipoSelo.Selo;
                }
            }
            else
            {
                TipoSelo = PDF.FrmPreview.eTipoSelo.SeloCertifico;
            }
            return TipoSelo;
        }

        public bool AssinarArquivo(byte[] Arquivo, out byte[] returnFile, bool SemAbrir = false)
        {
            PDF.FrmPreview.eTipoSelo TipoSelo;
            TipoSelo = SeloUtilizado();
            oFrm = new PDF.FrmPreview(Arquivo, TipoSelo);
            oFrm.PosicaoSelo += new INB.PDF.FrmPreview.PosicaoSeloEventHandler(this.PosicaoSelo);
            oFrm.ShowDialog();
            if (oFrm.AssinaturaConfirmada)
            {
                oFrm.Close();
                int Pagina, largura, altura;
                int X, Y;
                Pagina = _Pagina;
                X = _Position.X;
                Y = _Position.Y;
                largura = _Largura;
                altura = _Altura;
                if (Pagina == 0 || X < 0 || Y < 0)
                {
                    MessageBox.Show("Foi impossível determinar a localização do selo, por favor, repetir o procedimento de assinatura.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    returnFile = null;
                    return false;
                }
                else
                {
                    MemoryStream SendFile = new MemoryStream(Arquivo);
                    byte[] ReceiveFile;
                    INB.Assinador.Model.AssinaComTokenITextSharp.AssinaPDF(SendFile, out ReceiveFile, CboCertificados.SelectedValue.ToString(), Pagina, X, Y, _Escala, ChkCargo.Checked, ChkCREA.Checked, ChkCRM.Checked, TxtCargo.Text, TxtCRMCREA.Text, ChkCarimboTempo.Checked, TxtTimeStampServer.Text, TxtUsuarioTS.Text, TxtSenhaTS.Text, "Assinatura Digital de Documento", ChkAplicaPolitica.Checked, CboDigestAlgorithm.Text);
                    //returnFile = new byte[ReceiveFile.Length];
                    returnFile = ReceiveFile;
                }
                return true;
            }
            else
            {
                returnFile = null;
                return false;
            }
        }

        public bool AssinarArquivo(string Arquivo, out string returnFileName, bool SemAbrir = false)
        {
            returnFileName = "";
            PDF.FrmPreview.eTipoSelo TipoSelo;
            TipoSelo = SeloUtilizado();
            oFrm = new PDF.FrmPreview(Arquivo, TipoSelo);
            oFrm.PosicaoSelo += new INB.PDF.FrmPreview.PosicaoSeloEventHandler(this.PosicaoSelo);
            oFrm.ShowDialog();
            if (oFrm.AssinaturaConfirmada)
            {

                int Pagina, largura, altura;
                int X, Y;
                Pagina = _Pagina;
                X = _Position.X;
                Y = _Position.Y;
                largura = _Largura;
                altura = _Altura;
                if (Pagina == 0 || X < 0 || Y < 0)
                {
                    MessageBox.Show("Foi impossível determinar a localização do selo, por favor, repetir o procedimento de assinatura.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    string SignedFileName;
                    //******
                    //pensar quando já tiver o nome do arquivo
                    string FileName = Path.GetFileName(Arquivo);
                    string PathFile = Path.GetDirectoryName(Arquivo) + "\\";
                    FileName = getFileName(PathFile, FileName.Substring(0, FileName.Length - 4));
                    SignedFileName = PathFile + FileName;
                    returnFileName = SignedFileName;
                    INB.Assinador.Model.AssinaComTokenITextSharp.AssinaPDF(Arquivo, SignedFileName, CboCertificados.SelectedValue.ToString(), Pagina, X, Y, _Escala, ChkCargo.Checked, ChkCREA.Checked, ChkCRM.Checked, TxtCargo.Text, TxtCRMCREA.Text, ChkCarimboTempo.Checked, TxtTimeStampServer.Text, TxtUsuarioTS.Text, TxtSenhaTS.Text, "Assinatura Digital de Documento", ChkAplicaPolitica.Checked, CboDigestAlgorithm.Text);

                    if (SemAbrir == false)
                    {
                        if (MessageBox.Show("Arquivo assinado com sucesso.Deseja abri-lo?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(SignedFileName);
                        }
                    }
                }
                try
                {
                    oFrm.Close();
                }
                catch (Exception ex) { }
                return true;
            }
            else
            {
                try
                {
                    oFrm.Dispose();
                }
                catch (Exception ex) { }
                return false;
            }

        }

        private void BtnAssinarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (CboCertificados.Items.Count == 0 || CboCertificados.SelectedIndex == -1)
                {
                    MessageBox.Show("Por favor, selecione um certificado válido.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    BtnAtualizar_Click(sender, e);
                    CboCertificados.Focus();
                }
                else
                {
                    openFileDialog1.Filter = "Arquivos pdf (*.pdf)|*.pdf";
                    openFileDialog1.FileName = "";
                    if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
                    {
                        string ArquivoAssinado;
                        try
                        {
                            AssinarArquivo(openFileDialog1.FileName, out ArquivoAssinado);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Um erro aconteceu ao executar o processo de assinatura verifique se o certificado está corretamente instalado e, em caso de Token, na porta USB. Erro: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    AtualizaSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Um erro aconteceu ao executar o processo de assinatura. Erro: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            AtualizaSettings();
            this.Close();
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            CarregaCertificado();
            AtualizaSettings();
        }

        private void ChkCREA_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCREA.Checked)
            {
                ChkCRM.Checked = false;
            }
            if (ChkCREA.Checked == false && ChkCRM.Checked == false)
            {
                TxtCRMCREA.Text = "";
                TxtCRMCREA.Enabled = false;
            }
            else
            {
                TxtCRMCREA.Enabled = true;
            }

        }

        private void ChkCRM_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCRM.Checked)
            {
                ChkCREA.Checked = false;
            }
            if (ChkCREA.Checked == false && ChkCRM.Checked == false)
            {
                TxtCRMCREA.Text = "";
                TxtCRMCREA.Enabled = false;
            }
            else
            {
                TxtCRMCREA.Enabled = true;
            }


        }

        private void ChkCargo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCargo.Checked == false)
            {
                TxtCargo.Text = "";
            }
            TxtCargo.Enabled = ChkCargo.Checked;
        }

        private void assinarPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Activate();
            this.Focus();

        }

        private void FrmAssinador_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!fecha)
            {

                e.Cancel = true;
                this.Visible = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            // MnuBandeja.Visible = true;
        }

        private void MnuSobre_Click(object sender, EventArgs e)
        {
            AboutBox1 Frm = new AboutBox1();
            Frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            eTipoMensagem TipoMensagem;

            bool RecebeuMensagem = false;

            timer1.Enabled = false;


            string Mensagem = "";


            //MENSAGEM VIA WEBSOCKET
            if (oWS.MensagemNova)
            {
                Mensagem = oWS.Mensagem;
                oWS.MensagemNova = false;
                oWS.Mensagem = "";
                TipoMensagem = eTipoMensagem.WebSocket;
                RecebeuMensagem = true;
            }

            //MENSAGEM VIA SOCKET
            if (INB.Assinador.Integracao.Service.SocketWS.MensagemNova)
            {
                Mensagem = INB.Assinador.Integracao.Service.SocketWS.Mensagem;
                INB.Assinador.Integracao.Service.SocketWS.MensagemNova = false;
                INB.Assinador.Integracao.Service.SocketWS.Mensagem = "";
                TipoMensagem = eTipoMensagem.WebSocket;
                RecebeuMensagem = true;
            }

            if (RecebeuMensagem)
            {
                Comunicacao oCom = null;

                //CONVERTE O TEXTO RECEBIDO EM OBJETO
                try
                {
                    if (Ignorar(Mensagem))
                    {
                        timer1.Enabled = true;
                        return; 
                    }
                    else
                    {
                        this.Visible = true;
                        this.Focus();
                        this.WindowState = FormWindowState.Normal;
                        this.BringToFront();
                        oCom = (Comunicacao)ConverterObjeto.RetornaObjeto(Mensagem);
                    }                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Um erro ocorreu ao converter o objeto para o tipo Comunicacao. O processo precisa ser repetido e se o erro persistir, notifique o Help-Desk | Mensagem REcebida: " + Mensagem + " | Erro: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timer1.Enabled = true;
                    return;
                }

                if (CboCertificados.Items.Count == 0 || CboCertificados.SelectedIndex == -1)
                {
                    MessageBox.Show("Para a integração funcionar é necessário ter um certificado válido selecionado. Insira e selecione o certificado e repita o processo.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    timer1.Enabled = true;
                    CboCertificados.Focus();
                    return;
                }


                try
                {
                    byte[] Arquivo;
                    try
                    {
                        Arquivo = Service.WSFile.getFile(oCom);
                        try
                        {
                            MemoryStream ArquivoMS;
                            if (ChkSalvaArquivo.Checked)
                            {
                                ArquivoMS = new MemoryStream(Arquivo);
                                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\";
                                string pathArquivoRecebido = path + oCom.Codigo.ToString() + "_" + oCom.Versao.ToString() + ".pdf";
                                using (FileStream fs = new FileStream(pathArquivoRecebido, FileMode.OpenOrCreate))
                                {
                                    ArquivoMS.CopyTo(fs);
                                    fs.Flush();
                                }
                                ArquivoMS.Close();
                                string FileNameAssinado;

                                if (AssinarArquivo(pathArquivoRecebido, out FileNameAssinado, true))
                                {
                                    byte[] ArquivoAssinado = INB.Assinador.Helper.FileHelper.ReadFile(FileNameAssinado);
                                    try
                                    {
                                        Service.WSFile.setFile(oCom, ArquivoAssinado);
                                        MessageBox.Show("Arquivo assinado com sucesso.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Ocorreu um erro na assinatura e o processo inteiro precisa ser refeito. Erro: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                try
                                {
                                    System.IO.File.Delete(FileNameAssinado);
                                    System.IO.File.Delete(pathArquivoRecebido);
                                }
                                catch (Exception ex) { }
                            }
                            else
                            {
                                byte[] ArquivoAssinado;
                                if (AssinarArquivo(Arquivo, out ArquivoAssinado, true))
                                {
                                    try
                                    {

                                        Service.WSFile.setFile(oCom, ArquivoAssinado);
                                        MessageBox.Show("Arquivo assinado com sucesso.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Ocorreu um erro na assinatura e o processo inteiro precisa ser refeito. Erro: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Um problema ocorreu no envio do arquivo assinado para o servidor. Assinatura não realizada. Repita todo o processo, por favor.", ProductName, MessageBoxButtons.OK);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ocorreu um erro no Sistema. Erro:" + ex.Message + ".", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao acessar o WebService de leitura de arquivo do Sistema. Erro:" + ex.Message + ".", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Um erro ocorreu ao tentar buscar o arquivo no Servidor.Erro: " + ex.Message + ".", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            timer1.Enabled = true;
        }

        private void MnuFechar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("O assinador será encerrado e não poderá ser utilizado até ser reiniciado novamente. Deseja realmente encerrar?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                fecha = true;
                this.Close();
                Environment.Exit(1);
            }
        }

        private void ChkFileSocket_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CboCertificados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CertSimples oCertSimples = (CertSimples)(CboCertificados.SelectedItem);
            //X509Certificate2 oCert = oCertSimples.Certificado;
            //X509ExtensionCollection oCol = oCert.Extensions;
            //string MSG="";
            //foreach (X509Extension oExt in oCol)
            //{
            //    AsnEncodedData asndata = new AsnEncodedData(oExt.Oid, oExt.RawData);
            //    MSG += "Tipo da Extensão: " + oExt.Oid.FriendlyName;
            //    MSG += "|| Oid Value : " +  asndata.Oid.Value;
            //    MSG += "|| Raw data length:  " +  asndata.RawData.Length.ToString();
            //    MSG += Convert.ToChar(Keys.Enter);


            //}

            // MessageBox.Show(MSG);
        }

        private void MnuInfoAdicionais_Click(object sender, EventArgs e)
        {
            FrmInfo oFrm = new FrmInfo(MnuInfoAdicionais);
            oFrm.Show();
        }

        private void MnuApresentacao_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\";
                System.Diagnostics.Process.Start(path + "Apresentacao.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmAssinador_Activated(object sender, EventArgs e)
        {
            this.Visible = true;
            this.Activate();
            this.BringToFront();
            this.Focus();
        }

        private void MnuVerificarAtualizacao_Click(object sender, EventArgs e)
        {
            FrmUpdate oFrm = new FrmUpdate(this, MnuVerificarAtualizacao);
            oFrm.Show();
        }

        private void FrmAssinador_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //serverSocket.EndAcceptSocket(null );
                // serverSocket.EndAcceptTcpClient(null);
                ContinuaServico = false;
                serverSocket.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                t.Abort();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                oWS.Close();
            }
            catch { }

            Application.Exit();
        }

        private void MnuIO_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\";
                System.Diagnostics.Process.Start(path + "AssinaturaINB.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}