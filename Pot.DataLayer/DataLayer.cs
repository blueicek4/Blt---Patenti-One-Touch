using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bluetech.Pot.DataLayer
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string RagioneSociale {get;set;}
        public DateTime? DataNascita { get; set; }
        public string ComuneNascita { get; set; }
        public string Nazionalita { get; set; }
        public string CodiceFiscale { get; set; }
        public string CodiceUnivoco { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }

    }

    public class Medico
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string RagioneSociale { get; set; }
        public DateTime? DataNascita { get; set; }
        public string ComuneNascita { get; set; }
        public string Nazionalita { get; set; }
        public string CodiceFiscale { get; set; }
        public string PartitaIva { get; set; }
        public string CodiceUnivoco { get; set; }
        public string Indirizzo { get; set; }
        public string Cap { get; set; }
        public string Comune { get; set; }
        public string Provincia { get; set; }

    }
    public class Articolo
    {
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public string Udm { get; set; }
        public string AliquotaIva { get; set; }
        
    }
    public class FatturaTestata
    {
        public string CodiceUnivocoControparte { get; set; }
        public DateTime? DataFattura { get; set; }
        public string ProgressivoFattura { get; set; }
        public string SerieFattura { get; set; }
        public decimal TotaleImponibile { get; set; }
        public decimal TotaleImposte { get; set; }
        public decimal TotaleFattura { get; set; }
        public string TipoPagamento { get; set; }
        public string BancaPagamento { get; set; }
        public List<FatturaRiga> Righe { get; set; }


    }
    public class FatturaRiga
    {
        public string CodiceArticolo { get; set; }
        public string DescrizioneRiga { get; set; }
        public string Udm { get; set; }
        public decimal Qta { get; set; }
        public decimal PrezzoUnitario { get; set; }
        public string AliquotaIva { get; set; }
    }
    public class EsitoVisita
    {
        public string ProgressivoFattura { get; set; }
        public string SerieFattura { get; set; }
        public DateTime DataFattura { get; set; }
        public string Esito { get; set; }
        public string CodiceUnivocoMedico { get; set; }
    }

}
