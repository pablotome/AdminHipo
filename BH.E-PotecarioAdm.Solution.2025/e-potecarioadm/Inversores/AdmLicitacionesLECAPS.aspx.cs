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
    public partial class AdmLicitacionesLECAPS : WebFormBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CheckSecurity("ADMINVERSORES,ADMINISTRACION");

            this.MenuTab2.ItemsMenu = this.GetItemsMenuPrincipal();
            this.MenuTab2.CurrentMenuItem = "AdmLicitacionesLECAPS";

            if (!Page.IsPostBack)
            {
                //Parametro p = ParametrosDB.ObtenerParametro("FechaUltimaLicitacionLEBACS");
                //fechaUltimaLicitacion.Value = DateTime.ParseExact(p.Valor, "yyyyMMdd", null).ToString("dd-MM-yyyy");
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            var calendario = ServicioSistema<CalendarioLicitacionesLECAPS>.GetAll().ToList().OrderBy(x => x.Fecha);

            dgvCalendario.DataSource = calendario;

            dgvCalendario.DataBind();

            var resultado = ServicioSistema<ResultadoUltimaLicitacionLECAPS>.GetAll().ToList();

            foreach (var item in resultado)
            {
                item.PrecioUnitrade = ObtenerPrecioUnitradeable(item.FechaEmision, item.FechaLiquidacion, item.FechaVencimiento, item.TM, item.TNA);
            }

            foreach (var item in resultado)
            {
                item.DIAS360Emision = item.FechaVencimiento.Days360(item.FechaEmision);
                item.DIAS360Liquidacion = item.FechaVencimiento.Days360(item.FechaLiquidacion);
            }

            dgvResultado.DataSource = resultado.ToList<ResultadoUltimaLicitacionLECAPS>().OrderByDescending(y => y.EsUltimaLicitacion).ThenBy(x => x.FechaEmision).ThenBy(x => x.Plazo);
            dgvResultado.DataBind();
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

            ServicioSistema<CalendarioLicitacionesLECAPS>.Delete(new CalendarioLicitacionesLECAPS { IdCalendarioLicitacionesLECAPS = Convert.ToInt32(gvr.Cells[0].Text) });
            var items = ServicioSistema<CalendarioLicitacionesLECAPS>.GetAll()
                          .ToList().OrderBy(x => x.Fecha);

            //List assigned to DataGridView
            dgvCalendario.DataSource = items;
            dgvCalendario.DataBind();

        }

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

                ServicioSistema<CalendarioLicitacionesLECAPS>.SaveOrUpdate(new CalendarioLicitacionesLECAPS { Fecha = Convert.ToDateTime(fecha.Value), HorarioLicitacionHB = horario.Value });
                var item = ServicioSistema<CalendarioLicitacionesLECAPS>.GetAll()
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

            ServicioSistema<ResultadoUltimaLicitacionLECAPS>.Delete(new ResultadoUltimaLicitacionLECAPS { IdResultadoUltimaLicitacionLECAPS = Convert.ToInt32(gvr.Cells[0].Text) });
            #region Bindind
            var resultadoUltima = ServicioSistema<ResultadoUltimaLicitacionLECAPS>.GetAll().ToList().OrderByDescending(y => y.EsUltimaLicitacion).ThenBy(x => x.Plazo);
            foreach (var item in resultadoUltima) { item.PrecioUnitrade = ObtenerPrecioUnitradeable(item.FechaEmision, item.FechaLiquidacion, item.FechaVencimiento, item.TM, item.TNA); }
            foreach (var item in resultadoUltima) { item.DIAS360Emision = item.FechaVencimiento.Days360(item.FechaEmision); }
            foreach (var item in resultadoUltima) { item.DIAS360Liquidacion = item.FechaVencimiento.Days360(item.FechaLiquidacion); }
            dgvResultado.DataSource = resultadoUltima;
            dgvResultado.DataBind();
            #endregion
        }

        protected void btnNuevoResultado_Click(object sender, EventArgs e)
        {
            //int v = 0;
            //decimal d = 0.00000M;

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

                ResultadoUltimaLicitacionLECAPS result = new ResultadoUltimaLicitacionLECAPS();
                result.Plazo = Convert.ToInt32(plazo.Value);
                result.FechaEmision = Convert.ToDateTime(fechaEmision.Value);
                result.FechaLiquidacion = Convert.ToDateTime(fechaLiquidacion.Value);
                result.FechaVencimiento = Convert.ToDateTime(fechaVencimiento.Value);
                result.TM = Convert.ToDecimal(TM.Value);
                result.TNA = Convert.ToDecimal(TNA.Value);
                result.EsUltimaLicitacion = EsUltimaLicitacion.Checked;

                ServicioSistema<ResultadoUltimaLicitacionLECAPS>.SaveOrUpdate(result);

                #region Bindind
                var resultadoUltima = ServicioSistema<ResultadoUltimaLicitacionLECAPS>.GetAll().ToList().OrderByDescending(y => y.EsUltimaLicitacion).ThenBy(x => x.Plazo);
                foreach (var item in resultadoUltima) { item.PrecioUnitrade = ObtenerPrecioUnitradeable(item.FechaEmision,item.FechaLiquidacion, item.FechaVencimiento, item.TM, item.TNA); }
                foreach (var item in resultadoUltima) { item.DIAS360Emision = item.FechaVencimiento.Days360(item.FechaEmision); }
                foreach (var item in resultadoUltima) { item.DIAS360Liquidacion = item.FechaVencimiento.Days360(item.FechaLiquidacion); }
                dgvResultado.DataSource = resultadoUltima;
                dgvResultado.DataBind();
                #endregion
            }
            catch (Exception)
            {
                lblErroresRes.Visible = true;
                lblErroresRes.Text = "Error al Crear Resultado";
            }
        }

        protected void btnFechaUltimaLicitacion_Click(object sender, EventArgs e)
        {
            Parametro p = ParametrosDB.ObtenerParametro("FechaUltimaLicitacionLECAPS");
            p.Valor = DateTime.ParseExact(fechaUltimaLicitacion.Value, "dd-MM-yyyy", null).ToString("yyyyMMdd");

            ParametrosDB.ActualizarParametro(p);

        }

        /*private int ObtenerDias360(DateTime FE, DateTime FV)
        {
            return (FV.Day - FE.Day) + ((FV.Month - FE.Month) * 30) + ((FV.Year - FE.Year) * 360);
        }*/

        private decimal ObtenerPrecioUnitradeable(DateTime FechaEmision, DateTime FechaLiquidacion, DateTime FechaVto, decimal TM, decimal TNA)
        {
            var DIA360Emision = FechaVto.Days360(FechaEmision);
            var DIA360Liquidacion = FechaVto.Days360(FechaLiquidacion);

            var _DIA360Emision = Math.Pow(Convert.ToDouble((1 + (Convert.ToDouble(TM) / 100))), Convert.ToDouble((Convert.ToDouble(DIA360Emision) / 360) * 12));
            var _DIA360Liquidacion = ((Math.Round((Convert.ToDouble(TNA) / 100), 6) * (Convert.ToDouble(DIA360Liquidacion) / 360)) + 1);

            return decimal.Round((decimal)(1 * (_DIA360Emision / _DIA360Liquidacion) * 1000), 2);
        }

        private bool existeFechaCalendario(DateTime fecha)
        {
            return ServicioSistema<CalendarioLicitacionesLECAPS>.Get(calendario => calendario.Fecha == fecha).Count() > 0;
        }


    }
}