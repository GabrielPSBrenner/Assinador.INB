namespace INB.Assinador.View
{
    partial class FrmAssinador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAssinador));
            this.CboCertificados = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ChkCargo = new System.Windows.Forms.CheckBox();
            this.ChkCRM = new System.Windows.Forms.CheckBox();
            this.ChkCREA = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtCargo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtCRMCREA = new System.Windows.Forms.TextBox();
            this.MnuBandeja = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.assinarPDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuApresentacao = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuInfoAdicionais = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuIO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuVerificarAtualizacao = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSobre = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ChkCarimboTempo = new System.Windows.Forms.CheckBox();
            this.ChkAplicaPolitica = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CboDigestAlgorithm = new System.Windows.Forms.ComboBox();
            this.TxtTimeStampServer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtUsuarioTS = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtSenhaTS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ChkWSService = new System.Windows.Forms.CheckBox();
            this.ChkFileSocket = new System.Windows.Forms.CheckBox();
            this.ChkSalvaArquivo = new System.Windows.Forms.CheckBox();
            this.ChkIgnora = new System.Windows.Forms.CheckBox();
            this.BtnAssinarPDF = new System.Windows.Forms.Button();
            this.BtnFechar = new System.Windows.Forms.Button();
            this.BtnAtualizar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.OptAsPadrao = new System.Windows.Forms.RadioButton();
            this.OptAsCertifico = new System.Windows.Forms.RadioButton();
            this.MnuBandeja.SuspendLayout();
            this.SuspendLayout();
            // 
            // CboCertificados
            // 
            this.CboCertificados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboCertificados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCertificados.FormattingEnabled = true;
            this.CboCertificados.Location = new System.Drawing.Point(105, 10);
            this.CboCertificados.Margin = new System.Windows.Forms.Padding(9);
            this.CboCertificados.Name = "CboCertificados";
            this.CboCertificados.Size = new System.Drawing.Size(589, 28);
            this.CboCertificados.TabIndex = 3;
            this.CboCertificados.SelectedIndexChanged += new System.EventHandler(this.CboCertificados_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Certificado";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ChkCargo
            // 
            this.ChkCargo.AutoSize = true;
            this.ChkCargo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCargo.Location = new System.Drawing.Point(6, 192);
            this.ChkCargo.Margin = new System.Windows.Forms.Padding(2);
            this.ChkCargo.Name = "ChkCargo";
            this.ChkCargo.Size = new System.Drawing.Size(112, 24);
            this.ChkCargo.TabIndex = 5;
            this.ChkCargo.Text = "Inclui Cargo";
            this.ChkCargo.UseVisualStyleBackColor = true;
            this.ChkCargo.Visible = false;
            this.ChkCargo.CheckedChanged += new System.EventHandler(this.ChkCargo_CheckedChanged);
            // 
            // ChkCRM
            // 
            this.ChkCRM.AutoSize = true;
            this.ChkCRM.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCRM.Location = new System.Drawing.Point(138, 193);
            this.ChkCRM.Margin = new System.Windows.Forms.Padding(2);
            this.ChkCRM.Name = "ChkCRM";
            this.ChkCRM.Size = new System.Drawing.Size(105, 24);
            this.ChkCRM.TabIndex = 5;
            this.ChkCRM.Text = "Inclui CRM";
            this.ChkCRM.UseVisualStyleBackColor = true;
            this.ChkCRM.Visible = false;
            this.ChkCRM.CheckedChanged += new System.EventHandler(this.ChkCRM_CheckedChanged);
            // 
            // ChkCREA
            // 
            this.ChkCREA.AutoSize = true;
            this.ChkCREA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCREA.Location = new System.Drawing.Point(258, 193);
            this.ChkCREA.Margin = new System.Windows.Forms.Padding(2);
            this.ChkCREA.Name = "ChkCREA";
            this.ChkCREA.Size = new System.Drawing.Size(114, 24);
            this.ChkCREA.TabIndex = 5;
            this.ChkCREA.Text = "Inclui CREA";
            this.ChkCREA.UseVisualStyleBackColor = true;
            this.ChkCREA.Visible = false;
            this.ChkCREA.CheckedChanged += new System.EventHandler(this.ChkCREA_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 225);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cargo";
            this.label3.Visible = false;
            // 
            // TxtCargo
            // 
            this.TxtCargo.Location = new System.Drawing.Point(105, 220);
            this.TxtCargo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtCargo.Name = "TxtCargo";
            this.TxtCargo.Size = new System.Drawing.Size(392, 26);
            this.TxtCargo.TabIndex = 7;
            this.TxtCargo.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 258);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "CRM/CREA";
            this.label4.Visible = false;
            // 
            // TxtCRMCREA
            // 
            this.TxtCRMCREA.Location = new System.Drawing.Point(105, 252);
            this.TxtCRMCREA.Margin = new System.Windows.Forms.Padding(2);
            this.TxtCRMCREA.Name = "TxtCRMCREA";
            this.TxtCRMCREA.Size = new System.Drawing.Size(203, 26);
            this.TxtCRMCREA.TabIndex = 11;
            this.TxtCRMCREA.Visible = false;
            // 
            // MnuBandeja
            // 
            this.MnuBandeja.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assinarPDFToolStripMenuItem,
            this.toolStripMenuItem1,
            this.MnuApresentacao,
            this.MnuInfoAdicionais,
            this.toolStripSeparator2,
            this.MnuIO,
            this.toolStripSeparator1,
            this.MnuVerificarAtualizacao,
            this.MnuSobre,
            this.toolStripMenuItem2,
            this.MnuFechar});
            this.MnuBandeja.Name = "MnuBandeja";
            this.MnuBandeja.Size = new System.Drawing.Size(199, 182);
            // 
            // assinarPDFToolStripMenuItem
            // 
            this.assinarPDFToolStripMenuItem.Image = global::INB.Assinador.View.Properties.Resources.assinatura;
            this.assinarPDFToolStripMenuItem.Name = "assinarPDFToolStripMenuItem";
            this.assinarPDFToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.assinarPDFToolStripMenuItem.Text = "&Assinar PDF";
            this.assinarPDFToolStripMenuItem.Click += new System.EventHandler(this.assinarPDFToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(195, 6);
            // 
            // MnuApresentacao
            // 
            this.MnuApresentacao.Name = "MnuApresentacao";
            this.MnuApresentacao.Size = new System.Drawing.Size(198, 22);
            this.MnuApresentacao.Text = "Apresentação";
            this.MnuApresentacao.Click += new System.EventHandler(this.MnuApresentacao_Click);
            // 
            // MnuInfoAdicionais
            // 
            this.MnuInfoAdicionais.Name = "MnuInfoAdicionais";
            this.MnuInfoAdicionais.Size = new System.Drawing.Size(198, 22);
            this.MnuInfoAdicionais.Text = "&Informações Adicionais";
            this.MnuInfoAdicionais.Click += new System.EventHandler(this.MnuInfoAdicionais_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(195, 6);
            // 
            // MnuIO
            // 
            this.MnuIO.Name = "MnuIO";
            this.MnuIO.Size = new System.Drawing.Size(198, 22);
            this.MnuIO.Text = "Instrução Operacional";
            this.MnuIO.Click += new System.EventHandler(this.MnuIO_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // MnuVerificarAtualizacao
            // 
            this.MnuVerificarAtualizacao.Name = "MnuVerificarAtualizacao";
            this.MnuVerificarAtualizacao.Size = new System.Drawing.Size(198, 22);
            this.MnuVerificarAtualizacao.Text = "&Verificar Atualização";
            this.MnuVerificarAtualizacao.Click += new System.EventHandler(this.MnuVerificarAtualizacao_Click);
            // 
            // MnuSobre
            // 
            this.MnuSobre.Name = "MnuSobre";
            this.MnuSobre.Size = new System.Drawing.Size(198, 22);
            this.MnuSobre.Text = "&Sobre";
            this.MnuSobre.Click += new System.EventHandler(this.MnuSobre_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(195, 6);
            // 
            // MnuFechar
            // 
            this.MnuFechar.Name = "MnuFechar";
            this.MnuFechar.Size = new System.Drawing.Size(198, 22);
            this.MnuFechar.Text = "&Fechar";
            this.MnuFechar.Click += new System.EventHandler(this.MnuFechar_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Assinador de documentos com logotipo da INB";
            this.notifyIcon1.BalloonTipTitle = "Assinador - INB";
            this.notifyIcon1.ContextMenuStrip = this.MnuBandeja;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Assinador - INB";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChkCarimboTempo
            // 
            this.ChkCarimboTempo.AutoSize = true;
            this.ChkCarimboTempo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCarimboTempo.Checked = true;
            this.ChkCarimboTempo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkCarimboTempo.Location = new System.Drawing.Point(286, 130);
            this.ChkCarimboTempo.Margin = new System.Windows.Forms.Padding(2);
            this.ChkCarimboTempo.Name = "ChkCarimboTempo";
            this.ChkCarimboTempo.Size = new System.Drawing.Size(162, 24);
            this.ChkCarimboTempo.TabIndex = 12;
            this.ChkCarimboTempo.Text = "Carimbo do Tempo";
            this.ChkCarimboTempo.UseVisualStyleBackColor = true;
            this.ChkCarimboTempo.Visible = false;
            // 
            // ChkAplicaPolitica
            // 
            this.ChkAplicaPolitica.AutoSize = true;
            this.ChkAplicaPolitica.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkAplicaPolitica.Checked = true;
            this.ChkAplicaPolitica.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkAplicaPolitica.Location = new System.Drawing.Point(380, 193);
            this.ChkAplicaPolitica.Margin = new System.Windows.Forms.Padding(2);
            this.ChkAplicaPolitica.Name = "ChkAplicaPolitica";
            this.ChkAplicaPolitica.Size = new System.Drawing.Size(125, 24);
            this.ChkAplicaPolitica.TabIndex = 12;
            this.ChkAplicaPolitica.Text = "Aplica Política";
            this.ChkAplicaPolitica.UseVisualStyleBackColor = true;
            this.ChkAplicaPolitica.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 255);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Algoritmo";
            this.label2.Visible = false;
            // 
            // CboDigestAlgorithm
            // 
            this.CboDigestAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboDigestAlgorithm.FormattingEnabled = true;
            this.CboDigestAlgorithm.Items.AddRange(new object[] {
            "SHA-1",
            "SHA-256",
            "SHA-512"});
            this.CboDigestAlgorithm.Location = new System.Drawing.Point(394, 251);
            this.CboDigestAlgorithm.Name = "CboDigestAlgorithm";
            this.CboDigestAlgorithm.Size = new System.Drawing.Size(103, 28);
            this.CboDigestAlgorithm.TabIndex = 14;
            this.CboDigestAlgorithm.Visible = false;
            // 
            // TxtTimeStampServer
            // 
            this.TxtTimeStampServer.Location = new System.Drawing.Point(105, 283);
            this.TxtTimeStampServer.Margin = new System.Windows.Forms.Padding(2);
            this.TxtTimeStampServer.Name = "TxtTimeStampServer";
            this.TxtTimeStampServer.Size = new System.Drawing.Size(392, 26);
            this.TxtTimeStampServer.TabIndex = 16;
            this.TxtTimeStampServer.Text = "http://timestamp.digicert.com";
            this.TxtTimeStampServer.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 286);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Servidor TS";
            this.label5.Visible = false;
            // 
            // TxtUsuarioTS
            // 
            this.TxtUsuarioTS.Location = new System.Drawing.Point(105, 315);
            this.TxtUsuarioTS.Margin = new System.Windows.Forms.Padding(2);
            this.TxtUsuarioTS.Name = "TxtUsuarioTS";
            this.TxtUsuarioTS.Size = new System.Drawing.Size(203, 26);
            this.TxtUsuarioTS.TabIndex = 18;
            this.TxtUsuarioTS.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 318);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Usuário TS";
            this.label6.Visible = false;
            // 
            // TxtSenhaTS
            // 
            this.TxtSenhaTS.Location = new System.Drawing.Point(104, 346);
            this.TxtSenhaTS.Margin = new System.Windows.Forms.Padding(2);
            this.TxtSenhaTS.Name = "TxtSenhaTS";
            this.TxtSenhaTS.Size = new System.Drawing.Size(204, 26);
            this.TxtSenhaTS.TabIndex = 20;
            this.TxtSenhaTS.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 349);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Senha TS";
            this.label7.Visible = false;
            // 
            // ChkWSService
            // 
            this.ChkWSService.AutoSize = true;
            this.ChkWSService.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkWSService.Checked = true;
            this.ChkWSService.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkWSService.Location = new System.Drawing.Point(25, 159);
            this.ChkWSService.Margin = new System.Windows.Forms.Padding(2);
            this.ChkWSService.Name = "ChkWSService";
            this.ChkWSService.Size = new System.Drawing.Size(211, 24);
            this.ChkWSService.TabIndex = 21;
            this.ChkWSService.Text = "WS Service (Porta 27525)";
            this.ChkWSService.UseVisualStyleBackColor = true;
            this.ChkWSService.Visible = false;
            // 
            // ChkFileSocket
            // 
            this.ChkFileSocket.AutoSize = true;
            this.ChkFileSocket.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFileSocket.Checked = true;
            this.ChkFileSocket.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkFileSocket.Location = new System.Drawing.Point(240, 162);
            this.ChkFileSocket.Margin = new System.Windows.Forms.Padding(2);
            this.ChkFileSocket.Name = "ChkFileSocket";
            this.ChkFileSocket.Size = new System.Drawing.Size(208, 24);
            this.ChkFileSocket.TabIndex = 22;
            this.ChkFileSocket.Text = "File Socket (Porta 27526)";
            this.ChkFileSocket.UseVisualStyleBackColor = true;
            this.ChkFileSocket.Visible = false;
            this.ChkFileSocket.CheckedChanged += new System.EventHandler(this.ChkFileSocket_CheckedChanged);
            // 
            // ChkSalvaArquivo
            // 
            this.ChkSalvaArquivo.AutoSize = true;
            this.ChkSalvaArquivo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSalvaArquivo.Location = new System.Drawing.Point(31, 131);
            this.ChkSalvaArquivo.Margin = new System.Windows.Forms.Padding(2);
            this.ChkSalvaArquivo.Name = "ChkSalvaArquivo";
            this.ChkSalvaArquivo.Size = new System.Drawing.Size(205, 24);
            this.ChkSalvaArquivo.TabIndex = 23;
            this.ChkSalvaArquivo.Text = "Salva Arquivo Integração";
            this.ChkSalvaArquivo.UseVisualStyleBackColor = true;
            this.ChkSalvaArquivo.Visible = false;
            // 
            // ChkIgnora
            // 
            this.ChkIgnora.AutoSize = true;
            this.ChkIgnora.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkIgnora.Checked = true;
            this.ChkIgnora.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkIgnora.Location = new System.Drawing.Point(65, 376);
            this.ChkIgnora.Margin = new System.Windows.Forms.Padding(2);
            this.ChkIgnora.Name = "ChkIgnora";
            this.ChkIgnora.Size = new System.Drawing.Size(243, 24);
            this.ChkIgnora.TabIndex = 24;
            this.ChkIgnora.Text = "Ignora CARGO, CREA e CRM";
            this.ChkIgnora.UseVisualStyleBackColor = true;
            this.ChkIgnora.Visible = false;
            // 
            // BtnAssinarPDF
            // 
            this.BtnAssinarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnAssinarPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAssinarPDF.Image = global::INB.Assinador.View.Properties.Resources.assinatura;
            this.BtnAssinarPDF.Location = new System.Drawing.Point(619, 43);
            this.BtnAssinarPDF.Margin = new System.Windows.Forms.Padding(5);
            this.BtnAssinarPDF.Name = "BtnAssinarPDF";
            this.BtnAssinarPDF.Size = new System.Drawing.Size(75, 64);
            this.BtnAssinarPDF.TabIndex = 0;
            this.BtnAssinarPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.BtnAssinarPDF, "Assinar PDF");
            this.BtnAssinarPDF.UseVisualStyleBackColor = false;
            this.BtnAssinarPDF.Click += new System.EventHandler(this.BtnAssinarPDF_Click);
            // 
            // BtnFechar
            // 
            this.BtnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFechar.Image = global::INB.Assinador.View.Properties.Resources.cancel;
            this.BtnFechar.Location = new System.Drawing.Point(463, 43);
            this.BtnFechar.Margin = new System.Windows.Forms.Padding(5);
            this.BtnFechar.Name = "BtnFechar";
            this.BtnFechar.Size = new System.Drawing.Size(75, 64);
            this.BtnFechar.TabIndex = 0;
            this.BtnFechar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.BtnFechar, "Ocultar");
            this.BtnFechar.UseVisualStyleBackColor = true;
            this.BtnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // BtnAtualizar
            // 
            this.BtnAtualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnAtualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAtualizar.Image = global::INB.Assinador.View.Properties.Resources.refresh;
            this.BtnAtualizar.Location = new System.Drawing.Point(541, 43);
            this.BtnAtualizar.Margin = new System.Windows.Forms.Padding(5);
            this.BtnAtualizar.Name = "BtnAtualizar";
            this.BtnAtualizar.Size = new System.Drawing.Size(75, 64);
            this.BtnAtualizar.TabIndex = 4;
            this.BtnAtualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.BtnAtualizar, "Recarregar Certificados");
            this.BtnAtualizar.UseVisualStyleBackColor = false;
            this.BtnAtualizar.Click += new System.EventHandler(this.BtnAtualizar_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(320, 311);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(371, 90);
            this.label8.TabIndex = 10;
            this.label8.Text = resources.GetString("label8.Text");
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label8.Visible = false;
            // 
            // OptAsPadrao
            // 
            this.OptAsPadrao.AutoSize = true;
            this.OptAsPadrao.Checked = true;
            this.OptAsPadrao.Location = new System.Drawing.Point(105, 51);
            this.OptAsPadrao.Name = "OptAsPadrao";
            this.OptAsPadrao.Size = new System.Drawing.Size(158, 24);
            this.OptAsPadrao.TabIndex = 25;
            this.OptAsPadrao.Text = "Assinatura Padrão";
            this.toolTip1.SetToolTip(this.OptAsPadrao, "Selo padrão de assinatura");
            this.OptAsPadrao.UseVisualStyleBackColor = true;
            // 
            // OptAsCertifico
            // 
            this.OptAsCertifico.AutoSize = true;
            this.OptAsCertifico.Location = new System.Drawing.Point(105, 78);
            this.OptAsCertifico.Name = "OptAsCertifico";
            this.OptAsCertifico.Size = new System.Drawing.Size(227, 24);
            this.OptAsCertifico.TabIndex = 25;
            this.OptAsCertifico.Text = "Certifico de Material/Produto";
            this.toolTip1.SetToolTip(this.OptAsCertifico, "Selo de certifico de Material/Produto entregue e verificado");
            this.OptAsCertifico.UseVisualStyleBackColor = true;
            // 
            // FrmAssinador
            // 
            this.AcceptButton = this.BtnAssinarPDF;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.BtnFechar;
            this.ClientSize = new System.Drawing.Size(702, 114);
            this.ContextMenuStrip = this.MnuBandeja;
            this.Controls.Add(this.OptAsCertifico);
            this.Controls.Add(this.OptAsPadrao);
            this.Controls.Add(this.ChkIgnora);
            this.Controls.Add(this.ChkSalvaArquivo);
            this.Controls.Add(this.BtnAtualizar);
            this.Controls.Add(this.BtnFechar);
            this.Controls.Add(this.BtnAssinarPDF);
            this.Controls.Add(this.ChkFileSocket);
            this.Controls.Add(this.ChkWSService);
            this.Controls.Add(this.TxtSenhaTS);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TxtUsuarioTS);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TxtTimeStampServer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CboDigestAlgorithm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ChkAplicaPolitica);
            this.Controls.Add(this.ChkCarimboTempo);
            this.Controls.Add(this.TxtCRMCREA);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtCargo);
            this.Controls.Add(this.ChkCREA);
            this.Controls.Add(this.ChkCRM);
            this.Controls.Add(this.ChkCargo);
            this.Controls.Add(this.CboCertificados);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FrmAssinador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assinador Eletrônico da INB  (Assinatura Digital com Certificado ICP-Brasil)";
            this.Activated += new System.EventHandler(this.FrmAssinador_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAssinador_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAssinador_FormClosed);
            this.Load += new System.EventHandler(this.FrmAssinador_Load);
            this.MnuBandeja.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAssinarPDF;
        private System.Windows.Forms.ComboBox CboCertificados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BtnFechar;
        private System.Windows.Forms.Button BtnAtualizar;
        private System.Windows.Forms.CheckBox ChkCargo;
        private System.Windows.Forms.CheckBox ChkCRM;
        private System.Windows.Forms.CheckBox ChkCREA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtCargo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtCRMCREA;
        private System.Windows.Forms.ContextMenuStrip MnuBandeja;
        private System.Windows.Forms.ToolStripMenuItem assinarPDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem MnuSobre;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem MnuFechar;
        private System.Windows.Forms.CheckBox ChkCarimboTempo;
        private System.Windows.Forms.CheckBox ChkAplicaPolitica;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CboDigestAlgorithm;
        private System.Windows.Forms.TextBox TxtTimeStampServer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtUsuarioTS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtSenhaTS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox ChkWSService;
        private System.Windows.Forms.CheckBox ChkFileSocket;
        private System.Windows.Forms.CheckBox ChkSalvaArquivo;
        private System.Windows.Forms.CheckBox ChkIgnora;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem MnuInfoAdicionais;
        private System.Windows.Forms.ToolStripMenuItem MnuApresentacao;
        private System.Windows.Forms.ToolStripMenuItem MnuVerificarAtualizacao;
        private System.Windows.Forms.ToolStripMenuItem MnuIO;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RadioButton OptAsPadrao;
        private System.Windows.Forms.RadioButton OptAsCertifico;
    }
}

