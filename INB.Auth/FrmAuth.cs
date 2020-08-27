using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;
using System.IO;
using System.Security.Cryptography;

namespace INB.Auth
{
    public partial class FrmAuth : Form
    {
        public FrmAuth()
        {
            InitializeComponent();
        }

        private void CarregaCertificado()
        {
            CboCertificados.DataSource = null;
            CboCertificados.ValueMember = "SerialNumber";
            CboCertificados.DisplayMember = "NomeCPFNPJ";
            CboCertificados.DataSource = CertSimples.ListaCertificado(Certificado.ListaCertificadosValidos());
        }

        private void FrmAuth_Load(object sender, EventArgs e)
        {
            CarregaCertificado();
        }

        private void BtnAutentica_Click(object sender, EventArgs e)
        {
            CertSimples oCert = (CertSimples)CboCertificados.SelectedItem;
            X509Certificate2 oCertificado = oCert.Certificado;
            //var RSA = oCertificado.GetRSAPrivateKey();
            RSACryptoServiceProvider privateKey = (RSACryptoServiceProvider)oCertificado.PrivateKey;
            byte[] signedData = ASCIIEncoding.ASCII.GetBytes("TESTE");
            privateKey.SignData(signedData, new SHA1CryptoServiceProvider());

        }
    }
}
