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
    public partial class Articulos : ReporteBase
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

                    // Solicitud al servicioy obtener datos.

                    llenarvacio();
                    CmbCodTipoArticulo.Items.AddRange(GetItemsConSeleccioneTodos(svc.ObtenerTiposArticuloCbo(), "DesTipoArticulo", "CodTipoArticulo"));


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
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            BE.ArticuloPars artPars = new BE.ArticuloPars();
            artPars.CodTipoArticulo = Convert.ToInt32(this.CmbCodTipoArticulo.SelectedItem.Value);


            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.Molde.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerMoldes(artPars);
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.Molde)item).ToObjectArray());
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
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerMoldes(artPars);
            UltraWebGrid1.DataSource  = Lista;
             if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {
              UltraWebGrid1.ResetColumns();
              UltraWebGrid1.DataBind();

            UltraWebGrid1.Columns[5].Hidden = true;
            UltraWebGrid1.Columns[7].Hidden = true;

            UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
            UltraWebGrid1.Columns[1].Header.Caption = "Clave artículo";
            UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[3].Header.Caption = "Número de Impresiones";
            UltraWebGrid1.Columns[4].Header.Caption = "Fecha";
            UltraWebGrid1.Columns[6].Header.Caption = "Activo";

            UltraWebGrid1.Columns[3].Header.Style.Wrap = true;

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
          
           

             UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
             UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Center;
             UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Left;
             UltraWebGrid1.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Center;
             UltraWebGrid1.Columns[4].CellStyle.HorizontalAlign = HorizontalAlign.Center;
             UltraWebGrid1.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;

             UltraWebGrid1.Columns[4].Format = "dd-MM-yyyy";

             UltraWebGrid1.Columns[2].Width = 250;
             UltraWebGrid1.Columns[3].Width = 135;
             UltraWebGrid1.Columns[6].Width = 70;
            }

        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Clave única");
            UltraWebGrid1.Columns.Add("Clave artículo");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Número de Impresiones");
            UltraWebGrid1.Columns.Add("Fecha");
            UltraWebGrid1.Columns.Add("Activo");
   

            UltraWebGrid1.Columns[0].Header.Caption = "Clave única";
            UltraWebGrid1.Columns[1].Header.Caption = "Clave artículo";
            UltraWebGrid1.Columns[2].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[3].Header.Caption = "Número de Impresiones";
            UltraWebGrid1.Columns[4].Header.Caption = "Fecha";
            UltraWebGrid1.Columns[5].Header.Caption = "Activo";

            UltraWebGrid1.Columns[3].Header.Style.Wrap = true;

            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center; 
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;


          
            UltraWebGrid1.Columns[1].Width = 110;
            UltraWebGrid1.Columns[3].Width = 120;
            UltraWebGrid1.Columns[5].Width = 150;
        }

        protected void cambio_pagina(object sender, Infragistics.WebUI.UltraWebGrid.PageEventArgs e)
        {
            UltraWebGrid1.DisplayLayout.Pager.CurrentPageIndex = e.NewPageIndex;
            CmbCodTipoArticulo_SelectedIndexChanged(sender,e);
        }
        #endregion


    }
}
