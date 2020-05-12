using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INB.Assinador.Helper
{
    public class MyPolicy
    {
        public string PolicyIdentifier { get; set; }
        public string PolicyDigestAlgorithm { get; set; }
        public string URLPolicy { get; set; }
        public string SubPolicyIdentifier { get; set; }
        public string SubURLPolicy { get; set; }
        public string Hash { get; set; }

    }
}
