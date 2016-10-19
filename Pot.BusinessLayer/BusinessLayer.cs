using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Bluetech.Pot.DataLayer;

namespace Bluetech.Pot.BusinessLayer
{
    
    public class Authentication
    {
        private const int _expirationMinutes = 10;
        private const string _alg = "HmacSHA256";
        private const string _salt = "GEZMdM58KdbvOqfAe0XE"; // Generated at https://www.random.org/strings
        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {
            string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString() });
            string hashLeft = "";
            string hashRight = "";
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
                hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                hashLeft = Convert.ToBase64String(hmac.Hash);
                hashRight = string.Join(":", new string[] { username, ticks.ToString() });
            }
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Join(":", new string[] { hashLeft, hashRight })));
        }
        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });
            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }
        public static bool IsTokenValid(string token, string ip, string userAgent)
        {
            checkMexalClient();
            bool result = false;
            try
            {
                // Base64 decode the string, obtaining the token:username:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                // Split the parts.
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 3)
                {
                    // Get the hash message, username, and timestamp.
                    string hash = parts[0];
                    string username = parts[1];
                    long ticks = long.Parse(parts[2]);
                    DateTime timeStamp = new DateTime(ticks);
                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes;
                    if (!expired)
                    {
                        //
                        // Lookup the user's account from the db.
                        //
                        if (username == "patenti")
                        {
                            string password = "bluetech";
                            // Hash the message with the key to generate a token.
                            string computedToken = GenerateToken(username, password, ip, userAgent, ticks);
                            // Compare the computed token with the one supplied and ensure they match.
                            result = (token == computedToken);
                        }
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        public static Boolean Login(string username, string password, out string token, out string message)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            long timeStamp = DateTime.Now.Ticks;
            string ip = HttpContext.Current.Request.UserHostAddress;
            bool result = false;

            if(username == "patenti" && password == "bluetech")
            {
                message = "Logged in!";
                token = GenerateToken(username, password, ip, userAgent, timeStamp);
                result = true;
                
            }
            else
            {
                token = String.Empty;
                message = "Login error!";
            }
            return result;
        }

        public static Boolean Authenticate (string token)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            long timeStamp = DateTime.Now.Ticks;
            string ip = HttpContext.Current.Request.UserHostAddress;

            return IsTokenValid(token, ip, userAgent);
        }

        public static Boolean checkMexalClient()
        {

            Mexal.MexalClient mc = (Mexal.MexalClient)HttpContext.Current.Application["mxlClient"];

            if (mc == null)
            {
                mc = new Mexal.MexalClient();
                string result;
                mc.AvviaConnessione(out result);
                HttpContext.Current.Application["mxlClient"] = mc;
            }

            return true;
        }
    }

    public static class Interface
    {
        public static Boolean ExecuteFattura(FatturaTestata fattura, out string message)
        {
            if (string.IsNullOrWhiteSpace(fattura.CodiceUnivocoControparte))
            {
                message = "Controparte Mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(fattura.ProgressivoFattura))
            {
                message = "Progressivo Mancante";
                return false;
            }
            if (fattura.DataFattura == DateTime.MinValue || fattura.DataFattura == null)
            {
                message = "Data Fattura Mancante o errata";
                return false;
            }
            if (string.IsNullOrWhiteSpace(fattura.TipoPagamento))
            {
                message = "Tipo pagamento Mancante";
                return false;
            }
            if (fattura.Righe.Sum(r => r.Qta * r.PrezzoUnitario) != fattura.TotaleImponibile)
            {
                message = "Importo righe diverso da Totale Imponibile";
                return false;
            }

            // elaborazione verso mexal
            message = "OK!";
            return true;
        }

        public static Boolean ExecuteVisita(EsitoVisita visita, out string message)
        {
            if (string.IsNullOrWhiteSpace(visita.ProgressivoFattura))
            {
                message = "Progressivo Mancante";
                return false;
            }
            if (visita.DataFattura == DateTime.MinValue || visita.DataFattura == null)
            {
                message = "Data Fattura Mancante o errata";
                return false;
            }
            if (string.IsNullOrWhiteSpace(visita.Esito))
            {
                message = "Esito Mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(visita.CodiceUnivocoMedico))
            {
                message = "Codice Medico Mancante";
                return false;
            }

            // elaborazione verso mexal
            message = "OK!";
            return true;
        }

        public static Boolean ExecuteCliente(Cliente cliente, out string message)
        {
            if(string.IsNullOrWhiteSpace(cliente.Nome))
            {
                message = "Nome Cliente Mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Cognome))
            {
                message = "Cognome cliente mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.CodiceUnivoco))
            {
                message = "Codice univoco mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.CodiceFiscale))
            {
                message = "Codice Fiscale mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.ComuneNascita))
            {
                message = "Comune di nascita mancante";
                return false;
            }
            if (cliente.DataNascita == null || cliente.DataNascita == DateTime.MinValue)
            {
                message = "Data di nascita mancante o errata";
                return false;
            }

            // elaborazione verso mexal
            Authentication.checkMexalClient();
            Mexal.MexalClient mc = (Mexal.MexalClient)HttpContext.Current.Application["mxlClient"];

            mc.Cliente = new Mexal.MexalAnagrafica();
            mc.Cliente.LoadFromCliente(cliente);
            mc.Cliente.Mastro = "530";
            mc.SetAnagrafica(mc.Cliente, out message);
            message = "OK!";
            return true;

        }

        public static Boolean ExecuteMedico(Medico medico, out string message)
        {
            if (string.IsNullOrWhiteSpace(medico.Nome))
            {
                message = "Nome Cliente Mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(medico.Cognome))
            {
                message = "Cognome cliente mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(medico.CodiceUnivoco))
            {
                message = "Codice univoco mancante";
                return false;
            }
            if (string.IsNullOrWhiteSpace(medico.CodiceFiscale) && string.IsNullOrWhiteSpace(medico.PartitaIva))
            {
                message = "Codice Fiscale e Partita iva mancanti, specificare almeno uno dei due valori";
                return false;
            }
            if (string.IsNullOrWhiteSpace(medico.ComuneNascita))
            {
                message = "Comune di nascita mancante";
                return false;
            }
            if (medico.DataNascita == null || medico.DataNascita == DateTime.MinValue)
            {
                message = "Data di nascita mancante o errata";
                return false;
            }

            // elaborazione verso mexal
            message = "OK!";
            return true;
        }

        public static Boolean ExecuteArticolo(Articolo articolo, out string message)
        {
            if (String.IsNullOrWhiteSpace(articolo.Codice))
            {
                message = "Codice articolo mancante";
                return false;
            }

            if (String.IsNullOrWhiteSpace(articolo.Descrizione))
            {
                message = "descrizione articolo mancante";
                return false;
            }

            if (String.IsNullOrWhiteSpace(articolo.AliquotaIva))
            {
                message = "Codice iva mancante";
                return false;
            }

            if (String.IsNullOrWhiteSpace(articolo.Udm))
            {
                message = "Unita di misura mancante";
                return false;
            }

            // elaborazione verso mexal
            message = "OK!";
            return true;

        }
    }
}
