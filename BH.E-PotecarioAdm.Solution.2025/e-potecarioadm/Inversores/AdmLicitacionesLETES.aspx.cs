using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MicroSitios.Componentes;
using MicroSitios.Dominio.Entidades;
using BH.EPotecario.Adm.Componentes;
using System.Data;

namespace BH.EPotecario.Adm.Inversores
{
    public partial class AdmLicitacionesLETES : WebFormBase
    {
        #region Metodos
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");

            this.MenuTab2.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab2.CurrentMenuItem = "AdmLicitacionesLETES";

            if (!Page.IsPostBack)
            {
                Parametro p = ParametrosDB.ObtenerParametro("FechaUltimaLicitacionLETES");
                fechaUltimaLicitacion.Value = DateTime.ParseExact(p.Valor, "yyyyMMdd", null).ToString("dd-MM-yyyy");
                CargarGrilla();
            }
        }
        #endregion

        #region Manejadores
        public void CargarGrilla()
        {
            try
            {
                var calendario = ServicioSistema<CalendarioLicitacionesLETES>.GetAll().ToList().OrderBy(x => x.Fecha);

                dgvCalendario.DataSource = calendario;

                dgvCalendario.DataBind();

                var resultado = ServicioSistema<ResultadoUltimaLicitacionLETES>.GetAll().ToList().OrderByDescending(y => y.EsUltimaLicitacion).ThenBy(x => x.Plazo);

                foreach (var item in resultado)
                {
                    item.PrecioCorte = ObtenerPrecio(item.TasaCorte, item.Plazo);
                }

                dgvResultado.DataSource = resultado;

                dgvResultado.DataBind();

                ValorTNC();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Metodos

        protected void btnNuevoCalendario_Click(object sender, EventArgs e)
        {
            //show the personal tab
            navCalendario.Attributes.Add("class", "active");
            calendario.Attributes.Add("class", "tab-pane active");

            //hide the employment tab (menu and content)
            resultado.Attributes.Remove("class");
            resultado.Attributes.Add("class", "tab-pane");
            navResultado.Attributes.Remove("class");
            navResultado.Attributes.Add("class", "nav nav-tabs");
            try
            {
                lblErroresCal.Visible = false;

                if (horario.Value.Trim() == "")
                {
                    lblErroresCal.Visible = true;
                    lblErroresCal.Text = "El campo Horario no puede estar vacio";
                    return;
                }

                if (existeFechaCalendario(DateTime.Parse(fecha.Value)))
                {
                    lblErroresCal.Visible = true;
                    lblErroresCal.Text = "La Fecha ya existe";
                    return;
                }

                ServicioSistema<CalendarioLicitacionesLETES>.SaveOrUpdate(new CalendarioLicitacionesLETES { Fecha = Convert.ToDateTime(fecha.Value), HorarioLicitacionHB = horario.Value });
                var item = ServicioSistema<CalendarioLicitacionesLETES>.GetAll()
                          .ToList().OrderBy(x => x.Fecha);

                //List assigned to DataGridView
                dgvCalendario.DataSource = item;
                dgvCalendario.DataBind();

            }
            catch (Exception)
            {
                lblErroresCal.Visible = true;
                lblErroresCal.Text = "Error al Crear Calendario";
            }
        }

        protected void btnNuevoResultado_Click(object sender, EventArgs e)
        {
            int v = 0;
            decimal d = 0.00000M;

            //show the employment tabl
            navResultado.Attributes.Add("class", "active");
            resultado.Attributes.Add("class", "tab-pane active");

            //hide the personal tab (menu and content)
            calendario.Attributes.Remove("class");
            calendario.Attributes.Add("class", "tab-pane");
            navCalendario.Attributes.Remove("class");
            navCalendario.Attributes.Add("class", "nav nav-tabs");
            try
            {
                lblErroresRes.Visible = false;

                if (plazo.Value.Trim() == "")
                {
                    lblErroresRes.Visible = true;
                    lblErroresRes.Text = "El campo Plazo no puede estar vacio";
                    return;
                }

                if (!int.TryParse(plazo.Value, out v))
                {
                    lblErroresRes.Visible = true;
                    lblErroresRes.Text = "El campo Plazo debe ser un número entero";
                    return;
                }

                if (tasaDeCorte.Value.Trim() == "")
                {
                    lblErroresRes.Visible = true;
                    lblErroresRes.Text = "El campo Tasa de Corte no puede estar vacio";
                    return;
                }

                if (!decimal.TryParse(tasaDeCorte.Value, out d))
                {
                    lblErroresRes.Visible = true;
                    lblErroresRes.Text = "El campo Tasa de Corte debe ser un número";
                    return;
                }

                if (existePlazoResultado(int.Parse(plazo.Value), EsUltimaLicitacion.Checked))
                {
                    lblErroresRes.Visible = true;
                    lblErroresRes.Text = "El Plazo ya existe";
                    return;
                }

                ServicioSistema<ResultadoUltimaLicitacionLETES>.SaveOrUpdate(new ResultadoUltimaLicitacionLETES { Plazo = Convert.ToInt32(plazo.Value), TasaCorte = Convert.ToDecimal(tasaDeCorte.Value), EsUltimaLicitacion = EsUltimaLicitacion.Checked });
                var items = ServicioSistema<ResultadoUltimaLicitacionLETES>.GetAll()
                              .ToList().OrderByDescending(y => y.EsUltimaLicitacion).ThenBy(x => x.Plazo);

                foreach (var item in items)
                {
                    item.PrecioCorte = ObtenerPrecio(item.TasaCorte, item.Plazo);
                }

                //List assigned to DataGridView
                dgvResultado.DataSource = items;
                dgvResultado.DataBind();
            }
            catch (Exception)
            {
                lblErroresRes.Visible = true;
                lblErroresRes.Text = "Error al Crear Resultado";
            }
        }

        protected void btnFechaUltimaLicitacion_Click(object sender, EventArgs e)
        {
            Parametro p = ParametrosDB.ObtenerParametro("FechaUltimaLicitacionLETES");
            p.Valor = DateTime.ParseExact(fechaUltimaLicitacion.Value, "dd-MM-yyyy", null).ToString("yyyyMMdd");

            ParametrosDB.ActualizarParametro(p);

        }

        private decimal ObtenerPrecio(decimal TasaCorte, decimal Plazo)
        {
            return (1 / (((TasaCorte / 100 * Plazo) / 365) + 1));
        }

        private bool existePlazoResultado(int Plazo, bool EsUltimaLicitacion)
        {
            return ServicioSistema<ResultadoUltimaLicitacionLEBACS>.Get(resultado => resultado.Plazo == Plazo && resultado.EsUltimaLicitacion == EsUltimaLicitacion).Count() > 0;
        }

        private bool existeFechaCalendario(DateTime fecha)
        {
            return ServicioSistema<CalendarioLicitacionesLETES>.Get(calendario => calendario.Fecha == fecha).Count() > 0;
        }

        protected void btnBorrarCalendario_Click(object sender, EventArgs e)
        {
            //show the personal tab
            navCalendario.Attributes.Add("class", "active");
            calendario.Attributes.Add("class", "tab-pane active");

            //hide the employment tab (menu and content)
            resultado.Attributes.Remove("class");
            resultado.Attributes.Add("class", "tab-pane");
            navResultado.Attributes.Remove("class");
            navResultado.Attributes.Add("class", "nav nav-tabs");
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            ServicioSistema<CalendarioLicitacionesLETES>.Delete(new CalendarioLicitacionesLETES { IdCalendarioLicitacionesLETES = Convert.ToInt32(gvr.Cells[0].Text) });
            var items = ServicioSistema<CalendarioLicitacionesLETES>.GetAll()
                          .ToList().OrderBy(x => x.Fecha);

            //List assigned to DataGridView
            dgvCalendario.DataSource = items;
            dgvCalendario.DataBind();

        }

        protected void btnBorrarResultado_Click(object sender, EventArgs e)
        {
            //show the employment tabl
            navResultado.Attributes.Add("class", "active");
            resultado.Attributes.Add("class", "tab-pane active");

            //hide the personal tab (menu and content)
            calendario.Attributes.Remove("class");
            calendario.Attributes.Add("class", "tab-pane");
            navCalendario.Attributes.Remove("class");
            navCalendario.Attributes.Add("class", "nav nav-tabs");
            Button btn = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;

            ServicioSistema<ResultadoUltimaLicitacionLETES>.Delete(new ResultadoUltimaLicitacionLETES { IdResultadoUltimaLicitacionLETES = Convert.ToInt32(gvr.Cells[0].Text) });
            var items = ServicioSistema<ResultadoUltimaLicitacionLETES>.GetAll()
                          .ToList().OrderByDescending(y => y.EsUltimaLicitacion).ThenBy(x => x.Plazo);

            foreach (var item in items)
            {
                item.PrecioCorte = ObtenerPrecio(item.TasaCorte, item.Plazo);
            }

            //List assigned to DataGridView
            dgvResultado.DataSource = items;
            dgvResultado.DataBind();
        }


        protected void ValorTNC()
        {
            tncMin.Value = ParametrosDB.ObtenerParametro("TNCMIN").Valor;
            tncMax.Value = ParametrosDB.ObtenerParametro("TNCMAX").Valor;
            tipoCambio.Value = ParametrosDB.ObtenerParametro("TIPOCAMBIO").Valor;

        }

        protected void btnActualizarTNC_Click(object sender, EventArgs e)
        {
            if (tncMin.Value != ParametrosDB.ObtenerParametro("TNCMIN").Valor || tncMax.Value != ParametrosDB.ObtenerParametro("TNCMAX").Valor || tipoCambio.Value != ParametrosDB.ObtenerParametro("TIPOCAMBIO").Valor)
            {
                #region ParamMin
                Parametro paramMin = new Parametro();
                paramMin = ParametrosDB.ObtenerParametro("TNCMIN");
                paramMin.Valor = tncMin.Value;
                ParametrosDB.ActualizarParametro(paramMin);
                #endregion

                #region ParamMax
                Parametro paramMax = new Parametro();
                paramMax = ParametrosDB.ObtenerParametro("TNCMAX");
                paramMax.Valor = tncMax.Value;
                ParametrosDB.ActualizarParametro(paramMax);
                #endregion

                #region ParamTipoCambio
                Parametro paramTipoCambio = new Parametro();
                paramTipoCambio = ParametrosDB.ObtenerParametro("TIPOCAMBIO");
                paramTipoCambio.Valor = tipoCambio.Value;
                ParametrosDB.ActualizarParametro(paramTipoCambio);
                #endregion
            }

        }
        #endregion
    }
}