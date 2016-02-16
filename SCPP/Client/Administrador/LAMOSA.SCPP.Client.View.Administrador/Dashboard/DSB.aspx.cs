using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LAMOSA.SCPP.Client.View.Administrador.Dashboard
{
    public partial class DSB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblfecha.InnerText = String.Format("{0:dd} {0:MMMM} {0:yyyy}", DateTime.Now);
            lblHora.InnerText = DateTime.Now.ToShortTimeString();
            lblAlmacen.InnerText = "Todos";
            lblPlanta.InnerText = "Todos";
            //lblRol.InnerText = "Administrador";
        }

        protected void UltraChart1_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
        {

        }
    }
}
