using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Helper
{
    public class ValidaUsuario
    {
        public static bool PermiteChancelaJuridica(string CPF)
        {
            List<string> CPFAdvogado = new List<string>();
            CPFAdvogado.Add("05288284792"); //GABRIEL PITÁGORAS 
            CPFAdvogado.Add("09174317741"); //MARCELO SOARES
            CPFAdvogado.Add("02365312713");
            CPFAdvogado.Add("21355134234");          
            CPFAdvogado.Add("09961094719");
            CPFAdvogado.Add("01400989760");
            CPFAdvogado.Add("02935482778");
            CPFAdvogado.Add("09369005714");
            CPFAdvogado.Add("01119098564");
            CPFAdvogado.Add("02136491520");
            CPFAdvogado.Add("07494187707");
            CPFAdvogado.Add("10005167680");
            CPFAdvogado.Add("02935482778");
            CPFAdvogado.Add("07837939794");
            CPFAdvogado.Add("10256397740");
            CPFAdvogado.Add("11216318786");
            CPFAdvogado.Add("14093033730");
            CPFAdvogado.Add("05492056751");
            CPFAdvogado.Add("08449944724");
            CPFAdvogado.Add("09369005714");
            CPFAdvogado.Add("09369005714");
            CPFAdvogado.Add("00911274758");
            CPFAdvogado.Add("12923769708");
            CPFAdvogado.Add("07841835701");
            CPFAdvogado.Add("05851332719");
            CPFAdvogado.Add("14908365784");

            var res = (from p in CPFAdvogado where p == CPF select p).FirstOrDefault();
            if (res == null)
                return false;
            else
                return true;
        }
    }
}
