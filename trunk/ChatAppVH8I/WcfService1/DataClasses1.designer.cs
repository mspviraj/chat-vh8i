﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService1
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Database1")]
	public partial class MessageAppDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertClient(Client instance);
    partial void UpdateClient(Client instance);
    partial void DeleteClient(Client instance);
    partial void InsertMessage(Message instance);
    partial void UpdateMessage(Message instance);
    partial void DeleteMessage(Message instance);
    #endregion
		
		public MessageAppDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["Database1ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MessageAppDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MessageAppDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MessageAppDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MessageAppDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Client> Clients
		{
			get
			{
				return this.GetTable<Client>();
			}
		}
		
		public System.Data.Linq.Table<Message> Messages
		{
			get
			{
				return this.GetTable<Message>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.clients")]
	public partial class Client : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _TelephoneNr;
		
		private EntitySet<Message> _MessagesFrom;
		
		private EntitySet<Message> _MessagesTo;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnTelephoneNrChanging(string value);
    partial void OnTelephoneNrChanged();
    #endregion
		
		public Client()
		{
			this._MessagesFrom = new EntitySet<Message>(new Action<Message>(this.attach_MessagesFrom), new Action<Message>(this.detach_MessagesFrom));
			this._MessagesTo = new EntitySet<Message>(new Action<Message>(this.attach_MessagesTo), new Action<Message>(this.detach_MessagesTo));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="telephone_nr", Storage="_TelephoneNr", DbType="VarChar(20) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string TelephoneNr
		{
			get
			{
				return this._TelephoneNr;
			}
			set
			{
				if ((this._TelephoneNr != value))
				{
					this.OnTelephoneNrChanging(value);
					this.SendPropertyChanging();
					this._TelephoneNr = value;
					this.SendPropertyChanged("TelephoneNr");
					this.OnTelephoneNrChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Client_Message", Storage="_MessagesFrom", ThisKey="TelephoneNr", OtherKey="TelephoneNrFrom")]
		public EntitySet<Message> MessagesFrom
		{
			get
			{
				return this._MessagesFrom;
			}
			set
			{
				this._MessagesFrom.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Client_Message1", Storage="_MessagesTo", ThisKey="TelephoneNr", OtherKey="TelephoneNrTo")]
		public EntitySet<Message> MessagesTo
		{
			get
			{
				return this._MessagesTo;
			}
			set
			{
				this._MessagesTo.Assign(value);
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
		
		private void attach_MessagesFrom(Message entity)
		{
			this.SendPropertyChanging();
			entity.ClientFrom = this;
		}
		
		private void detach_MessagesFrom(Message entity)
		{
			this.SendPropertyChanging();
			entity.ClientFrom = null;
		}
		
		private void attach_MessagesTo(Message entity)
		{
			this.SendPropertyChanging();
			entity.ClientTo = this;
		}
		
		private void detach_MessagesTo(Message entity)
		{
			this.SendPropertyChanging();
			entity.ClientTo = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.messages")]
	public partial class Message : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _TelephoneNrTo;
		
		private string _TelephoneNrFrom;
		
		private System.DateTime _Datetime;
		
		private string _Content;
		
		private EntityRef<Client> _ClientFrom;
		
		private EntityRef<Client> _ClientTo;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnTelephoneNrToChanging(string value);
    partial void OnTelephoneNrToChanged();
    partial void OnTelephoneNrFromChanging(string value);
    partial void OnTelephoneNrFromChanged();
    partial void OnDatetimeChanging(System.DateTime value);
    partial void OnDatetimeChanged();
    partial void OnContentChanging(string value);
    partial void OnContentChanged();
    #endregion
		
		public Message()
		{
			this._ClientFrom = default(EntityRef<Client>);
			this._ClientTo = default(EntityRef<Client>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="to_telephone_nr", Storage="_TelephoneNrTo", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string TelephoneNrTo
		{
			get
			{
				return this._TelephoneNrTo;
			}
			set
			{
				if ((this._TelephoneNrTo != value))
				{
					this.OnTelephoneNrToChanging(value);
					this.SendPropertyChanging();
					this._TelephoneNrTo = value;
					this.SendPropertyChanged("TelephoneNrTo");
					this.OnTelephoneNrToChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="from_telephone_nr", Storage="_TelephoneNrFrom", DbType="VarChar(20) NOT NULL", CanBeNull=false)]
		public string TelephoneNrFrom
		{
			get
			{
				return this._TelephoneNrFrom;
			}
			set
			{
				if ((this._TelephoneNrFrom != value))
				{
					this.OnTelephoneNrFromChanging(value);
					this.SendPropertyChanging();
					this._TelephoneNrFrom = value;
					this.SendPropertyChanged("TelephoneNrFrom");
					this.OnTelephoneNrFromChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="datetime", Storage="_Datetime", DbType="DateTime NOT NULL")]
		public System.DateTime Datetime
		{
			get
			{
				return this._Datetime;
			}
			set
			{
				if ((this._Datetime != value))
				{
					this.OnDatetimeChanging(value);
					this.SendPropertyChanging();
					this._Datetime = value;
					this.SendPropertyChanged("Datetime");
					this.OnDatetimeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="msg_content", Storage="_Content", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				if ((this._Content != value))
				{
					this.OnContentChanging(value);
					this.SendPropertyChanging();
					this._Content = value;
					this.SendPropertyChanged("Content");
					this.OnContentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Client_Message", Storage="_ClientFrom", ThisKey="TelephoneNrFrom", OtherKey="TelephoneNr", IsForeignKey=true)]
		public Client ClientFrom
		{
			get
			{
				return this._ClientFrom.Entity;
			}
			set
			{
				Client previousValue = this._ClientFrom.Entity;
				if (((previousValue != value) 
							|| (this._ClientFrom.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ClientFrom.Entity = null;
						previousValue.MessagesFrom.Remove(this);
					}
					this._ClientFrom.Entity = value;
					if ((value != null))
					{
						value.MessagesFrom.Add(this);
						this._TelephoneNrFrom = value.TelephoneNr;
					}
					else
					{
						this._TelephoneNrFrom = default(string);
					}
					this.SendPropertyChanged("ClientFrom");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Client_Message1", Storage="_ClientTo", ThisKey="TelephoneNrTo", OtherKey="TelephoneNr", IsForeignKey=true)]
		public Client ClientTo
		{
			get
			{
				return this._ClientTo.Entity;
			}
			set
			{
				Client previousValue = this._ClientTo.Entity;
				if (((previousValue != value) 
							|| (this._ClientTo.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._ClientTo.Entity = null;
						previousValue.MessagesTo.Remove(this);
					}
					this._ClientTo.Entity = value;
					if ((value != null))
					{
						value.MessagesTo.Add(this);
						this._TelephoneNrTo = value.TelephoneNr;
					}
					else
					{
						this._TelephoneNrTo = default(string);
					}
					this.SendPropertyChanged("ClientTo");
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