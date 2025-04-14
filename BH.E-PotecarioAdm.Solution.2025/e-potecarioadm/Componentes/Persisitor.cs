using System;
using System.Data;
using System.Data.SqlClient;
using BH.Sysnet;

namespace BH.EPotecario.Adm.Componentes
{
	
	public class Persistor
	{
		
		SysNet sysnet = null;
		DalRowUpdatedHandler dalRowUpdatedHandler = null;
		DalRowUpdatingHandler dalRowUpdatingHandler = null;
		CommandType updateDbMode = CommandType.Text;
				
		public Persistor(SysNet sysnet)
		{
			this.sysnet = sysnet;
		}					

		public Persistor(SysNet sysnet, 
			DalRowUpdatedHandler dalRowUpdatedHandler, 
			DalRowUpdatingHandler dalRowUpdatingHandler) : this (sysnet)
		{
			this.dalRowUpdatedHandler = dalRowUpdatedHandler;
			this.dalRowUpdatingHandler = dalRowUpdatingHandler;
		}

		public CommandType UpdateDbMode
		{
			get {return updateDbMode;}
			set {updateDbMode = value;}
		}

		
		
		public void AddRowUpdatedHandler(DalRowUpdatedHandler function)
		{
			this.dalRowUpdatedHandler += function;
		}

		public void RemoveRowUpdatedHandler(DalRowUpdatedHandler function)
		{
			this.dalRowUpdatedHandler -= function;
		}

		public void AddRowUpdatingHandler(DalRowUpdatingHandler function)
		{
			this.dalRowUpdatingHandler += function;
		}

		public void RemoveRowUpdatingHandler(DalRowUpdatingHandler function)
		{
			this.dalRowUpdatingHandler -= function;
		}
										 

		public DataSet Update(DataSet ds, string tableName)
		{
			return Update(ds, tableName, false);
		}

		public DataSet Update(DataSet ds, string tableName, bool returnIdentity)
		{
			DataTable tableSchema = DBSchemaSingleton.GetTable(tableName);
			return Update(ds, tableSchema, returnIdentity);
		}	

		
		public DataSet Update(DataSet ds, DataTable tableSchema)
		{
			return Update(ds, tableSchema, false);
		}

		public DataSet Update(DataSet ds, DataTable tableSchema, bool returnIdentity)
		{						
			try
			{				
				
				string tableName = tableSchema.TableName;
				DataTable tableUpdate = ds.Tables[tableName];
				
				this.sysnet.DB.DataAdapters.Clear();
				this.sysnet.DB.DataAdapters.Add( tableName);												
				
				if (returnIdentity)
					this.sysnet.DB.DataAdapters[tableName].ReturnIdentity = true;				
				
				if (this.updateDbMode == CommandType.StoredProcedure)
					this.sysnet.DB.DataAdapters[tableName].UpdateMode = UpdateMode.StoredProcedure;																
				
				this.sysnet.DB.DataAdapters[tableName].CreateCommands(tableSchema);		
								

				//Agrego PrimaryKey para el sysnet pueda retornar el Identity				
				if (returnIdentity	&& tableUpdate.PrimaryKey.Length == 0)
				{											
					
					bool isIdentity = tableSchema.PrimaryKey[0].AutoIncrement;
					
					if (isIdentity)
					{
						//Pongo algun dato a la PK Nula para que no de error
						DataView dv = new DataView(tableUpdate);
						dv.RowStateFilter = DataViewRowState.Added;
						
						int id = -100;
						foreach (DataRowView drv in dv)
						{
							if (drv[tableSchema.PrimaryKey[0].ColumnName] == DBNull.Value)
							{
								drv[tableSchema.PrimaryKey[0].ColumnName] = id;
								id--;
							}
						}												
					}					
					
					tableUpdate.DataSet.Merge(tableSchema, true, MissingSchemaAction.AddWithKey);
										
					if (isIdentity)
						tableUpdate.PrimaryKey[0].AutoIncrement = true;
				}
				////////////////////////////////////////////////////				

				//Eventos
				if (this.dalRowUpdatedHandler != null)
					this.sysnet.DB.DataAdapters[tableName].RowUpdated += this.dalRowUpdatedHandler;

				if (this.dalRowUpdatingHandler != null)
					this.sysnet.DB.DataAdapters[tableName].RowUpdating += this.dalRowUpdatingHandler;


				//Si estoy en contexto de transaccion debo iniciar la transaccion porque estoy modificando la DB
				if (ContextoTrans.GetFromThread() != null)   
					ContextoTrans.GetFromThread().BeginTransaction();				
				
				this.sysnet.DB.DataAdapters[tableName].Update(ds, tableName);																																	

				return ds;
			}
			catch (Exception ex)
			{
				if (ex.InnerException is SqlException)
				{
					//Obntego la Ex de Sql Server				
					SqlException sqlEx = (SqlException) ex.InnerException;
					switch (sqlEx.Number)
					{
						case 547:						
							throw new ApplicationException
								("Error de integridad referencial", ex);						
						
						case 2627:						
							throw new ApplicationException
								("Error de clave duplicada.", ex);												
							
						default:
							throw ex;	
					}
				}
            
				else			
					throw ex;
			}			
			
		}	
		
	}
}
