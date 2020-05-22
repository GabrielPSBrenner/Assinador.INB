namespace INB.Assinador.View
{
    partial class FrmInfo
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
            this.LinkFAQ = new System.Windows.Forms.LinkLabel();
            this.LinkValidador = new System.Windows.Forms.LinkLabel();
            this.LinkMP = new System.Windows.Forms.LinkLabel();
            this.LinkLei13874 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // LinkFAQ
            // 
            this.LinkFAQ.AutoSize = true;
            this.LinkFAQ.Location = new System.Drawing.Point(29, 21);
            this.LinkFAQ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkFAQ.Name = "LinkFAQ";
            this.LinkFAQ.Size = new System.Drawing.Size(179, 20);
            this.LinkFAQ.TabIndex = 5;
            this.LinkFAQ.TabStop = true;
            this.LinkFAQ.Text = "FAQ - Assinatura Digital";
            this.LinkFAQ.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkFAQ_LinkClicked);
            // 
            // LinkValidador
            // 
            this.LinkValidador.AutoSize = true;
            this.LinkValidador.Location = new System.Drawing.Point(29, 65);
            this.LinkValidador.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkValidador.Name = "LinkValidador";
            this.LinkValidador.Size = new System.Drawing.Size(271, 20);
            this.LinkValidador.TabIndex = 5;
            this.LinkValidador.TabStop = true;
            this.LinkValidador.Text = "Validador de Assinatura Digital do ITI";
            this.LinkValidador.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkValidador_LinkClicked);
            // 
            // LinkMP
            // 
            this.LinkMP.AutoSize = true;
            this.LinkMP.Location = new System.Drawing.Point(29, 106);
            this.LinkMP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkMP.Name = "LinkMP";
            this.LinkMP.Size = new System.Drawing.Size(205, 20);
            this.LinkMP.TabIndex = 5;
            this.LinkMP.TabStop = true;
            this.LinkMP.Text = "Medida Provisória n°2.200-2";
            this.LinkMP.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkMP_LinkClicked);
            // 
            // LinkLei13874
            // 
            this.LinkLei13874.AutoSize = true;
            this.LinkLei13874.Location = new System.Drawing.Point(29, 144);
            this.LinkLei13874.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LinkLei13874.Name = "LinkLei13874";
            this.LinkLei13874.Size = new System.Drawing.Size(349, 20);
            this.LinkLei13874.TabIndex = 5;
            this.LinkLei13874.TabStop = true;
            this.LinkLei13874.Text = "LEI Nº 13.874, DE 20 DE SETEMBRO DE 2019";
            this.LinkLei13874.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLei13874_LinkClicked);
            // 
            // FrmInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 185);
            this.Controls.Add(this.LinkLei13874);
            this.Controls.Add(this.LinkMP);
            this.Controls.Add(this.LinkValidador);
            this.Controls.Add(this.LinkFAQ);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informações Adicionais";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmInfo_FormClosed);
            this.Load += new System.EventHandler(this.FrmInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel LinkFAQ;
        private System.Windows.Forms.LinkLabel LinkValidador;
        private System.Windows.Forms.LinkLabel LinkMP;
        private System.Windows.Forms.LinkLabel LinkLei13874;
    }
}