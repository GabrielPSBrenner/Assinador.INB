using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Model
{
    public class Graphic
    {
        public static Bitmap GeraSelo(CertSimples cert, Bitmap Selo, TipoAssinatura Tipo, string Cargo = "", string CREACRM = "")
        {


            Graphics g = Graphics.FromImage(Selo);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            RectangleF qNome;
            RectangleF qCPF;
            RectangleF qData;
            RectangleF qCargo;
            RectangleF qCREACRM;

            if (Tipo == TipoAssinatura.Certifico)
            {
                qNome = new RectangleF(7, 91, 220, 30);
                qCPF = new RectangleF(7, 115, 90, 30);
                qData = new RectangleF(130, 115, 120, 30);
            }
            else if (Tipo == TipoAssinatura.Buena || Tipo == TipoAssinatura.Caetite || Tipo == TipoAssinatura.Caldas || Tipo == TipoAssinatura.Fortaleza || Tipo == TipoAssinatura.Resende || Tipo == TipoAssinatura.RioDeJaneiro || Tipo == TipoAssinatura.SaoPaulo)
            {
                qNome = new RectangleF(7, 91, 220, 30);
                qCPF = new RectangleF(7, 115, 90, 30);
                qData = new RectangleF(130, 115, 120, 30);
            }
            else if (Tipo == TipoAssinatura.ConferidoOriginal)
            {
                qNome = new RectangleF(7, 91, 220, 30);
                qCPF = new RectangleF(7, 115, 90, 30);
                qData = new RectangleF(130, 115, 120, 30);
            }
            else if (Tipo == TipoAssinatura.ChancelaJuridica)
            {
                qNome = new RectangleF(7, 91, 220, 30);
                qCPF = new RectangleF(7, 115, 90, 30);
                qData = new RectangleF(130, 115, 120, 30);
            }
            else if (Tipo == TipoAssinatura.Normal)
            {
                qNome = new RectangleF(7, 36, 220, 30);
                qCPF = new RectangleF(7, 66, 90, 30);
                qData = new RectangleF(130, 66, 120, 30);
            }
            else
            {

                qNome = new RectangleF(7, 36, 220, 30);
                qCPF = new RectangleF(7, 66, 90, 30);
                qData = new RectangleF(130, 66, 120, 30);

                if (Cargo.Trim() != "" && CREACRM.Trim() == "")
                {
                    qCargo = new RectangleF(7, 91, 220, 30);
                    g.DrawString(Cargo, new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qCargo);
                }
                else if (Cargo.Trim() == "" && CREACRM.Trim() != "")
                {
                    qCREACRM = new RectangleF(7, 91, 220, 30);
                    g.DrawString(CREACRM, new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qCREACRM);
                }
                else if (Cargo.Trim() != "" && CREACRM.Trim() != "")
                {
                    qCargo = new RectangleF(7, 91, 220, 30);
                    qCREACRM = new RectangleF(7, 115, 220, 30);
                    g.DrawString(Cargo, new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qCargo);
                    g.DrawString(CREACRM, new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qCREACRM);
                }
            }

            string CPFCNPJ;
            if (cert.Tipo == "F")
            {
                CPFCNPJ = cert.CPF.Substring(0, 3) + "." + cert.CPF.Substring(3, 3) + "." + cert.CPF.Substring(6, 3) + "-" + cert.CPF.Substring(9, 2);
            }
            else
            {
                //formatar CNPJ;
                CPFCNPJ = cert.CNPJ;
            }

            g.DrawString(CPFCNPJ, new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qCPF);
            int fontSize = 7;
            if (cert.Nome.Trim().Length > 38)
            {
                fontSize = 6;
            }

            g.DrawString(cert.Nome, new System.Drawing.Font("Tahoma", fontSize, FontStyle.Bold), Brushes.Black, qNome);
            g.DrawString(System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qData);
            g.Flush();

            return Selo;
        }

        //public static Bitmap ConfiguraBMP(X509Certificate2 cert, bool SeloCargo, bool SeloCREA, bool SeloCRM, string Cargo, string CREACRM, out int Altura, bool SeloCertifico)
        //{
        //    Bitmap bmp;
        //    if (SeloCertifico)
        //    {
        //        bmp = new Bitmap(Properties.Resources.seloCertifico);
        //        Altura = 90;
        //    }
        //    else
        //    {
        //        if (SeloCargo == true && (SeloCREA == false && SeloCRM == false))
        //        {
        //            //SOMENTE SELO DE CARGO
        //            bmp = new Bitmap(Properties.Resources.seloCargo);
        //            Altura = 75;
        //        }
        //        else if (SeloCargo == false && (SeloCREA == true || SeloCRM == true))
        //        {
        //            //SOMENTE SELO DO CREA OU DO CRM.
        //            if (SeloCREA)
        //            {
        //                bmp = new Bitmap(Properties.Resources.seloCREA);
        //            }
        //            else
        //            {
        //                bmp = new Bitmap(Properties.Resources.seloCRM);
        //            }
        //            Altura = 75;
        //        }
        //        else if (SeloCargo == true && (SeloCREA == true || SeloCRM == true))
        //        {
        //            //SELO COM CARGO CREA OU CRM
        //            if (SeloCREA)
        //            {
        //                bmp = new Bitmap(Properties.Resources.seloCargoCREA);
        //            }
        //            else
        //            {
        //                bmp = new Bitmap(Properties.Resources.seloCargoCRM);
        //            }
        //            Altura = 90;
        //        }
        //        else
        //        {
        //            bmp = new Bitmap(Properties.Resources.selo);
        //            Altura = 63;
        //            //SELO NORMAL
        //        }
        //    }
        //    bmp = GeraSelo(cert, bmp, Cargo, CREACRM, SeloCertifico);
        //    return bmp;
        //}
        public static Bitmap ConfiguraBMP(CertSimples cert, out int Altura, TipoAssinatura Tipo, string Cargo = "", string CREACRM = "")
        {
            Bitmap bmp;
            if (Tipo == TipoAssinatura.Certifico)
            {
                bmp = new Bitmap(Properties.Resources.seloCertifico);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.Cargo)
            {
                //SOMENTE SELO DE CARGO
                bmp = new Bitmap(Properties.Resources.seloCargo);
                Altura = 75;
            }
            else if (Tipo == TipoAssinatura.CargoCrea)
            {
                bmp = new Bitmap(Properties.Resources.seloCargoCREA);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.CargoCRM)
            {
                bmp = new Bitmap(Properties.Resources.seloCargoCRM);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.CREA)
            {
                bmp = new Bitmap(Properties.Resources.seloCREA);
                Altura = 75;
            }
            else if (Tipo == TipoAssinatura.CRM)
            {
                bmp = new Bitmap(Properties.Resources.seloCRM);
                Altura = 75;
            }
            else if (Tipo == TipoAssinatura.Fortaleza)
            {
                bmp = new Bitmap(Properties.Resources.CarimboFortaleza);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.Resende)
            {
                bmp = new Bitmap(Properties.Resources.CarimboResende);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.RioDeJaneiro)
            {
                bmp = new Bitmap(Properties.Resources.CarimboRJ);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.Caetite)
            {
                bmp = new Bitmap(Properties.Resources.CarimboCaetite);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.Buena)
            {
                bmp = new Bitmap(Properties.Resources.CarimboBuena);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.SaoPaulo)
            {
                bmp = new Bitmap(Properties.Resources.CarimboSP);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.Caldas)
            {
                bmp = new Bitmap(Properties.Resources.CarimboCaldas);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.ConferidoOriginal)
            {
                bmp = new Bitmap(Properties.Resources.ConferidoOriginal);
                Altura = 90;
            }
            else if (Tipo == TipoAssinatura.ChancelaJuridica)
            {
                bmp = new Bitmap(Properties.Resources.SeloChancela);
                Altura = 90;
            }
            else
            {
                bmp = new Bitmap(Properties.Resources.selo);
                Altura = 63;
                //SELO NORMAL
            }

            bmp = GeraSelo(cert, bmp, Tipo, Cargo, CREACRM);

            return bmp;
        }

        public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }



    }
}
