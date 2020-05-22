namespace INB.Assinador.TesteClientTCP
{
    partial class FrmTeste
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
            this.BtnEnviaMSG = new System.Windows.Forms.Button();
            this.BtnEnviaArquivo = new System.Windows.Forms.Button();
            this.BtnIniciaServidorArquivo = new System.Windows.Forms.Button();
            this.BtnServidorArquivo2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtVersao = new System.Windows.Forms.TextBox();
            this.TxtArquivo = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BtnEncerrarServidor = new System.Windows.Forms.Button();
            this.BtnTestaTimeStamp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnEnviaMSG
            // 
            this.BtnEnviaMSG.Location = new System.Drawing.Point(12, 12);
            this.BtnEnviaMSG.Name = "BtnEnviaMSG";
            this.BtnEnviaMSG.Size = new System.Drawing.Size(208, 59);
            this.BtnEnviaMSG.TabIndex = 0;
            this.BtnEnviaMSG.Text = "Envia Mensagem";
            this.BtnEnviaMSG.UseVisualStyleBackColor = true;
            this.BtnEnviaMSG.Click += new System.EventHandler(this.BtnEnviaMSG_Click);
            // 
            // BtnEnviaArquivo
            // 
            this.BtnEnviaArquivo.Location = new System.Drawing.Point(12, 208);
            this.BtnEnviaArquivo.Name = "BtnEnviaArquivo";
            this.BtnEnviaArquivo.Size = new System.Drawing.Size(208, 59);
            this.BtnEnviaArquivo.TabIndex = 1;
            this.BtnEnviaArquivo.Text = "Transfere Arquivo ";
            this.BtnEnviaArquivo.UseVisualStyleBackColor = true;
            this.BtnEnviaArquivo.Click += new System.EventHandler(this.BtnEnviaArquivo_Click);
            // 
            // BtnIniciaServidorArquivo
            // 
            this.BtnIniciaServidorArquivo.Location = new System.Drawing.Point(12, 77);
            this.BtnIniciaServidorArquivo.Name = "BtnIniciaServidorArquivo";
            this.BtnIniciaServidorArquivo.Size = new System.Drawing.Size(208, 59);
            this.BtnIniciaServidorArquivo.TabIndex = 2;
            this.BtnIniciaServidorArquivo.Text = "Servidor Arquivo 1";
            this.BtnIniciaServidorArquivo.UseVisualStyleBackColor = true;
            this.BtnIniciaServidorArquivo.Click += new System.EventHandler(this.BtnIniciaServidorArquivo_Click);
            // 
            // BtnServidorArquivo2
            // 
            this.BtnServidorArquivo2.Location = new System.Drawing.Point(12, 142);
            this.BtnServidorArquivo2.Name = "BtnServidorArquivo2";
            this.BtnServidorArquivo2.Size = new System.Drawing.Size(208, 59);
            this.BtnServidorArquivo2.TabIndex = 3;
            this.BtnServidorArquivo2.Text = "Servidor Arquivo 2";
            this.BtnServidorArquivo2.UseVisualStyleBackColor = true;
            this.BtnServidorArquivo2.Click += new System.EventHandler(this.BtnServidorArquivo2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Arquivo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Versão";
            // 
            // TxtVersao
            // 
            this.TxtVersao.Location = new System.Drawing.Point(155, 280);
            this.TxtVersao.MaxLength = 4;
            this.TxtVersao.Name = "TxtVersao";
            this.TxtVersao.Size = new System.Drawing.Size(42, 20);
            this.TxtVersao.TabIndex = 5;
            this.TxtVersao.Text = "1";
            // 
            // TxtArquivo
            // 
            this.TxtArquivo.Location = new System.Drawing.Point(62, 280);
            this.TxtArquivo.MaxLength = 4;
            this.TxtArquivo.Name = "TxtArquivo";
            this.TxtArquivo.Size = new System.Drawing.Size(42, 20);
            this.TxtArquivo.TabIndex = 6;
            this.TxtArquivo.Text = "1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BtnEncerrarServidor
            // 
            this.BtnEncerrarServidor.Location = new System.Drawing.Point(226, 77);
            this.BtnEncerrarServidor.Name = "BtnEncerrarServidor";
            this.BtnEncerrarServidor.Size = new System.Drawing.Size(208, 59);
            this.BtnEncerrarServidor.TabIndex = 2;
            this.BtnEncerrarServidor.Text = "Encerrar Servidor";
            this.BtnEncerrarServidor.UseVisualStyleBackColor = true;
            this.BtnEncerrarServidor.Click += new System.EventHandler(this.BtnEncerrarServidor_Click);
            // 
            // BtnTestaTimeStamp
            // 
            this.BtnTestaTimeStamp.Location = new System.Drawing.Point(226, 12);
            this.BtnTestaTimeStamp.Name = "BtnTestaTimeStamp";
            this.BtnTestaTimeStamp.Size = new System.Drawing.Size(208, 59);
            this.BtnTestaTimeStamp.TabIndex = 7;
            this.BtnTestaTimeStamp.Text = "Testa Servidor de TimeStamp";
            this.BtnTestaTimeStamp.UseVisualStyleBackColor = true;
            this.BtnTestaTimeStamp.Click += new System.EventHandler(this.BtnTestaTimeStamp_Click);
            // 
            // FrmTeste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 318);
            this.Controls.Add(this.BtnTestaTimeStamp);
            this.Controls.Add(this.TxtArquivo);
            this.Controls.Add(this.TxtVersao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnServidorArquivo2);
            this.Controls.Add(this.BtnEncerrarServidor);
            this.Controls.Add(this.BtnIniciaServidorArquivo);
            this.Controls.Add(this.BtnEnviaArquivo);
            this.Controls.Add(this.BtnEnviaMSG);
            this.Name = "FrmTeste";
            this.Text = "Teste de comunicação";
            this.Load += new System.EventHandler(this.FrmTeste_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnEnviaMSG;
        private System.Windows.Forms.Button BtnEnviaArquivo;
        private System.Windows.Forms.Button BtnIniciaServidorArquivo;
        private System.Windows.Forms.Button BtnServidorArquivo2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtVersao;
        private System.Windows.Forms.TextBox TxtArquivo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button BtnEncerrarServidor;
        private System.Windows.Forms.Button BtnTestaTimeStamp;
    }
}