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
using BC = LAMOSA.SCPP.Server.BusinessComponent;
using System.Xml;

namespace LAMOSA.SCPP.Client.View.Administrador.Clasificacion
{
    public partial class PantallaInicial : ReporteBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                svcSCPPHH.SCPP_HHClient d = new svcSCPPHH.SCPP_HHClient();
                BC.SCPP_HH bc = new BC.SCPP_HH();
                DataSet ds = new DataSet();//d.SyncServHH(DropDownList1.SelectedValue.ToString(), DateTime.Today.Date.AddDays(-20), DateTime.Today, DateTime.Today);
                //DataSet ds = bc.SyncServHH(DropDownList1.SelectedValue.ToString(), DateTime.Today.Date.AddDays(-20), DateTime.Today, DateTime.Today);
                //List<DataSet> lds = d.SyncServHHLDS(DateTime.Today.Date.AddDays(-30), DateTime.Today, DateTime.Today);
                //List<StringReader[]> lxmlDS = bc.SyncServHH(DateTime.Today.Date.AddDays(-30), DateTime.Today, DateTime.Today);
                //DataSet beRes = new DataSet();
                //List<DataSet> lds = new List<DataSet>();
                //foreach (StringReader[] xmlSR in lxmlDS)
                //{
                //    beRes.Clear();
                //    beRes = new DataSet();
                //    beRes.ReadXmlSchema(xmlSR[0]);
                //    beRes.ReadXml(xmlSR[1], XmlReadMode.Auto);
                //    lds.Add(beRes);
                //}
                //DataTable dt = d.TablasCatalogosHH();

                DataTable dt = null;
                try
                {
                dt = d.Login("a","a");
                }
                catch { }
                try
                {dt = d.ObtenerSupervisorPorDefecto(42);}
                catch { }
                DataSet xmlDS = new DataSet();
                DataSet ds2 = new DataSet();
                try
                {
                    StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                    StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                    StringReader[] xmlDataSet = { xmlSchema, xmlInfo };

                    ds2.ReadXmlSchema(xmlDataSet[0]);
                    ds2.ReadXml(xmlDataSet[1], XmlReadMode.Auto);
                }
                catch { }

                if (ds2.Tables.Count > 0)
                {
                    UltraWebGrid1.DataSource = ds2.Tables[0];
                    String tableName = ds2.Tables[1].TableName.Substring(3).ToString();
                    ObtenerUpdate(ds2.Tables[1], tableName);
                    UltraWebGrid2.DataSource = ds2.Tables[1];
                    UltraWebGrid3.DataSource = ds2.Tables[2];
                }
                int i = d.ActualizarColorPieza(21, 3);
                UltraWebGrid1.DataBind();
                UltraWebGrid2.DataBind();
                UltraWebGrid3.DataBind();
            }
            catch (Exception er)
            {
                CallBackManager.AddScriptBlock(this.Page, WebAsyncRefreshPanel1, "<script type='text/javascript'>alert('" + er.Message.Replace("'", "\"") + "');</script>");
                Console.Write("Error:" + er.Message);
            }

        }

        public List<String> ObtenerUpdate(DataTable dtUpd, String tableName)
        {
            List<String> lSentences = new List<String>();

            String[] columnName = new String[dtUpd.Columns.Count];
            String qry = "";
            for (int i = 0; i < columnName.Length; i++)
                columnName[i] = dtUpd.Columns[i].ColumnName;

            foreach (DataRow dr in dtUpd.Rows)
            {
                qry = "update " + tableName + " set ";
                for (int i = 0; i < columnName.Length; i++)
                    qry += columnName[i] + " = " + columnValue(dr[i].GetType(), dr[i].ToString());
                
                Label1.Text += qry + "------";
            }
            return lSentences;
        }

        private String columnValue(Type type, String value)
        {

            return "";
        }

    }
}
