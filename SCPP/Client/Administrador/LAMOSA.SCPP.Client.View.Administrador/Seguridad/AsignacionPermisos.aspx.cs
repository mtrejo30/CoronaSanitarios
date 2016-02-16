using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAMOSA.SCPP.Server.BusinessEntity.Server;
using Infragistics.WebUI.UltraWebGrid;
using Infragistics.WebUI.Shared;

namespace LAMOSA.SCPP.Client.View.Administrador.Seguridad
{
    public partial class AsignacionPermisos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cmbRol.DataSource = new Combos().ObtenerRolCbo();
                cmbRol.DataTextField = "DescripcionRol";
                cmbRol.DataValueField = "ClaveRol";
                cmbRol.DataBind();
                cmbModulo.DataSource = new Combos().GetModulesCbo();
                cmbModulo.DataTextField = "descripcion";
                cmbModulo.DataValueField = "cod_modulo";
                cmbModulo.DataBind();
                Button2_Click(null, null);Button2_Click(null, null);
            }
            else
            {
                int a = UltraWebGrid1.Rows.Count;
                if (hAux.Value.ToString().Equals("1"))
                {
                    Button1_Click(null, null);
                }
            }
        }
        private void fillGrid()
        {
            int rol = Convert.ToInt32(cmbRol.SelectedValue);
            int modulo = Convert.ToInt32(cmbModulo.SelectedValue);
            hRol.Value = rol.ToString();
            hModulo.Value = modulo.ToString();
            DataTable dt_actions = new Combos().GetActionSreens(rol, modulo);
            DataTable dt_screens = new Combos().GetScreens(rol, modulo);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt_screens);
            ds.Tables.Add(dt_actions);
            DataRelation drel = new DataRelation("RolPantallas", dt_screens.Columns["cod_pantalla"], dt_actions.Columns["cod_pantalla"]);
            ds.Relations.Add(drel);
            UltraWebGrid1.DataSource = ds;
            UltraWebGrid1.DataBind();
            UltraWebGrid1.DisplayLayout.ViewType = Infragistics.WebUI.UltraWebGrid.ViewType.Hierarchical;
            UltraWebGrid1.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
            UltraWebGrid1.Columns[0].Type = ColumnType.CheckBox;
            UltraWebGrid1.Columns[0].EditorControlID = new CheckBox().UniqueID;
            UltraWebGrid1.Columns[1].Hidden = true;
            UltraWebGrid1.Columns[2].Width = 320;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int rol = Convert.ToInt32(hRol.Value);
            int modulo = Convert.ToInt32(hModulo.Value);
            new Actions().DeleteActionSreens(rol, modulo);
            foreach (UltraGridRow rwP in UltraWebGrid1.Rows)
            {
                if (rwP.Cells[0].Value.ToString().ToLower().Equals("true"))
                {
                    int screen = Convert.ToInt32(rwP.Cells[1].Text.Trim());
                    new Actions().InsertActionSreens(-1, screen, 1, rol, modulo);
                    foreach (UltraGridRow Srw in rwP.Rows)
                    {
                        if (Srw.Cells[0].ToString().ToLower().Equals("true"))
                        {
                            int accion = Convert.ToInt32(Srw.Cells[2].ToString());
                            new Actions().InsertActionSreens(accion, screen, -1, rol, modulo);
                        }
                    }
                }
            }
            fillGrid();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            fillGrid();
        }

        protected void UltraWebGrid1_InitializeRow(object sender, RowEventArgs e)
        {
            if (e.Row.Band.Key.Equals("Table2"))
            {
                e.Row.Cells[1].Column.Hidden = true;
                e.Row.Cells[2].Column.Hidden = true;
                e.Row.Cells[3].Column.Hidden = true;
                e.Row.Cells[5].Column.Hidden = true;
            }
        }
    }
}
