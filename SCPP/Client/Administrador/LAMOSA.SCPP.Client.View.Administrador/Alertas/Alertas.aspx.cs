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
//using Lamosa.SCPP.ServiceObject.Entities.Interfaces;
//using Lamosa.SCPP.ServiceObject.Entities.ClassImpl;
using Lamosa.SCPP.Common.Entities.Interfaces;
using Lamosa.SCPP.Common.Entities.ClassImpl;
using LAMOSA.SCPP.Client.View.Administrador.svcAlerta;
using LAMOSA.SCPP.Client.View.Administrador.svcConfigAlerta;
using LAMOSA.SCPP.Client.View.Administrador.svcTipoAlerta;

namespace LAMOSA.SCPP.Client.View.Administrador.Alertas
{
    public partial class Alertas : ReporteBase
    {
        #region Constants
        //private IAlertaServiceObject aAlertaSO;
        //private IAlertaServiceObject Alerta
        //{ get { return (aAlertaSO == null) ? aAlertaSO = new AlertaServiceObject() : aAlertaSO; } }
        #endregion
        #region Methods

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                LAMOSA.SCPP.Server.BusinessEntity.Usuario user = (LAMOSA.SCPP.Server.BusinessEntity.Usuario)Session["UserLogged"];
                if (user != null)
                {
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    svcTipoAlerta.ITipoAlertaServiceManager svcTipoAlertaSM = new svcTipoAlerta.TipoAlertaServiceManagerClient();
                    svcAlerta.IAlertaServiceManager svcAlertaSM = new svcAlerta.AlertaServiceManagerClient();
                    svcConfigAlerta.IConfigAlertaServiceManager svcConfigAlertaSM = new  svcConfigAlerta.ConfigAlertaServiceManagerClient();

                    ddlPlanta.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlantaCbo(), "DescripcionPlanta", "ClavePlanta"));
                    ddlProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(ddlPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
                 //   Instaciar el servicio.

               //     ddlTipoAlerta.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));
                  // ITipoAlerta i = new TipoAlerta();
                  //IList <ITipoAlerta> Lista = Alerta.ObtenerTipoAlerta(i);
                
              // ddlTipoAlerta.Items.AddRange(GetItemsConSeleccioneTodos(Alerta.ObtenerTipoAlerta(i), "DesTipoArticulo", "CodTipoArticulo");

                //  ddlTipoAlerta.Items.AddRange(GetItemsConSeleccioneObj(svcTipoAlertaSM.Obtener(new svcTipoAlerta.TipoAlerta()), "Descripcion", "Codigo"));
                  ddlTipoAlerta.Items.AddRange(GetItemsConSeleccioneTodosObj(svcTipoAlertaSM.Obtener(new svcTipoAlerta.TipoAlerta()), "Descripcion", "Codigo"));
                  llenarvacio(); 

/*
                 
                    LExport.Visible = true;
                    LAddNew.Visible = true;
                    bool editar = false;
                    foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                    {
                        switch (sp.ActionCode)
                        {
                            case 1: //Buscar

                                break;
                            case 2: //Exportar
                                LExport.Visible = true;
                                break;
                            case 3: //Nuevo
                                LAddNew.Visible = true;
                                break;
                            case 4: //Editar
                                editar = true;
                                UltraWebGrid1.DisplayLayout.AllowUpdateDefault = AllowUpdate.RowTemplateOnly;
                                break;
                        }

                    }

                    if (editar == false)
                    {
                        UltraWebGrid1.DisplayLayout.AllowUpdateDefault = AllowUpdate.No;
                    }*/
                }


            }
        }

        protected void cmbPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            ddlProceso.Items.Clear();
 
            ddlProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(ddlPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
            llenarvacio();
        }

         protected void btnBuscar_click(object sender, EventArgs e)
        {
              svcAlerta.IAlertaServiceManager svcAlertaSM = new svcAlerta.AlertaServiceManagerClient();

              svcAlerta.Alerta Alerta = new LAMOSA.SCPP.Client.View.Administrador.svcAlerta.Alerta();
              svcAlerta.ConfigAlerta configAlerta = new LAMOSA.SCPP.Client.View.Administrador.svcAlerta.ConfigAlerta();

              svcAlerta.Alerta a = new LAMOSA.SCPP.Client.View.Administrador.svcAlerta.Alerta();
              svcAlerta.TipoAlerta ta = new LAMOSA.SCPP.Client.View.Administrador.svcAlerta.TipoAlerta();
              svcAlerta.Planta pl = new LAMOSA.SCPP.Client.View.Administrador.svcAlerta.Planta();
              svcAlerta.Proceso pr = new LAMOSA.SCPP.Client.View.Administrador.svcAlerta.Proceso();

             ta.Codigo = int.Parse(ddlTipoAlerta.SelectedValue);
             pl.Codigo = int.Parse(ddlPlanta.SelectedValue);
             pr.Codigo = int.Parse(ddlProceso.SelectedValue);

             a.TipoAlerta = ta;
             configAlerta.Alerta = a;
             configAlerta.Planta = pl;
             configAlerta.Proceso = pr;

            IList<object> ListaAlerta = svcAlertaSM.ObtenerConfigAlerta(configAlerta);
            UltraWebGrid1.DataSource = ListaAlerta;
             if (ListaAlerta != null)
                 {
                    if (ListaAlerta.Count <= 0)
                    {
                        llenarvacio();
                    }
                    else
                    {
                        UltraWebGrid1.ResetColumns();
                        UltraWebGrid1.DataBind();
                        
                        UltraWebGrid1.Columns[4].Hidden = true;

                        UltraWebGrid1.Columns[0].Move(3) ;
                        UltraWebGrid1.Columns[0].Move(3);

                        UltraWebGrid1.Columns[0].Header.Caption = "Código Alerta";
                        UltraWebGrid1.Columns[1].Header.Caption = "Clave Alerta";
                        UltraWebGrid1.Columns[2].Header.Caption = "Asunto";
                        UltraWebGrid1.Columns[3].Header.Caption = "Mensaje";
            

                        UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                        UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                        UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                        UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;

                        UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                        UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                        UltraWebGrid1.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Left;

                        UltraWebGrid1.Columns[0].Width = 90;
                        UltraWebGrid1.Columns[1].Width = 100;
                        UltraWebGrid1.Columns[2].Width = 200;
                        UltraWebGrid1.Columns[3].Width = 410;
                        

                        UltraWebGrid1.Columns[5].Hidden = true;
                        UltraWebGrid1.Columns[6].Hidden = true;

                        UltraGridColumn col = new UltraGridColumn("ColBtnEliminar", "Eliminar", ColumnType.Button, null);
                        col.Width = 60;
                        col.CellButtonDisplay = CellButtonDisplay.Always;
                        col.CellStyle.HorizontalAlign = HorizontalAlign.Center;
    
                       // col.CellButtonStyle.BackgroundImage = @"http://localhost/LAMOSA.SCPP.Client.View.Administrador/Imagenes/delete.gif";
                        col.CellButtonStyle.BackgroundImage = @"../Imagenes/delete.gif";
                        col.CellButtonStyle.Width = 20;
                        col.CellButtonStyle.Height = 20;
                        col.Hidden = false;
                        //e.Layout.Grid.Columns.Add(col);
                        UltraWebGrid1.Columns.Add(col);

                        //UltraGridColumn col = new UltraGridColumn("ColBtnEliminar", "Eliminar", ColumnType.Button, null);
                        //col.Width = 70;
                        //col.CellButtonStyle.BackgroundImage = @"C:\LMSA\LAMOSA\SCPP\Client\Administrador\LAMOSA.SCPP.Client.View.Administrador\Imagenes\botonEliminar.jpg";
                        //col.CellStyle.BackgroundImage = @"C:\LMSA\LAMOSA\SCPP\Client\Administrador\LAMOSA.SCPP.Client.View.Administrador\Imagenes\botonEliminar.jpg";
                        //col.Hidden = false;
                        //UltraWebGrid1.Columns.Add(col);

                        //UltraWebGrid1.Columns.Add("Eliminar");
                        //UltraWebGrid1.Columns[7].Header.Caption = "Eliminar";
                        //UltraWebGrid1.Columns[7].Type = ColumnType.Button;
                        //UltraWebGrid1.Columns[7].Width = 70;
                    }
                 }
             else
             {
                 llenarvacio();
             }
        }


        protected void llenarvacio()
         {
            
            UltraWebGrid1.Columns.Clear();
            UltraWebGrid1.Rows.Clear();
           
            UltraWebGrid1.Columns.Add("Codigo","Codigo");
            UltraWebGrid1.Columns.Add("Clave", "Clave");
            UltraWebGrid1.Columns.Add("Asunto", "Asunto");
            UltraWebGrid1.Columns.Add("Mensaje", "Mensaje");
            UltraWebGrid1.Columns.Add("Eliminar", "Eliminar");
       
        }

      

        protected void BotonEliminar_click(object sender, EventArgs e)
        {
            

            svcAlerta.AlertaServiceManagerClient svca = new AlertaServiceManagerClient();
            svcConfigAlerta.Alerta obj = new LAMOSA.SCPP.Client.View.Administrador.svcConfigAlerta.Alerta();

           
            try
            {
                int CodigoAlerta= -1;
                int.TryParse(hddCodigo.Value, out CodigoAlerta);
                obj.Codigo = CodigoAlerta;
                svca.Eliminar(obj);
                btnBuscar_click(null, null);
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            
        }


        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            //DataSet dsReportXLS = new DataSet();
            //dsReportXLS.Tables.Add();

            //string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Turno.GetPropertyNamesArray();
            //foreach (string colname in colnames)
            //{
            //    dsReportXLS.Tables[0].Columns.Add(colname);
            //}
            //List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerTurnos();
            //foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            //{
            //    dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Turno)item).ToObjectArray());
            //}
            //ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

      
        #endregion

        //protected void UltraWebGrid1_InitializeLayout(object sender, LayoutEventArgs e)
        //{
        //    //UltraGridColumn col = new UltraGridColumn("ColBtnEliminar", "Eliminar", ColumnType.Button, null);
        //    //col.Width = 60;
        //    //col.CellButtonDisplay = CellButtonDisplay.Always;
        //    //col.CellStyle.HorizontalAlign = HorizontalAlign.Center;
        //    //col.CellButtonStyle.BackgroundImage = @"http://localhost/LAMOSA.SCPP.Client.View.Administrador/Imagenes/delete.gif";
        //    //col.CellButtonStyle.Width = 20;
        //    //col.CellButtonStyle.Height = 20;
        //    //col.Hidden = false;
        //    //e.Layout.Grid.Columns.Add(col);
        //    //UltraWebGrid1.Columns.Add(col);
        //}

        #endregion

        //protected void UltraWebGrid1_InitializeRow(object sender, RowEventArgs e)
        //{
        //    //if (e.Row.Cells.Exists("ColBtnEliminar"))
        //    //{ 
        //    //    //e.Row.Cells.FromKey("ColBtnEliminar").Text = "Eliminar";
        //    //    //e.Row.Cells.FromKey("ColBtnEliminar").Style.BackgroundImage = @"http://localhost/LAMOSA.SCPP.Client.View.Administrador/Imagenes/delete.gif";
        //    //    //e.Row.Cells.FromKey("ColBtnEliminar").Style.HorizontalAlign = HorizontalAlign.Center;
        //    //    //e.Row.Cells.FromKey("ColBtnEliminar").Style.Width = 20;
        //    //    //e.Row.Cells.FromKey("ColBtnEliminar").Style.Font.Size = 8;
                
        //    //}
        //}

    }
}
