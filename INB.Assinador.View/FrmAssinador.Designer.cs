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
            this.MnuSobre = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.MnuFechar = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.BtnAssinarPDF = new System.Windows.Forms.Button();
            this.BtnFechar = new System.Windows.Forms.Button();
            this.BtnAtualizar = new System.Windows.Forms.Button();
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
            this.MnuBandeja.SuspendLayout();
            this.SuspendLayout();
            // 
            // CboCertificados
            // 
            this.CboCertificados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboCertificados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCertificados.FormattingEnabled = true;
            this.CboCertificados.Location = new System.Drawing.Point(126, 7);
            this.CboCertificados.Margin = new System.Windows.Forms.Padding(9);
            this.CboCertificados.Name = "CboCertificados";
            this.CboCertificados.Size = new System.Drawing.Size(606, 28);
            this.CboCertificados.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
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
            this.ChkCargo.Location = new System.Drawing.Point(27, 46);
            this.ChkCargo.Margin = new System.Windows.Forms.Padding(2);
            this.ChkCargo.Name = "ChkCargo";
            this.ChkCargo.Size = new System.Drawing.Size(112, 24);
            this.ChkCargo.TabIndex = 5;
            this.ChkCargo.Text = "Inclui Cargo";
            this.ChkCargo.UseVisualStyleBackColor = true;
            this.ChkCargo.CheckedChanged += new System.EventHandler(this.ChkCargo_CheckedChanged);
            // 
            // ChkCRM
            // 
            this.ChkCRM.AutoSize = true;
            this.ChkCRM.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCRM.Location = new System.Drawing.Point(152, 47);
            this.ChkCRM.Margin = new System.Windows.Forms.Padding(2);
            this.ChkCRM.Name = "ChkCRM";
            this.ChkCRM.Size = new System.Drawing.Size(105, 24);
            this.ChkCRM.TabIndex = 5;
            this.ChkCRM.Text = "Inclui CRM";
            this.ChkCRM.UseVisualStyleBackColor = true;
            this.ChkCRM.CheckedChanged += new System.EventHandler(this.ChkCRM_CheckedChanged);
            // 
            // ChkCREA
            // 
            this.ChkCREA.AutoSize = true;
            this.ChkCREA.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCREA.Location = new System.Drawing.Point(272, 47);
            this.ChkCREA.Margin = new System.Windows.Forms.Padding(2);
            this.ChkCREA.Name = "ChkCREA";
            this.ChkCREA.Size = new System.Drawing.Size(114, 24);
            this.ChkCREA.TabIndex = 5;
            this.ChkCREA.Text = "Inclui CREA";
            this.ChkCREA.UseVisualStyleBackColor = true;
            this.ChkCREA.CheckedChanged += new System.EventHandler(this.ChkCREA_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cargo";
            // 
            // TxtCargo
            // 
            this.TxtCargo.Location = new System.Drawing.Point(127, 75);
            this.TxtCargo.Margin = new System.Windows.Forms.Padding(2);
            this.TxtCargo.Name = "TxtCargo";
            this.TxtCargo.Size = new System.Drawing.Size(604, 26);
            this.TxtCargo.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nº CRM/CREA";
            // 
            // TxtCRMCREA
            // 
            this.TxtCRMCREA.Location = new System.Drawing.Point(127, 106);
            this.TxtCRMCREA.Margin = new System.Windows.Forms.Padding(2);
            this.TxtCRMCREA.Name = "TxtCRMCREA";
            this.TxtCRMCREA.Size = new System.Drawing.Size(203, 26);
            this.TxtCRMCREA.TabIndex = 11;
            // 
            // MnuBandeja
            // 
            this.MnuBandeja.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assinarPDFToolStripMenuItem,
            this.toolStripMenuItem1,
            this.MnuSobre,
            this.toolStripMenuItem2,
            this.MnuFechar});
            this.MnuBandeja.Name = "MnuBandeja";
            this.MnuBandeja.Size = new System.Drawing.Size(137, 82);
            // 
            // assinarPDFToolStripMenuItem
            // 
            this.assinarPDFToolStripMenuItem.Name = "assinarPDFToolStripMenuItem";
            this.assinarPDFToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.assinarPDFToolStripMenuItem.Text = "&Assinar PDF";
            this.assinarPDFToolStripMenuItem.Click += new System.EventHandler(this.assinarPDFToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // MnuSobre
            // 
            this.MnuSobre.Name = "MnuSobre";
            this.MnuSobre.Size = new System.Drawing.Size(136, 22);
            this.MnuSobre.Text = "&Sobre";
            this.MnuSobre.Click += new System.EventHandler(this.MnuSobre_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 6);
            // 
            // MnuFechar
            // 
            this.MnuFechar.Name = "MnuFechar";
            this.MnuFechar.Size = new System.Drawing.Size(136, 22);
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
            // BtnAssinarPDF
            // 
            this.BtnAssinarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnAssinarPDF.Image = global::INB.Assinador.View.Properties.Resources.assinatura;
            this.BtnAssinarPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnAssinarPDF.Location = new System.Drawing.Point(604, 148);
            this.BtnAssinarPDF.Margin = new System.Windows.Forms.Padding(5);
            this.BtnAssinarPDF.Name = "BtnAssinarPDF";
            this.BtnAssinarPDF.Size = new System.Drawing.Size(127, 72);
            this.BtnAssinarPDF.TabIndex = 0;
            this.BtnAssinarPDF.Text = "Assinar";
            this.BtnAssinarPDF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAssinarPDF.UseVisualStyleBackColor = false;
            this.BtnAssinarPDF.Click += new System.EventHandler(this.BtnAssinarPDF_Click);
            // 
            // BtnFechar
            // 
            this.BtnFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnFechar.Image = global::INB.Assinador.View.Properties.Resources.escritorio_3237_close64;
            this.BtnFechar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnFechar.Location = new System.Drawing.Point(335, 147);
            this.BtnFechar.Margin = new System.Windows.Forms.Padding(5);
            this.BtnFechar.Name = "BtnFechar";
            this.BtnFechar.Size = new System.Drawing.Size(137, 72);
            this.BtnFechar.TabIndex = 0;
            this.BtnFechar.Text = "Fechar";
            this.BtnFechar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnFechar.UseVisualStyleBackColor = true;
            this.BtnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // BtnAtualizar
            // 
            this.BtnAtualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnAtualizar.Image = global::INB.Assinador.View.Properties.Resources._628091264414857649;
            this.BtnAtualizar.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnAtualizar.Location = new System.Drawing.Point(474, 148);
            this.BtnAtualizar.Margin = new System.Windows.Forms.Padding(5);
            this.BtnAtualizar.Name = "BtnAtualizar";
            this.BtnAtualizar.Size = new System.Drawing.Size(127, 72);
            this.BtnAtualizar.TabIndex = 4;
            this.BtnAtualizar.Text = "Atualizar";
            this.BtnAtualizar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAtualizar.UseVisualStyleBackColor = false;
            this.BtnAtualizar.Click += new System.EventHandler(this.BtnAtualizar_Click);
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
            this.ChkCarimboTempo.Location = new System.Drawing.Point(395, 45);
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
            this.ChkAplicaPolitica.Location = new System.Drawing.Point(564, 45);
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
            this.label2.Location = new System.Drawing.Point(351, 110);
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
            this.CboDigestAlgorithm.Location = new System.Drawing.Point(429, 104);
            this.CboDigestAlgorithm.Name = "CboDigestAlgorithm";
            this.CboDigestAlgorithm.Size = new System.Drawing.Size(139, 28);
            this.CboDigestAlgorithm.TabIndex = 14;
            this.CboDigestAlgorithm.Visible = false;
            // 
            // TxtTimeStampServer
            // 
            this.TxtTimeStampServer.Location = new System.Drawing.Point(127, 137);
            this.TxtTimeStampServer.Margin = new System.Windows.Forms.Padding(2);
            this.TxtTimeStampServer.Name = "TxtTimeStampServer";
            this.TxtTimeStampServer.Size = new System.Drawing.Size(203, 26);
            this.TxtTimeStampServer.TabIndex = 16;
            this.TxtTimeStampServer.Text = "https://freetsa.org/tsr";
            this.TxtTimeStampServer.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 140);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Servidor TS";
            this.label5.Visible = false;
            // 
            // TxtUsuarioTS
            // 
            this.TxtUsuarioTS.Location = new System.Drawing.Point(127, 167);
            this.TxtUsuarioTS.Margin = new System.Windows.Forms.Padding(2);
            this.TxtUsuarioTS.Name = "TxtUsuarioTS";
            this.TxtUsuarioTS.Size = new System.Drawing.Size(203, 26);
            this.TxtUsuarioTS.TabIndex = 18;
            this.TxtUsuarioTS.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 170);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Usuário TS";
            this.label6.Visible = false;
            // 
            // TxtSenhaTS
            // 
            this.TxtSenhaTS.Location = new System.Drawing.Point(126, 197);
            this.TxtSenhaTS.Margin = new System.Windows.Forms.Padding(2);
            this.TxtSenhaTS.Name = "TxtSenhaTS";
            this.TxtSenhaTS.Size = new System.Drawing.Size(204, 26);
            this.TxtSenhaTS.TabIndex = 20;
            this.TxtSenhaTS.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 200);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Senha TS";
            this.label7.Visible = false;
            // 
            // FrmAssinador
            // 
            this.AcceptButton = this.BtnAssinarPDF;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.BtnFechar;
            this.ClientSize = new System.Drawing.Size(748, 229);
            this.ContextMenuStrip = this.MnuBandeja;
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
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtCargo);
            this.Controls.Add(this.ChkCREA);
            this.Controls.Add(this.ChkCRM);
            this.Controls.Add(this.ChkCargo);
            this.Controls.Add(this.BtnAtualizar);
            this.Controls.Add(this.CboCertificados);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnFechar);
            this.Controls.Add(this.BtnAssinarPDF);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "FrmAssinador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selecionar o certificado que será utilizado para assinar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAssinador_FormClosing);
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
    }
}

