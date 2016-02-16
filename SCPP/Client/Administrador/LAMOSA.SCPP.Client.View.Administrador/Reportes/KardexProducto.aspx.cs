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
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using LAMOSA.SCPP.Server.BusinessEntity.Server;
using LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Client.View.Administrador.Reportes
{
    public partial class KardexProducto : ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                    llenarvacioP();
                    //LExport.Visible = false;
                    btnLlenarDefectos.Enabled = true;
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



        protected void BuscarCodigo(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            var code = TxtCode.Text;
            if (code == "")
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('Debe ingresar un código');</script>");
            else
            {
                List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerKardexProductoBusqueda(TxtCode.Text);
                BE.KardexProductoBusqueda kpb = Lista[0] as BE.KardexProductoBusqueda;
                TxtPlanta1.Text = kpb.DesPlanta;
                TxtColor.Text = kpb.Color;
                TxtTipoArt.Text = kpb.DesTipoArticulo;
                TxtCalidad.Text = kpb.Calidad;
                TxtModelo.Text = kpb.DesArticulo;
                TextCodPieza.Text = (kpb.CodPieza.ToString());
                llenargrid();
            }
        }

        protected void llenargrid()
        {
            llenarvacio();
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerKardexProducto(int.Parse(TextCodPieza.Text));

            if (Lista.Count <= 0)
            {
                llenarvacioP();
            }
            else
            {
                List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista2 = svc.ObtenerKardexProducto(int.Parse(TextCodPieza.Text));
                UltraWebGrid1.DataSource = Lista2;
                BE.KardexProducto kp = Lista2[0] as BE.KardexProducto;
                CodPiezaTransaccion.Value = kp.CodPiezaTransaccion.ToString();
                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();


                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[7].Hidden = true;

                UltraWebGrid1.Columns[0].Header.Caption = "Fecha";
                UltraWebGrid1.Columns[1].Hidden = true;
                UltraWebGrid1.Columns[2].Header.Caption = "Proceso";
                UltraWebGrid1.Columns[3].Hidden = true;
                UltraWebGrid1.Columns[4].Header.Caption = "Centro de trabajo";
                UltraWebGrid1.Columns[5].Hidden = true;
                UltraWebGrid1.Columns[6].Header.Caption = "Maquina";
                UltraWebGrid1.Columns[7].Header.Caption = "Posicion";
                UltraWebGrid1.Columns[8].Hidden = true;
                UltraWebGrid1.Columns[9].Header.Caption = "Operador";
                UltraWebGrid1.Columns[10].Hidden = true;
                UltraWebGrid1.Columns[11].Header.Caption = "Defectos";


                UltraWebGrid1.Columns[11].Type = Infragistics.WebUI.UltraWebGrid.ColumnType.Button;
                UltraWebGrid1.Columns[0].Format = "dd-MM-yyyy";
                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;


                UltraWebGrid1.Columns[4].Width = 120;
                UltraWebGrid1.Columns[6].Width = 150;
                UltraWebGrid1.Columns[9].Width = 160;

            }



        }

        protected void LlenarDefectos(object sender, EventArgs e)
        {
            /* llenargrid();*/



            llenarvacio(); svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            /*  List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista2 = svc.ObtenerKardexProducto(int.Parse(TextCodPieza.Text));
              UltraWebGrid1.DataSource = Lista2;
              BE.KardexProducto kp = Lista2[0] as BE.KardexProducto;
              CodPiezaTransaccion.Value = kp.CodPiezaTransaccion.ToString();*/


            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerKardexProductoDefecto(int.Parse(TextCodPieza.Text));
            UltraWebGrid4.DataSource = Lista;
            if (Lista.Count <= 0)
            {
                llenarvacio();
            }
            else
            {


                UltraWebGrid4.ResetColumns();
                UltraWebGrid4.DataBind();

                UltraWebGrid4.Columns[0].Header.Caption = "Zona";
                UltraWebGrid4.Columns[1].Header.Caption = "Defecto";
                UltraWebGrid4.Columns[2].Header.Caption = "Acción Defecto";
                UltraWebGrid4.Columns[3].Hidden = true;


                UltraWebGrid4.Columns[0].Width = 180;
                UltraWebGrid4.Columns[1].Width = 180;
                UltraWebGrid4.Columns[2].Width = 160;

                UltraWebGrid4.DataBind();

            }


        }
        protected void llenarvacioP()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Fecha");
            UltraWebGrid1.Columns.Add("Proceso");
            UltraWebGrid1.Columns.Add("Centro de trabajo");
            UltraWebGrid1.Columns.Add("Maquina");
            UltraWebGrid1.Columns.Add("Posicion");
            UltraWebGrid1.Columns.Add("Operador");
            UltraWebGrid1.Columns.Add("Defectos");


            UltraWebGrid1.Columns[0].Header.Caption = "Fecha";
            UltraWebGrid1.Columns[1].Header.Caption = "Proceso";
            UltraWebGrid1.Columns[2].Header.Caption = "Centro de trabajo";
            UltraWebGrid1.Columns[3].Header.Caption = "Maquina";
            UltraWebGrid1.Columns[4].Header.Caption = "Posicion";
            UltraWebGrid1.Columns[5].Header.Caption = "Operador";
            UltraWebGrid1.Columns[6].Header.Caption = "Defectos";

            UltraWebGrid1.Columns[4].Hidden = true;


            UltraWebGrid1.Columns[2].Width = 120;
            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;

        }
        protected void llenarvacio()
        {
            UltraWebGrid4.ResetColumns();
            UltraWebGrid4.DataBind();
            UltraWebGrid4.Columns.Add("Zona");
            UltraWebGrid4.Columns.Add("Defecto");
            UltraWebGrid4.Columns.Add("Acción Defecto");

            UltraWebGrid4.Columns[0].Header.Caption = "Zona";
            UltraWebGrid4.Columns[1].Header.Caption = "Defecto";
            UltraWebGrid4.Columns[2].Header.Caption = "Acción Defecto";


            UltraWebGrid4.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid4.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid4.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
        }

        protected void WebDialogWindow2_StateChanged(object sender, Infragistics.Web.UI.LayoutControls.DialogWindowStateChangedEventArgs e)
        {
            if (e.NewState == Infragistics.Web.UI.LayoutControls.DialogWindowState.Hidden)
            {

            }
            if (e.NewState == Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal)
            {
                LlenarDefectos(sender, e);
            }
        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            DataSet dsReportXLS = new DataSet();
            DataTable dtRes = new DataTable();
            //string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.KardexProducto.GetPropertyNamesArray();
            //foreach (string colname in colnames)
            //{
            //    dsReportXLS.Tables[0].Columns.Add(colname);
            //}
            //List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerKardexExportar(int.Parse(TextCodPieza.Text));
            //foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            //{
            //    dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.KardexProducto)item).ToObjectArray());
            //}
            dsReportXLS = svc.ObtenerKardexExportar(int.Parse(TextCodPieza.Text));
            //dsReportXLS.Tables.Add(dtRes);
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);

        }
    }
}
