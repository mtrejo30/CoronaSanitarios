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
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using SE = Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Client.View.Administrador.Administracion
{
    public partial class Subarticulos : ReporteBase
    {
        #region Methods

        #region Event Handlers
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    // Instaciar el servicio.
                    svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

                    llenarvacio();

                    // Enlazar datos al control.


                    // Insertar los elementos: Selcciona... y Todos.

                    CmbCodTipoArticulo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));
                    BE.ArticuloPars artPars = new BE.ArticuloPars();
                    CmbCodMolde.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerMoldesCbo(artPars), "DesMolde", "CodMolde"));
                    LExport.Visible = false;


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

                                break;
                            case 4: //Editar

                                break;
                        }


                    }
                }

            }
        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            BE.ArticuloPars artPars = new BE.ArticuloPars();
            artPars.CodTipoArticulo = Convert.ToInt32(this.CmbCodTipoArticulo.SelectedItem.Value);
            artPars.CodMolde = Convert.ToInt32(this.CmbCodMolde.SelectedItem.Value);

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Articulo.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerArticulos(artPars);
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Articulo)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

        #endregion

        protected void CmbCodTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LLenar el combo Molde.

            BE.ArticuloPars artPars = new BE.ArticuloPars();
            artPars.CodTipoArticulo = Convert.ToInt32(this.CmbCodTipoArticulo.SelectedItem.Value);


            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            CmbCodMolde.Items.Clear();
            CmbCodMolde.Items.AddRange(GetItemsConSeleccioneTodosMol(svc.ObtenerMoldesCbo(artPars), "DesMolde", "CodMolde"));





        }
        protected void CmbCodMolde_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Llenar el Grid.
            BE.ArticuloPars artPars = new BE.ArticuloPars();
            artPars.CodTipoArticulo = Convert.ToInt32(this.CmbCodTipoArticulo.SelectedItem.Value);
            artPars.CodMolde = Convert.ToInt32(this.CmbCodMolde.SelectedItem.Value);

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerArticulos(artPars);
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
                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[6].Hidden = true;
                UltraWebGrid1.Columns[7].Hidden = true;
                UltraWebGrid1.Columns[8].Hidden = true;
                UltraWebGrid1.Columns[9].Hidden = true;
                UltraWebGrid1.Columns[11].Hidden = true;

                UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
                UltraWebGrid1.Columns[1].Header.Caption = "Clave artículo";
                UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
                UltraWebGrid1.Columns[4].Header.Caption = "Tipo artículo";
                UltraWebGrid1.Columns[10].Header.Caption = "Activo";

                UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[10].Header.Style.HorizontalAlign = HorizontalAlign.Center;
       

                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[10].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraWebGrid1.Columns[2].Width = 250;
     
                UltraWebGrid1.Columns[10].Width = 70;



            }
        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave única");
            UltraWebGrid1.Columns.Add("Clave artículo");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Tipo artículo");
            UltraWebGrid1.Columns.Add("Activo");

            UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
            UltraWebGrid1.Columns[1].Header.Caption = "Clave artículo";
            UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[3].Header.Caption = "Tipo artículo";
            UltraWebGrid1.Columns[4].Header.Caption = "Activo";


            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;

            UltraWebGrid1.Columns[1].Width = 110;
            UltraWebGrid1.Columns[3].Width = 110;

        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            CmbCodMolde_SelectedIndexChanged(sender, e);
        }
        #endregion



    }

}
