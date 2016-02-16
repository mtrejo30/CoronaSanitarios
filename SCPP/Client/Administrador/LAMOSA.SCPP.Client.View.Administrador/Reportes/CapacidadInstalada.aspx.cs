using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using Infragistics.WebUI.Shared;
using System.Web.UI;
using System.Drawing;
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

namespace LAMOSA.SCPP.Client.View.Administrador.Reportes
{
    public partial class CapacidadInstalada : ReporteBase
    {
        public DataTable workTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !Page.IsCallback)
            {
                Usuario user = (Usuario)Session["UserLogged"];
                if (user != null)
                {
                llenarvacio();
                LlenaCombos();

                // Insertar los elementos: Selcciona... y Todos.
                CmbCt.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
                CmbCt.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                CmbCt.Items.FindByValue("0").Selected = true;

                // Insertar los elementos: Selcciona... y Todos.
                CmbBanco.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
                CmbBanco.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                CmbBanco.Items.FindByValue("0").Selected = true;

                // Insertar los elementos: Selcciona... y Todos.
                CmbModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
                CmbModelo.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                CmbModelo.Items.FindByValue("0").Selected = true;

                // Insertar los elementos: Selcciona... y Todos.
                CmbPlanta.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
                CmbPlanta.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                CmbPlanta.Items.FindByValue("0").Selected = true;

                // Insertar los elementos: Selcciona... y Todos.
                CmbCodTipoArticulo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
                CmbCodTipoArticulo.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
                CmbCodTipoArticulo.Items.FindByValue("0").Selected = true;

                // Insertar los elementos: Selcciona... y Todos.
                CmbAgrupa.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Detalle", "1"));
                CmbAgrupa.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Tipo artículo", "2"));
                CmbAgrupa.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Tipo artículo y banco", "3"));
                CmbAgrupa.Items.FindByValue("1").Selected = true;
                LExport.Visible = false;
                btnBuscar.Enabled = true;
                bool editar = false;
                foreach (ScreenPermission sp in new Actions().GetActionBySreen(user.CodRol, Request.Url.LocalPath))
                {
                    switch (sp.ActionCode)
                    {
                        case 1: //Buscar
                            btnBuscar.Enabled = true;
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

        protected void LlenaCombos()
        {

            // Instaciar el servicio.
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            // Solicitud al servicioy obtener datos.
            List<BE.PlantaCbo> l_PlantaCbo = new List<BE.PlantaCbo>();
            foreach (SE.BaseSolutionEntity bse in svc.ObtenerPlantaCbo())
            {
                l_PlantaCbo.Add(bse as BE.PlantaCbo);
            }
            // Enlazar datos al control.
            CmbPlanta.DataSource = l_PlantaCbo;
            CmbPlanta.DataValueField = "ClavePlanta";
            CmbPlanta.DataTextField = "DescripcionPlanta";
            CmbPlanta.DataBind();
            

            // Solicitud al servicioy obtener datos.
            List<BE.TipoArticuloCbo> l_TipoArticuloCbo = new List<BE.TipoArticuloCbo>();
            foreach (SE.BaseSolutionEntity bse in svc.ObtenerTiposArticuloCbo())
            {
                l_TipoArticuloCbo.Add(bse as BE.TipoArticuloCbo);
            }

            // Enlazar datos al control.
            CmbCodTipoArticulo.DataSource = l_TipoArticuloCbo;
            CmbCodTipoArticulo.DataValueField = "CodTipoArticulo";
            CmbCodTipoArticulo.DataTextField = "DesTipoArticulo";
            CmbCodTipoArticulo.DataBind();
           
        }

        protected void LlenaTabla()
        {

            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            List<Common.SolutionEntityFramework.BaseSolutionEntity> Lista = svc.ObtenerRepCapInstalada(Convert.ToInt32(this.CmbAgrupa.SelectedItem.Value),
                                                                     Convert.ToInt32(this.CmbPlanta.SelectedItem.Value),
                                                                     Convert.ToInt32(this.CmbCt.SelectedItem.Value),
                                                                     Convert.ToInt32(this.CmbBanco.SelectedItem.Value),
                                                                     Convert.ToInt32(this.CmbCodTipoArticulo.SelectedItem.Value),
                                                                     Convert.ToInt32(this.CmbModelo.SelectedItem.Value));
            UltraWebGrid1.DataSource = Lista;

            if (Lista.Count  > 0)
            {
                /*UltraWebGrid1.DisplayLayout.ScrollBar = ScrollBar.Always;*/
                UltraWebGrid1.Width = 800;

                UltraWebGrid1.ResetColumns();
                UltraWebGrid1.DataBind();

        
            

                UltraWebGrid1.Columns[0].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[3].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[5].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[6].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[7].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[8].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[9].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[10].Hidden = true;

                UltraWebGrid1.Columns[0].Header.Caption = "Tipo artículo";
                UltraWebGrid1.Columns[1].Header.Caption = "Banco";
                UltraWebGrid1.Columns[2].Header.Caption = "Modelo";
                UltraWebGrid1.Columns[3].Header.Caption = "Descripción";
                UltraWebGrid1.Columns[4].Header.Caption = "Cantidad moldes";
                UltraWebGrid1.Columns[5].Header.Caption = "Número de imp.";
                UltraWebGrid1.Columns[6].Header.Caption = "Vaciadas x día";
                UltraWebGrid1.Columns[7].Header.Caption = "Piezas vaciadas x día";
                UltraWebGrid1.Columns[8].Header.Caption = "Vaciadas acum.";
                UltraWebGrid1.Columns[9].Header.Caption = "*6/7";

                UltraWebGrid1.Columns[0].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[3].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[5].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[6].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[7].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[8].Header.Style.Wrap = true;
                UltraWebGrid1.Columns[9].Header.Style.Wrap = true;
             

                UltraWebGrid1.Columns[0].Width = 75;
                UltraWebGrid1.Columns[1].Width = 120;
                UltraWebGrid1.Columns[2].Width = 57;
                UltraWebGrid1.Columns[3].Width = 165;
                UltraWebGrid1.Columns[4].Width = 65;
                UltraWebGrid1.Columns[5].Width = 65;
                UltraWebGrid1.Columns[6].Width = 65;
                UltraWebGrid1.Columns[7].Width = 65;
                UltraWebGrid1.Columns[8].Width = 65;
                UltraWebGrid1.Columns[9].Width = 65;

                UltraWebGrid1.Columns[0].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[1].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[2].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[3].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[4].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[5].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[6].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[7].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[8].AllowResize = AllowSizing.Fixed;
                UltraWebGrid1.Columns[9].AllowResize = AllowSizing.Fixed;

                UltraWebGrid1.Columns[0].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                UltraWebGrid1.Columns[1].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                UltraWebGrid1.Columns[2].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                UltraWebGrid1.Columns[3].CellStyle.HorizontalAlign = HorizontalAlign.Left;
                UltraWebGrid1.Columns[4].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[5].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[6].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[7].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[8].CellStyle.HorizontalAlign = HorizontalAlign.Center;
                UltraWebGrid1.Columns[9].CellStyle.HorizontalAlign = HorizontalAlign.Center;

                UltraGridBand Band = UltraWebGrid1.Bands[0];
                Band.ColFootersVisible = ShowMarginInfo.Yes;
                Band.FooterStyle.BackColor = System.Drawing.Color.Gray;
                Band.Columns[3].Footer.Caption = "Total";
                Band.Columns[3].Footer.Style.HorizontalAlign = HorizontalAlign.Right;
                Band.Columns[4].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
                Band.Columns[5].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
                Band.Columns[6].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
                Band.Columns[7].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
                Band.Columns[8].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;
                Band.Columns[9].Footer.Total = Infragistics.WebUI.UltraWebGrid.SummaryInfo.Sum;


                
            }
            else
            {
                llenarvacio();
              /* CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('No se encontraron registros');</script>");*/
            }
        }

        protected void llenarvacio()
        {
            UltraWebGrid1.ResetColumns();
            UltraWebGrid1.DataBind();
            UltraWebGrid1.Columns.Add("Tipo artículo");
            UltraWebGrid1.Columns.Add("Banco");
            UltraWebGrid1.Columns.Add("Modelo");
            UltraWebGrid1.Columns.Add("Descripción");
            UltraWebGrid1.Columns.Add("Cantidad de moldes");
            UltraWebGrid1.Columns.Add("Número de impresiones");
            UltraWebGrid1.Columns.Add("Vaciadas por día");
            UltraWebGrid1.Columns.Add("Piezas vaciadas por día");
            UltraWebGrid1.Columns.Add("Piezas vaciadas acumuladas");
            UltraWebGrid1.Columns.Add("*6/7");

            UltraWebGrid1.Columns[0].Header.Caption = "Tipo artículo";
            UltraWebGrid1.Columns[1].Header.Caption = "Banco";
            UltraWebGrid1.Columns[2].Header.Caption = "Modelo";
            UltraWebGrid1.Columns[3].Header.Caption = "Descripción";
            UltraWebGrid1.Columns[4].Header.Caption = "Cantidad moldes";
            UltraWebGrid1.Columns[5].Header.Caption = "Núm. impresiones";
            UltraWebGrid1.Columns[6].Header.Caption = "Vaciadas por día";
            UltraWebGrid1.Columns[7].Header.Caption = "Piezas vac. x día";
            UltraWebGrid1.Columns[8].Header.Caption = "Vaciadas acumuladas";
            UltraWebGrid1.Columns[9].Header.Caption = "*6/7";

            UltraWebGrid1.Columns[0].Width = 80;
            UltraWebGrid1.Columns[1].Width = 50;
            UltraWebGrid1.Columns[2].Width = 50;
            UltraWebGrid1.Columns[3].Width = 80;
            UltraWebGrid1.Columns[4].Width = 110;
            UltraWebGrid1.Columns[5].Width = 120;
            UltraWebGrid1.Columns[6].Width = 110;
            UltraWebGrid1.Columns[7].Width = 110;
            UltraWebGrid1.Columns[8].Width = 110;
            UltraWebGrid1.Columns[8].Width = 45;

            UltraWebGrid1.Columns[0].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[1].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[2].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[3].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[4].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[5].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[6].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[7].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[8].Header.Style.Wrap = true;
            UltraWebGrid1.Columns[9].Header.Style.Wrap = true;


            UltraWebGrid1.Columns[0].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[1].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[2].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[3].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[4].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[5].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[6].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[7].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[8].Header.Style.HorizontalAlign = HorizontalAlign.Center;
            UltraWebGrid1.Columns[9].Header.Style.HorizontalAlign = HorizontalAlign.Center;

            UltraWebGrid1.DisplayLayout.ScrollBar = ScrollBar.Never;

        }
        protected void CmbCodTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LLenar el combo Molde.

            // Instaciar el servicio.
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            BE.ArticuloPars artPars = new BE.ArticuloPars();
            artPars.CodTipoArticulo = Convert.ToInt32(this.CmbCodTipoArticulo.SelectedItem.Value);

            // Solicitud al servicioy obtener datos.
            List<BE.MoldeCbo> l_MoldeCbo = new List<BE.MoldeCbo>();
            foreach (SE.BaseSolutionEntity bse in svc.ObtenerMoldesCbo (artPars))
            {
                l_MoldeCbo.Add(bse as BE.MoldeCbo);
            }

            // Enlazar datos al control.
            CmbModelo.DataSource = l_MoldeCbo;
            CmbModelo.DataValueField = "CodMolde";
            CmbModelo.DataTextField = "DesMolde";
            CmbModelo.DataBind();
            // Insertar los elementos: Selcciona... y Todos.
            CmbModelo.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
            CmbModelo.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            CmbModelo.Items.FindByValue("0").Selected = true;
        }

        protected void CmbPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LLenar el combo CT.

            // Instaciar el servicio.
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
           
            // Solicitud al servicioy obtener datos.
            List<BE.CentroTrabajo> l_CTCbo = new List<BE.CentroTrabajo>();
            foreach (SE.BaseSolutionEntity bse in svc.ObtenerCentroTrabajoCbo(Convert.ToInt32(this.CmbPlanta.SelectedItem.Value), 1))
            {
                l_CTCbo.Add(bse as BE.CentroTrabajo);
            }

            // Enlazar datos al control.
            CmbCt.DataSource = l_CTCbo;
            CmbCt.DataValueField = "CodCentroTrabajo";
            CmbCt.DataTextField = "DesCentroTrabajo";
            CmbCt.DataBind();
            // Insertar los elementos: Selcciona... y Todos.
            CmbCt.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
            CmbCt.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            CmbCt.Items.FindByValue("0").Selected = true;

        }

        protected void CmbCt_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LLenar el combo Banco.

            // Instaciar el servicio.
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();

            // Solicitud al servicioy obtener datos.
            List<BE.MaquinaCbo> l_MaquinaCbo = new List<BE.MaquinaCbo>();
            foreach (SE.BaseSolutionEntity bse in svc.ObtenerMaquinaCbo(-1, Convert.ToInt32(this.CmbCt.SelectedItem.Value)))
            {
                l_MaquinaCbo.Add(bse as BE.MaquinaCbo);
            }

            // Enlazar datos al control.
            CmbBanco.DataSource = l_MaquinaCbo;
            CmbBanco.DataValueField = "CodMaquina";
            CmbBanco.DataTextField = "DesMaquina";
            CmbBanco.DataBind();
            // Insertar los elementos: Selcciona... y Todos.
            CmbBanco.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Selecciona...", "0"));
            CmbBanco.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            CmbBanco.Items.FindByValue("0").Selected = true;

        }
        protected void btnExporta_Click(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            BE.RepCapInstalada artPars = new BE.RepCapInstalada();

            DataSet dsReportXLS = new DataSet();
            dsReportXLS.Tables.Add();

            string[] colnames = LAMOSA.SCPP.Server.BusinessEntity.RepCapInstalada.GetPropertyNamesArray();
            foreach (string colname in colnames)
            {
                dsReportXLS.Tables[0].Columns.Add(colname);
            }
            List<Common.SolutionEntityFramework.BaseSolutionEntity> datos = svc.ObtenerRepCapInstalada(     Convert.ToInt32(this.CmbAgrupa.SelectedItem.Value), 
                                                                                                            Convert.ToInt32(this.CmbPlanta.SelectedItem.Value),
                                                                                                            Convert.ToInt32(this.CmbCt.SelectedItem.Value),
                                                                                                            Convert.ToInt32(this.CmbBanco.SelectedItem.Value),
                                                                                                            Convert.ToInt32(this.CmbCodTipoArticulo.SelectedItem.Value),
                                                                                                            Convert.ToInt32(this.CmbModelo.SelectedItem.Value));
            foreach (Common.SolutionEntityFramework.BaseSolutionEntity item in datos)
            {
                dsReportXLS.Tables[0].Rows.Add(((LAMOSA.SCPP.Server.BusinessEntity.RepCapInstalada)item).ToObjectArray());
            }
            ExportToExcel(dsReportXLS, 0, Response, nombre.Value);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            LlenaTabla();
        }

    }
}
