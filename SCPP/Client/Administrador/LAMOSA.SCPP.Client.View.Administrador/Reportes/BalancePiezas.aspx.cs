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

namespace LAMOSA.SCPP.Client.View.Administrador.Reportes
{
    public partial class BalancePiezas : ReporteBase
    {
        public DataTable workTable;
        public DataTable workTable2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                LlenaTabla();
                this.UltraWebGrid1.DataSource = workTable;
                this.UltraWebGrid1.DataBind();
                LExport.Visible = false;
                igtbl_reBuscaBtn.Disabled = false;
                bool editar = false;
                foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                {
                    switch (sp.ActionCode)
                    {
                        case 1: //Buscar
                            igtbl_reBuscaBtn.Disabled = false;
                            break;
                        case 2: //Exportar
                            LExport.Visible = true;
                            break;
                        case 3: //Nuevo
          
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

        protected void LlenaTabla()
        {


            workTable = new DataTable("balancepiezas");

            DataColumn workCol =

            workTable.Columns.Add("CT", typeof(String));
            workTable.Columns.Add("Inv Inicial", typeof(String));
            workTable.Columns.Add("Entradas", typeof(String));
            workTable.Columns.Add("Salidas", typeof(String));
            workTable.Columns.Add("Desperdicio", typeof(String));
            workTable.Columns.Add("Inv Fisico", typeof(String));
            workTable.Columns.Add("Ajuste", typeof(String));


            workTable.Rows.Add(new Object[] { "6031", "10,315", "136,284", "-128,324", "1,675","6,995", "4,817" });
            workTable.Rows.Add(new Object[] { "603S", "-", "128,324", "-124,907", "-", "5,227", "-1,810" });
            workTable.Rows.Add(new Object[] { "603R", "720", "124,907", "-109,446", "-14,684", "322", "1,175" });
            workTable.Rows.Add(new Object[] { "6032", "3,054", "109,446", "-108,867", "-2,838", "2,470", "-1,675" });
            workTable.Rows.Add(new Object[] { "6033", "1,798", "108,867", "-108,971", "-", "1,739", "-45" });
            workTable.Rows.Add(new Object[] { "6036", "11,650", "108,971", "-85,714", "-18,619", "14,472", "1,816" });
            workTable.Rows.Add(new Object[] { "6034", "-", "-", "-", "-", "-", "-"});
            workTable.Rows.Add(new Object[] { "", "", "", "", "", "", "" });
            workTable.Rows.Add(new Object[] { "", "27,537", "136,284", "-85,714", ",42,604", "31,225", "4,278" });
         


            /*UltraGridBand band = UltraWebGrid1.Bands[0];
            band.ColFootersVisible = ShowMarginInfo.Yes;
            band.Columns[0].Footer.Caption = "Total :";

            band.Columns[1].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;*/

        }

        protected void LlenaModal(object sender, EventArgs e)
        {

            /* UltraWebGrid1.DisplayLayout.ActiveCell.Style.BackColor = System.Drawing.Color.DarkOrange;*/

            Llenatabla2();
            this.UltraWebGrid2.DataSource = workTable2;
            this.UltraWebGrid2.DataBind();

        }

        protected void Llenatabla2()
        {

            workTable2 = new DataTable("balancepiezas2");

            DataColumn workCol2 =
            workTable2.Columns.Add("#", typeof(String));
            workTable2.Columns.Add("Linea", typeof(String));
            workTable2.Columns.Add("Modelo", typeof(String));
            workTable2.Columns.Add("Modelo2", typeof(String));
            workTable2.Columns.Add("Descripción", typeof(String));
            workTable2.Columns.Add("M/C", typeof(String));
            workTable2.Columns.Add("Pta", typeof(String));
            workTable2.Columns.Add("Pta2", typeof(String));
            workTable2.Columns.Add("Inv Inicial", typeof(String));
            workTable2.Columns.Add("Entradas", typeof(String));
            workTable2.Columns.Add("Salidas", typeof(String));
            workTable2.Columns.Add("Desperdicio", typeof(String));
            workTable2.Columns.Add("Inv Fisico", typeof(String));
            workTable2.Columns.Add("Ajuste", typeof(String));

            workTable2.Rows.Add(new Object[] { 1, "Tz EL", 3102, "603S-3102","Tz Regency RF","M","P3", "Tz EL" , 0 , 921, -941, 0, 25,45 });
            workTable2.Rows.Add(new Object[] { 2, "Tz EL", 3103, "603S-3103","Tz Eden", "M","P2", "Tz EL" , 0 , 0, 0, 0, 0, 0 });
            workTable2.Rows.Add(new Object[] { 3, "Tz EL", 3104, "603S-3104","Tz Regency", "M", "P3" , "Tz EL", 0 , 4492, -4580, 0, 157, -245});
         

        }

        protected void btnExporta_Click(object sender, EventArgs e)
        {
            //Metodo para Generar el Reporte
            if (ddlSeleccion.SelectedItem.Text.ToString() == "MS Excel (XLS)")
            {
                Workbook Reporte = new Workbook();
                //uwgTiposdef.Export(UltraWebGrid1, Reporte, 0, 0);

            }
            else
            {
                //GenerarReporte(ddlSeleccion.SelectedItem.Text.ToString(),cvrInvProceso, null, rptInventario);

                //////Reports.DataSet.dsUnidadAdmin dsUnidadA = new ControlPisoLamosa.CatalogosCommons.Reports.DataSet.dsUnidadAdmin();
                //////Reports.DataSet.dsUnidadAdminTableAdapters.PI_unidadadmin_sucursalTableAdapter ts = new ControlPisoLamosa.CatalogosCommons.Reports.DataSet.dsUnidadAdminTableAdapters.PI_unidadadmin_sucursalTableAdapter();
                //////ts.Fill(dsUnidadA.PI_unidadadmin_sucursal, Convert.ToInt32(cmbSucursal.SelectedValue));
                //GenerarReporte(ddlSeleccion.SelectedItem.Text.ToString(), null, new DataSet(), new Reportes.RPTurnos());

            }

        }
    }
}
