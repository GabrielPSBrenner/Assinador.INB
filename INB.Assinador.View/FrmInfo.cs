using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INB.Assinador.View
{
    public partial class FrmInfo : Form
    {
        ToolStripMenuItem _Menu;
        public FrmInfo(ToolStripMenuItem Menu)
        {
            InitializeComponent();
            _Menu = Menu;
        }

        private void FrmInfo_Load(object sender, EventArgs e)
        {
            _Menu.Enabled = false;
        }

        private void LinkFAQ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkFAQ.LinkVisited = true;            
            System.Diagnostics.Process.Start("https://www.iti.gov.br/perguntas-frequentes/41-perguntas-frequentes/112-sobre-certificacao-digital");
        }

      
        private void LinkValidador_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkValidador.LinkVisited = true;
            System.Diagnostics.Process.Start("https://verificador.iti.gov.br/verifier-2.5.2/");
        }

        private void LinkMP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkMP.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.planalto.gov.br/ccivil_03/MPV/Antigas_2001/2200-2.htm");
        }

        private void LinkLei13874_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkMP.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.planalto.gov.br/ccivil_03/_ato2019-2022/2019/lei/L13874.htm");
        }

        private void FrmInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Menu.Enabled = true;
        }
    }
}
