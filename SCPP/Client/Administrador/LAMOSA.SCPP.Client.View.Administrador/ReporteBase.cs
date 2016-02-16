using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.IO;


namespace LAMOSA.SCPP.Client.View.Administrador
{
    public abstract class ReporteBase : System.Web.UI.Page
    {
        //private Usuario usr;
         
        /// <summary>
        /// Metodo para Generar el Reporte

        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dSet.Tables[TableIndex];
            gv.DataBind();
            gv.RenderControl(hw);  
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void creaCombo(ListItemCollection cbo, string idControl, string columnKey, int width, out string htmlString)
        {
            creaCombo(cbo, idControl, columnKey, out htmlString, true,width);
        }
        protected void creaCombo(ListItemCollection cbo, string idControl, int width, out string htmlString)
        {
            creaCombo(cbo, idControl, null, out htmlString, false,width);
        }

        private void creaCombo(ListItemCollection cbo, string idControl, string columnKey, out string htmlString, bool ck, int ancho)
        {
            string aplicaWidth = "";
            if (ancho > 0) aplicaWidth = "style='width:" + ancho.ToString() + "'";
            if (ck) htmlString = "<select name='" + idControl + "' id='" + idControl + "' columnkey='" + columnKey + "' "+aplicaWidth+">";
            else htmlString = "<select name='" + idControl + "' id='" + idControl + "'>";
            foreach (System.Web.UI.WebControls.ListItem item in cbo)
            {
                htmlString += "<option value='" + item.Value + "'>" + item.Text + "</option>";
            }
            htmlString += "</select>";
        }

        protected ListItem[] GetItemsConSeleccioneTodos(System.Collections.Generic.List<Common.SolutionEntityFramework.BaseSolutionEntity> Source, string DataTextField, string DataValueField)
        {
            System.Web.UI.WebControls.DropDownList ddl = new DropDownList();
            ddl.DataSource = Source;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            System.Web.UI.WebControls.ListItemCollection lista1 = new System.Web.UI.WebControls.ListItemCollection();
            lista1.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            lista1.Add(new System.Web.UI.WebControls.ListItem("Todos", "-1"));
            System.Web.UI.WebControls.ListItem[] lista2 = new System.Web.UI.WebControls.ListItem[ddl.Items.Count+2];
            lista1.CopyTo(lista2, 0);
            ddl.Items.CopyTo(lista2, 2);
            return lista2;
        }

        protected ListItem[] GetItemsConSeleccioneTodosMol(System.Collections.Generic.List<Common.SolutionEntityFramework.BaseSolutionEntity> Source, string DataTextField, string DataValueField)
        {
            System.Web.UI.WebControls.DropDownList ddl = new DropDownList();
            ddl.DataSource = Source;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            System.Web.UI.WebControls.ListItemCollection lista1 = new System.Web.UI.WebControls.ListItemCollection();
            lista1.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            lista1.Add(new System.Web.UI.WebControls.ListItem("Todos", "-2"));
            System.Web.UI.WebControls.ListItem[] lista2 = new System.Web.UI.WebControls.ListItem[ddl.Items.Count + 2];
            lista1.CopyTo(lista2, 0);
            ddl.Items.CopyTo(lista2, 2);
            return lista2;
        }

        protected ListItem[] GetItems(System.Collections.Generic.List<Common.SolutionEntityFramework.BaseSolutionEntity> Source, string DataTextField, string DataValueField)
        {
            System.Web.UI.WebControls.DropDownList ddl = new DropDownList();
            ddl.DataSource = Source;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            System.Web.UI.WebControls.ListItemCollection lista1 = new System.Web.UI.WebControls.ListItemCollection();
           
            System.Web.UI.WebControls.ListItem[] lista2 = new System.Web.UI.WebControls.ListItem[ddl.Items.Count];
            lista1.CopyTo(lista2, 0);
            ddl.Items.CopyTo(lista2, 0);
            return lista2;
        }


        protected ListItem[] GetItemsConSeleccione(System.Collections.Generic.List<Common.SolutionEntityFramework.BaseSolutionEntity> Source, string DataTextField, string DataValueField)
        {
            System.Web.UI.WebControls.DropDownList ddl = new DropDownList();
            ddl.DataSource = Source;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            System.Web.UI.WebControls.ListItemCollection lista1 = new System.Web.UI.WebControls.ListItemCollection();
            lista1.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));

            System.Web.UI.WebControls.ListItem[] lista2 = new System.Web.UI.WebControls.ListItem[ddl.Items.Count + 1];
            lista1.CopyTo(lista2, 0);
            ddl.Items.CopyTo(lista2, 1);
            return lista2;

        }

        protected ListItem[] GetItemsConSeleccioneObj(System.Collections.Generic.List<object> Source, string DataTextField, string DataValueField)
        {
            System.Web.UI.WebControls.DropDownList ddl = new DropDownList();
            ddl.DataSource = Source;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            System.Web.UI.WebControls.ListItemCollection lista1 = new System.Web.UI.WebControls.ListItemCollection();
            lista1.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));

            System.Web.UI.WebControls.ListItem[] lista2 = new System.Web.UI.WebControls.ListItem[ddl.Items.Count + 1];
            lista1.CopyTo(lista2, 0);
            ddl.Items.CopyTo(lista2, 1);
            return lista2;

        }

        protected ListItem[] GetItemsConSeleccioneTodosObj(System.Collections.Generic.List<object> Source, string DataTextField, string DataValueField)
        {
            System.Web.UI.WebControls.DropDownList ddl = new DropDownList();
            ddl.DataSource = Source;
            ddl.DataTextField = DataTextField;
            ddl.DataValueField = DataValueField;
            ddl.DataBind();
            System.Web.UI.WebControls.ListItemCollection lista1 = new System.Web.UI.WebControls.ListItemCollection();
            lista1.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            lista1.Add(new System.Web.UI.WebControls.ListItem("Todos", "-1"));

            System.Web.UI.WebControls.ListItem[] lista2 = new System.Web.UI.WebControls.ListItem[ddl.Items.Count + 2];
            lista1.CopyTo(lista2, 0);
            ddl.Items.CopyTo(lista2, 2);
            return lista2;

        }
 
    }
}
