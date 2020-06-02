namespace INB.Assinador.View
{
    partial class FrmUpdate
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
            this.ProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.LblDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Location = new System.Drawing.Point(15, 48);
            this.ProgressBar1.Margin = new System.Windows.Forms.Padding(6);
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(550, 22);
            this.ProgressBar1.TabIndex = 3;
            // 
            // LblDisplay
            // 
            this.LblDisplay.BackColor = System.Drawing.Color.White;
            this.LblDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblDisplay.Location = new System.Drawing.Point(15, 9);
            this.LblDisplay.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.LblDisplay.Name = "LblDisplay";
            this.LblDisplay.Size = new System.Drawing.Size(550, 32);
            this.LblDisplay.TabIndex = 2;
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 83);
            this.Controls.Add(this.ProgressBar1);
            this.Controls.Add(this.LblDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdate";
            this.Text = "Verificando atualização do Assinador";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmUpdate_FormClosed);
            this.Load += new System.EventHandler(this.FrmUpdate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ProgressBar ProgressBar1;
        internal System.Windows.Forms.Label LblDisplay;
    }
}