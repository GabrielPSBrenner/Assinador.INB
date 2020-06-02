using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment.Application;

namespace INB.Assinador.View
{
    public partial class FrmUpdate : Form
    {
        private long sizeOfUpdate = 0;
        ApplicationDeployment ADUpdateAsync;

        private FrmAssinador oFrm;
        private ToolStripMenuItem oMenu;

        private void UpdateApplication()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ADUpdateAsync.CheckForUpdateAsync();
            }
        }

        private void ADUpdateAsync_CheckForUpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            try
            {
                LblDisplay.Text = e.BytesCompleted / 1024 + " de " + e.BytesTotal / 1024;
                ProgressBar1.Value = e.ProgressPercentage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO - Motivo: " + ex.Message + " - Por favor, notifique esse erro ao Administrador.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public FrmUpdate(FrmAssinador _Frm, ToolStripMenuItem _Menu)
        {
            InitializeComponent();
            oFrm = _Frm;
            oMenu = _Menu;
            oMenu.Enabled = false;

        }
    
        private void ADUpdateAsync_UpdateCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled)
                {
                    MessageBox.Show("A atualização da última versão foi cancelada.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show("ERRO: Não foi possível instalar a última versão da aplicação. Razão: " + e.Error.Message + " - Por favor, notifique esse erro ao Administrador.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO - Motivo: " + ex.Message + " - Por favor, notifique esse erro ao Administrador.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void ADUpdateAsync_UpdateProgressChanged(object sender, DeploymentProgressChangedEventArgs e)
        {
            string progressText = e.BytesCompleted / 1024 + " de " + e.BytesTotal / 1024;
            LblDisplay.Text = progressText;
            ProgressBar1.Value = e.ProgressPercentage;
        }

        private void BeginUpdate()
        {
            ADUpdateAsync = ApplicationDeployment.CurrentDeployment;
            ADUpdateAsync.UpdateAsync();
        }

        private void ADUpdateAsync_CheckForUpdateCompleted(object sender, CheckForUpdateCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("ERRO: Não foi possível verificar se existe uma atualização disponível. Motivo: " + e.Error.Message + " - Por favor, notifique esse erro ao Administrador.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (e.Cancelled == true)
                {
                    MessageBox.Show("A atualização foi cancelada.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (e.UpdateAvailable)
            {
                sizeOfUpdate = e.UpdateSizeBytes;

                if (!e.IsUpdateRequired)
                {
                    System.Windows.Forms.DialogResult dr = MessageBox.Show("Uma nova versão foi encontrada. Deseja atualizar o sistema agora?", ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (System.Windows.Forms.DialogResult.Yes == dr)
                    {
                        BeginUpdate();
                    }
                }
                else
                {

                    MessageBox.Show("Uma nova versão obrigatória foi encontrada. Essa atualização é obrigatória e após, o sistema será reiniciado.");
                    BeginUpdate();
                }
            }
            else
            {
                MessageBox.Show("Não tem atualização pendente.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
               
                try
                {
                    info = ADUpdateAsync.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("Não foi possível baixar a nova versão. Erro: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Arquivo de instalação do Click-Once corrompido. Erro: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("A aplicação não está sendo executada como uma aplicação do Click-Once. Erro: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show("foi identificada uma nova versão, deseja atualizar?",ProductName, MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("A aplicação identificou uma versão mandatória que precisará ser atualiada " + info.MinimumRequiredVersion.ToString() +
                            ". Após a atualização o aplicativo será reinicializado.",
                            ProductName, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ADUpdateAsync.Update();
                            MessageBox.Show("O aplicativo foi atualizado e será reiniciado.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            oFrm.Close();                            
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Erro: " + dde);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Não tem atualização pendente.", ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

            }
        }


        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                ADUpdateAsync = ApplicationDeployment.CurrentDeployment;
                ADUpdateAsync.CheckForUpdateProgressChanged += ADUpdateAsync_CheckForUpdateProgressChanged;
                ADUpdateAsync.CheckForUpdateCompleted += ADUpdateAsync_CheckForUpdateCompleted;
                ADUpdateAsync.UpdateProgressChanged += ADUpdateAsync_UpdateProgressChanged;
                ADUpdateAsync.UpdateCompleted += ADUpdateAsync_UpdateCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar a atualização CurrentDeployment: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            try
            {
                InstallUpdateSyncWithInfo();


                //UpdateApplication();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o Sistema: " + ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void FrmUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            oMenu.Enabled = true;
        }
    }
}
