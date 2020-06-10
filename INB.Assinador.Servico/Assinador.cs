using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using INB.Assinador.Servico.Base;

namespace INB.Assinador.Servico
{
    public partial class Assinador : System.ServiceProcess.ServiceBase
    {
        private Base.ServiceBase oServ;

        public Assinador()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            oServ = new Base.ServiceBase();
            oServ.OpenServer();
            oServ.OpenServerWS();
        }

        protected override void OnStop()
        {
            oServ.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;



            timer1.Enabled = true;
        }
    }
}
