using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Utilities;

namespace INB.Assinador.Model_IT
{
    public class CertSimples
    {
        private string _Subject;
        private string _OAB = "";

        public string SerialNumber { get; set; }

        public string Tipo { get; set; } //F = física; J = Jurídica

        public string CPF { get; set; }

        public string CNPJ { get; set; }

        public string Nome { get; set; }

        public string NomeResponsavel { get; set; }

        public string NomeCPFNPJ
        {
            get
            {
                if (this.OAB.Trim() != "")
                {
                    return Nome + " |CPF: " + CPF + " |OAB:" + OAB;
                }
                else
                {
                    return Nome + " - CPF: " + CPF;
                }

            }
        }


        public string DataNascimento { get; set; }

        public string NIS { get; set; }

        public string RG { get; set; }

        public string OrgaoExpedidor { get; set; }

        public string TituloEleitor { get; set; }

        public string ZonaEleitoral { get; set; }

        public string SecaoEleitoral { get; set; }

        public string MunicipioEleitoral { get; set; }

        public string Email { get; set; }

        public string OAB
        {
            get
            {
                return _OAB;
            }
            set
            {
                _OAB = value;
            }
        }

        public X509Certificate2 Certificado { get; set; }

        public string Subject { get; set; }


        public static List<CertSimples> ListaCertificado(X509Certificate2Collection Certificados)
        {
            List<CertSimples> oLista = new List<CertSimples>();
            for (int i = 0; i < Certificados.Count; i++)
            {
                X509Certificate2 oCertificado = Certificados[i];

                CertSimples oCert = new CertSimples();
                oCert.SerialNumber = oCertificado.SerialNumber;
                oCert.Subject = oCertificado.Subject;

                try
                {
                    string[] DadosSubject = oCertificado.Subject.Split(',');
                    if (DadosSubject[0].IndexOf(":") > -1)
                    {
                        oCert.Nome = DadosSubject[0].Substring(3, DadosSubject[0].IndexOf(":") - 3);
                    }
                    else
                    {
                        oCert.Nome = DadosSubject[0].Substring(3);
                    }
                }
                catch (Exception ex)
                {
                    oCert.Nome = oCert.Subject;
                }




                foreach (var obj in oCertificado.Extensions)
                {
                    if (obj.Oid.Value == "2.5.29.17") //otherName
                    {
                        byte[] Dados = obj.RawData;
                        Stream sm = new MemoryStream(Dados);
                        // StreamReader oSr = new StreamReader(sm);

                        //string teste = System.Text.Encoding.ASCII.GetString(Dados);
                        DerSequence otherName = (DerSequence)Asn1Object.FromStream(sm);
                        var objCollection = otherName.GetEnumerator();
                        while (objCollection.MoveNext())
                        {
                            Org.BouncyCastle.Asn1.DerTaggedObject iSub = (Org.BouncyCastle.Asn1.DerTaggedObject)objCollection.Current;
                            Asn1Object derObject = iSub.GetObject();
                            if (derObject.GetType().Name.Contains("DerSequence"))
                            {
                                var objSubCollection = ((DerSequence)derObject).GetEnumerator();
                                byte count = 0;
                                string strOID = "";
                                DerOctetString strOctet;// = (DerOctetString)derObject;
                                string strTexto = "";

                                while (objSubCollection.MoveNext())
                                {
                                    var Conteudo = objSubCollection.Current;
                                    if (count == 0)
                                    {
                                        strOID = Conteudo.ToString();
                                    }
                                    else
                                    {
                                        Org.BouncyCastle.Asn1.DerTaggedObject subCampos = (Org.BouncyCastle.Asn1.DerTaggedObject)Conteudo;
                                        Asn1Object derSub = subCampos.GetObject();
                                        try
                                        {

                                            if (derSub.GetType().Name.Contains("DerOctetString"))
                                            {
                                                strOctet = (DerOctetString)derSub;
                                                byte[] Texto = strOctet.GetOctets();
                                                strTexto = System.Text.Encoding.ASCII.GetString(Texto);
                                            }
                                            else
                                            {
                                                DerPrintableString strPtrString = (DerPrintableString)derSub;
                                                strTexto = strPtrString.GetString();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            strTexto = derSub.ToString();
                                        }

                                    }
                                    count++;
                                }


                                if (strOID == "2.16.76.1.3.1") //PESSOA FÍSICA
                                {
                                    //i· OID = 2.16.76.1.3.1 e conteúdo = nas primeiras 8(oito) posições, a data de nascimento do titular, no formato ddmmaaaa; nas 11(onze) posições subseqüentes, o Cadastro de Pessoa Física(CPF) do titular; nas 11(onze) posições subseqüentes, o Número de Identificação Social – NIS(PIS, PASEP ou CI); nas 15(quinze) posições subseqüentes, o número do Registro Geral(RG) do titular; nas 10(dez) posições subseqüentes, as siglas do órgão expedidor do RG e respectiva unidade da federação;
                                    try
                                    {
                                        oCert.DataNascimento = strTexto.Substring(0, 8);
                                        oCert.CPF = strTexto.Substring(8, 11);
                                        oCert.NIS = strTexto.Substring(19, 11);
                                        oCert.RG = strTexto.Substring(30, 15);
                                        oCert.OrgaoExpedidor = strTexto.Substring(45);
                                        oCert.Tipo = "F";
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro na leitura da OID=2.16.76.1.3.1:" + ex.Message, ex);
                                    }
                                }
                                else if (strOID == "2.16.76.1.3.6") //PESSOA FÍSICA
                                {
                                    //ii· OID = 2.16.76.1.3.6 e conteúdo = nas 12 (doze) posições o número do Cadastro Específico do INSS (CEI) da pessoa física titular do certificado;


                                }
                                else if (strOID == "2.16.76.1.3.6") //PESSOA FÍSICA
                                {
                                    try
                                    {
                                        //iii· OID = 2.16.76.1.3.5 e conteúdo nas primeiras 12(doze) posições, o número de inscrição do Título de Eleitor; nas 3(três) posições subseqüentes, a Zona Eleitoral; nas 4(quatro) posições seguintes, a Seção; nas 22(vinte e duas) posições subseqüentes, o município e a UF do Título de Eleitor.
                                        oCert.TituloEleitor = strTexto.Substring(0, 12);
                                        oCert.ZonaEleitoral = strTexto.Substring(12, 3);
                                        oCert.SecaoEleitoral = strTexto.Substring(15, 4);
                                        oCert.MunicipioEleitoral = strTexto.Substring(19, 22);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro na leitura da OID=2.16.76.1.3.6:" + ex.Message, ex);
                                    }
                                }
                                else if (strOID == "2.16.76.1.4.2.1.1")
                                {
                                    try
                                    {
                                        oCert.OAB = strTexto;
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro na leitura da OID=2.16.76.1.4.2.1.1:" + ex.Message, ex);
                                    }
                                }
                                else if (strOID == "2.16.76.1.3.4")    //PESSOA JURÍDICA
                                {
                                    try
                                    {
                                        oCert.Tipo = "J";
                                        //i· OID = 2.16.76.1.3.4 e conteúdo = nas primeiras 8(oito) posições, a data de nascimento do responsável pelo certificado, no formato ddmmaaaa; nas 11(onze) posições subseqüentes, o Cadastro de Pessoa Física(CPF) do responsável; nas 11(onze) posições subseqüentes, o Número de Identificação Social – NIS(PIS, PASEP ou CI); nas 15(quinze) posições subseqüentes, o número do Registro Geral(RG) do responsável; nas 10(dez) posições subseqüentes, as siglas do órgão expedidor do RG e respectiva Unidade da Federação;
                                        oCert.DataNascimento = strTexto.Substring(0, 8);
                                        oCert.CPF = strTexto.Substring(8, 11);
                                        try
                                        {
                                            oCert.NIS = strTexto.Substring(19, 11);
                                            oCert.RG = strTexto.Substring(30, 15);
                                            oCert.OrgaoExpedidor = strTexto.Substring(45, 10);
                                        }
                                        catch (Exception ex)
                                        { }
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro na leitura da OID=2.16.76.1.3.4:" + strTexto + "." + ex.Message, ex);
                                    }
                                }
                                else if (strOID == "2.16.76.1.3.2")    //PESSOA JURÍDICA
                                {
                                    //ii· OID = 2.16.76.1.3.2 e conteúdo = nome do responsável pelo certificado;
                                    try
                                    {
                                        oCert.NomeResponsavel = strTexto;
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro na leitura da OID=2.16.76.1.3.2:" + ex.Message, ex);
                                    }

                                }
                                else if (strOID == "2.16.76.1.3.3")    //PESSOA JURÍDICA
                                {
                                    //iii· OID = 2.16.76.1.3.3 e conteúdo = nas 14(quatorze) posições o número do Cadastro Nacional de Pessoa Jurídica(CNPJ) da pessoa jurídica titular do certificado;
                                    try
                                    {
                                        oCert.CNPJ = strTexto;
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("Erro na leitura da OID=2.16.76.1.3.3:" + ex.Message, ex);
                                    }


                                }
                                else if (strOID == "2.16.76.1.3.7")    //PESSOA JURÍDICA
                                {
                                    //iv. OID = 2.16.76.1.3.7 e conteúdo = nas 12 (doze) posições o número do Cadastro Específico do INSS (CEI) da pessoa jurídica titular do certificado.

                                }

                                count = 0;
                            }
                            else
                            {
                                //i. rfc822Name contendo o endereço e-mail do titular do certificado.
                                if (derObject.GetType().Name == "DerOctetString")
                                {
                                    DerOctetString strOctet = (DerOctetString)derObject;
                                    byte[] Texto = strOctet.GetOctets();
                                    string strTexto = System.Text.Encoding.ASCII.GetString(Texto);
                                    oCert.Email = strTexto;
                                }
                                else
                                {
                                    string texto = derObject.GetType().Name;


                                }

                            }
                        }
                        sm.Close();
                    }
                }
                oCert.Certificado = oCertificado;
                oLista.Add(oCert);
            }

            return oLista;
        }

    }

}
