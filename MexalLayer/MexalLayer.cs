using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSprixDn;
using MSprixDn.Classi;

namespace Bluetech.Mexal
{
public class MexalClient
    {
        private MxSpxDotNet client { get; set; }
        public Int32 Porta { get; set; }
        public string Indirizzo { get; set; }
        public string WindowsUsername { get; set; }
        public string WindowsPassword { get; set; }

        public string MexalUsername { get; set; }
        public string MexalPassword { get; set; }
        public Int32 Terminale { get; set; }
        public DateTime DataTerminale { get; set; }

        public string Azienda { get; set; }

        public MexalAnagrafica Cliente { get; set; }

        public MexalClient()
        {
            this.Porta = 9810;
            this.Indirizzo = "192.168.0.22";
            this.WindowsUsername = "claudio";
            this.WindowsPassword = "SECCO";
            this.MexalUsername = "ADMIN";
            this.MexalPassword = "ADMIN";
            this.Terminale = 0;
            this.DataTerminale = DateTime.Now.Date;
            this.Azienda = "ANT";
            this.client = new MxSpxDotNet(this);
        }
        public MexalClient( Int32 _porta, string _indirizzo, string _winUsername, string _winPassword, string _mxlUsername, string _mxlPassword, Int32 _terminale, string _azienda, DateTime _data)
        {

        }

        public Boolean AvviaConnessione (out string message)
        {
            client.PORTA = Double.Parse(this.Porta.ToString());
            client.INDIRIZZO = this.Indirizzo;
            client.LOGINMXSRV = this.WindowsUsername;
            client.PASSWORDMXSRV = this.WindowsPassword;
            client.PASSWORDPASS = this.MexalUsername + ":" + this.MexalPassword;
            client.TERMINALE = "t" + this.Terminale.ToString();
            client.DATAAPTERM = this.DataTerminale.ToString("ddMMyyyy");
            client.AZIENDA = this.Azienda;
            
            client.AVVIACONNESSIONE();

            if(string.IsNullOrWhiteSpace(client.ERRORE))
            {
                message = "OK!";
                return true;
            }
            else
            {
                message = client.ERRORE;
                return false;
            }
        }

        public Boolean SetAnagrafica(MexalAnagrafica _anagrafica, out string message)
        {
            if (String.IsNullOrWhiteSpace(_anagrafica.CodiceCliente))
                this.client.PCCOD_S = _anagrafica.Mastro + ".AUTO";
            else
                this.client.PCCOD_S = _anagrafica.Mastro + "." + _anagrafica.CodiceCliente;
            this.client.PCCAL_S = _anagrafica.CodiceRiferimento;
            this.client.PCDES_S = _anagrafica.Descrizione;
            this.client.PCNOM_S = _anagrafica.Nome;
            this.client.PCCOG_S = _anagrafica.Cognome;
            this.client.PCDTNASC_S = _anagrafica.DataNascita.ToString("ggMMyyyy");
            this.client.PCNAZ_S = _anagrafica.Nazionalita;
            this.client.PCCFI_S = _anagrafica.CodiceFiscale;
            this.client.PCNPI_S = _anagrafica.PartitaIva;
            if (String.IsNullOrWhiteSpace(_anagrafica.Nome) && String.IsNullOrWhiteSpace(_anagrafica.PartitaIva))
                this.client.PCPFS_S = "N";
            else
                this.client.PCPFS_S = "S";
            if (String.IsNullOrWhiteSpace(_anagrafica.PartitaIva))
                this.client.PCPRI_S = "S";
            else
                this.client.PCPRI_S = "N";

            this.client.PCTEL_S = _anagrafica.Telefono;
            this.client.PCFAX_S = _anagrafica.Cellulare;
            this.client.PCINT_S = _anagrafica.Email;
            this.client.PCINT1_S = _anagrafica.Web;
            this.client.PCIND_S = _anagrafica.Indirizzo;
            this.client.PCLOC_S = _anagrafica.Localita;
            this.client.PCCAP_S = _anagrafica.Cap;
            this.client.PCPAE_S = _anagrafica.Stato;
            this.client.PCPRO_S = _anagrafica.Provincia;

            this.client.PUTPC();
            if (String.IsNullOrWhiteSpace(this.client.ERRPC_S))
            {
                message = "Ok!";
                return true;
            }
            else
            {
                message = this.client.ERRPC_S;
                return false;
            }

        }

        public void ResetVariabili()
        {
            this.client.AZZVARSYS(1);
        }

    }

    public class MexalAnagrafica
    {
        public string Mastro { get; set; }
        public string CodiceCliente { get; set; }
        public string Descrizione { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public Int32 Listino { get; set; }
        public string CodiceFiscale { get; set; }
        public string PartitaIva { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Cellulare { get; set; }
        public string Web { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Localita { get; set; }
        public string Provincia { get; set; }
        public string Stato { get; set; }
        public string Nazionalita { get; set; }
        public DateTime DataNascita { get; set; }
        public string CodiceRiferimento { get; set; }

        public MexalAnagrafica()
        {

        }

        public void LoadFromCliente(Bluetech.Pot.DataLayer.Cliente _cliente)
        {
            this.Nome = _cliente.Nome;
            this.Cognome = _cliente.Cognome;
            this.CodiceRiferimento =  _cliente.CodiceUnivoco;
            this.CodiceFiscale = _cliente.CodiceFiscale;
            this.DataNascita = _cliente.DataNascita.Value;// .ToString("ggMMyyyy");
            this.Indirizzo = _cliente.Indirizzo;
            this.Localita = _cliente.Comune;
            this.Cap = _cliente.Cap;
            this.Stato = _cliente.Nazione;
            this.Nazionalita = _cliente.Nazionalita;
            
        }
    }
}
