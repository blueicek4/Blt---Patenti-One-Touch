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
        public Int32 SottoAzienda { get; set; }
        public MexalVisita Visita { get; set; }
        public MexalAnagrafica Cliente { get; set; }
        public MexalDocumentoTestata Documento { get; set; }

        public MexalClient()
        {
            this.Porta = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["MexalPort"]);
            this.Indirizzo = System.Configuration.ConfigurationManager.AppSettings["MexalServer"];
            this.WindowsUsername = System.Configuration.ConfigurationManager.AppSettings["MexalWindowsUsername"];
            this.WindowsPassword = System.Configuration.ConfigurationManager.AppSettings["MexalWindowsPassword"];
            this.MexalUsername = System.Configuration.ConfigurationManager.AppSettings["MexalUsername"];
            this.MexalPassword = System.Configuration.ConfigurationManager.AppSettings["MexalPassword"];
            this.Terminale = 0;
            this.SottoAzienda = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["MexalSottoAzienda"]);
            this.DataTerminale = DateTime.Now.Date;
            this.Azienda = System.Configuration.ConfigurationManager.AppSettings["MexalAzienda"];
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
            //vedere se sottoazienda da usare
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

        public Boolean SetAnagrafica(MexalAnagrafica _anagrafica, out string message, Boolean _isMedico)
        {
            if (this.client.ERRORE == "Errore: il server SHAKER potrebbe non essere in esecuzione. Eseguire la riconnessione")
                this.client.AVVIACONNESSIONE();

            Pot.DataLayer.GestioneLookUp lkp = new Pot.DataLayer.GestioneLookUp();

            this.ResetVariabili(1);
            var result = lkp.GetAnagrafica(_anagrafica.CodiceRiferimento);
            if (!String.IsNullOrWhiteSpace(result))
                this.client.GETPC(result);

            else if (String.IsNullOrWhiteSpace(_anagrafica.CodiceCliente))
                this.client.PCCOD_S = _anagrafica.Mastro + ".AUTO";
            else
                this.client.PCCOD_S = _anagrafica.Mastro + "." + _anagrafica.CodiceCliente;
            this.client.PCCAL_S = _anagrafica.CodiceRiferimento;
            this.client.PCDES_S = _anagrafica.Descrizione;
            this.client.PCNOM_S = _anagrafica.Nome;
            this.client.PCCOG_S = _anagrafica.Cognome;
            if (_anagrafica.DataNascita != null)
                this.client.PCDTNASC_S = Convert.ToDateTime(_anagrafica.DataNascita).ToString("ggMMyyyy");
            //if (_anagrafica.Nazionalita != null)
                this.client.PCNAZ_S = "I";//_anagrafica.Nazionalita;
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
            this.client.PCINT1_S = _anagrafica.Email;
            this.client.PCINT_S= _anagrafica.Web;
            this.client.PCIND_S = _anagrafica.Indirizzo;
            this.client.PCLOC_S = _anagrafica.Localita;
            this.client.PCCAP_S = _anagrafica.Cap;
            this.client.PCPAE_S = _anagrafica.Stato;
            this.client.PCPRO_S = _anagrafica.Provincia;

            if (!String.IsNullOrWhiteSpace(_anagrafica.Iban))
            {
                //IBAN
                this.client.PCCCB_S = _anagrafica.Iban.Substring(15);
                this.client.PCBBCIN_S = _anagrafica.Iban.Substring(4, 1);
                this.client.PCABI = Double.Parse(_anagrafica.Iban.Substring(5, 5));
                this.client.PCCAB = Double.Parse(_anagrafica.Iban.Substring(10, 5));

                this.client.PCIBPAE_S = _anagrafica.Iban.Substring(0, 2);
                this.client.PCIBCIN = Double.Parse(_anagrafica.Iban.Substring(2, 2));
                this.client.PCIBAN_S = _anagrafica.Iban;
                this.client.PCBBAN_S = _anagrafica.Iban.Substring(4);
            }
           //STANDARD AZIENDALI
            this.client.PCLIS = _anagrafica.Listino;
            this.client.PCVAL = 1;

            this.client.PUTPC();
            if (String.IsNullOrWhiteSpace(this.client.ERRPC_S))
            {
                //registrazione mydb per codiceriferimento
                if (lkp.SetAnagrafica(_anagrafica.CodiceRiferimento, this.client.PCCOD_S, _isMedico))
                {


                    message = this.client.PCCOD_S;

                    return true;
                }
                else
                {
                    message = "Errore Registrazione Tabella Lookup";
                    return false;
                }
            }
            else
            {
                message = this.client.ERRPC_S;
                return false;
            }

        }

        public Boolean SetOrdine(MexalDocumentoTestata _documento, string _medico, decimal _importoMedico, out string message)
        {
            Pot.DataLayer.GestioneLookUp lkp = new Pot.DataLayer.GestioneLookUp();
            if (!String.IsNullOrWhiteSpace(lkp.GetCodicePratica(_documento.RiferimentoEsterno)))
            {
                message = "Pratica già Esistente!";
                return false;
            }

            if (this.client.ERRORE == "Errore: il server SHAKER potrebbe non essere in esecuzione. Eseguire la riconnessione")
                this.client.AVVIACONNESSIONE();
            string numFatt = String.Empty;
            string dataFatt = String.Empty;
            string serieFatt = String.Empty;
            string numPag = String.Empty;

            //CERCO CODICE CLIENTE SU DATABASE
            this.client.MYDBK_S[2] = _documento.Controparte;
            
            //manca la funzione per leggere 

            //CREO DOCUMENTO "FT" ORDINE CLIENTE SU MEXAL
            this.ResetVariabili(4);
            this.client.MMSAZ = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["MexalSottoAzienda"]);
            this.client.MMSIG_S = _documento.Sigla;
            this.client.MMSER = Double.Parse(_documento.Serie);
            this.client.MMNUM = _documento.Numero;
            this.client.MMDAT_S = _documento.Data.ToString("yyyyMMdd");
            this.client.MMMAG = _documento.Magazzino;
            this.client.MMCLI_S = _documento.Controparte;
            //this.client.MMPAG = _documento.Pagamento;
            //this.client.MMBAPP = _documento.Banca;
            this.client.MMNUMRE_S = _documento.RiferimentoEsterno;
            this.client.MMDATRE_S = _documento.Data.ToString("yyyyMMdd");
            this.client.MMCCR = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);

            for (int r = 0; r < _documento.Righe.Count; r++)
            {
                this.client.MMTPR_S[r + 1] = _documento.Righe[r].TipoRiga;
                this.client.MMART_S[r + 1] = _documento.Righe[r].CodiceArticolo;
                this.client.MMDES_S[r + 1] = _documento.Righe[r].DescrizioneRiga;
                this.client.MMQTA[r + 1] = Double.Parse(_documento.Righe[r].Quantita.ToString());
                this.client.MMALI_S[r + 1] = _documento.Righe[r].Iva;
                this.client.MMPRZ[r + 1] = Double.Parse(_documento.Righe[r].Prezzo.ToString());
                if (_documento.Righe[r].CodiceArticolo == "A-SP0001" && _documento.Sconto != 0)
                {
                    this.client.MMSCO_S[r + 1] = "-" + _documento.Sconto.ToString();
                }
            }
            
            this.client.PUTMM(1);

            numFatt = this.client.MMNUM.ToString();
            serieFatt = this.client.MMSER.ToString();
            dataFatt = this.client.MMDAT_S;

            //SE VA A BUON FINE ALLORA PROSEGUO ELABORAZIONE
            if (String.IsNullOrWhiteSpace(this.client.ERRMM_S))
            {
                //CREO GIROCONTO SU MEDICO PER APRIRE IL "DEBITO" VERSO DI ESSO
                this.ResetVariabili(2);

                this.client.PNDRE_S = _documento.Data.ToString("yyyyMMdd");
                this.client.PNCAU_S = System.Configuration.ConfigurationManager.AppSettings["CausalePNAperturaMedico"];
                this.client.PNNDO = lkp.GetProgressivo("GrAnticipoMedici");
                this.client.PNDDO_S = DateTime.Now.ToString("yyyyMMdd");
                this.client.PNTDE_S = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneAperturaPraticaTestata"] + _documento.RiferimentoEsterno;
                this.client.PNCTO_S[1] = _documento.Controparte;
                this.client.PNDES_S[1] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneAperturaPraticaRiga"] + _documento.RiferimentoEsterno;
                this.client.PNIMP[1] = double.Parse(        _importoMedico.ToString());
                this.client.PNCCR[1] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);
                this.client.PNCTO_S[2] = System.Configuration.ConfigurationManager.AppSettings["ContoDepositoMedici"];
                this.client.PNDES_S[2] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneAperturaPraticaRiga"] + _documento.RiferimentoEsterno;
                this.client.PNIMP[2] = double.Parse((0 - _importoMedico).ToString());
                this.client.PNCCR[2] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);
                                                
                this.client.PUTPN();

                //registro pagamento del cliente tra merci e conto anticipi
                this.ResetVariabili(2);


                this.client.PNSCT_S[1] = _documento.Controparte;
                this.client.PNSDS_S[1] = _documento.Data.ToString("yyyyMMdd");
                this.client.PNSTP_S[1] = "M";
                this.client.PNNDO = lkp.GetProgressivo("PgIncassoCliente");
                this.client.PNDDO_S = DateTime.Now.ToString("yyyyMMdd");
                this.client.PNCAU_S = System.Configuration.ConfigurationManager.AppSettings["CausalePNPagamentoCliente"];
                this.client.PNDRE_S = _documento.Data.ToString("yyyyMMdd");
                this.client.PNRPR = 2;
                this.client.PNDDO_S = _documento.Data.ToString("yyyyMMdd");
                this.client.PNCTO_S[1] = _documento.Controparte;
                this.client.PNDES_S[1] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneIncassoClienteTestata"] + _documento.RiferimentoEsterno;
                this.client.PNIMP[1] = double.Parse((0 - _documento.TotaleDocumento).ToString());
                this.client.PNCTO_S[2] = System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoContanti"];
                this.client.PNDES_S[2] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneIncassoClienteRiga"] + _documento.RiferimentoEsterno;
                this.client.PNIMP[2] = double.Parse((_documento.TotaleDocumento).ToString());

                this.client.PUTPN();


                numPag = this.client.PNDDO_S.ToString();

                if (String.IsNullOrWhiteSpace(this.client.ERRPN_S))
                {
                    if (lkp.SetPratica(_documento.RiferimentoEsterno, _documento.Controparte, _medico, _documento.Pagamento, int.Parse(_documento.Sconto.ToString()), int.Parse(_importoMedico.ToString()), int.Parse(_documento.TotaleDocumento.ToString()), numFatt + "/" + serieFatt, dataFatt, numPag))
                    {
                        message = "Ok!";
                        return true;
                    }
                    else
                    {
                        message = "Ordine Salvato con Errore Registrazione Tabella Lookup";
                        return false;
                    }
                }
                else
                {
                    message = this.client.ERRPN_S;
                    return false;
                }

                

            }
            else
            {
                message = this.client.ERRMM_S;
                return false;
            }

        }

        public Boolean SetVisita(MexalVisita _visita, out string message)
        {
            if (this.client.ERRORE == "Errore: il server SHAKER potrebbe non essere in esecuzione. Eseguire la riconnessione")
                this.client.AVVIACONNESSIONE();

            Pot.DataLayer.GestioneLookUp lkp = new Pot.DataLayer.GestioneLookUp();
            if (String.IsNullOrWhiteSpace(lkp.GetCodicePratica(_visita.CodicePratica)))
            {
                message = "Pratica non Esistente!";
                return false;
            }


            message = String.Empty;
            try
            {
                string numFT = String.Empty;
                string serieFT = String.Empty;
                string dataFT = String.Empty;
                string numNC = String.Empty;
                string serieNC = String.Empty;
                string dataNC = String.Empty;

                switch (_visita.Esito)
                {
                    case "OK":

                        this.client.AZZVARSYS(2);

                        this.client.PNDRE_S = DateTime.Now.ToString("yyyyMMdd");
                        this.client.PNCAU_S = System.Configuration.ConfigurationManager.AppSettings["CausalePNAperturaMedico"];
                        this.client.PNNDO = lkp.GetProgressivo("GrAntDebMedici");
                        this.client.PNDDO_S = DateTime.Now.ToString("yyyyMMdd");
                        this.client.PNTDE_S = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizionePagamentoClienteMedico"] + _visita.CodicePratica;
                        this.client.PNCTO_S[1] = System.Configuration.ConfigurationManager.AppSettings["ContoDepositoMedici"];
                        this.client.PNDES_S[1] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizionePagamentoClienteMedico"] + _visita.CodicePratica;
                        this.client.PNIMP[1] = double.Parse(20.ToString());
                        this.client.PNCCR[1] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);
                        this.client.PNCTO_S[2] = System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoMedici"];
                        this.client.PNDES_S[2] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizionePagamentoClienteMedico"] + _visita.CodicePratica;
                        this.client.PNIMP[2] = double.Parse((0 - 20).ToString());
                        this.client.PNCCR[2] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);

                        this.client.PUTPN();

                        this.ResetVariabili(2);


                        this.client.PNDRE_S = DateTime.Now.ToString("yyyyMMdd");
                        this.client.PNCAU_S = System.Configuration.ConfigurationManager.AppSettings["CausalePNAperturaMedico"];
                        this.client.PNNDO = lkp.GetProgressivo("GrDebitoMedico");
                        this.client.PNDDO_S = DateTime.Now.ToString("yyyyMMdd");
                        //this.client.PNSTP_S[1] = "B";
                        this.client.PNPAG = 12;
                        this.client.PNTDE_S = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneGrAnticipoDebitoMedici"] + _visita.CodicePratica;
                        this.client.PNCTO_S[1] = System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoMedici"];
                        this.client.PNDES_S[1] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneGrAnticipoDebitoMedici"] + _visita.CodicePratica;
                        this.client.PNIMP[1] = double.Parse(20.ToString());
                        this.client.PNCCR[1] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);
                        this.client.PNCTO_S[2] = lkp.GetAnagrafica(_visita.CodiceMedico);
                        this.client.PNDES_S[2] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneGrAnticipoDebitoMedici"] + _visita.CodicePratica;
                        this.client.PNIMP[2] = double.Parse((0 - 20).ToString());
                        this.client.PNCCR[2] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);

                        this.client.PUTPN();

                        lkp.SetEsito(_visita.CodicePratica, _visita.Esito, _visita.CodiceMedico, this.client.PNNDO.ToString(), DateTime.Now.ToString("yyyyMMdd"), null, null, null, null);
                        break;
                    case "KO":
                        //visita non passata, pago medico
                        string numPag = String.Empty;

                        this.client.AZZVARSYS(2);

                        this.client.PNDRE_S = DateTime.Now.ToString("yyyyMMdd");
                        this.client.PNCAU_S = System.Configuration.ConfigurationManager.AppSettings["CausalePNAperturaMedico"];
                        this.client.PNNDO = lkp.GetProgressivo("GrAntDebMedici");
                        this.client.PNDDO_S = DateTime.Now.ToString("yyyyMMdd");
                        this.client.PNTDE_S = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizionePagamentoClienteMedico"] + _visita.CodicePratica;
                        this.client.PNCTO_S[1] = System.Configuration.ConfigurationManager.AppSettings["ContoDepositoMedici"];
                        this.client.PNDES_S[1] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizionePagamentoClienteMedico"] + _visita.CodicePratica;
                        this.client.PNIMP[1] = double.Parse(20.ToString());
                        this.client.PNCCR[1] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);
                        this.client.PNCTO_S[2] = System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoMedici"];
                        this.client.PNDES_S[2] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizionePagamentoClienteMedico"] + _visita.CodicePratica;
                        this.client.PNIMP[2] = double.Parse((0 - 20).ToString());
                        this.client.PNCCR[2] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);

                        this.client.PUTPN();

                        this.ResetVariabili(2);


                        this.client.PNDRE_S = DateTime.Now.ToString("yyyyMMdd");
                        this.client.PNCAU_S = System.Configuration.ConfigurationManager.AppSettings["CausalePNAperturaMedico"];
                        this.client.PNNDO = lkp.GetProgressivo("GrDebitoMedico");
                        this.client.PNDDO_S = DateTime.Now.ToString("yyyyMMdd");
                        //this.client.PNSTP_S[1] = "B";
                        this.client.PNPAG = 12;
                        this.client.PNTDE_S = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneGrAnticipoDebitoMedici"] + _visita.CodicePratica;
                        this.client.PNCTO_S[1] = System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoMedici"];
                        this.client.PNDES_S[1] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneGrAnticipoDebitoMedici"] + _visita.CodicePratica;
                        this.client.PNIMP[1] = double.Parse(20.ToString());
                        this.client.PNCCR[1] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);
                        this.client.PNCTO_S[2] = lkp.GetAnagrafica(_visita.CodiceMedico);
                        this.client.PNDES_S[2] = System.Configuration.ConfigurationManager.AppSettings["PrefissoDescrizioneGrAnticipoDebitoMedici"] + _visita.CodicePratica;
                        this.client.PNIMP[2] = double.Parse((0 - 20).ToString());
                        this.client.PNCCR[2] = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["CentrodiCosto"]);

                        this.client.PUTPN();

                        numPag = this.client.PNNDO.ToString();

                        //emetto nota credito
                        var pratica = lkp.GetPratica(_visita.CodicePratica);
                        string ordine = System.Configuration.ConfigurationManager.AppSettings["TipoDocAperturaPratica"] + pratica.NumeroFattura.Substring(pratica.NumeroFattura.IndexOf('/')+1) + "/" + pratica.NumeroFattura.Substring(0,pratica.NumeroFattura.IndexOf('/'));
                        this.client.GETMM_EXT(ordine);

                        this.client.MMSIG_S = "NC";
                        this.client.MMSER = Double.Parse(System.Configuration.ConfigurationManager.AppSettings["Sezionale"]);
                        this.client.MMNUM = 0;
                        this.client.MMDAT_S = DateTime.Now.ToString("yyyyMMdd");
                        Double Pagamento = 0;
                        switch(pratica.TipoPagamento)
                        {
                            case 1:
                                Pagamento = 1;
                                break;
                            case 4:
                                Pagamento = 12;
                                break;
                            default:
                                break;
                        }
                        this.client.MMPAG = Pagamento;

                        for (int r = 1; r <= this.client.NMM; r++)
                        {
                            if (!new List<string>() { "A-SP0002", "A-SP0003", "A-SP0004" }.Any(s => s == this.client.MMART_S[r]))
                            {
                                this.client.MMTPR_S[r] = string.Empty;
                                this.client.MMART_S[r] = string.Empty;
                                this.client.MMDES_S[r] = string.Empty;
                                this.client.MMQTA[r] = 0;
                                this.client.MMALI_S[r] = string.Empty;
                                this.client.MMPRZ[r] = 0;
                            }
                        }
                        this.client.PUTMM(1);
                        numNC = this.client.MMNUM.ToString();
                        serieNC = this.client.MMSER.ToString();
                        dataNC = this.client.MMDAT_S;

                        if (!String.IsNullOrWhiteSpace(this.client.ERRMM_S))
                        {
                            message = this.client.ERRMM_S;
                            return false;
                        }

                        lkp.SetEsito(_visita.CodicePratica, _visita.Esito, _visita.CodiceMedico, numPag, DateTime.Now.ToString("yyyyMMdd"), numNC, dataNC, null, null);
                        break;

                    case "MANCATA":

                        lkp.SetEsito(_visita.CodicePratica, _visita.Esito, _visita.CodiceMedico, "0", DateTime.Now.ToString("yyyyMMdd"), numNC, dataNC, "0", "0");
                        message = "ANCORA NON GESTITO";
                        return false;

                        break;
                    default:
                        break;


                }

                return true;

            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }


        public string ContoPagamento(string _conto)
        {
            switch(_conto)
            {
                case "1":
                    return System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoContanti"];
                case "4":
                    return System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoCarta"];
                case "3":
                    return System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoPaypal"];
                default:
                    return System.Configuration.ConfigurationManager.AppSettings["ContoPagamentoDefault"];
            }
        }

    public void ResetVariabili(Int32 _struttura)
        {
            this.client.AZZVARSYS(_struttura);
        }

        public string CercaCliente(string _codiceEsterno)
        {
            this.client.GETPC(_codiceEsterno);
            return this.client.PCCOD_S;
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
        public DateTime? DataNascita { get; set; }
        public string CodiceRiferimento { get; set; }
        public string Iban { get; set; }

        public MexalAnagrafica()
        {

        }

        public void LoadFromCliente(Bluetech.Pot.DataLayer.Cliente _cliente)
        {
            this.Nome = _cliente.Nome;
            this.Cognome = _cliente.Cognome;
            this.CodiceRiferimento =  _cliente.CodiceUnivoco;
            this.CodiceFiscale = _cliente.CodiceFiscale;
            if (_cliente.DataNascita != null)
                this.DataNascita = _cliente.DataNascita.Value;// .ToString("ggMMyyyy");
            this.Indirizzo = _cliente.Indirizzo;
            this.Localita = _cliente.Comune;
            this.Cap = _cliente.Cap;
            this.Stato = _cliente.Nazione;
            this.Nazionalita = _cliente.Nazionalita;
            this.Iban = _cliente.Iban;
        }

        public void LoadFromMedico(Bluetech.Pot.DataLayer.Medico _medico)
        {
            this.Nome = _medico.Nome;
            this.Cognome = _medico.Cognome;
            this.CodiceRiferimento = _medico.CodiceUnivoco;
            this.CodiceFiscale = _medico.CodiceFiscale;
            this.PartitaIva = _medico.PartitaIva;
            if (_medico.DataNascita != null)
                this.DataNascita = _medico.DataNascita.Value;// .ToString("ggMMyyyy");
            this.Indirizzo = _medico.Indirizzo;
            this.Localita = _medico.Comune;
            this.Cap = _medico.Cap;
            this.Stato = _medico.Nazione;
            this.Nazionalita = _medico.Nazionalita;
            this.Iban = _medico.Iban;
        }

    }

    public class MexalDocumentoTestata
    {
        public string Sigla { get; set; }
        public string Serie { get; set; }
        public Int32 Numero { get; set; }
        public DateTime Data { get; set; }
        public Int32 Magazzino { get; set; }
        public string Controparte { get; set; }
        public string RiferimentoEsterno { get; set; }

        //PIEDE
        public decimal TotaleDocumento { get; set; }
        public decimal TotaleImponibile { get; set; }
        public decimal TotaleImposta { get; set; }
        public Int32 Pagamento { get; set; }
        public Int32 Banca { get; set; }

        public decimal Sconto { get; set; }

        public List<MexalDocumentoRiga> Righe { get; set; }

        public void LoadFromOrdine(Bluetech.Pot.DataLayer.OrdineTestata _ordine)
        {
            this.Serie = _ordine.SerieOrdine;
            this.Numero = Int32.Parse(_ordine.ProgressivoOrdine);
            this.Data = (DateTime)_ordine.DataOrdine;
            this.Controparte = _ordine.CodiceUnivocoControparte;
            this.TotaleImponibile = _ordine.TotaleImponibile;
            this.TotaleImposta = _ordine.TotaleImposte;
            this.TotaleDocumento = _ordine.TotaleFattura;
            this.Pagamento = _ordine.TipoPagamento;
            this.Banca = _ordine.BancaPagamento;
            this.RiferimentoEsterno = _ordine.CodicePratica;
            this.Sconto = _ordine.ImportoSconto;

            List<MexalDocumentoRiga> righe = new List<MexalDocumentoRiga>();
            foreach(var r in _ordine.Righe)
            {
                MexalDocumentoRiga ri = new MexalDocumentoRiga();
                ri.TipoRiga = "R";
                ri.CodiceArticolo = r.CodiceArticolo;
                ri.DescrizioneRiga = r.DescrizioneRiga;
                ri.Quantita = r.Qta;
                ri.Iva = r.AliquotaIva;
                ri.Prezzo = r.PrezzoUnitario;

                righe.Add(ri);
            }

            this.Righe = righe;
        }
    }

    public class MexalDocumentoRiga
    {
        public string TipoRiga { get; set; }
        public string CodiceArticolo { get; set; }
        public decimal Quantita { get; set; }
        public string Iva { get; set; }
        public string DescrizioneRiga { get; set; }
        public decimal Prezzo { get; set; }

    }

    public class MexalVisita
    {
        public string CodicePratica { get; set; }
        public string CodiceMedico { get; set; }
        public string Esito { get; set; }

        public void LoadFromEsitoVisita( Bluetech.Pot.DataLayer.EsitoVisita _esito)
        {
            this.CodicePratica = _esito.CodicePratica;
            this.CodiceMedico = _esito.CodiceUnivocoMedico;
            this.Esito = _esito.Esito;
        }
    }
}
