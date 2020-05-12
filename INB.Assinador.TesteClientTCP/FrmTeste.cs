using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using INB.Assinador.Integracao;

namespace INB.Assinador.TesteClientTCP
{
    public partial class FrmTeste : Form
    {
        public FrmTeste()
        {
            InitializeComponent();
        }

        private void BtnEnviaMSG_Click(object sender, EventArgs e)
        {
            Comunicacao oCom = new Comunicacao();
            oCom.Codigo = 1;
            oCom.Versao = 1;
            oCom.UserID = "admcomp";
            oCom.Senha = "TESTE";
             oCom.URLWS = "http://RES700850/WSTESTE/WS.xxx";
            var retorno = INB.Assinador.Integracao.Service.Cliente.EnviaDados(oCom, "127.0.0.1", 27525);
            MessageBox.Show(retorno);
        }
    }
}
    