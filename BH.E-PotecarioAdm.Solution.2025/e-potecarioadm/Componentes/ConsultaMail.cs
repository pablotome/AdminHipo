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
    public class ProductoConsulta
    {
        int codProductoConsulta;
        string desProductoConsulta;
        string eMail;
        int orden;
        bool habilitado;

        public int CodProductoConsulta
        {
            get { return codProductoConsulta; }
            set { codProductoConsulta = value; }
        }

        public string DesProductoConsulta
        {
            get { return desProductoConsulta; }
            set { desProductoConsulta = value; }
        }

        public string Email
        {
            get { return eMail; }
            set { eMail = value; }
        }

        public int Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        public bool Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }
    }

    public class TemaConsulta
    {
        int codTemaConsulta;
        string desTemaConsulta;
        int orden;
        bool habilitado;

        public int CodTemaConsulta
        {
            get { return codTemaConsulta; }
            set { codTemaConsulta = value; }
        }

        public string DesTemaConsulta
        {
            get { return desTemaConsulta; }
            set { desTemaConsulta = value; }
        }

        public int Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        public bool Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }
    }

    public class TemaProducto
    {
        int codTemaProducto;
        int codProductoConsulta;
        int codTemaConsulta;
        bool habilitado;

        public int CodTemaProducto
        {
            get { return codTemaProducto; }
            set { codTemaProducto = value; }
        }

        public int CodProductoConsulta
        {
            get { return codProductoConsulta; }
            set { codProductoConsulta = value; }
        }

        public int CodTemaConsulta
        {
            get { return codTemaConsulta; }
            set { codTemaConsulta = value; }
        }

        public bool Habilitado
        {
            get { return habilitado; }
            set { habilitado = value; }
        }
    }

    public class ConsultaMail : ObjectBase
    {
        public ConsultaMail() { }

        #region Productos Consulta
        public string AgregarProducto(ProductoConsulta producto)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                ProductoConsulta productoExiste = ObtenerProducto(producto.CodProductoConsulta);

                if (productoExiste == null)
                {
                    sql = "insert into ProductoConsulta ( DesProductoConsulta, Email, Orden, Habilitado) values ( @DesProductoConsulta, @Email, @Orden, @Habilitado )";
                    sysnet.DB.Parameters.Add("@DesProductoConsulta", producto.DesProductoConsulta);
                    sysnet.DB.Parameters.Add("@Email", producto.Email);
                    sysnet.DB.Parameters.Add("@Orden", producto.Orden);
                    sysnet.DB.Parameters.Add("@Habilitado", producto.Habilitado);

                    producto.CodProductoConsulta = (int)sysnet.DB.Execute(sql, true);
                }
                else
                {

                    sql = "update ProductoConsulta set DesProductoConsulta = @DesProductoConsulta, Email = @Email, Orden = @Orden, Habilitado = @Habilitado where CodProductoConsulta = @CodProductoConsulta";
                    sysnet.DB.Parameters.Add("@CodProductoConsulta", producto.CodProductoConsulta);
                    sysnet.DB.Parameters.Add("@DesProductoConsulta", producto.DesProductoConsulta);
                    sysnet.DB.Parameters.Add("@Email", producto.Email);
                    sysnet.DB.Parameters.Add("@Orden", producto.Orden);
                    sysnet.DB.Parameters.Add("@Habilitado", producto.Habilitado);

                    producto.CodProductoConsulta = productoExiste.CodProductoConsulta;
                    
                    sysnet.DB.Execute(sql);
                }
                sysnet.DB.CommitTransaction();

                return "Se actualizo correctamente el Producto.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar el Producto para la Consulta");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public string EliminarProducto(int codProducto)
        { 
           SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Delete from ProductoConsulta where CodProductoConsulta = @CodProductoConsulta";
                sysnet.DB.Parameters.Add("@CodProductoConsulta", codProducto);

                sysnet.DB.Execute(sql);

                sysnet.DB.CommitTransaction();

                return "Se elimino correctamente el Producto.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar el Producto para la Consulta");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public bool HayAsociacionesConProducto(int codProducto)
        {
            SysNet sysnet = SI.GetSysNet();
            bool auxRes = false;

            //ProductoConsulta producto = null;

            string sql = "select CodTemaProducto from TemaProducto where CodProductoConsulta = @CodProductoConsulta";
            sysnet.DB.Parameters.Add("@CodProductoConsulta", codProducto);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                auxRes = true;
            }
            reader.Close();

            return auxRes;
        }

        public ProductoConsulta ObtenerProducto(int codProducto)
        {
            SysNet sysnet = SI.GetSysNet();

            ProductoConsulta producto = null;

            string sql = "select CodProductoConsulta, DesProductoConsulta, Email, Orden, Habilitado from ProductoConsulta where CodProductoConsulta = @CodProductoConsulta";
            sysnet.DB.Parameters.Add("@CodProductoConsulta", codProducto);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                producto = new ProductoConsulta();
                producto.CodProductoConsulta = int.Parse(reader[0].ToString());
                producto.DesProductoConsulta= reader[1].ToString();
                producto.Email = reader[2].ToString();
                producto.Orden = int.Parse(reader[3].ToString());
                producto.Habilitado = bool.Parse(reader[4].ToString());
            }
            reader.Close();

            return producto;
        }

        public IList<ProductoConsulta> ObtenerProductos()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<ProductoConsulta> productos = new List<ProductoConsulta>();
            ProductoConsulta producto = null;

            string sql = "select CodProductoConsulta, DesProductoConsulta, Email, Orden, Habilitado from ProductoConsulta order by Orden ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                producto = new ProductoConsulta();
                producto.CodProductoConsulta = int.Parse(reader[0].ToString());
                producto.DesProductoConsulta = reader[1].ToString();
                producto.Email = reader[2].ToString();
                producto.Orden = int.Parse(reader[3].ToString());
                producto.Habilitado = bool.Parse(reader[4].ToString());

                productos.Add(producto);
            }
            reader.Close();

            return productos;
        }
        #endregion

        #region Temas Consulta
        public string AgregarTema(TemaConsulta tema)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                TemaConsulta temaExiste = ObtenerTema(tema.CodTemaConsulta);

                if (temaExiste == null)
                {
                    sql = "insert into TemaConsulta ( DesTemaConsulta, Orden, Habilitado) values ( @DesTemaConsulta, @Orden, @Habilitado )";
                    sysnet.DB.Parameters.Add("@DesTemaConsulta", tema.DesTemaConsulta);
                    sysnet.DB.Parameters.Add("@Orden", tema.Orden);
                    sysnet.DB.Parameters.Add("@Habilitado", tema.Habilitado);

                    tema.CodTemaConsulta = (int)sysnet.DB.Execute(sql, true);
                }
                else
                {

                    sql = "update TemaConsulta set DesTemaConsulta = @DesTemaConsulta, Orden = @Orden, Habilitado = @Habilitado where CodTemaConsulta = @CodTemaConsulta";
                    sysnet.DB.Parameters.Add("@CodTemaConsulta", tema.CodTemaConsulta);
                    sysnet.DB.Parameters.Add("@DesTemaConsulta", tema.DesTemaConsulta);
                    sysnet.DB.Parameters.Add("@Orden", tema.Orden);
                    sysnet.DB.Parameters.Add("@Habilitado", tema.Habilitado);

                    tema.CodTemaConsulta = temaExiste.CodTemaConsulta;

                    sysnet.DB.Execute(sql);
                }
                sysnet.DB.CommitTransaction();

                return "Se actualizo correctamente el Tema.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar el Tema para la Consulta");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public string EliminarTema(int codTema)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Delete from TemaConsulta where CodTemaConsulta = @CodTemaConsulta";
                sysnet.DB.Parameters.Add("@CodTemaConsulta", codTema);

                sysnet.DB.Execute(sql);

                sysnet.DB.CommitTransaction();

                return "Se elimino correctamente el Tema.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar el Tema.");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public bool HayAsociacionesConTema(int codTema)
        {
            SysNet sysnet = SI.GetSysNet();
            bool auxRes = false;

            //ProductoConsulta producto = null;

            string sql = "select CodTemaProducto from TemaProducto where CodTemaConsulta = @CodTemaConsulta";
            sysnet.DB.Parameters.Add("@CodTemaConsulta", codTema);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                auxRes = true;
            }
            reader.Close();

            return auxRes;
        }


        public TemaConsulta ObtenerTema(int codTema)
        {
            SysNet sysnet = SI.GetSysNet();

            TemaConsulta tema = null;

            string sql = "select CodTemaConsulta, DesTemaConsulta, Orden, Habilitado from TemaConsulta where CodTemaConsulta = @CodTemaConsulta";
            sysnet.DB.Parameters.Add("@CodTemaConsulta", codTema);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                tema = new TemaConsulta();
                tema.CodTemaConsulta = int.Parse(reader[0].ToString());
                tema.DesTemaConsulta = reader[1].ToString();
                tema.Orden = int.Parse(reader[2].ToString());
                tema.Habilitado = bool.Parse(reader[3].ToString());
            }
            reader.Close();

            return tema;
        }

        public IList<TemaConsulta> ObtenerTemas()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<TemaConsulta> temas = new List<TemaConsulta>();
            TemaConsulta tema = null;

            string sql = "select CodTemaConsulta, DesTemaConsulta, Orden, Habilitado from TemaConsulta order by Orden ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                tema = new TemaConsulta();
                tema.CodTemaConsulta = int.Parse(reader[0].ToString());
                tema.DesTemaConsulta = reader[1].ToString();
                tema.Orden = int.Parse(reader[2].ToString());
                tema.Habilitado = bool.Parse(reader[3].ToString());

                temas.Add(tema);
            }
            reader.Close();

            return temas;
        }
        #endregion

        #region Temas Producto
        public TemaProducto AgregarTemaProducto(TemaProducto temaPro)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                TemaProducto temaProExiste = ObtenerTemaProducto(temaPro.CodTemaProducto);

                if (temaProExiste == null)
                {
                    sql = "insert into TemaProducto ( CodProductoConsulta, CodTemaConsulta, Habilitado) values ( @CodProductoConsulta, @CodTemaConsulta, @Habilitado )";
                    sysnet.DB.Parameters.Add("@CodProductoConsulta", temaPro.CodProductoConsulta);
                    sysnet.DB.Parameters.Add("@CodTemaConsulta", temaPro.CodTemaConsulta);
                    sysnet.DB.Parameters.Add("@Habilitado", temaPro.Habilitado);

                    temaPro.CodTemaProducto = (int)sysnet.DB.Execute(sql, true);
                }
                else
                {

                    sql = "update TemaProducto set CodProductoConsulta = @CodProductoConsulta, CodTemaConsulta = @CodTemaConsulta, Habilitado = @Habilitado where CodTemaProducto = @CodTemaProducto";
                    sysnet.DB.Parameters.Add("@CodProductoConsulta", temaPro.CodProductoConsulta);
                    sysnet.DB.Parameters.Add("@CodTemaConsulta", temaPro.CodTemaConsulta);
                    sysnet.DB.Parameters.Add("@CodTemaProducto", temaPro.CodTemaProducto);
                    sysnet.DB.Parameters.Add("@Habilitado", temaPro.Habilitado);

                    temaPro.CodTemaProducto = temaProExiste.CodTemaProducto;

                    sysnet.DB.Execute(sql);
                }
                sysnet.DB.CommitTransaction();

                return temaPro;
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al cargar la relacion ''Producto - Tema''.");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public string EliminarTemaProducto(int codTemaProducto)
        {
            SysNet sysnet = SI.GetSysNet();
            string sql;
            try
            {
                sysnet.DB.BeginTransaction();

                sql = "Delete from TemaProducto where CodTemaProducto = @CodTemaProducto";
                sysnet.DB.Parameters.Add("@CodTemaProducto", codTemaProducto);

                sysnet.DB.Execute(sql);

                sysnet.DB.CommitTransaction();

                return "Se elimino correctamente la relacion Producto - Tema.";
            }
            catch (Exception ex)
            {
                sysnet.DB.Rollback();
                string mensaje = string.Format("Error al eliminar la relacion Producto - Tema. Existe una relacion con el Registro de las Consultas.");
                throw new ApplicationException(mensaje, ex);
            }
        }

        public TemaProducto ObtenerTemaProducto(int codTemaProd)
        {
            SysNet sysnet = SI.GetSysNet();

            TemaProducto temaPro = null;

            string sql = "select CodTemaProducto, CodProductoConsulta, CodTemaConsulta, Habilitado from TemaProducto where CodTemaProducto = @CodTemaProducto";
            sysnet.DB.Parameters.Add("@CodTemaProducto", codTemaProd);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                temaPro = new TemaProducto();
                temaPro.CodTemaProducto = int.Parse(reader[0].ToString());
                temaPro.CodProductoConsulta = int.Parse(reader[1].ToString());
                temaPro.CodTemaConsulta = int.Parse(reader[2].ToString());
                temaPro.Habilitado = bool.Parse(reader[3].ToString());
            }
            reader.Close();

            return temaPro;
        }

        public IList<TemaProducto> ObtenerTemasProductos()
        {
            SysNet sysnet = SI.GetSysNet();

            IList<TemaProducto> temaPros = new List<TemaProducto>();
            TemaProducto temaPro = null;

            string sql = "Select CodTemaProducto, CodProductoConsulta, CodTemaConsulta, Habilitado from TemaProducto order by CodTemaProducto ASC";

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                temaPro = new TemaProducto();
                temaPro.CodTemaProducto = int.Parse(reader[0].ToString());
                temaPro.CodProductoConsulta = int.Parse(reader[1].ToString());
                temaPro.CodTemaConsulta = int.Parse(reader[2].ToString());
                temaPro.Habilitado = bool.Parse(reader[3].ToString());

                temaPros.Add(temaPro);
            }
            reader.Close();

            return temaPros;
        }

        public IList<TemaProducto> ObtenerTemasProductosPorCodigos(int CodProducto, int CodTema)
        {
            SysNet sysnet = SI.GetSysNet();

            IList<TemaProducto> temaPros = new List<TemaProducto>();
            TemaProducto temaPro = null;

            string sql = "Select CodTemaProducto, CodProductoConsulta, CodTemaConsulta, Habilitado from TemaProducto where CodProductoConsulta = @CodProductoConsulta and CodTemaConsulta = @CodTemaConsulta order by CodTemaProducto ASC";
            sysnet.DB.Parameters.Add("@CodProductoConsulta", CodProducto);
            sysnet.DB.Parameters.Add("@CodTemaConsulta", CodTema);

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            while (reader.Read())
            {
                temaPro = new TemaProducto();
                temaPro.CodTemaProducto = int.Parse(reader[0].ToString());
                temaPro.CodProductoConsulta = int.Parse(reader[1].ToString());
                temaPro.CodTemaConsulta = int.Parse(reader[2].ToString());
                temaPro.Habilitado = bool.Parse(reader[3].ToString());

                temaPros.Add(temaPro);
            }
            reader.Close();

            return temaPros;
        }

        public DataSet ObtenerEstadisticas(DateTime FechaDesde, DateTime FechaHasta)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();
            string sql;

            sql = @" Select pc.DesProductoConsulta as Producto, tc.DesTemaConsulta as Tema, Count(rc.CodTemaProducto) as Cantidad
                    from RegistroContacto rc, TemaProducto tp, TemaConsulta tc, ProductoConsulta pc
                    where rc.CodTemaProducto = tp.CodTemaProducto 
                    AND tp.CodProductoConsulta = pc.CodProductoConsulta 
                    AND tp.CodTemaConsulta = tc.CodTemaConsulta
                    AND rc.Fecha >= @FechaDesde AND rc.Fecha < @FechaHasta 
                    Group by tc.DesTemaConsulta, pc.DesProductoConsulta  ";

            sysnet.DB.Parameters.Add("@FechaDesde", FechaDesde);
            sysnet.DB.Parameters.Add("@FechaHasta", FechaHasta);

            sysnet.DB.FillDataSet(sql, ds, "TemaProducto");

            return ds;
        }
        #endregion


        public string ObtenerParametroMail()
        {
            SysNet sysnet = SI.GetSysNet();

            string Valor = String.Empty;

            string sql = "select Parametro, Valor from Parametros where Parametro = @Parametro";
            sysnet.DB.Parameters.Add("@Parametro", "DestinatariosMailContactos");

            SqlDataReader reader = (SqlDataReader)sysnet.DB.ExecuteReturnDR(sql, CommandType.Text);
            if (reader.Read())
            {
                Valor = reader[1].ToString();
            }
            reader.Close();

            return Valor;
        }

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

        public DataTable ObtenerMails(string FechaDesde, string codProducto)
        {
            SysNet sysnet = SI.GetSysNet();
            DataSet ds = new DataSet();

            string sql = @"	select pr.email from RegistroContacto rc, Promociones_Registrados pr, TemaProducto tp , ProductoConsulta pc
                                            where rc.IDPromociones_Registrados = pr.ID
                                            and rc.CodTemaProducto = tp.CodTemaProducto
                                            and tp.CodProductoConsulta = pc.CodProductoConsulta
                                            and tp.CodProductoConsulta = @CodProducto and rc.Fecha >= @Fecha";

            sysnet.DB.Parameters.Add("@CodProducto", codProducto);
            sysnet.DB.Parameters.Add("@Fecha", Convert.ToDateTime(FechaDesde));

            sysnet.DB.FillDataSet(sql, ds, "RegistroContacto");
            return ds.Tables[0];
        }
    }
}
