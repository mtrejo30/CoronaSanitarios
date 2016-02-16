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
    public partial class RolesUsuarios : ReporteBase
    {
        #region Constants
        protected string comilla = "'";
        protected string HTMLCboPlanta = String.Empty;
        #endregion

        #region Methods

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
                    cmbPlanta.Items.AddRange(GetItemsConSeleccione(svc.ObtenerPlantaCbo(), "DescripcionPlanta", "ClavePlanta"));
                    llenaCombo();

                    //ddlCT.DataTextField = "DesCentroTrabajo";
                    //ddlCT.DataValueField = "CodCentroTrabajo";
                    //ddlCT.DataBind();
                    llenaGrid();
                LExport.Visible = true;
                LAddNew.Visible = true;
                bool editar = true;
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
                }
                }

            }
        }
        private void llenaGrid()
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista  = svc.ObtenerRol();
            UltraWebGrid1.DataSource = Lista;
             if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();

            UltraWebGrid1.Columns[3].Hidden = true;

            UltraWebGrid1.Columns[0].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[1].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[2].AllowResize = AllowSizing.Fixed;
            UltraWebGrid1.Columns[3].AllowResize = AllowSizing.Fixed;

            UltraWebGrid1.Columns[0].Width = 100;
            UltraWebGrid1.Columns[1].Width = 200;
            UltraWebGrid1.Columns[2].Width = 70;

            UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Left;
            UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Center;

  

            UltraWebGrid1.Columns[0].Header.Caption = "Clave rol";
            UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[2].Header.Caption = "Activo";

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;

            UltraWebGrid1.Columns[2].Type = ColumnType.CheckBox;
            }




        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave rol");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Activo");
         

            UltraWebGrid1.Columns[0].Header.Caption = "Clave rol";
            UltraWebGrid1.Columns[1].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[2].Header.Caption = "Activo";

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
        
          
            UltraWebGrid1.Columns[2].Width = 70;
        }


        protected void BotonGuardar_click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            LAMOSA.SCPP.Server.BusinessEntity.rolplanta rp = new LAMOSA.SCPP.Server.BusinessEntity.rolplanta();

            try
            {
                int ClaveRol = -1;
                int.TryParse(hddClaveRol.Value, out ClaveRol);
                rp.ClaveRol = ClaveRol;
                rp.DescripcionRol = hddDescripcionRol.Value;
                int ClavePlanta = -1;
                int.TryParse(hddCodPlanta.Value, out ClavePlanta);
                rp.CodPlanta = ClavePlanta;
                if (hddActivo.Value.ToLower() == @"'true'")
                {
                    rp.Activo = true;
                }
                else
                {
                    rp.Activo = false;
                }
                svc.GuardaRol(rp);
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
            LAMOSA.SCPP.Server.BusinessEntity.Rol t = new LAMOSA.SCPP.Server.BusinessEntity.Rol();

            try
            {



                svc.EliminaRol(int.Parse(hddClaveRol.Value));
                llenaGrid();
                WebAsyncRefreshPanel1.DataBind();
            }
            catch (Exception err)
            {

                throw err;
            }
        }


        protected void btnExporta_Click(object sender, EventArgs e)
        {
           svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Rol.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerRol();
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Rol)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value); 
        }

        protected void llenaCombo()
        {
            System.Web.UI.WebControls.ListItemCollection cboitems = new System.Web.UI.WebControls.ListItemCollection();
            System.Web.UI.WebControls.ListItem[] li = new System.Web.UI.WebControls.ListItem[cmbPlanta.Items.Count];
            cmbPlanta.Items.CopyTo(li, 0);
            cboitems.AddRange(li);
            cboitems.RemoveAt(1);

            creaCombo(cboitems, "cboPlanta", 0, out HTMLCboPlanta);



        }


        #endregion
        #endregion

    }
}
