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
        public string Nazione { get; set; }
        public string Email { get; set; }
        public string Iban { get; set; }
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
        public string Nazione { get; set; }
        public string Iban { get; set; }

    }
    public class Articolo
    {
        public string Codice { get; set; }
        public string Descrizione { get; set; }
        public string Udm { get; set; }
        public string AliquotaIva { get; set; }
        
    }
    public class OrdineWeb
    {
        public string CodiceUnivocoControparte { get; set; }
        public DateTime? DataPratica { get; set; }
        public string CodicePratica { get; set; }
        public decimal TotaleFattura { get; set; }
        public int TipoPagamento { get; set; }
        public int BancaPagamento { get; set; }
        public string CodiceUnivocoMedico { get; set; }
        public decimal ImportoMedico { get; set; }
        public decimal ImportoSconto { get; set; }
        public int IdConfigurazione { get; set; }

    }
    public class OrdineTestata
    {
        public string CodiceUnivocoControparte { get; set; }
        public DateTime? DataOrdine { get; set; }
        public string ProgressivoOrdine { get; set; }
        public string SerieOrdine { get; set; }
        public decimal TotaleImponibile { get; set; }
        public decimal TotaleImposte { get; set; }
        public decimal TotaleFattura { get; set; }
        public int TipoPagamento { get; set; }
        public int BancaPagamento { get; set; }
        public string CodicePratica { get; set; }
        public List<OrdineRiga> Righe { get; set; }
        public string CodiceUnivocoMedico { get; set; }
        public decimal ImportoMedico { get; set; }
        public decimal ImportoSconto { get; set; }
        public decimal ImportoNotaCredito { get; set; }

        public OrdineTestata()
        {
            Righe = new List<OrdineRiga>();
        }

        public static OrdineTestata CaricaDaOrdineWeb(OrdineWeb _ordine)
        {
            OrdineTestata o = new OrdineTestata();
            

            switch (_ordine.IdConfigurazione)
            {
                case 1:
                    o.CodiceUnivocoControparte = _ordine.CodiceUnivocoControparte;
                    o.CodiceUnivocoMedico = _ordine.CodiceUnivocoMedico;
                    o.DataOrdine = _ordine.DataPratica;
                    o.SerieOrdine = System.Configuration.ConfigurationManager.AppSettings["Sezionale"];
                    o.TotaleFattura = _ordine.TotaleFattura;
                    o.CodicePratica = _ordine.CodicePratica;
                    if (_ordine.ImportoSconto > 0)
                        o.ImportoSconto = _ordine.ImportoSconto;
                    else if (_ordine.ImportoSconto < 0)
                        o.ImportoNotaCredito = _ordine.ImportoSconto;
                    o.ProgressivoOrdine = "0";
                    o.TipoPagamento = _ordine.TipoPagamento;
                    o.BancaPagamento = _ordine.BancaPagamento;

                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0001", DescrizioneRiga = "PRATICA NR. " + o.CodicePratica, AliquotaIva = "22", Qta = 1, PrezzoUnitario = 22, Udm = "UR" });
                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0002", DescrizioneRiga = "DIRITTO DELLA MOTORIZZAZIONE", AliquotaIva = "S15", Qta = 1, PrezzoUnitario = 11.98M, Udm = "UR" });
                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0003", DescrizioneRiga = "IMPOSTA DI BOLLO", AliquotaIva = "S15", Qta = 1, PrezzoUnitario = 17.78M, Udm = "UR" });
                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0004", DescrizioneRiga = "SPESE AMMINISTRATIVE", AliquotaIva = "S15", Qta = 1, PrezzoUnitario = 3.4M, Udm = "UR" });
                    //o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0005", DescrizioneRiga = "ONORARIO DOTTORE" + o.CodiceUnivocoMedico, AliquotaIva = "22", Qta = 1, PrezzoUnitario = 22, Udm = "UR" });
                    

                    break;
                default:
                    break;
            }

            return o;
        }

    }
    public class OrdineRiga
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
        public string CodicePratica { get; set; }
        public string SerieFattura { get; set; }
        public DateTime DataPratica { get; set; }
        public string Esito { get; set; }
        public string CodiceUnivocoMedico { get; set; }
    }

    public class GestioneLookUp
    {
        public Int32 GetProgressivo(string _contatore)
        {
            Int32 res = 0;

            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();

            if (dc.Progressivi.Where(p => p.NomeContatore == _contatore).Count() > 0)
            {
                var result = dc.Progressivi.Where(p => p.NomeContatore == _contatore).First();//.Select(p => p.Progressivo).First();
                if (result.Progressivo >= Int32.MaxValue)
                    result.Progressivo = 0;
                else
                    result.Progressivo++;

                res = Int32.Parse(result.Progressivo.ToString());

                dc.SubmitChanges();
            }
            else
            {
                Database.Progressivi p = new Database.Progressivi();
                p.NomeContatore = _contatore;
                p.Progressivo = 1;
                res = 1;
                dc.Progressivi.InsertOnSubmit(p);
                dc.SubmitChanges();
            }


            return res;
        }
        public string GetCodicePratica(string _pratica)
        {
            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
            if (dc.Pratiche.Where(p => p.CodicePratica == _pratica).Count() > 0)
                return dc.Pratiche.Where(p => p.CodicePratica == _pratica).Select(p => p.CodicePratica).First();
            else
                return String.Empty;
        }

        public Database.Pratiche GetPratica(string _codice)
        {
            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
            if (dc.Pratiche.Where(p => p.CodicePratica == _codice).Count() > 0)
                return dc.Pratiche.Where(p => p.CodicePratica == _codice).First();
            else
                return null;

        }

        public Boolean SetPratica(string _pratica, string _cliente, string _medico, int _pag, int _sconto, int _importoMedico, int _importo, string _numFattura, string _dataFattura, string _numPag)
        {
            try
            {
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                Database.Pratiche pr = new Database.Pratiche();
                pr.CodicePratica = _pratica;
                pr.CodiceCliente = _cliente;
                pr.CodiceMedico = _medico;
                pr.TipoPagamento = _pag;
                pr.NumeroFattura = _numFattura;
                pr.DataFattura = _dataFattura;
                pr.Importo = _importo;
                pr.ImportoMedico = _importoMedico;
                pr.ImportoSconto = _sconto;
                pr.NumeroPagamento = _numPag;
                
                dc.Pratiche.InsertOnSubmit(pr);
                dc.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean SetEsito(string _pratica, string _esito, string _medico, string _numPag, string _dataPag, string _numNC, string _dataNC, string _numStorno, string _dataStorno)
        {
            try
            {
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                var pr = dc.Pratiche.Where(p => p.CodicePratica == _pratica).First();
                pr.Esito = _esito;
                pr.CodiceMedicoEsito = _medico;
                pr.NumeroNotaCredito = _numNC;
                pr.DataNotaCredito = _dataNC;
                pr.NumeroStorno = _numStorno;
                pr.DataStorno = _dataStorno;
                pr.NumeroPagamentoMedico = _numPag;
                pr.DataPagamentoMedico = _dataPag;

                dc.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetAnagrafica(string _codiceEsterno)
        {
            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
            string res = String.Empty;
            if(dc.Anagrafiche.Any(a=>a.CodiceEsterno == _codiceEsterno))
                res = dc.Anagrafiche.Where(a => a.CodiceEsterno == _codiceEsterno).Select(a=>a.CodiceMexal).First();
            return res;
        }

        public Boolean SetAnagrafica(string _codiceEsterno, string _codiceMexal, bool _isMedico)
        {
            try
            {
                bool insert = true;
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                Database.Anagrafiche an = new Database.Anagrafiche();
                if (dc.Anagrafiche.Any(a => a.CodiceEsterno == _codiceEsterno))
                {
                    insert = false;
                    an = dc.Anagrafiche.Where(a => a.CodiceEsterno == _codiceEsterno).First();
                }
                an.CodiceEsterno = _codiceEsterno;
                an.CodiceMexal = _codiceMexal;
                an.IsMedico = _isMedico;

                if (insert)
                    dc.Anagrafiche.InsertOnSubmit(an);
                dc.SubmitChanges();

                return true;
            }
            catch(Exception e)
            {
                return false;
            }

        }


    }


}
