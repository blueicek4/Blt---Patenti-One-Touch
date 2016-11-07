﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bluetech.Database
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="PatentiLookUp")]
	public partial class LookupDatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Definizioni metodo Extensibility
    partial void OnCreated();
    partial void InsertPratiche(Pratiche instance);
    partial void UpdatePratiche(Pratiche instance);
    partial void DeletePratiche(Pratiche instance);
    partial void InsertAnagrafiche(Anagrafiche instance);
    partial void UpdateAnagrafiche(Anagrafiche instance);
    partial void DeleteAnagrafiche(Anagrafiche instance);
    #endregion
		
		public LookupDatabaseDataContext() : 
				base(global::Pot.DataLayer.Properties.Settings.Default.PatentiLookUpConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public LookupDatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LookupDatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LookupDatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LookupDatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Pratiche> Pratiche
		{
			get
			{
				return this.GetTable<Pratiche>();
			}
		}
		
		public System.Data.Linq.Table<Anagrafiche> Anagrafiche
		{
			get
			{
				return this.GetTable<Anagrafiche>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Pratiche")]
	public partial class Pratiche : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _CodicePratica;
		
		private string _NumeroOrdineMexal;
		
		private string _SerieOrdineMexal;
		
		private string _DataOrdineMexal;
		
		private string _NumeroClienteMexal;
		
		private string _NumeroMedicoMexal;
		
		private string _ProgressivoPrimaNotaMexal;
		
		private string _NumeroPrimaNotaMexal;
		
		private string _SeriePrimaNotaMexal;
		
		private string _DataPrimaNotaMexal;
		
		private string _NumeroFatturaMexal;
		
		private string _SerieFatturaMexal;
		
		private string _DataFatturaMexal;
		
		private string _NumeroNotaCreditoMexal;
		
		private string _SerieNotaCreditoMexal;
		
		private string _DataNotaCreditoMexal;
		
    #region Definizioni metodo Extensibility
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCodicePraticaChanging(string value);
    partial void OnCodicePraticaChanged();
    partial void OnNumeroOrdineMexalChanging(string value);
    partial void OnNumeroOrdineMexalChanged();
    partial void OnSerieOrdineMexalChanging(string value);
    partial void OnSerieOrdineMexalChanged();
    partial void OnDataOrdineMexalChanging(string value);
    partial void OnDataOrdineMexalChanged();
    partial void OnNumeroClienteMexalChanging(string value);
    partial void OnNumeroClienteMexalChanged();
    partial void OnNumeroMedicoMexalChanging(string value);
    partial void OnNumeroMedicoMexalChanged();
    partial void OnProgressivoPrimaNotaMexalChanging(string value);
    partial void OnProgressivoPrimaNotaMexalChanged();
    partial void OnNumeroPrimaNotaMexalChanging(string value);
    partial void OnNumeroPrimaNotaMexalChanged();
    partial void OnSeriePrimaNotaMexalChanging(string value);
    partial void OnSeriePrimaNotaMexalChanged();
    partial void OnDataPrimaNotaMexalChanging(string value);
    partial void OnDataPrimaNotaMexalChanged();
    partial void OnNumeroFatturaMexalChanging(string value);
    partial void OnNumeroFatturaMexalChanged();
    partial void OnSerieFatturaMexalChanging(string value);
    partial void OnSerieFatturaMexalChanged();
    partial void OnDataFatturaMexalChanging(string value);
    partial void OnDataFatturaMexalChanged();
    partial void OnNumeroNotaCreditoMexalChanging(string value);
    partial void OnNumeroNotaCreditoMexalChanged();
    partial void OnSerieNotaCreditoMexalChanging(string value);
    partial void OnSerieNotaCreditoMexalChanged();
    partial void OnDataNotaCreditoMexalChanging(string value);
    partial void OnDataNotaCreditoMexalChanged();
    #endregion
		
		public Pratiche()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodicePratica", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string CodicePratica
		{
			get
			{
				return this._CodicePratica;
			}
			set
			{
				if ((this._CodicePratica != value))
				{
					this.OnCodicePraticaChanging(value);
					this.SendPropertyChanging();
					this._CodicePratica = value;
					this.SendPropertyChanged("CodicePratica");
					this.OnCodicePraticaChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroOrdineMexal", DbType="VarChar(50)")]
		public string NumeroOrdineMexal
		{
			get
			{
				return this._NumeroOrdineMexal;
			}
			set
			{
				if ((this._NumeroOrdineMexal != value))
				{
					this.OnNumeroOrdineMexalChanging(value);
					this.SendPropertyChanging();
					this._NumeroOrdineMexal = value;
					this.SendPropertyChanged("NumeroOrdineMexal");
					this.OnNumeroOrdineMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SerieOrdineMexal", DbType="VarChar(50)")]
		public string SerieOrdineMexal
		{
			get
			{
				return this._SerieOrdineMexal;
			}
			set
			{
				if ((this._SerieOrdineMexal != value))
				{
					this.OnSerieOrdineMexalChanging(value);
					this.SendPropertyChanging();
					this._SerieOrdineMexal = value;
					this.SendPropertyChanged("SerieOrdineMexal");
					this.OnSerieOrdineMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DataOrdineMexal", DbType="VarChar(50)")]
		public string DataOrdineMexal
		{
			get
			{
				return this._DataOrdineMexal;
			}
			set
			{
				if ((this._DataOrdineMexal != value))
				{
					this.OnDataOrdineMexalChanging(value);
					this.SendPropertyChanging();
					this._DataOrdineMexal = value;
					this.SendPropertyChanged("DataOrdineMexal");
					this.OnDataOrdineMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroClienteMexal", DbType="VarChar(50)")]
		public string NumeroClienteMexal
		{
			get
			{
				return this._NumeroClienteMexal;
			}
			set
			{
				if ((this._NumeroClienteMexal != value))
				{
					this.OnNumeroClienteMexalChanging(value);
					this.SendPropertyChanging();
					this._NumeroClienteMexal = value;
					this.SendPropertyChanged("NumeroClienteMexal");
					this.OnNumeroClienteMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroMedicoMexal", DbType="VarChar(50)")]
		public string NumeroMedicoMexal
		{
			get
			{
				return this._NumeroMedicoMexal;
			}
			set
			{
				if ((this._NumeroMedicoMexal != value))
				{
					this.OnNumeroMedicoMexalChanging(value);
					this.SendPropertyChanging();
					this._NumeroMedicoMexal = value;
					this.SendPropertyChanged("NumeroMedicoMexal");
					this.OnNumeroMedicoMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ProgressivoPrimaNotaMexal", DbType="VarChar(50)")]
		public string ProgressivoPrimaNotaMexal
		{
			get
			{
				return this._ProgressivoPrimaNotaMexal;
			}
			set
			{
				if ((this._ProgressivoPrimaNotaMexal != value))
				{
					this.OnProgressivoPrimaNotaMexalChanging(value);
					this.SendPropertyChanging();
					this._ProgressivoPrimaNotaMexal = value;
					this.SendPropertyChanged("ProgressivoPrimaNotaMexal");
					this.OnProgressivoPrimaNotaMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroPrimaNotaMexal", DbType="VarChar(50)")]
		public string NumeroPrimaNotaMexal
		{
			get
			{
				return this._NumeroPrimaNotaMexal;
			}
			set
			{
				if ((this._NumeroPrimaNotaMexal != value))
				{
					this.OnNumeroPrimaNotaMexalChanging(value);
					this.SendPropertyChanging();
					this._NumeroPrimaNotaMexal = value;
					this.SendPropertyChanged("NumeroPrimaNotaMexal");
					this.OnNumeroPrimaNotaMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SeriePrimaNotaMexal", DbType="VarChar(50)")]
		public string SeriePrimaNotaMexal
		{
			get
			{
				return this._SeriePrimaNotaMexal;
			}
			set
			{
				if ((this._SeriePrimaNotaMexal != value))
				{
					this.OnSeriePrimaNotaMexalChanging(value);
					this.SendPropertyChanging();
					this._SeriePrimaNotaMexal = value;
					this.SendPropertyChanged("SeriePrimaNotaMexal");
					this.OnSeriePrimaNotaMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DataPrimaNotaMexal", DbType="VarChar(50)")]
		public string DataPrimaNotaMexal
		{
			get
			{
				return this._DataPrimaNotaMexal;
			}
			set
			{
				if ((this._DataPrimaNotaMexal != value))
				{
					this.OnDataPrimaNotaMexalChanging(value);
					this.SendPropertyChanging();
					this._DataPrimaNotaMexal = value;
					this.SendPropertyChanged("DataPrimaNotaMexal");
					this.OnDataPrimaNotaMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroFatturaMexal", DbType="VarChar(50)")]
		public string NumeroFatturaMexal
		{
			get
			{
				return this._NumeroFatturaMexal;
			}
			set
			{
				if ((this._NumeroFatturaMexal != value))
				{
					this.OnNumeroFatturaMexalChanging(value);
					this.SendPropertyChanging();
					this._NumeroFatturaMexal = value;
					this.SendPropertyChanged("NumeroFatturaMexal");
					this.OnNumeroFatturaMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SerieFatturaMexal", DbType="VarChar(50)")]
		public string SerieFatturaMexal
		{
			get
			{
				return this._SerieFatturaMexal;
			}
			set
			{
				if ((this._SerieFatturaMexal != value))
				{
					this.OnSerieFatturaMexalChanging(value);
					this.SendPropertyChanging();
					this._SerieFatturaMexal = value;
					this.SendPropertyChanged("SerieFatturaMexal");
					this.OnSerieFatturaMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DataFatturaMexal", DbType="VarChar(50)")]
		public string DataFatturaMexal
		{
			get
			{
				return this._DataFatturaMexal;
			}
			set
			{
				if ((this._DataFatturaMexal != value))
				{
					this.OnDataFatturaMexalChanging(value);
					this.SendPropertyChanging();
					this._DataFatturaMexal = value;
					this.SendPropertyChanged("DataFatturaMexal");
					this.OnDataFatturaMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NumeroNotaCreditoMexal", DbType="VarChar(50)")]
		public string NumeroNotaCreditoMexal
		{
			get
			{
				return this._NumeroNotaCreditoMexal;
			}
			set
			{
				if ((this._NumeroNotaCreditoMexal != value))
				{
					this.OnNumeroNotaCreditoMexalChanging(value);
					this.SendPropertyChanging();
					this._NumeroNotaCreditoMexal = value;
					this.SendPropertyChanged("NumeroNotaCreditoMexal");
					this.OnNumeroNotaCreditoMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SerieNotaCreditoMexal", DbType="VarChar(50)")]
		public string SerieNotaCreditoMexal
		{
			get
			{
				return this._SerieNotaCreditoMexal;
			}
			set
			{
				if ((this._SerieNotaCreditoMexal != value))
				{
					this.OnSerieNotaCreditoMexalChanging(value);
					this.SendPropertyChanging();
					this._SerieNotaCreditoMexal = value;
					this.SendPropertyChanged("SerieNotaCreditoMexal");
					this.OnSerieNotaCreditoMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DataNotaCreditoMexal", DbType="VarChar(50)")]
		public string DataNotaCreditoMexal
		{
			get
			{
				return this._DataNotaCreditoMexal;
			}
			set
			{
				if ((this._DataNotaCreditoMexal != value))
				{
					this.OnDataNotaCreditoMexalChanging(value);
					this.SendPropertyChanging();
					this._DataNotaCreditoMexal = value;
					this.SendPropertyChanged("DataNotaCreditoMexal");
					this.OnDataNotaCreditoMexalChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Anagrafiche")]
	public partial class Anagrafiche : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _CodiceEsterno;
		
		private string _CodiceMexal;
		
		private System.Nullable<bool> _IsMedico;
		
    #region Definizioni metodo Extensibility
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCodiceEsternoChanging(string value);
    partial void OnCodiceEsternoChanged();
    partial void OnCodiceMexalChanging(string value);
    partial void OnCodiceMexalChanged();
    partial void OnIsMedicoChanging(System.Nullable<bool> value);
    partial void OnIsMedicoChanged();
    #endregion
		
		public Anagrafiche()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodiceEsterno", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string CodiceEsterno
		{
			get
			{
				return this._CodiceEsterno;
			}
			set
			{
				if ((this._CodiceEsterno != value))
				{
					this.OnCodiceEsternoChanging(value);
					this.SendPropertyChanging();
					this._CodiceEsterno = value;
					this.SendPropertyChanged("CodiceEsterno");
					this.OnCodiceEsternoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CodiceMexal", DbType="VarChar(50)")]
		public string CodiceMexal
		{
			get
			{
				return this._CodiceMexal;
			}
			set
			{
				if ((this._CodiceMexal != value))
				{
					this.OnCodiceMexalChanging(value);
					this.SendPropertyChanging();
					this._CodiceMexal = value;
					this.SendPropertyChanged("CodiceMexal");
					this.OnCodiceMexalChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsMedico", DbType="Bit")]
		public System.Nullable<bool> IsMedico
		{
			get
			{
				return this._IsMedico;
			}
			set
			{
				if ((this._IsMedico != value))
				{
					this.OnIsMedicoChanging(value);
					this.SendPropertyChanging();
					this._IsMedico = value;
					this.SendPropertyChanged("IsMedico");
					this.OnIsMedicoChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591