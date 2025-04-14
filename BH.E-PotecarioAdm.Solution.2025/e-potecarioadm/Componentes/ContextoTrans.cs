using System;
using System.Data;
//using System.Threading;
using System.Web;

using BH.Sysnet;


namespace BH.EPotecario.Adm.Componentes
{
	
	public class ContextoTrans
	{
		SysNet sysnet = null;
		static string name = "contextoTrans";

		public ContextoTrans()
		{
			this.sysnet = SI.GetSysNet();
			
			//Thread.SetData(Thread.GetNamedDataSlot(name), this);
			HttpContext.Current.Items[name] = this;			
		}
		
		public static ContextoTrans GetFromThread()
		{
			//return (ContextoTrans) Thread.GetData(Thread.GetNamedDataSlot(name));
			return (ContextoTrans) HttpContext.Current.Items[name];			
		}

		public static void RemoveFromThread()
		{
			//Thread.SetData(Thread.GetNamedDataSlot(name), null);				   
			HttpContext.Current.Items.Remove(name);
		}
		
		public SysNet SysNet
		{
			get {return this.sysnet;}
		}
		
		public int TimeOut
		{
			get {return this.sysnet.DB.CommandTimeout;}
			set {this.sysnet.DB.CommandTimeout = value;}
		}

		
		public IsolationLevel IsolationLevel
		{
			get {return this.sysnet.DB.IsolationLevel;}
			set {this.sysnet.DB.IsolationLevel = value;}
		}




		public void BeginTransaction()
		{
			if (!this.sysnet.DB.InTransaction)
				this.sysnet.DB.BeginTransaction();
		}

		

		public void CommitTransaction()
		{		
			if (this.sysnet.DB.InTransaction)			
				this.sysnet.DB.CommitTransaction();
			
			RemoveFromThread();
		}


		public void Rollback()
		{
			if (this.sysnet.DB.InTransaction)
				this.sysnet.DB.Rollback();

			RemoveFromThread();
		}


		
	}
}
