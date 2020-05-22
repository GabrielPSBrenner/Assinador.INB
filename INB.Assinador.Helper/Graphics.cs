using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Helper
{
    public class Graphic
    {
        public static Bitmap GeraSelo(X509Certificate2 cert, Bitmap Selo, string Cargo = "", string CREACRM = "")
        {


            Graphics g = Graphics.FromImage(Selo);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            RectangleF qNome = new RectangleF(7, 36, 220, 30);
            RectangleF qCPF = new RectangleF(7, 66, 90, 30);
            RectangleF qData = new RectangleF(130, 66, 120, 30);

            RectangleF qCargo;
            RectangleF qCREACRM;
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

            string[] dados = cert.Subject.Split(',');
            string[] dadosbase = dados[0].Split(':');
            dadosbase[0] = dadosbase[0].Substring(3);
            if (dadosbase[1].Trim().Length == 11)
            {
                dadosbase[1] = dadosbase[1].Substring(0, 3) + "." + dadosbase[1].Substring(3, 3) + "." + dadosbase[1].Substring(6, 3) + "-" + dadosbase[1].Substring(9, 2);
            }
            else if (dadosbase[1].Trim().Length == 14)
            {
                //formatar CNPJ;
            }

            g.DrawString(dadosbase[1], new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qCPF);
            int fontSize = 7;
            if (dadosbase[0].Trim().Length > 38)
            {
                fontSize = 6;
            }

            g.DrawString(dadosbase[0], new System.Drawing.Font("Tahoma", fontSize, FontStyle.Bold), Brushes.Black, qNome);
            g.DrawString(System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), new System.Drawing.Font("Tahoma", 7, FontStyle.Bold), Brushes.Black, qData);
            g.Flush();

           

            return Selo;
        }

        public static Bitmap ConfiguraBMP(X509Certificate2 cert, bool SeloCargo, bool SeloCREA, bool SeloCRM, string Cargo, string CREACRM,  out int Altura)
        {
            Bitmap bmp;
            if (SeloCargo == true && (SeloCREA == false && SeloCRM == false))
            {
                //SOMENTE SELO DE CARGO
                bmp = new Bitmap(Properties.Resources.seloCargo);
                Altura = 75;
            }
            else if (SeloCargo == false && (SeloCREA == true || SeloCRM == true))
            {
                //SOMENTE SELO DO CREA OU DO CRM.
                if (SeloCREA)
                {
                    bmp = new Bitmap(Properties.Resources.seloCREA);
                }
                else
                {
                    bmp = new Bitmap(Properties.Resources.seloCRM);
                }
                Altura = 75;
            }
            else if (SeloCargo == true && (SeloCREA == true || SeloCRM == true))
            {
                //SELO COM CARGO CREA OU CRM
                if (SeloCREA)
                {
                    bmp = new Bitmap(Properties.Resources.seloCargoCREA);
                }
                else
                {
                    bmp = new Bitmap(Properties.Resources.seloCargoCRM);
                }
                Altura = 90;
            }
            else
            {
                bmp = new Bitmap(Properties.Resources.selo);
                Altura = 63;
                //SELO NORMAL
            }

            bmp = GeraSelo(cert, bmp, Cargo, CREACRM);
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
