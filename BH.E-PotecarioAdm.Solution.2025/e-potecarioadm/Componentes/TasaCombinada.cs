using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

using BH.Sysnet;

namespace BH.EPotecario.Adm.Componentes
{
    public class PreguntaDB
    {
        int codPregunta;
        string desPregunta;
        string desRespuestaSI;
        string desRespuestaNO;
		bool respuestaNOEsPositiva;
        string aclaracion;
        int codDestinoCredito;

        public int CodPregunta
        {
            get { return codPregunta; }
            set { codPregunta = value; }
        }

        public string DesPregunta
        {
            get { return desPregunta; }
            set { desPregunta = value; }
        }

        public string DesRespuestaSI
        {
            get { return desRespuestaSI; }
            set { desRespuestaSI = value; }
        }

        public string DesRespuestaNO
        {
            get { return desRespuestaNO; }
            set { desRespuestaNO = value; }
        }

		public bool RespuestaNOEsPositiva
        {
			get { return respuestaNOEsPositiva; }
			set { respuestaNOEsPositiva = value; }
        }

        public string Aclaracion
        {
            get { return aclaracion; }
            set { aclaracion = value; }
        }

        public int CodDestinoCredito
        {
            get { return codDestinoCredito; }
            set { codDestinoCredito = value; }
        }
    }

    public class TipoDocumentacionDB
    {
        int codTipoDocumentacion;
        string desTipoDocumentacion;

        public int CodTipoDocumentacion
        {
            get { return codTipoDocumentacion; }
            set { codTipoDocumentacion = value; }
        }

        public string DesTipoDocumentacion
        {
            get { return desTipoDocumentacion; }
            set { desTipoDocumentacion = value; }
        }
    }

    public class DocumentacionDB
    {
        int codDocumentacion;
        string desDocumentacion;
        int codTipoDocumentacion;
        string condTitular;
        string condCoTitular;

        public int CodDocumentacion
        {
            get { return codDocumentacion; }
            set { codDocumentacion = value; }
        }

        public string DesDocumentacion
        {
            get { return desDocumentacion; }
            set { desDocumentacion = value; }
        }

        public int CodTipoDocumentacion
        {
            get { return codTipoDocumentacion; }
            set { codTipoDocumentacion = value; }
        }

        public string CondTitular
        {
            get { return condTitular; }
            set { condTitular = value; }
        }

        public string CondCoTitular
        {
            get { return condCoTitular; }
            set { condCoTitular = value; }
        }
    }

    public class PreguntaFrecuenteDB
    {
        int codPreguntaFrecuente;
        int codDestinoCredito;
        int codSituacionLaboral;
        string desPregunta;
        string desRespuesta;

        public int CodPreguntaFrecuente
        {
            get { return codPreguntaFrecuente; }
            set { codPreguntaFrecuente = value; }
        }

        public int CodDestinoCredito
        {
            get { return codDestinoCredito; }
            set { codDestinoCredito = value; }
        }

        public int CodSituacionLaboral
        {
            get { return codSituacionLaboral; }
            set { codSituacionLaboral = value; }
        }

        public string DesPregunta
        {
            get { return desPregunta; }
            set { desPregunta = value; }
        }

        public string DesRespuesta
        {
            get { return desRespuesta; }
            set { desRespuesta = value; }
        }
    }

    public class DestinoCredito
    {
        int codDestinoCredito;
        string desDestinoCredito;
		string caracteristicas;

        public int CodDestinoCredito
        {
            get { return codDestinoCredito; }
            set { codDestinoCredito = value; }
        }

        public string DesDestinoCredito
        {
            get { return desDestinoCredito; }
            set { desDestinoCredito = value; }
        }

		public string Caracteristicas
		{
			get { return caracteristicas; }
			set { caracteristicas = value; }
		}
    }

    public class SituacionLaboral
    {
        int codSituacionLaboral;
        string desSituacionLaboral;

