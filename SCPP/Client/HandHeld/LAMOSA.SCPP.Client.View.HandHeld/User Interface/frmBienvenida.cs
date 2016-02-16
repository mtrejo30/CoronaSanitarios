using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    public partial class frmBienvenida : Form
    {
        Timer tChange = new Timer();
        public frmBienvenida()
        {
            InitializeComponent();
        }

        private void frmBienvenida_Load(object sender, EventArgs e)
        {
            HHsvc.SCPP_HH serviceSCPP = new LAMOSA.SCPP.Client.View.HandHeld.HHsvc.SCPP_HH();
            try
            {
                this.lblMensajeInicioSesion.Text = serviceSCPP.ObtenerMensajeInicioSesion();
            }
            catch
            {
                this.lblMensajeInicioSesion.Text = "La información contenida en este sistema es confidencial y su uso está restringido exclusivamente para usuarios autorizados por Grupo Lamosa y/o filiales. Cualquier acceso o intento de acceso, uso o modificación del o al sistema y/o su información no autorizados por Grupo Lamosa y/o filiales está estrictamente prohibido. Usuarios no autorizados serán sujetos a penalidades civiles, criminales y de cualquier otra índole. El uso de este sistema es monitoreado por Grupo Lamosa y/o filiales por razones administrativas, legales y de seguridad. Al acceder al sistema usted es responsable de su uso y acuerda hacerlo con estricto apego a las políticas de Seguridad de la Información vigentes.";
            }            
            serviceSCPP.Dispose();
            this.lblNumeroVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            tChange.Interval = 10000;
            tChange.Enabled = true;
            tChange.Tick += new EventHandler(this.tChangeEvent);
        }

        private void tChangeEvent(object sender, EventArgs e)
        {
            try
            {
                tChange.Enabled = false;
                tChange.Dispose();
                //this.Hide();
                this.Close();
                //a01_Login frmObj = new a01_Login();
                //frmObj.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
    }
}