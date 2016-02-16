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
using CrystalDecisions.Shared;
using Infragistics.Shared;
using Infragistics.Excel;

namespace LAMOSA.SCPP.Client.View.Administrador.Configuraciones
{
    public partial class ConfBancos : ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtPlanta.Text = ((DropDownList)Page.Master.FindControl("cmbplanta")).SelectedItem.Text;

            UltraWebGrid1.Columns.Add("DesCentroTrabajo", "Column 1");
            UltraWebGrid1.Columns.Add("DesMaquina", "Banco");
            UltraWebGrid1.Columns.Add("FechaInicio", "Fecha inicio");
            UltraWebGrid1.Columns.Add("FechaFin", "Fecha fin");
            UltraWebGrid1.Columns.Add("Autorizado", "Autorizado");
            UltraWebGrid1.Columns.Add("Autoriza", "Autoriza");
            UltraWebGrid1.Columns.Add("Activo", "Activo");
            UltraWebGrid1.Columns.Add("CodCT", "CodCT");
            UltraWebGrid1.Columns.Add("CodMaquina", "CodMaquina");
            UltraWebGrid1.Columns.Add("CodUsuarioAutoriza","CodUsuarioAutoriza");
            UltraWebGrid1.Columns.Add("CodUsuarioAlta", "CodUsuarioAlta");
            UltraWebGrid1.Columns.Add("CodConfigBanco", "CodConfigBanco");
            UltraWebGrid1.Columns.Add("ExceptionMessage", "ExceptionMessage");
        }
        private void llenaGrid()
        {
            DropDownList cmbPlanta = ((DropDownList)Page.Master.FindControl("cmbPlanta"));
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
           
            cmbCentro.DataSource = svc.ObtenerCentroTrabajo(int.Parse(cmbPlanta.SelectedValue), 1);
            cmbCentro.DataTextField = "CodProceso";
            cmbCentro.DataValueField = "DesProceso";
            cmbCentro.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            cmbCentro.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            cmbCentro.DataBind();

            UltraWebGrid1.Columns[0].Header.Caption = "Centro trabajo";
            UltraWebGrid1.Columns[1].Header.Caption = "Banco";
            UltraWebGrid1.Columns[2].Header.Caption = "Fecha inicio";
            UltraWebGrid1.Columns[3].Header.Caption = "Fecha fin";
            UltraWebGrid1.Columns[4].Header.Caption = "Autorizado";
            UltraWebGrid1.Columns[5].Header.Caption = "Autoriza";
            UltraWebGrid1.Columns[6].Header.Caption = "Activo";
            UltraWebGrid1.Columns[7].Header.Caption = "CodCT";
            UltraWebGrid1.Columns[8].Header.Caption = "CodMaquina";
            UltraWebGrid1.Columns[9].Header.Caption = "CodUsuarioAutoriza";
            UltraWebGrid1.Columns[10].Header.Caption = "CodUsuarioAlta";
            UltraWebGrid1.Columns[11].Header.Caption = "CodConfigBanco";
            UltraWebGrid1.Columns[12].Header.Caption = "ExceptionMessage";

            UltraWebGrid1.Columns[7].Hidden = true;
            UltraWebGrid1.Columns[8].Hidden = true;
            UltraWebGrid1.Columns[9].Hidden = true;
            UltraWebGrid1.Columns[10].Hidden = true;
            UltraWebGrid1.Columns[11].Hidden = true;
            UltraWebGrid1.Columns[12].Hidden = true;
        
        }
        protected void cmbCentro_SelectedIndexChanged(object sender, EventArgs e)
        {
            svcSCPP.SCPPClient svc = new svcSCPP.SCPPClient();
            cmbMaquina.DataSource = svc.ObtenerMaquinaCbo(-1, int.Parse(cmbCentro.SelectedValue));
            cmbMaquina.DataTextField = "DesMaquina";
            cmbMaquina.DataValueField = "codMaquina";
            cmbMaquina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            cmbMaquina.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            cmbMaquina.DataBind();
        }

    }
}