        public int CodSituacionLaboral
        {
            get { return codSituacionLaboral; }
            set { codSituacionLaboral = value; }
        }

        public string DesSituacionLaboral
        {
            get { return desSituacionLaboral; }
            set { desSituacionLaboral = value; }
        }
    }

    public class EstadoCivil
    {
        int codEstadoCivil;
        string desEstadoCivil;

        public int CodEstadoCivil
        {
            get { return codEstadoCivil; }
            set { codEstadoCivil = value; }
        }

        public string DesEstadoCivil
        {
            get { return desEstadoCivil; }
            set { desEstadoCivil = value; }
        }
    }
    

    public class TasaCombinada : ObjectBase
    {
        public TasaCombinada() { }

        public string ObtenerParametro(string Parametro)
        {
            SysNet sysnet = SI.GetSysNet();

            string Valor = String.Empty;

            string sql = "select Parametro, Valor from Parametros where Parametro = @Parametro";
            sysnet.DB.Parameters.Add("@Parametro", Parametro);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                Valor = reader[1].ToString();
            }
            reader.Close();

            return Valor;
        }

        #region Preguntas
        public IList<PreguntaDB> ObtenerPreguntas()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<PreguntaDB> preguntas = new List<PreguntaDB>();
            PreguntaDB pregunta = null;

            string sql = "select CodPregunta, DesPregunta, DesRespuestaSI, DesRespuestaNO, CodDestinoCredito, Aclaracion from Pregunta order by CodPregunta, CodDestinoCredito ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                pregunta = new PreguntaDB();
                pregunta.CodPregunta = int.Parse(reader["CodPregunta"].ToString());
                pregunta.DesPregunta = reader["DesPregunta"].ToString();
                pregunta.DesRespuestaSI = reader["DesRespuestaSI"].ToString();
                pregunta.DesRespuestaNO = reader["DesRespuestaNO"].ToString();
                pregunta.CodDestinoCredito = int.Parse(reader["CodDestinoCredito"].ToString());
                pregunta.Aclaracion = reader["Aclaracion"].ToString();

