using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using INB.Assinador.Integracao;
using INB.Assinador.Integracao.Interfaces;

namespace PortalTesteAssinador.Servicos 
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicoTeste" in both code and config file together.
    [ServiceContract]
    public interface IServicoTeste : IWS
    {
      
    }
}
