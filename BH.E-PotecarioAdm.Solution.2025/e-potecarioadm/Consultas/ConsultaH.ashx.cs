using System;
using System.IO;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using BH.EPotecario.Adm.Componentes;

namespace BH.EPotecario.Adm.Consultas
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ConsultaH : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string strResponse = string.Empty;
            try
            {
                if (context.Request["Accion"] != null)
                {
                    if (context.Request["Accion"] == "HabilitaTemaProducto")
                    {
                        strResponse = HabilitaTemaProducto(context);
                        context.Response.ClearContent();
                        context.Response.ContentType = "application/json";
                        context.Response.Write(strResponse);
                    }
                    else if (context.Request["Accion"] == "EliminaTemaProducto")
                    {
                        strResponse = EliminaTemaProducto(context);
                        context.Response.ClearContent();
                        context.Response.ContentType = "application/json";
                        context.Response.Write(strResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                context.Response.ClearContent();
                context.Response.ContentType = "text/xml";
                context.Response.StatusCode = 500;
                context.Response.Write("<xmlOut>" + ex.Message + "</xmlOut>");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string HabilitaTemaProducto(HttpContext context)
        {
            //codProdTem
            ConsultaMail cm = new ConsultaMail();
            TemaProducto tp = null;

            try
            {
                if (context.Request.Form["codProdTem"].ToString() != string.Empty)
                    tp = cm.ObtenerTemaProducto(int.Parse(context.Request.Form["codProdTem"].ToString()));
                else
                    tp = new TemaProducto();
                if (tp != null)
                {
                    if (tp.Habilitado)
                        tp.Habilitado = false;
                    else
                        tp.Habilitado = true;

                    cm.AgregarTemaProducto(tp);

                    return string.Format("[{{resultado:'{0}'}}]", tp.CodTemaProducto);
                }
                else
                    return string.Format("[{{resultado:'{0}'}}]", "Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string EliminaTemaProducto(HttpContext context)
        {
            //codProdTem
            ConsultaMail cm = new ConsultaMail();
            TemaProducto tp = null;

            try
            {
                if (context.Request.Form["codProdTem"].ToString() != string.Empty)
                    tp = cm.ObtenerTemaProducto(int.Parse(context.Request.Form["codProdTem"].ToString()));
                if (tp != null)
                {
                    cm.EliminarTemaProducto(tp.CodTemaProducto);

                    return string.Format("[{{resultado:'{0}'}}]", tp.CodTemaProducto);
                }
                else
                    return string.Format("[{{resultado:'{0}'}}]", "Error");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