                preguntas.Add(pregunta);
            }
            reader.Close();

            return preguntas;
        }

		public IList<PreguntaDB> ObtenerPreguntas(int codDestinoCredito)
		{
			SysNet sysnet = SI.GetSysNet();

			IList<PreguntaDB> preguntas = new List<PreguntaDB>();
			PreguntaDB pregunta = null;

			string sql = "select CodPregunta, DesPregunta, DesRespuestaSI, DesRespuestaNO, CodDestinoCredito, Aclaracion from Pregunta where codDestinoCredito = @codDestinoCredito order by CodPregunta, CodDestinoCredito ASC";

			sysnet.DB.Parameters.Add("@codDestinoCredito", codDestinoCredito);
			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			while (reader.Read())
			{
				pregunta = new PreguntaDB();
				pregunta.CodPregunta = int.Parse(reader["CodPregunta"].ToString());
				pregunta.DesPregunta = System.Web.HttpUtility.HtmlDecode(reader["DesPregunta"].ToString());
				pregunta.DesRespuestaSI = System.Web.HttpUtility.HtmlDecode(reader["DesRespuestaSI"].ToString());
				pregunta.DesRespuestaNO = System.Web.HttpUtility.HtmlDecode(reader["DesRespuestaNO"].ToString());
				pregunta.CodDestinoCredito = int.Parse(reader["CodDestinoCredito"].ToString());
				pregunta.Aclaracion = System.Web.HttpUtility.HtmlDecode(reader["Aclaracion"].ToString());

				preguntas.Add(pregunta);
			}
			reader.Close();

			return preguntas;
		}

        public PreguntaDB ObtenerPregunta(int codPregunta)
        {
            SysNet sysnet = SI.GetSysNet();

            PreguntaDB pregunta = null;

			string sql = "select CodPregunta, DesPregunta, DesRespuestaSI, DesRespuestaNO, RespuestaNOEsPositiva, CodDestinoCredito, Aclaracion from Pregunta where CodPregunta = @CodPregunta";
            sysnet.DB.Parameters.Add("@CodPregunta", codPregunta);
            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            
            if (reader.Read())
            {
                pregunta = new PreguntaDB();
                pregunta.CodPregunta = int.Parse(reader["CodPregunta"].ToString());
                pregunta.DesPregunta = reader["DesPregunta"].ToString();
                pregunta.DesRespuestaSI = reader["DesRespuestaSI"].ToString();
				pregunta.DesRespuestaNO = reader["DesRespuestaNO"].ToString();
				pregunta.RespuestaNOEsPositiva = bool.Parse(reader["RespuestaNOEsPositiva"].ToString());
                pregunta.CodDestinoCredito = int.Parse(reader["CodDestinoCredito"].ToString());
                pregunta.Aclaracion = reader["Aclaracion"].ToString();
            }
            reader.Close();

            return pregunta;
        }

        public string AgregarPregunta(PreguntaDB pregunta)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                PreguntaDB preguntaExiste = ObtenerPregunta(pregunta.CodPregunta);

                if (preguntaExiste == null)
                {
					sql = "insert into Pregunta ( DesPregunta, DesRespuestaSI, DesRespuestaNO, RespuestaNOEsPositiva, CodDestinoCredito, Aclaracion) values ( @DesPregunta, @DesRespuestaSI, @DesRespuestaNO, @RespuestaNOEsPositiva, @CodDestinoCredito, @Aclaracion )";
                    sysnet.DB.Parameters.Add("@DesPregunta", pregunta.DesPregunta);
                    sysnet.DB.Parameters.Add("@DesRespuestaSI", pregunta.DesRespuestaSI);
					sysnet.DB.Parameters.Add("@DesRespuestaNO", pregunta.DesRespuestaNO);
					sysnet.DB.Parameters.Add("@RespuestaNOEsPositiva", pregunta.RespuestaNOEsPositiva);
					sysnet.DB.Parameters.Add("@CodDestinoCredito", pregunta.CodDestinoCredito);
                    sysnet.DB.Parameters.Add("@Aclaracion", pregunta.Aclaracion);

                    pregunta.CodPregunta = (int)sysnet.DB.Execute(sql, true);
                }
                else
                {

					sql = "update Pregunta set DesPregunta = @DesPregunta, DesRespuestaSI = @DesRespuestaSI, DesRespuestaNO = @DesRespuestaNO, RespuestaNOEsPositiva = @RespuestaNOEsPositiva, CodDestinoCredito = @CodDestinoCredito, Aclaracion = @Aclaracion where CodPregunta = @CodPregunta";
                    sysnet.DB.Parameters.Add("@CodPregunta", pregunta.CodPregunta);
                    sysnet.DB.Parameters.Add("@DesPregunta", pregunta.DesPregunta);
                    sysnet.DB.Parameters.Add("@DesRespuestaSI", pregunta.DesRespuestaSI);
					sysnet.DB.Parameters.Add("@DesRespuestaNO", pregunta.DesRespuestaNO);
					sysnet.DB.Parameters.Add("@RespuestaNOEsPositiva", pregunta.RespuestaNOEsPositiva);
					sysnet.DB.Parameters.Add("@CodDestinoCredito", pregunta.CodDestinoCredito);
                    sysnet.DB.Parameters.Add("@Aclaracion", pregunta.Aclaracion);

                    pregunta.CodPregunta = preguntaExiste.CodPregunta;

                    sysnet.DB.Execute(sql);
                }
                sysnet.DB.CommitTransaction();

                return "Se actualizo correctamente la Pregunta.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar la Pregunta.");
                throw new ApplicationException(mensaje, ex);
            }
        }

		public string EliminarPregunta(int codPregunta)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Delete from Pregunta where CodPregunta = @CodPregunta";
                sysnet.DB.Parameters.Add("@CodPregunta", codPregunta);

                sysnet.DB.Execute(sql);

                sysnet.DB.CommitTransaction();

                return "Se elimino correctamente la Pregunta.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar la Pregunta.");
                throw new ApplicationException(mensaje, ex);
            }
        }
        #endregion

        #region Documentación
        public IList<DocumentacionDB> ObtenerDocumentaciones()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<DocumentacionDB> documentaciones = new List<DocumentacionDB>();
            DocumentacionDB documentacion = null;

            string sql = "select CodDocumentacion, DesDocumentacion, CodTipoDocumentacion, CondTitular, CondCoTitular from Documentacion order by CodDocumentacion ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                documentacion = new DocumentacionDB();
                documentacion.CodDocumentacion = int.Parse(reader["CodDocumentacion"].ToString());
                documentacion.DesDocumentacion = reader["DesDocumentacion"].ToString();
                documentacion.CodTipoDocumentacion = int.Parse(reader["CodTipoDocumentacion"].ToString());
                documentacion.CondTitular = reader["CondTitular"].ToString();
                documentacion.CondCoTitular = reader["CondCoTitular"].ToString();

                documentaciones.Add(documentacion);
            }
            reader.Close();

            return documentaciones;
        }

        public DocumentacionDB ObtenerDocumentacion(int codDocumentacion)
        {
            SysNet sysnet = SI.GetSysNet();

            DocumentacionDB documentacion = null;

            string sql = "select CodDocumentacion, DesDocumentacion, CodTipoDocumentacion, CondTitular, CondCoTitular from Documentacion where CodDocumentacion = @CodDocumentacion";
            sysnet.DB.Parameters.Add("@CodDocumentacion", codDocumentacion);
            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                documentacion = new DocumentacionDB();
                documentacion.CodDocumentacion = int.Parse(reader["CodDocumentacion"].ToString());
                documentacion.DesDocumentacion = reader["DesDocumentacion"].ToString();
                documentacion.CodTipoDocumentacion = int.Parse(reader["CodTipoDocumentacion"].ToString());
                documentacion.CondTitular = reader["CondTitular"].ToString();
                documentacion.CondCoTitular = reader["CondCoTitular"].ToString();
            }
            reader.Close();

            return documentacion;
        }

        public string AgregarDocumentacion(DocumentacionDB documentacion)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                DocumentacionDB documentacionExiste = ObtenerDocumentacion(documentacion.CodDocumentacion);

                if (documentacionExiste == null)
                {
                    sql = "insert into Documentacion ( DesDocumentacion, CodTipoDocumentacion, CondTitular, CondCoTitular) values ( @DesDocumentacion, @CodTipoDocumentacion, @CondTitular, @CondCoTitular )";
                    sysnet.DB.Parameters.Add("@DesDocumentacion", documentacion.DesDocumentacion);
                    sysnet.DB.Parameters.Add("@CodTipoDocumentacion", documentacion.CodTipoDocumentacion);
                    sysnet.DB.Parameters.Add("@CondTitular", documentacion.CondTitular);
                    sysnet.DB.Parameters.Add("@CondCoTitular", documentacion.CondCoTitular);

                    documentacion.CodDocumentacion = (int)sysnet.DB.Execute(sql, true);
                }
                else
                {

                    sql = "update Documentacion set DesDocumentacion = @DesDocumentacion, CodTipoDocumentacion = @CodTipoDocumentacion, CondTitular = @CondTitular, CondCoTitular = @CondCoTitular where CodDocumentacion = @CodDocumentacion";
                    sysnet.DB.Parameters.Add("@CodDocumentacion", documentacion.CodDocumentacion);
                    sysnet.DB.Parameters.Add("@DesDocumentacion", documentacion.DesDocumentacion);
                    sysnet.DB.Parameters.Add("@CodTipoDocumentacion", documentacion.CodTipoDocumentacion);
                    sysnet.DB.Parameters.Add("@CondTitular", documentacion.CondTitular);
                    sysnet.DB.Parameters.Add("@CondCoTitular", documentacion.CondCoTitular);

                    documentacion.CodDocumentacion = documentacionExiste.CodDocumentacion;

                    sysnet.DB.Execute(sql);
                }
                sysnet.DB.CommitTransaction();

                return "Se actualizo correctamente la Documentacion.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar a Documentacion.");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public string EliminarDocumentacion(int codDocumentacion)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Delete from Documentacion where CodDocumentacion = @CodDocumentacion";
                sysnet.DB.Parameters.Add("@CodDocumentacion", codDocumentacion);

                sysnet.DB.Execute(sql);

                sysnet.DB.CommitTransaction();

                return "Se elimino correctamente la Documentacion.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar la Documentacion.");
                throw new ApplicationException(mensaje, ex);
            }
        }
        #endregion

        #region Preguntas Frecuentes
        public IList<PreguntaFrecuenteDB> ObtenerPreguntasFrecuentes()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<PreguntaFrecuenteDB> preguntas = new List<PreguntaFrecuenteDB>();
            PreguntaFrecuenteDB pregunta = null;

            string sql = "select CodPreguntaFrecuente, CodDestinoCredito, CodSituacionLaboral, DesPregunta, DesRespuesta from PreguntaFrecuente order by CodPreguntaFrecuente ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                pregunta = new PreguntaFrecuenteDB();
                pregunta.CodPreguntaFrecuente = int.Parse(reader["CodPreguntaFrecuente"].ToString());
                if (reader["CodDestinoCredito"].ToString() != "")
                    pregunta.CodDestinoCredito = int.Parse(reader["CodDestinoCredito"].ToString());
                if (reader["CodSituacionLaboral"].ToString() != "")
                    pregunta.CodSituacionLaboral = int.Parse(reader["CodSituacionLaboral"].ToString());
                pregunta.DesPregunta = reader["DesPregunta"].ToString();
                pregunta.DesRespuesta = reader["DesRespuesta"].ToString();

                preguntas.Add(pregunta);
            }
            reader.Close();

            return preguntas;
        }

        public PreguntaFrecuenteDB ObtenerPreguntaFrecuente(int codPregunta)
        {
            SysNet sysnet = SI.GetSysNet();

            PreguntaFrecuenteDB pregunta = null;

            string sql = "select CodPreguntaFrecuente, CodDestinoCredito, CodSituacionLaboral, DesPregunta, DesRespuesta from PreguntaFrecuente where CodPreguntaFrecuente = @CodPreguntaFrecuente";
            sysnet.DB.Parameters.Add("@CodPreguntaFrecuente", codPregunta);
            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text); 
            if (reader.Read())
            {
                pregunta = new PreguntaFrecuenteDB();
                pregunta.CodPreguntaFrecuente = int.Parse(reader["CodPreguntaFrecuente"].ToString());
                if (reader["CodDestinoCredito"].ToString() != "")
                    pregunta.CodDestinoCredito = int.Parse(reader["CodDestinoCredito"].ToString());
                if (reader["CodSituacionLaboral"].ToString() != "")
                    pregunta.CodSituacionLaboral = int.Parse(reader["CodSituacionLaboral"].ToString());
                pregunta.DesPregunta = reader["DesPregunta"].ToString();
                pregunta.DesRespuesta = reader["DesRespuesta"].ToString();
            }
            reader.Close();

            return pregunta;
        }

        public string AgregarPreguntaFrecuente(PreguntaFrecuenteDB pregunta)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                PreguntaFrecuenteDB preguntaExiste = ObtenerPreguntaFrecuente(pregunta.CodPreguntaFrecuente);

                if (preguntaExiste == null)
                {
                    sql = "insert into PreguntaFrecuente ( CodDestinoCredito, CodSituacionLaboral, DesPregunta, DesRespuesta) values ( @CodDestinoCredito, @CodSituacionLaboral, @DesPregunta, @DesRespuesta )";
                    if (pregunta.CodDestinoCredito != -1)
                        sysnet.DB.Parameters.Add("@CodDestinoCredito", pregunta.CodDestinoCredito);
                    else
                        sysnet.DB.Parameters.Add("@CodDestinoCredito", DBNull.Value);
                    if (pregunta.CodSituacionLaboral != -1)
                        sysnet.DB.Parameters.Add("@CodSituacionLaboral", pregunta.CodSituacionLaboral);
                    else
                        sysnet.DB.Parameters.Add("@CodSituacionLaboral", DBNull.Value);
                    sysnet.DB.Parameters.Add("@DesPregunta", pregunta.DesPregunta);
                    sysnet.DB.Parameters.Add("@DesRespuesta", pregunta.DesRespuesta);

                    pregunta.CodPreguntaFrecuente = (int)sysnet.DB.Execute(sql, true);
                }
                else
                {

                    sql = "update PreguntaFrecuente set CodDestinoCredito = @CodDestinoCredito, CodSituacionLaboral = @CodSituacionLaboral, DesPregunta = @DesPregunta, DesRespuesta = @DesRespuesta where CodPreguntaFrecuente = @CodPreguntaFrecuente";
                    if (pregunta.CodDestinoCredito != -1)
                        sysnet.DB.Parameters.Add("@CodDestinoCredito", pregunta.CodDestinoCredito);
                    else
                        sysnet.DB.Parameters.Add("@CodDestinoCredito", DBNull.Value);
                    if (pregunta.CodSituacionLaboral != -1)
                        sysnet.DB.Parameters.Add("@CodSituacionLaboral", pregunta.CodSituacionLaboral);
                    else
                        sysnet.DB.Parameters.Add("@CodSituacionLaboral", DBNull.Value);
                    sysnet.DB.Parameters.Add("@DesPregunta", pregunta.DesPregunta);
                    sysnet.DB.Parameters.Add("@DesRespuesta", pregunta.DesRespuesta);
                    sysnet.DB.Parameters.Add("@CodPreguntaFrecuente", pregunta.CodPreguntaFrecuente);

                    pregunta.CodPreguntaFrecuente = preguntaExiste.CodPreguntaFrecuente;

                    sysnet.DB.Execute(sql);
                }
                sysnet.DB.CommitTransaction();

                return "Se actualizo correctamente la Pregunta.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar la Pregunta.");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public string EliminarPreguntaFrecuente(int codPregunta)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Delete from PreguntaFrecuente where CodPreguntaFrecuente = @CodPreguntaFrecuente";
                sysnet.DB.Parameters.Add("@CodPreguntaFrecuente", codPregunta);

                sysnet.DB.Execute(sql);

                sysnet.DB.CommitTransaction();

                return "Se elimino correctamente la Pregunta.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar la Pregunta.");
                throw new ApplicationException(mensaje, ex);
            }
        }
        #endregion

        public IList<TipoDocumentacionDB> ObtenerTipoDocumentaciones()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<TipoDocumentacionDB> documentaciones = new List<TipoDocumentacionDB>();
            TipoDocumentacionDB documentacion = null;

            string sql = "select CodTipoDocumentacion, DesTipoDocumentacion from TipoDocumentacion order by CodTipoDocumentacion ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                documentacion = new TipoDocumentacionDB();
                documentacion.CodTipoDocumentacion = int.Parse(reader["CodTipoDocumentacion"].ToString());
                documentacion.DesTipoDocumentacion = reader["DesTipoDocumentacion"].ToString();

                documentaciones.Add(documentacion);
            }
            reader.Close();

            return documentaciones;
        }

        public IList<DestinoCredito> ObtenerDestinosCreditos()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<DestinoCredito> destinos = new List<DestinoCredito>();
            DestinoCredito destino = null;

			string sql = "select CodDestinoCredito, DesDestinoCredito from DestinoCredito order by orden ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                destino = new DestinoCredito();
                destino.CodDestinoCredito = int.Parse(reader["CodDestinoCredito"].ToString());
                destino.DesDestinoCredito = reader["DesDestinoCredito"].ToString();

                destinos.Add(destino);
            }
            reader.Close();

            return destinos;
        }

		public string AgregarDestinoCredito(DestinoCredito destinoCredito)
		{
			SysNet sysnet = SI.GetSysNet();
			string sql;
			try
			{
				sysnet.DB.BeginTransaction();

				DestinoCredito destinoCreditoDB = ObtenerDestinoCredito(destinoCredito.CodDestinoCredito);

				if (destinoCreditoDB == null)
				{
					sql = "insert into DestinoCredito ( CodDestinoCredito, DesDestinoCredito, Orden, Caracteristicas ) values ( @CodDestinoCredito, @DesDestinoCredito, @Orden, @Caracteristicas )";
					sysnet.DB.Parameters.Add("@CodDestinoCredito", destinoCredito.CodDestinoCredito);
					sysnet.DB.Parameters.Add("@DesDestinoCredito", destinoCredito.DesDestinoCredito);
					sysnet.DB.Parameters.Add("@Orden", DBNull.Value);
					sysnet.DB.Parameters.Add("@Caracteristicas", destinoCredito.Caracteristicas);

					sysnet.DB.Execute(sql, true);
				}
				else
				{

					sql = "update DestinoCredito set Caracteristicas = @Caracteristicas where CodDestinoCredito = @CodDestinoCredito";
					sysnet.DB.Parameters.Add("@CodDestinoCredito", destinoCredito.CodDestinoCredito);
					sysnet.DB.Parameters.Add("@Caracteristicas", destinoCredito.Caracteristicas);

					sysnet.DB.Execute(sql);
				}
				sysnet.DB.CommitTransaction();

				return "Se actualizo correctamente el destino de crédito.";
			}
			catch (Exception ex)
			{
				sysnet.DB.Rollback();
				string mensaje = string.Format("Error al cargar el destino del crédito.");
				throw new ApplicationException(mensaje, ex);
			}
		}

		public DestinoCredito ObtenerDestinoCredito(int codDestino)
		{
			SysNet sysnet = SI.GetSysNet();

			DestinoCredito destino = new DestinoCredito();

			string sql = "select CodDestinoCredito, DesDestinoCredito, Caracteristicas from DestinoCredito where codDestinoCredito = @codDestinoCredito order by orden ASC";
			sysnet.DB.Parameters.Add("@codDestinoCredito", codDestino);

			SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
			if (reader.Read())
			{
				destino.CodDestinoCredito = int.Parse(reader["CodDestinoCredito"].ToString());
				destino.DesDestinoCredito = reader["DesDestinoCredito"].ToString();
				destino.Caracteristicas = reader["Caracteristicas"].ToString();
			}
			reader.Close();

			return destino;
		}

        public IList<SituacionLaboral> ObtenerSituacionesLaborales()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<SituacionLaboral> situaciones = new List<SituacionLaboral>();
            SituacionLaboral situacion = null;

            string sql = "select CodSituacionLaboral, DesSituacionLaboral from SituacionLaboral order by CodSituacionLaboral ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                situacion = new SituacionLaboral();
                situacion.CodSituacionLaboral = int.Parse(reader["CodSituacionLaboral"].ToString());
                situacion.DesSituacionLaboral = reader["DesSituacionLaboral"].ToString();

                situaciones.Add(situacion);
            }
            reader.Close();

            return situaciones;
        }

        public IList<EstadoCivil> ObtenerEstadosCiviles()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<EstadoCivil> estados = new List<EstadoCivil>();
            EstadoCivil estado = null;

            string sql = "select CodEstadoCivil, DesEstadoCivil from EstadoCivil order by CodEstadoCivil ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                estado = new EstadoCivil();
                estado.CodEstadoCivil = int.Parse(reader["CodEstadoCivil"].ToString());
                estado.DesEstadoCivil = reader["DesEstadoCivil"].ToString();

                estados.Add(estado);
            }
            reader.Close();

            return estados;
        }
        
    }
}
