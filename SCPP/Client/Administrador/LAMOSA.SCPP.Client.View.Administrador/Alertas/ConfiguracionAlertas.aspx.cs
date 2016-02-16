
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using Infragistics.WebUI.Shared;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Infragistics.Web.UI.ListControls;
using System.Collections.Generic;
using Infragistics.WebUI.UltraWebGrid;
using System.IO;
using System.ComponentModel;

using Infragistics.Shared;
using Infragistics.Excel;
using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;

using LAMOSA.SCPP.Client.View.Administrador.svcConfigAlerta;

namespace LAMOSA.SCPP.Client.View.Administrador.Alertas
{

    public partial class ConfiguracionAlertas : ReporteBase
    {
       

        #region Methods

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            //hdnId.Value = "25";
            if (!Page.IsPostBack && !Page.IsCallback)
            {
               Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    if (string.IsNullOrEmpty(Request.Params["config"]))
                        hdnId.Value = "0";
                    else
                        hdnId.Value = Request.Params["config"];
                    
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    svcTipoAlerta.ITipoAlertaServiceManager svcTipoAlertaSM = new svcTipoAlerta.TipoAlertaServiceManagerClient();
                    svcFrecuencia.IFrecuenciaServiceManager svcFrecuenciaSM = new svcFrecuencia.FrecuenciaServiceManagerClient();
                    svcAlerta.IAlertaServiceManager svcAlertaSM = new svcAlerta.AlertaServiceManagerClient();


                    ddlTipoArticulo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));

