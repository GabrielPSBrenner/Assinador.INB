
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

namespace INB.Assinador.View
{
    public partial class FrmAssinador : Form
    {
        private INB.PDF.FrmPreview oFrm;

        Point _Position;
        int _Pagina;
        int _Largura;
        int _Altura;
        double _Escala;
        bool fecha = false;

        private Thread t;

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
            try
            {
               
                //INB.Assinador.Helper.Certificado.InstalaCertificadoTimeStamp();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao instalar o certificado do TimeStamp:" + ex.Message + ".", ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            try
            {

                INB.Assinador.Helper.Registro.SetStartup(true, Process.GetCurrentProcess().ProcessName);
            }
            catch (Exception ex)
            { }

            CboDigestAlgorithm.SelectedIndex = 2;


            CarregaCertificado();
            ReadSettings();

            t = new Thread(() => OpenServer());
            t.Start();
        }

        private void OpenServer(int Porta = 27525)
        {
            TcpListener serverSocket = new TcpListener(Porta);
            serverSocket.Start();
            int counter = 0;
            while (true)
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
                TipoSelo = PDF.FrmPreview.eTipoSelo.SEloCargoCREA;
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
                return true;
            }
            else
            {
                return false;
            }

        }

        private void BtnAssinarPDF_Click(object sender, EventArgs e)
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
                    AssinarArquivo(openFileDialog1.FileName, out ArquivoAssinado);
                }
                AtualizaSettings();
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
            if (INB.Assinador.Integracao.Service.SocketWS.MensagemNova)
            {
                this.Visible = true;
                this.Focus();
                timer1.Enabled = false;
                if (CboCertificados.Items.Count == 0 || CboCertificados.SelectedIndex == -1)
                {
                    MessageBox.Show("Para a integração funcionar é necessário ter um certificado válido selecionado. Insira o certificado, atualize e clique novamente na opção de assinar, no Sistema desejado.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CboCertificados.Focus();
                }
                else
                {

                    string Mensagem = INB.Assinador.Integracao.Service.SocketWS.Mensagem;
                    try
                    {

                        byte[] Arquivo;
                        Comunicacao oCom = (Comunicacao)ConverterObjeto.RetornaObjeto(Mensagem);
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
                                        Service.WSFile.setFile(oCom, ArquivoAssinado);
                                        MessageBox.Show("Arquivo assinado com sucesso.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                        Service.WSFile.setFile(oCom, ArquivoAssinado);                                       
                                        MessageBox.Show("Arquivo assinado com sucesso.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Assinatura não realizada. Caso deseje assinar, repita o processo, por favor.", ProductName, MessageBoxButtons.OK);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ocorreu um erro ao acessar o WebService de gravação do arquivo assinado, no Sistema. Erro:" + ex.Message + ".", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    INB.Assinador.Integracao.Service.SocketWS.MensagemNova = false;
                    timer1.Enabled = true;
                }

            }
        }

        private void MnuFechar_Click(object sender, EventArgs e)
        {
            fecha = true;
            this.Close();
            Environment.Exit(1);
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
            try { 
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "Temp\\";
            System.Diagnostics.Process.Start(path + "Apresentacao.pdf");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}