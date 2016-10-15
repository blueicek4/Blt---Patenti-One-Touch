using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.Services;
using Newtonsoft.Json;
using System.Dynamic;
using Bluetech.Pot.DataLayer;

namespace Bluetech
{
    /// <summary>
    /// Descrizione di riepilogo per Interface
    /// </summary>
    [WebService(Namespace = "http://bluetech.pot/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente. 
    // [System.Web.Script.Services.ScriptService]
    public class Interface : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string Login(string username, string password)
        {
            string token;
            string result;
            Bluetech.Pot.BusinessLayer.Authentication.Login(username, password, out token, out result);
            var message = new { Token = token, Result = result };

            return new JavaScriptSerializer().Serialize(message);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetArticolo(string token, string ArticoloJson)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string message = String.Empty;
            Boolean result = false;

            if (Pot.BusinessLayer.Authentication.IsTokenValid(token, ip, userAgent))
            {
                Pot.DataLayer.Articolo articolo = (Pot.DataLayer.Articolo)new JavaScriptSerializer().Deserialize(ArticoloJson, typeof(Pot.DataLayer.Articolo));

                result = Pot.BusinessLayer.Interface.ExecuteArticolo(articolo, out message);
                System.Threading.Thread.Sleep(3 * 1000);
            }

            else
            {
                message = "Token Expired!";
                result = false;
            }

            var response = new { Message = message, Result = result };
            return new JavaScriptSerializer().Serialize(response);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetCliente(string token, string ClienteJson)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string message = String.Empty;
            Boolean result = false;

            if (Pot.BusinessLayer.Authentication.IsTokenValid(token, ip, userAgent))
            {
                Pot.DataLayer.Cliente cliente = (Pot.DataLayer.Cliente)new JavaScriptSerializer().Deserialize(ClienteJson, typeof(Pot.DataLayer.Cliente));

                result = Pot.BusinessLayer.Interface.ExecuteCliente(cliente, out message);
                System.Threading.Thread.Sleep(3 * 1000);
            }

            else
            {
                message = "Token Expired!";
                result = false;
            }

            var response = new { Message = message, Result = result };
            return new JavaScriptSerializer().Serialize(response);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetMedico(string token, string MedicoJson)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string message = String.Empty;
            Boolean result = false;

            if (Pot.BusinessLayer.Authentication.IsTokenValid(token, ip, userAgent))
            {
                Pot.DataLayer.Medico medico = (Pot.DataLayer.Medico)new JavaScriptSerializer().Deserialize(MedicoJson, typeof(Pot.DataLayer.Medico));

                result = Pot.BusinessLayer.Interface.ExecuteMedico(medico, out message);
                System.Threading.Thread.Sleep(3 * 1000);
            }

            else
            {
                message = "Token Expired!";
                result = false;
            }

            var response = new { Message = message, Result = result };
            return new JavaScriptSerializer().Serialize(response);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetFattura(string token, string FatturaJson)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string message = String.Empty;
            Boolean result = false;

            if (Pot.BusinessLayer.Authentication.IsTokenValid(token, ip, userAgent))
            {
                Pot.DataLayer.FatturaTestata fattura = (Pot.DataLayer.FatturaTestata)new JavaScriptSerializer().Deserialize(FatturaJson, typeof(Pot.DataLayer.FatturaTestata));

                result = Pot.BusinessLayer.Interface.ExecuteFattura(fattura, out message);
                System.Threading.Thread.Sleep(3 * 1000);
            }

            else
            {
                message = "Token Expired!";
                result = false;
            }

            var response = new { Message = message, Result = result };
            return new JavaScriptSerializer().Serialize(response);
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetVisita(string token, string VisitaJson)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string message = String.Empty;
            Boolean result = false;

            if (Pot.BusinessLayer.Authentication.IsTokenValid(token, ip, userAgent))
            {
                Pot.DataLayer.EsitoVisita visita = (Pot.DataLayer.EsitoVisita)new JavaScriptSerializer().Deserialize(VisitaJson, typeof(Pot.DataLayer.EsitoVisita));

                result = Pot.BusinessLayer.Interface.ExecuteVisita(visita, out message);
                System.Threading.Thread.Sleep(3 * 1000);
            }

            else
            {
                message = "Token Expired!";
                result = false;
            }

            var response = new { Message = message, Result = result };
            return new JavaScriptSerializer().Serialize(response);
        }





        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetModelClienteJson()
        {
            Pot.DataLayer.Cliente c = new Pot.DataLayer.Cliente();
            c.CodiceFiscale = "CDCFSC16Z21H501Z";
            c.CodiceUnivoco = "12345678";
            c.Cognome = "Cognome";
            c.Nome = "Nome";
            c.RagioneSociale = "Ragione Sociale";
            c.ComuneNascita = "Roma";
            c.Nazionalita = "ITALIA";
            c.DataNascita = DateTime.Now.Date;
            c.Indirizzo = "Via di casa, 29";
            c.Comune = "Ariccia";
            c.Cap = "00100";
            c.Provincia = "ROMA";
            return new JavaScriptSerializer().Serialize(c);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetModelMedicoJson()
        {
            Pot.DataLayer.Medico m = new Pot.DataLayer.Medico();
            m.CodiceFiscale = "CDCFSC16Z21H501Z";
            m.PartitaIva = "12345678901";
            m.CodiceUnivoco = "12345678";
            m.Cognome = "Cognome";
            m.Nome = "Nome";
            m.RagioneSociale = "Ragione Sociale";
            m.ComuneNascita = "Roma";
            m.Nazionalita = "ITALIA";
            m.DataNascita = DateTime.Now.Date;
            m.Indirizzo = "Via di casa, 29";
            m.Comune = "Ariccia";
            m.Cap = "00100";
            m.Provincia = "ROMA";
            return new JavaScriptSerializer().Serialize(m);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetModelArticoloJson()
        {
            Articolo a = new Articolo();
            a.Codice = "012345";
            a.Descrizione = "Descrizione Articolo";
            a.AliquotaIva = "04";
            a.Udm = "NR";

            return new JavaScriptSerializer().Serialize(a);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetModelFatturaJson()
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            FatturaTestata f = new FatturaTestata();
            f.CodiceUnivocoControparte = "12345678";
            f.ProgressivoFattura = "123456";
            f.SerieFattura = "ABC";
            f.DataFattura = DateTime.Now.Date;
            f.TipoPagamento = "CC";
            f.BancaPagamento = "CODICEBANCA";
            f.TotaleFattura = 80;
            f.TotaleImponibile = 75.16M;
            f.TotaleImposte = 4.84M;

            List<FatturaRiga> r = new List<FatturaRiga>();
            r.Add(new FatturaRiga() { CodiceArticolo = "A-SP0001", DescrizioneRiga = "PRATICA NR. 6SOM53KW", Udm = "UR", Qta = 1, AliquotaIva = "22", PrezzoUnitario = 22 });
            r.Add(new FatturaRiga() { CodiceArticolo = "A-SP0002", DescrizioneRiga = "DIRITTO DELLA MOTORIZZAZIONE", Udm = "UR", Qta = 1, AliquotaIva = "VE", PrezzoUnitario = 11.98M });
            r.Add(new FatturaRiga() { CodiceArticolo = "A-SP0003", DescrizioneRiga = "IMPOSTA DI BOLLO", Udm = "UR", Qta = 1, AliquotaIva = "VE", PrezzoUnitario = 17.78M });
            r.Add(new FatturaRiga() { CodiceArticolo = "A-SP0004", DescrizioneRiga = "SPESE AMMINISTRATIVE", Udm = "UR", Qta = 1, AliquotaIva = "VE", PrezzoUnitario = 3.4M });
            r.Add(new FatturaRiga() { CodiceArticolo = "A-SP0005", DescrizioneRiga = "ONORARIO DOTTORE MARCO ROSSI", Udm = "UR", Qta = 1, AliquotaIva = "VE", PrezzoUnitario = 20 });

            f.Righe = r;

            return new JavaScriptSerializer().Serialize(f);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetModelEsitoVisitaJson()
        {
            EsitoVisita e = new EsitoVisita();
            e.ProgressivoFattura = "123456";
            e.SerieFattura = "ABC";
            e.DataFattura = DateTime.Now.Date;
            e.CodiceUnivocoMedico = "12345678";
            e.Esito = "OK";

            return new JavaScriptSerializer().Serialize(e);
        }


    }



}
