namespace INB.Auth
{
    partial class FrmAuth
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
            this.CboCertificados = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAutentica = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CboCertificados
            // 
            this.CboCertificados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CboCertificados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboCertificados.FormattingEnabled = true;
            this.CboCertificados.Location = new System.Drawing.Point(21, 34);
            this.CboCertificados.Margin = new System.Windows.Forms.Padding(9);
            this.CboCertificados.Name = "CboCertificados";
            this.CboCertificados.Size = new System.Drawing.Size(570, 21);
            this.CboCertificados.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Certificado";
            // 
            // BtnAutentica
            // 
            this.BtnAutentica.Location = new System.Drawing.Point(426, 62);
            this.BtnAutentica.Name = "BtnAutentica";
            this.BtnAutentica.Size = new System.Drawing.Size(165, 48);
            this.BtnAutentica.TabIndex = 6;
            this.BtnAutentica.Text = "Autenticação com certificado";
            this.BtnAutentica.UseVisualStyleBackColor = true;
            this.BtnAutentica.Click += new System.EventHandler(this.BtnAutentica_Click);
            // 
            // FrmAuth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 122);
            this.Controls.Add(this.BtnAutentica);
            this.Controls.Add(this.CboCertificados);
            this.Controls.Add(this.label1);
            this.Name = "FrmAuth";
            this.Text = "Autenticação com certificado";
            this.Load += new System.EventHandler(this.FrmAuth_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CboCertificados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnAutentica;
    }
}

