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
                    o.SerieOrdine = "1";
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
                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0002", DescrizioneRiga = "DIRITTO DELLA MOTORIZZAZIONE", AliquotaIva = "22", Qta = 1, PrezzoUnitario = 22, Udm = "UR" });
                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0003", DescrizioneRiga = "IMPOSTA DI BOLLO", AliquotaIva = "22", Qta = 1, PrezzoUnitario = 22, Udm = "UR" });
                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0004", DescrizioneRiga = "SPESE AMMINISTRATIVE", AliquotaIva = "22", Qta = 1, PrezzoUnitario = 22, Udm = "UR" });
                    o.Righe.Add(new OrdineRiga() { CodiceArticolo = "A-SP0005", DescrizioneRiga = "ONORARIO DOTTORE" + o.CodiceUnivocoMedico, AliquotaIva = "22", Qta = 1, PrezzoUnitario = 22, Udm = "UR" });
                    

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
        public string GetCodiceOrdine(string _pratica)
        {
            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
            return dc.Pratiche.Where(p => p.CodicePratica == _pratica).Select(p => p.CodicePratica).First();
        }

        public Boolean SetCodiceOrdine(string _pratica, string _numOrdine, string _serieOrdine, string _dataOrdine)
        {
            try
            {
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                Database.Pratiche pr = new Database.Pratiche();
                pr.CodicePratica = _pratica;
                pr.NumeroOrdineMexal = _numOrdine;
                pr.SerieOrdineMexal = _serieOrdine;
                pr.DataOrdineMexal = _dataOrdine;
                dc.Pratiche.InsertOnSubmit(pr);
                dc.SubmitChanges();

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean SetCodiceOrdine(string _pratica, string _numOrdine, string _serieOrdine, string _dataOrdine, string _numPN, string _seriePN, string _numRegPN, string _dataPN)
        {
            try
            {
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                Database.Pratiche pr = new Database.Pratiche();
                pr.CodicePratica = _pratica;
                pr.NumeroOrdineMexal = _numOrdine;
                pr.SerieOrdineMexal = _serieOrdine;
                pr.DataOrdineMexal = _dataOrdine;
                pr.NumeroPrimaNotaMexal = _numPN;
                pr.SeriePrimaNotaMexal = _seriePN;
                pr.ProgressivoPrimaNotaMexal = _numRegPN;
                pr.DataPrimaNotaMexal = _dataPN;

                dc.Pratiche.InsertOnSubmit(pr);

                dc.SubmitChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public Boolean SetPrimaNota(string _pratica, string _numPN, string _seriePN, string _numRegPN, string _dataPN)
        {
            try
            {
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                Database.Pratiche pr = dc.Pratiche.Where(p => p.CodicePratica == _pratica).First();

                pr.CodicePratica = _pratica;
                pr.NumeroPrimaNotaMexal = _numPN;
                pr.SeriePrimaNotaMexal = _seriePN;
                pr.ProgressivoPrimaNotaMexal = _numRegPN;
                pr.DataPrimaNotaMexal = _dataPN;

                //dc.Pratiche.InsertOnSubmit(pr);

                dc.SubmitChanges();

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public Boolean SetEsito(string _pratica, string _numFT, string _serieFT, string _dataFT, string _numNC, string _serieNC, string _dataNC)
        {
            try
            {
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                Database.Pratiche pr = dc.Pratiche.Where(p => p.CodicePratica == _pratica).First();

                pr.CodicePratica = _pratica;
                pr.NumeroFatturaMexal = _numFT;
                pr.SerieFatturaMexal = _serieFT;
                pr.DataFatturaMexal = _dataFT;
                pr.NumeroNotaCreditoMexal = _numNC;
                pr.SerieNotaCreditoMexal = _serieNC;
                pr.DataNotaCreditoMexal = _dataNC;

                //dc.Pratiche.InsertOnSubmit(pr);

                dc.SubmitChanges();

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<string> GetOrdine (string _pratica)
        {
            List<string> res = new List<string>();
            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
            Database.Pratiche pr = dc.Pratiche.Where(p => p.CodicePratica == _pratica).First();

            res.Add(pr.NumeroOrdineMexal);
            res.Add(pr.SerieOrdineMexal);
            res.Add(pr.DataOrdineMexal);

            return res;
        }

        public List<string> GetPrimaNota(string _pratica)
        {
            List<string> res = new List<string>();
            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
            Database.Pratiche pr = dc.Pratiche.Where(p => p.CodicePratica == _pratica).First();

            res.Add(pr.NumeroPrimaNotaMexal);
            res.Add(pr.SeriePrimaNotaMexal);
            res.Add(pr.DataPrimaNotaMexal);
            res.Add(pr.ProgressivoPrimaNotaMexal);

            return res;
        }

        public string GetAnagrafica(string _codiceEsterno)
        {
            Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
            string res = dc.Anagrafiche.Where(a => a.CodiceEsterno == _codiceEsterno).Select(a=>a.CodiceMexal).First();
            return res;
        }

        public Boolean SetAnagrafica(string _codiceEsterno, string _codiceMexal, bool _isMedico)
        {
            try
            {
                Database.LookupDatabaseDataContext dc = new Database.LookupDatabaseDataContext();
                Database.Anagrafiche an = new Database.Anagrafiche();
                an.CodiceEsterno = _codiceEsterno;
                an.CodiceMexal = _codiceMexal;
                an.IsMedico = _isMedico;

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