                    ddlColor.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerColores(), "ClaveColor", "CodColor"));

                    ddlTurno.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTurnos(), "DesTurno", "CodTurno"));

                    ddlZonaDefecto.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerZonaDefectoCbo(), "DesZonaDefecto", "CodZonaDefecto"));

                    ddlPlanta.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlantaCbo(), "DescripcionPlanta", "ClavePlanta"));

                    ddlDesperdicio.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTiposDefecto(), "DesTipoDefecto", "CodTipoDefecto"));

                    dllTipoAlerta.DataSource = svcTipoAlertaSM.Obtener(new svcTipoAlerta.TipoAlerta());
                    dllTipoAlerta.DataTextField = "Descripcion";
                    dllTipoAlerta.DataValueField = "Codigo";
                    dllTipoAlerta.DataBind();

                    dllFrecuencia.DataSource = svcFrecuenciaSM.Obtener(new svcFrecuencia.Frecuencia());
                    dllFrecuencia.DataTextField = "Descripcion";
                    dllFrecuencia.DataValueField = "Codigo";
                    dllFrecuencia.DataBind();

                    if (hdnId.Value != "0")
                        llenaControles(int.Parse(hdnId.Value));
                    else
                    {
                        ddlProceso.Items.AddRange(GetItemsConSeleccioneTodos(null, "", ""));
                        ddlMaquina.Items.AddRange(GetItemsConSeleccioneTodos(null, "", ""));
                        ddlModelo.Items.AddRange(GetItemsConSeleccioneTodos(null, "", ""));
                        ddlCalidad.Items.AddRange(GetItemsConSeleccioneTodos(null, "", ""));
                        ddlDefecto.Items.AddRange(GetItemsConSeleccioneTodos(null, "", "")); 
                    }
                    
                    LNuevo.Visible = false;
                    ViewState["nuevo"] = false;
                    ViewState["editar"] = false;
                    
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar
                                break;
                            case 2: //Exportar
                                break;
                            case 3: //Nuevo
                                LNuevo.Visible = true;
                                btnGuardar.Visible=true;
                                ViewState["nuevo"] = true;
                                break;
                            case 4: //Editar
                                ViewState["editar"] = true;
                                btnGuardar.Visible = true;
                                break;
                        }

                    }
                    
 
                }
            

            }
        }

        private void llenaControles(int id)
        {
            
            IConfigAlertaServiceManager svcConfigAlertaSM = new ConfigAlertaServiceManagerClient();

            svcConfigAlerta.ConfigAlerta config = new ConfigAlerta();
            config.Codigo = id;
            List<object> configuraciones =  svcConfigAlertaSM.Obtener(config);


            if (configuraciones !=null && configuraciones.Count > 0) 
            {
                ConfigAlerta configuracion = (ConfigAlerta)configuraciones[0];

                hdnId.Value = configuracion.Codigo.ToString();
                /*alerta*/
                txtAsunto.Text = ((Alerta)configuracion.Alerta).Asunto;
                //txtClave.Text = ((Alerta)configuracion.Alerta).Descripcion;
                txtMensaje.Text = ((Alerta)configuracion.Alerta).Mensaje;

                if (((Alerta)configuracion.Alerta).TipoAlerta == null)
                    dllTipoAlerta.SelectedValue = "-1";
                else
                    dllTipoAlerta.SelectedValue = ((TipoAlerta)((Alerta)configuracion.Alerta).TipoAlerta).Codigo.ToString();

                if (((Alerta)configuracion.Alerta).Frecuencia == null)
                    dllTipoAlerta.SelectedValue = "-1";
                else
                    dllFrecuencia.SelectedValue = ((Frecuencia)((Alerta)configuracion.Alerta).Frecuencia).Codigo.ToString();


                if (((Alerta)configuracion.Alerta).Destinatarios != null && ((Alerta)configuracion.Alerta).Destinatarios.Count > 0)
                {
                    if (((Alerta)configuracion.Alerta).Destinatarios[0] != null)
                        txtDestinatario.Text = ((Destinatario)((Alerta)configuracion.Alerta).Destinatarios[0]).Correo.ToString();
                }

                if (configuracion.TipoDefecto == null)
                    ddlDesperdicio.SelectedValue = "-1";
                else
                    ddlDesperdicio.SelectedValue = ((svcConfigAlerta.TipoDefecto)configuracion.TipoDefecto).Codigo.ToString();
                

                ddlColor.SelectedValue = configuracion.CodColor.ToString();
                txtLimiteMaximo.Text = configuracion.CodLimiteMaximo.ToString();
                txtLimiteMinimo.Text = configuracion.CodLimiteMinimo.ToString();
                txtOperador.Text = configuracion.CodOperador < 0 ? "" : configuracion.CodOperador.ToString();
                ddlTipoArticulo.SelectedValue = configuracion.CodTipoArticulo.ToString();
                ddlTurno.SelectedValue = configuracion.CodTurno.ToString();
                
                
                GetModeloCbo(configuracion.CodTipoArticulo);
                ddlModelo.SelectedValue = configuracion.Modelo.ToString();


                int codPlanta = -1;
                if (configuracion.Planta != null)
                    codPlanta = ((svcConfigAlerta.Planta)configuracion.Planta).Codigo;

                ddlPlanta.SelectedValue = codPlanta.ToString();

                int codProceso = -1;
                if (configuracion.Proceso != null)
                    codProceso = ((svcConfigAlerta.Proceso)configuracion.Proceso).Codigo;

                GetProcesoCbo(codPlanta);
                ddlProceso.SelectedValue = codProceso.ToString();

                int codMaquina = configuracion.Maquina;
                GetMaquinaCbo(codPlanta, codProceso);
                ddlMaquina.SelectedValue = codMaquina.ToString();

                ddlZonaDefecto.SelectedValue = configuracion.CodZonaDefecto.ToString();

                GetPlantaCalidadCbo(codPlanta);
                ddlCalidad.SelectedValue = configuracion.ClaveCalidad;

                if (configuracion.TipoDefecto == null)
                    ddlDefecto.SelectedValue = "-1";
                else
                {
                    int codTipoDefecto = ((svcConfigAlerta.TipoDefecto)configuracion.TipoDefecto).Codigo;
                    GetDefectoCbo(codProceso,codTipoDefecto);
                    ddlDefecto.SelectedValue = configuracion.CodDefecto.ToString();
                }
            }
        }
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            svcConfigAlerta.IConfigAlertaServiceManager svc = new svcConfigAlerta.ConfigAlertaServiceManagerClient();
            svcConfigAlerta.ConfigAlerta obj = new LAMOSA.SCPP.Client.View.Administrador.svcConfigAlerta.ConfigAlerta();

            obj = llenaObjeto();


            try
            {
                if (obj.Codigo <= 0)
                {
                    if ((bool)ViewState["nuevo"])
                    {
                        int respuesta = svc.Agregar(obj);
                        if (respuesta > 0)
                            hdnId.Value = respuesta.ToString();

                        if (respuesta > 0)
                            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('Se guardo correctamente');</script>");
                        else
                            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('Ocurrio un error al momento de guardar');</script>");
                    }
                    else
                        CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('No tiene permiso de agregar un registro nuevo');</script>");
                }
                else
                {
                    if ((bool)ViewState["editar"])
                    {
                        bool actualizado = svc.Actualizar(obj);
                        if (actualizado)
                            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('Se actualizo el registro correctamente');</script>");
                        else
                            CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('Ocurrio un error al momento de actualizar');</script>");
                    }
                    else
                        CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('No tiene permiso de actualizar');</script>");
                }
            }
            catch (Exception ex)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + ex.Message +"');</script>");
            }
            
        }

        private svcConfigAlerta.ConfigAlerta llenaObjeto()
        { 
            svcConfigAlerta.ConfigAlerta obj = new LAMOSA.SCPP.Client.View.Administrador.svcConfigAlerta.ConfigAlerta();
  
            obj.Alerta = new svcConfigAlerta.Alerta();
            ((svcConfigAlerta.Alerta)obj.Alerta).Codigo = string.IsNullOrEmpty(hdnId.Value) ? 0 : int.Parse(hdnId.Value);
            ((svcConfigAlerta.Alerta)obj.Alerta).Asunto = txtAsunto.Text;
            ((svcConfigAlerta.Alerta)obj.Alerta).Clave = "";
            ((svcConfigAlerta.Alerta)obj.Alerta).Mensaje = txtMensaje.Text;
            ((svcConfigAlerta.Alerta)obj.Alerta).Descripcion = "";

            ((svcConfigAlerta.Alerta)obj.Alerta).TipoAlerta = new svcConfigAlerta.TipoAlerta();
            ((svcConfigAlerta.TipoAlerta)((svcConfigAlerta.Alerta)obj.Alerta).TipoAlerta).Codigo = int.Parse(dllTipoAlerta.SelectedValue);

            ((svcConfigAlerta.Alerta)obj.Alerta).Frecuencia = new svcConfigAlerta.Frecuencia();
            ((svcConfigAlerta.Frecuencia)((svcConfigAlerta.Alerta)obj.Alerta).Frecuencia).Codigo = int.Parse(dllFrecuencia.SelectedValue);
            
            ((svcConfigAlerta.Alerta)obj.Alerta).Destinatarios = new List<object>();
            svcConfigAlerta.Destinatario dest = new svcConfigAlerta.Destinatario();
            dest.Codigo = 0;
            dest.Correo = txtDestinatario.Text;
            dest.Clave = "";
            dest.Descripcion = "";
            dest.Nombre = "";
            ((svcConfigAlerta.Alerta)obj.Alerta).Destinatarios.Add(dest);

            obj.Clave = "";

            if (ddlCalidad.SelectedValue == "-1" || ddlCalidad.SelectedValue == "0")
                obj.ClaveCalidad = string.Empty;
            else
                obj.ClaveCalidad = ddlCalidad.SelectedValue;

            obj.CodColor = int.Parse(ddlColor.SelectedValue);
            obj.Codigo = string.IsNullOrEmpty(hdnId.Value) ? 0 : int.Parse(hdnId.Value);
            obj.CodLimiteMaximo = int.Parse(string.IsNullOrEmpty(txtLimiteMaximo.Text) ? "0" : txtLimiteMaximo.Text);
            obj.CodLimiteMinimo = int.Parse(string.IsNullOrEmpty(txtLimiteMinimo.Text) ? "0" : txtLimiteMinimo.Text);
            obj.CodOperador = int.Parse(string.IsNullOrEmpty(txtOperador.Text) ? "0" : txtOperador.Text);
            obj.CodTipoArticulo = int.Parse(ddlTipoArticulo.SelectedValue);
            obj.CodTurno = int.Parse(ddlTurno.SelectedValue);
            obj.Descripcion = "";
            obj.Maquina = int.Parse(ddlMaquina.SelectedValue);
            obj.Modelo = int.Parse(ddlModelo.SelectedValue);
            obj.CodDefecto = int.Parse(ddlDefecto.SelectedValue);
            obj.CodZonaDefecto = int.Parse(ddlZonaDefecto.SelectedValue);

            obj.Planta = new svcConfigAlerta.Planta();
            ((svcConfigAlerta.Planta)obj.Planta).Codigo = int.Parse(ddlPlanta.SelectedValue);

            obj.Proceso = new svcConfigAlerta.Proceso();
            ((svcConfigAlerta.Proceso)obj.Proceso).Codigo = int.Parse(ddlProceso.SelectedValue);

            obj.TipoDefecto = new svcConfigAlerta.TipoDefecto();
            ((svcConfigAlerta.TipoDefecto)obj.TipoDefecto).Codigo = int.Parse(ddlDesperdicio.SelectedValue);

            return obj;
        }

        protected void ddlPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlProceso.Items.Clear();
            ddlCalidad.Items.Clear();
            int codPlanta = int.Parse(ddlPlanta.SelectedValue);
            GetProcesoCbo(codPlanta);
            GetPlantaCalidadCbo(codPlanta);
        }

        private void GetPlantaCalidadCbo(int codPlanta)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            ddlCalidad.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlantaCalidadCbo(codPlanta), "Calidad", "Calidad"));
            svc.Close();
        }

        private void GetProcesoCbo(int codPlanta)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            ddlProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(codPlanta), "DescripcionProceso", "ClaveProceso"));
            svc.Close();
        }

        private void GetDefectoCbo(int codProceso,int codTipoDefecto)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            ddlDefecto.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerDefectosCbo(codProceso, codTipoDefecto), "DesDefecto", "CodDefecto"));
            svc.Close();
        }


        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMaquina.Items.Clear();
            int codPlanta = int.Parse(ddlPlanta.SelectedValue);
            int codProceso = int.Parse(ddlProceso.SelectedValue);
            GetMaquinaCbo(codPlanta, codProceso); 
            
        }

        private void GetMaquinaCbo(int codPlanta, int codProceso)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            ddlMaquina.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerMaquina(codPlanta,codProceso), "DesMaquina", "CodMaquina"));
            svc.Close();
           
        }

        protected void ddlTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlModelo.Items.Clear();
            int codTipoArticulo = int.Parse(ddlTipoArticulo.SelectedValue);
            GetModeloCbo(codTipoArticulo);
        }

        private void GetModeloCbo(int codTipoArticulo)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            int art = codTipoArticulo;
            ddlModelo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerModelosCbo(art), "DesArticulo", "CodArticulo"));
            svc.Close();
        }
        
        #endregion

        protected void ddlDesperdicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDefecto.Items.Clear();
            int codProceso = int.Parse(ddlProceso.SelectedValue);
            int codTipoDefecto = int.Parse(ddlDesperdicio.SelectedValue);
            GetDefectoCbo(codProceso, codTipoDefecto);
        }
        #endregion


    }
}
