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

namespace LAMOSA.SCPP.Client.View.Administrador.Seguridad
{
    public partial class usuarios : ReporteBase
    {
        #region Constants
        protected string comilla = "'";
        protected string HTMLCboRol = String.Empty;
        #endregion
        #region Methods

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if(hideResetearContrasena.Value == "1")
            {
                if (hideCuentaUsuario.Value != string.Empty && hddCodUsuario.Value != string.Empty)
                {
                    ResetearContrasenaUsuario(Convert.ToInt32(hddCodUsuario.Value), hideCuentaUsuario.Value.ToString());
                }
                hideResetearContrasena.Value = "0";
            }
            if (hideDesbloquearUsuario.Value == "1")
            {
                if (hideCuentaUsuario.Value != string.Empty && hddCodUsuario.Value != string.Empty)
                    DesbloquearUsuario(Convert.ToInt32(hddCodUsuario.Value));
                hideDesbloquearUsuario.Value = "0";
            }

            if (!Page.IsPostBack && !Page.IsCallback)
            {
                 Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
                
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                
                cmbPlanta2.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerPlantaCbo(), "DescripcionPlanta", "ClavePlanta"));
                llenaGrid();
                   

                cmbRol.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerRolCbo(), "DescripcionRol", "ClaveRol"));

                cmbRol2.Items.AddRange(GetItems(svc.ObtenerRolCbo(), "DescripcionRol", "ClaveRol"));
           

                llenaCombo();
                //Button4.Enabled = false;
                //LExport.Visible = false;
                //LAddNew.Visible = false;
                bool editar = true;//false;
                foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                {
                    switch (sp.ActionCode)
                    {
                        case 1: //Buscar
                            Button4.Enabled = true;
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
                }
                }
            }
        }
        
        protected void cmbPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            llenaGrid();
            
        }
        protected void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

            llenaGrid();
        }
        protected void btnLlenaGrid_Click(object sender, EventArgs e)
        {

            llenaGrid();
        }
        protected void llenaGrid()
        {
          
            if (cmbPlanta2.SelectedIndex > 0 & cmbRol.SelectedIndex > 0)
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = new List<Common.SolutionEntityFramework.BaseSolutionEntity>();
                Lista = svc.ObtenerUsuarios(int.Parse(cmbPlanta2.SelectedValue), int.Parse(cmbRol.SelectedValue), txtUsuario.Text);
                if (Lista.Count <= 0)
                {
                    UltraWebGrid1.ResetColumns();
                    UltraWebGrid1.DataBind();
                    UltraWebGrid1.Columns.Add("Nombre");
                    UltraWebGrid1.Columns.Add("Email");
                    UltraWebGrid1.Columns.Add("Rol");
                    UltraWebGrid1.Columns.Add("Usuario");
                    UltraWebGrid1.Columns.Add("Activo");
                    UltraWebGrid1.Columns.Add("Bloqueado");

                    UltraWebGrid1.Columns[0].Header.Caption = "Nombre";
                    UltraWebGrid1.Columns[1].Header.Caption = "Email";
                    UltraWebGrid1.Columns[2].Header.Caption = "Rol";
                    UltraWebGrid1.Columns[3].Header.Caption = "Usuario";
                    UltraWebGrid1.Columns[4].Header.Caption = "Activo";
                    UltraWebGrid1.Columns[5].Header.Caption = "Bloqueado";

                    UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;


                }
                else
                {
                    UltraWebGrid1.ResetColumns();
                    UltraWebGrid1.DataSource = Lista;
                    llenaCombo();
                    UltraWebGrid1.DataBind();
                    UltraWebGrid1.Columns[0].Hidden = true;
                    UltraWebGrid1.Columns[1].Hidden = true;
                    UltraWebGrid1.Columns[2].Header.Caption = "Nombre";
                    UltraWebGrid1.Columns[3].Hidden = true;
                    UltraWebGrid1.Columns[4].Hidden = true;
                    UltraWebGrid1.Columns[5].Header.Caption = "Email";
                    UltraWebGrid1.Columns[6].Hidden = true;
                    UltraWebGrid1.Columns[7].Header.Caption = "Rol";
                    UltraWebGrid1.Columns[8].Header.Caption = "Usuario";
                    UltraWebGrid1.Columns[9].Hidden = true;
                    UltraWebGrid1.Columns[10].Hidden = true;
                    UltraWebGrid1.Columns[11].Header.Caption = "Activo";
                    UltraWebGrid1.Columns[12].Header.Caption = "Bloqueado";
                    UltraWebGrid1.Columns[13].Hidden = true;
                    UltraWebGrid1.Columns[14].Hidden = true;
                    UltraWebGrid1.Columns[15].Hidden = true;
                    UltraWebGrid1.Columns[11].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
                    UltraWebGrid1.Columns[12].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.CheckBox;
                    UltraWebGrid1.Columns[11].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[12].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[2].Width = 200;
                    UltraWebGrid1.Columns[5].Width = 200;
                    UltraWebGrid1.Columns[7].Width = 120;
                    UltraWebGrid1.Columns[8].Width = 120;
                    UltraWebGrid1.Columns[11].Width = 80;
                    UltraWebGrid1.Columns[12].Width = 80;

                    UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[8].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[11].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                    UltraWebGrid1.Columns[12].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                }
            }
            else
            {
                UltraWebGrid1.DataBind();
                UltraWebGrid1.Columns.Add("Nombre");
                UltraWebGrid1.Columns.Add("Email");
                UltraWebGrid1.Columns.Add("Rol");
                UltraWebGrid1.Columns.Add("Usuario");
                UltraWebGrid1.Columns.Add("Activo");
                UltraWebGrid1.Columns.Add("Bloqueado");
            
                UltraWebGrid1.Columns[0].Header.Caption = "Nombre";
                UltraWebGrid1.Columns[1].Header.Caption = "Email";
                UltraWebGrid1.Columns[2].Header.Caption = "Rol";
                UltraWebGrid1.Columns[3].Header.Caption = "Usuario";
                UltraWebGrid1.Columns[4].Header.Caption = "Activo";
                UltraWebGrid1.Columns[5].Header.Caption = "Bloqueado";

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                
            }
            
           
        }
        protected void llenaCombo()
        {
            System.Web.UI.WebControls.ListItemCollection cboitems = new System.Web.UI.WebControls.ListItemCollection();
            System.Web.UI.WebControls.ListItem[] li = new System.Web.UI.WebControls.ListItem[cmbRol.Items.Count];
            cmbRol.Items.CopyTo(li,0);
            cboitems.AddRange(li);
            cboitems.RemoveAt(1);
          
            creaCombo(cboitems, "cboRol", 0, out HTMLCboRol);
               


        }
        protected void btnLlenaGridEmp_Click(object sender, EventArgs e)
        {

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            int NumEmpleado = -1;
            int.TryParse(NumEmpleadoWD.Text, out NumEmpleado);
            GridView1.DataSource = svc.ObtenerEmpleadoBusqueda(NumEmpleado, NomEmpleadoWD.Text,-1);
            GridView1.DataBind();
            //UltraWebGrid3.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
           
        }
        protected void btnLlenaGridSup_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            int NumEmpleado = -1;
            int.TryParse(NumSup.Text, out NumEmpleado);
            GridView2.DataSource = svc.ObtenerEmpleadoBusqueda(NumEmpleado, NomSup.Text, 2);
            GridView2.DataBind();
            //UltraWebGrid3.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
        }
        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.Usuario u = new LAMOSA.SCPP.Server.BusinessEntity.Usuario();
            try
            {
                
                u.NombreUsuario= hddNombreUsuario.Value;
                //u.Contrasena= hddContrasena.Value;
                u.Contrasena = "Lamosa06";
                int CodEmpleado = -1;
                int.TryParse(hddCodEmpleado.Value, out CodEmpleado);
                u.CodEmpleado = CodEmpleado;
                
                int CodRol = -1;
                int.TryParse(hddCodRol.Value, out CodRol);
                u.CodRol = CodRol;

                int Supervisor = -1;
                int.TryParse(hddSupervisor.Value, out Supervisor);
                u.CodSupervisor= Supervisor;

                if (hddBloqueado.Value.ToLower() == @"'true'")
                    u.Bloqueado = true;

                u.Email = hddCorreo.Value;

                int CodUsuario = -1;
                int.TryParse(hddCodUsuario.Value, out CodUsuario);
                u.CodUsuario=CodUsuario;
              
                u = svc.GuardarUsuario(u);

                WebAsyncRefreshPanel1.DataBind();
                if (u.ExceptionMessage != null && u.ExceptionMessage.Length > 1)
                    throw new Exception(u.ExceptionMessage);
            }
            catch (Exception err)
            {

                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + err.Message + "');</script>");
            }
                llenaGrid();          
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Usuario.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerUsuarios(int.Parse(cmbPlanta2.SelectedValue), int.Parse(cmbRol.SelectedValue), txtUsuario.Text);
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Usuario)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }
        #endregion
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx", false);
        }
        #endregion
        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            llenaGrid();
        }
        protected void BotonEliminar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.Usuario u = new LAMOSA.SCPP.Server.BusinessEntity.Usuario();
            try
            {
                u.CodUsuario = int.Parse(hddCodUsuario.Value);
                svc.EliminaUsuario(u);
                llenaGrid();
                WebAsyncRefreshPanel1.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        private void DesbloquearUsuario(int CodigoUsuario)
        {
            try
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                svc.DesbloquearUsuario(CodigoUsuario);
            }
            catch (Exception ex)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + ex.Message + "');</script>");
            }
        }
        private void ResetearContrasenaUsuario(int CodigoUsuario, string CuentaUsuario)
        {
            try
            {
                svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                ContrasenaC contrasena = new ContrasenaC();
                contrasena.codUsuario = Convert.ToInt32(CodigoUsuario);
                contrasena.ContrasenaNueva = contrasena.Contrasena = "Lamosa06";
                contrasena = svc.CambiarContrasena(contrasena);
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + contrasena.ExceptionMessage + "');</script>");
            }
            catch (Exception ex)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + ex.Message + "');</script>");
            }
        }
    }
}
