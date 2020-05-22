using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using INB.Assinador.Integracao;

namespace PortalTesteAssinador.Controllers
{
    public class TesteController : Controller
    {
        // GET: Teste
        public ActionResult Index()
        {
            var Lista = Models.Anexo.ListaAnexos();
            return View(Lista);
        }

        public ActionResult Assinar(int Codigo, int Versao)
        {
            Comunicacao oCom = new Comunicacao();            
            string URL = "http://localhost/PortalTesteAssinador/Servicos/ServicoTeste.svc";        
            oCom.Codigo = Codigo;
            oCom.Versao = Versao;
            oCom.UserID = "YYYY";
            oCom.Senha = "XXXX";
            oCom.URLWS = URL;
            oCom.UsuarioAutenticado = "1495";
            string userIpAddress = GetIP();
            INB.Assinador.Integracao.Service.Cliente.EnviaDados(oCom, userIpAddress);
            var Lista = Models.Anexo.ListaAnexos();
            return View("Index", Lista);
        }

        private string GetIP()
        {
            string Result = "";
            try
            {
               
                    Result = System.Web.HttpContext.Current.Request.UserHostAddress;
                    if (Result == "::1")
                    {
                        Result = "127.0.0.1";
                    }
                    else if (Result.StartsWith("10.") == true)
                    {
                        return Result;
                    }

                    IPAddress oIpAddress = null;
                    IPAddress.TryParse(Result, out oIpAddress);

                if (oIpAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    //IPV6 não me interessa, mas não temos como resolver IPV6 atualmente na INB. Se tentar localizar o host um exception será gerado
                }
                else if (Result.StartsWith("10.") == false)
                {
                    //em máquinas que tem mais de 1 IP esse método é necessário para buscar o IP local
                    var hostEntry = System.Net.Dns.GetHostEntry((Result == "127.0.0.1") ? System.Net.Dns.GetHostName() : Result);
                    string ip = GetLocalIPFromHost(hostEntry);
                    if (!string.IsNullOrWhiteSpace(ip))
                    {
                        Result = ip;
                    }
                }
              
            }
            catch (Exception ex)
            {
                var foo = ex;
            }
            return Result;
        }

        private string GetLocalIPFromHost(IPHostEntry hostEntry)
        {
            if (hostEntry != null)
            {
                var ipList = hostEntry.AddressList;
                var Qry = (from p in ipList
                           where p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork
                           select p);
                if (Qry.Count() > 0)
                {
                    foreach (var item in Qry)
                    {
                        if (item.ToString().StartsWith("10."))
                        {
                            return item.ToString();
                        }
                    }
                    return Qry.First().ToString();
                }
            }
            return "";
        }


    }
}