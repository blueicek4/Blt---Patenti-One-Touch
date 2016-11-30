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
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string Login(string username, string password)
        {
            try
            {
                string token;
                string result;
                Bluetech.Pot.BusinessLayer.Authentication.Login(username, password, out token, out result);
                var message = new { Token = token, Result = result };
                HTDebug.Debug dbg = new HTDebug.Debug();
                dbg.DebugLine(String.Format("Login per {0} - {1}", username, result), System.Diagnostics.EventLogEntryType.Information, 0);

                return new JavaScriptSerializer().Serialize(message);
            }
            catch (Exception e)
            {
                HTDebug.Debug dbg = new HTDebug.Debug();
                dbg.DebugLine(String.Format("Errore Login per {0} - {1}", username, e.Message), System.Diagnostics.EventLogEntryType.Error, 0);

                return e.Message;
            }


        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetArticolo(string token, string ArticoloJson)
        {
            try
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

                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Configurato Articolo {0} - {1}", message, result), System.Diagnostics.EventLogEntryType.Information, 0);

                return new JavaScriptSerializer().Serialize(response);
            }
            catch (Exception e)
            {
                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Errore Configurazione Articolo {0}", e.Message), System.Diagnostics.EventLogEntryType.Error, 0);

                return e.Message;
            }

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetCliente(string token, string ClienteJson)
        {
            try
            {
            string userAgent = HttpContext.Current.Request.UserAgent;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string message = String.Empty;
            Boolean result = false;

            if (Pot.BusinessLayer.Authentication.IsTokenValid(token, ip, userAgent))
            {
                Pot.DataLayer.Cliente cliente = (Pot.DataLayer.Cliente)new JavaScriptSerializer().Deserialize(ClienteJson, typeof(Pot.DataLayer.Cliente));

                result = Pot.BusinessLayer.Interface.ExecuteCliente(cliente, out message);
            }

            else
            {
                message = "Token Expired!";
                result = false;
            }

            var response = new { Message = message, Result = result };

                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Configurato Cliente {0} - {1}", message, result), System.Diagnostics.EventLogEntryType.Information, 0);

                return new JavaScriptSerializer().Serialize(response);
            }
            catch (Exception e)
            {
                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Errore Configurazione Cliente - {0}", e.Message), System.Diagnostics.EventLogEntryType.Error, 0);

                return e.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetMedico(string token, string MedicoJson)
        {
            try
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

                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Configurato Medico {0} - {1}", message, result), System.Diagnostics.EventLogEntryType.Information, 0);

                return new JavaScriptSerializer().Serialize(response);
            }
            catch (Exception e)
            {
                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Errore Configurazione Medico - {0}", e.Message), System.Diagnostics.EventLogEntryType.Error, 0);

                return e.Message;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string SetOrdine(string token, string OrdineJson)
        {
            try
            {            
            string userAgent = HttpContext.Current.Request.UserAgent;
            string ip = HttpContext.Current.Request.UserHostAddress;
            string message = String.Empty;
            Boolean result = false;

            if (Pot.BusinessLayer.Authentication.IsTokenValid(token, ip, userAgent))
            {
                Pot.DataLayer.OrdineWeb ordine = (Pot.DataLayer.OrdineWeb)new JavaScriptSerializer().Deserialize(OrdineJson, typeof(Pot.DataLayer.OrdineWeb));

                result = Pot.BusinessLayer.Interface.ExecuteOrdine(ordine, out message);
            }

            else
            {
                message = "Token Expired!";
                result = false;
            }

            var response = new { Message = message, Result = result };

                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Inserito Ordine {0} - {1}", message, result), System.Diagnostics.EventLogEntryType.Information, 0);

                return new JavaScriptSerializer().Serialize(response);
            }
            catch (Exception e)
            {
                //HTDebug.Debug dbg = new HTDebug.Debug();
                //dbg.DebugLine(String.Format("Errore Inserimento Ordine - {0}", e.Message), System.Diagnostics.EventLogEntryType.Error, 0);

                return e.Message;
            }

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
            Pot.DataLayer.Cliente m = new Pot.DataLayer.Cliente();
            m.CodiceFiscale = "CDCFSC16Z21H" + new Random().Next(100, 999).ToString() + "Z";
            //m.PartitaIva = "123456" + new Random().Next(1000, 9999).ToString() + "1";
            m.CodiceUnivoco = "ABC" + new Random().Next(1000, 9999).ToString() + "E";
            m.Cognome = "Cognome Casuale" + new Random().Next(0, 9999).ToString();
            m.Nome = "Nome Casuale" + new Random().Next(0, 9999).ToString();
            //m.RagioneSociale = "Ragione Sociale Casuale" + new Random().Next(0, 9999).ToString();
            m.ComuneNascita = "Roma";
            m.Nazionalita = "ITALIA";
            m.DataNascita = DateTime.Now.Date;
            m.Indirizzo = "Via di casa, 29";
            m.Comune = "Ariccia";
            m.Cap = "00100";
            m.Provincia = "ROMA";
            m.Email = "nome.cognome@email.it";
            m.Iban = "IT96R0123454321000000012345".Trim();
            return new JavaScriptSerializer().Serialize(m);
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //[ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetModelMedicoJson()
        {
            Pot.DataLayer.Medico m = new Pot.DataLayer.Medico();
            m.CodiceFiscale = "CDCFSC16Z21H" + new Random().Next(100, 999).ToString()+"Z";
            m.PartitaIva = "123456" + new Random().Next(1000, 9999).ToString() + "1";
            m.CodiceUnivoco = "ABC" + new Random().Next(1000, 9999).ToString() + "E";
            m.Cognome = "Cognome Casuale" + new Random().Next(0,9999).ToString();
            m.Nome = "Nome Casuale" + new Random().Next(0, 9999).ToString();
            m.RagioneSociale = "Ragione Sociale Casuale" + new Random().Next(0, 9999).ToString();
            m.ComuneNascita = "Roma";
            m.Nazionalita = "ITALIA";
            m.DataNascita = DateTime.Now.Date;
            m.Indirizzo = "Via di casa, 29";
            m.Comune = "Ariccia";
            m.Cap = "00100";
            m.Provincia = "ROMA";
            m.Iban = "IT96R0123454321000000012345".Trim();
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
        public string GetModelOrdineJson()
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            OrdineWeb f = new OrdineWeb();
            f.CodiceUnivocoControparte = "ABCDEFGH";
            f.CodiceUnivocoMedico = "QWERTYUI";
            f.CodicePratica = "6SO" + new Random().Next(1000,9999).ToString()+ "W";
            f.DataPratica = DateTime.Now.Date;
            f.TipoPagamento = 1;
            f.BancaPagamento = 1;
            f.TotaleFattura = 80;
            f.ImportoMedico = 20;
            f.ImportoSconto = 2;
            f.IdConfigurazione = 1;



            return new JavaScriptSerializer().Serialize(f);

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string GetModelEsitoVisitaJson()
        {
            EsitoVisita e = new EsitoVisita();
            e.CodicePratica = "123456AB";
            e.DataPratica = DateTime.Now.Date;
            e.CodiceUnivocoMedico = "12345678";
            e.Esito = "OK/KO/ASSENTE";

            return new JavaScriptSerializer().Serialize(e);
        }


    }



}
