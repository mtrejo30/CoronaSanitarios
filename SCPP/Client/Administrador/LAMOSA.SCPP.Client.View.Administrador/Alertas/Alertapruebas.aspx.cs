﻿using System;
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

using Lamosa.SCPP.Common.Entities.Interfaces;
using Lamosa.SCPP.Common.Entities.ClassImpl;

namespace LAMOSA.SCPP.Client.View.Administrador.Alertas
{
    public partial class Alertapruebas : ReporteBase
    {
        #region Constants
        
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
                    ddlPlanta.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlantaCbo(), "DescripcionPlanta", "ClavePlanta"));
                    ddlProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(ddlPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
                 //   Instaciar el servicio.

               //     ddlTipoAlerta.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));
                 //  ITipoAlerta i = new TipoAlerta();
                  //IList <ITipoAlerta> Lista = Alerta.ObtenerTipoAlerta(i);
                
              // ddlTipoAlerta.Items.AddRange(GetItemsConSeleccioneTodos(Alerta.ObtenerTipoAlerta(i), "DesTipoArticulo", "CodTipoArticulo");
                    
                    //ddlTipoAlerta.DataSource = Alerta.ObtenerTipoAlerta(i);
                    //ddlTipoAlerta.DataTextField = "DescripcionTipoAlerta";
                    //ddlTipoAlerta.DataValueField = "ClaveTipoAlerta";
                    //ddlTipoAlerta.DataBind();
                    //ddlTipoAlerta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    //ddlTipoAlerta.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
/*
                    ddlPlanta.DataSource = new Combos().Get_Planta_RolCbo(user.CodRol);
                    ddlPlanta.DataTextField = "descripcionPlanta";
                    ddlPlanta.DataValueField = "ClavePlanta";
                    ddlPlanta.DataBind();
                    ddlPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    ddlPlanta.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                    llenaGrid();*/

                    /*
                    ddlProceso.DataSource = new Combos().Get_ProcesoCbo();
                    ddlProceso.DataTextField = "DescripcionProceso";
                    ddlProceso.DataValueField = "ClaveProceso";
                    ddlProceso.DataBind();
                    ddlProceso.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
                    ddlProceso.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                    ddlProceso.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerProcesoCbo(int.Parse(ddlPlanta.SelectedValue)), "DescripcionProceso", "ClaveProceso"));
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

        }
        private void llenaGrid()
        {
           // Planta.Value = ((DropDownList)Page.Master.FindControl("cmbPlanta")).SelectedItem.Text;

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerTurnos();
            UltraWebGrid1.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

                UltraWebGrid1.Columns[4].Hidden = true;
                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[7].Hidden = true;

                UltraWebGrid1.Columns[0].Header.Caption = "Clave turno";
                UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
                UltraWebGrid1.Columns[2].Header.Caption = "Hora inicio";
                UltraWebGrid1.Columns[3].Header.Caption = "Hora fin";
                UltraWebGrid1.Columns[6].Header.Caption = "Activo";

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;


                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[2].Format = "HH:mm";
                UltraWebGrid1.Columns[3].Format = "HH:mm";
            }
        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave turno");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Hora inicio");
            UltraWebGrid1.Columns.Add("Hora fin");
            UltraWebGrid1.Columns.Add("Activo");


            UltraWebGrid1.Columns[0].Header.Caption = "Clave turno";
            UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[2].Header.Caption = "Hora inicio";
            UltraWebGrid1.Columns[3].Header.Caption = "Hora fin";
            UltraWebGrid1.Columns[4].Header.Caption = "Activo";

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;



            UltraWebGrid1.Columns[4].Width = 70;
       
        }
        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.Turno t = new LAMOSA.SCPP.Server.BusinessEntity.Turno();

            try
            {
              
                int codTutno = -1;
                int.TryParse(hddCodTurno.Value, out codTutno);
                t.CodTurno = codTutno;
                t.DesTurno = hddDesTurno.Value;
                t.HoraInicio = DateTime.Parse(hddHoraInicio.Value.ToString());
                t.HoraFin = DateTime.Parse(hddHoraFin.Value.ToString());
                if (hddActivo.Value.ToLower() == @"'true'")
                {
                    t.Activo = true;
                }
                else
                {
                    t.Activo = false;
                }

                svc.GuardarTurno(t);
                llenaGrid();
                WebAsyncRefreshPanel1.DataBind();
            }
            catch (Exception err)
            {
                
                throw err;
            }
        }

        protected void BotonEliminar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.Turno t = new LAMOSA.SCPP.Server.BusinessEntity.Turno();

            try
            {

               

                svc.EliminaTurno(int.Parse(hddCodTurno.Value));
                llenaGrid();
                WebAsyncRefreshPanel1.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            llenaGrid();
        }


        protected void btnExporta_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Turno.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerTurnos();
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Turno)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

        #endregion

        #endregion

    }
}
