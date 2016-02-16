using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    public partial class frmAutorizacionImpresion : Form
    {
        private int codigoRol;
        private a11_CapturaEmpaque frmSecuencia;
        private c01_Login oDA = new c01_Login();
        private HHsvc.Etiqueta etiqueta = null;
        private LoginUsuario lu = null;
        private HHsvc.ConfigImpresora configuracionImpresora = null;
        public int CodigoRol { get { return codigoRol; } set { codigoRol = value; } }
        public HHsvc.Etiqueta Etiqueta { get { return etiqueta; } set { etiqueta = value; } }
        public HHsvc.ConfigImpresora ConfiguracionImpresora { get { return configuracionImpresora; } set { configuracionImpresora = value; } }
        public LoginUsuario LU { get { return lu; } set { lu = value; } }
        public a11_CapturaEmpaque FormSecuencia
        {
            get { return frmSecuencia; }
            set { frmSecuencia = value; }
        }
        public frmAutorizacionImpresion()
        {
            InitializeComponent();
            this.encabezado.Titulo = "Autorización de Impresión";
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (EsCapturaValida())
            {
                if (EsUsuarioValido())
                {
                    if (TienePermisoReimpresion())
                    {
                        ImprimirEtiquetaPieza();
                        this.Close();
                    }
                }
            }
        }
        private void ImprimirEtiquetaPieza()
        {
            HHsvc.SCPP_HH proxy = null;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                proxy.ImprimirEtiqueta(ConfiguracionImpresora, Etiqueta);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, encabezado.Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                proxy = null;
            }
        }
        private bool EsCapturaValida()
        {
            encabezado.Mensaje = string.Empty;
            tbUsuario.Text = tbUsuario.Text.Trim();
            tbContrasena.Text = tbContrasena.Text.Trim();
            if (Etiqueta.TipoEtiqueta == 0)
            {
                encabezado.Mensaje = "Favor de capturar el tipo de etiqueta.";
                return false;
            }
            if (tbUsuario.Text == string.Empty)
            {
                encabezado.Mensaje = "Favor de capturar el usuario.";
                return false;
            }
            if (tbContrasena.Text == string.Empty)
            {
                encabezado.Mensaje = "Favor de capturar la contraseña.";
                return false;
            }
            if (Etiqueta == null)
            {
                encabezado.Mensaje = "No se ha establecido la configuración de etiqueta.";
                return false;
            }
            if (ConfiguracionImpresora == null)
            {
                encabezado.Mensaje = "No se ha establecido la configuración de impresora.";
                return false;
            }
            return true;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool EsUsuarioValido()
        {
            LoginUsuario lu = null;
            try
            {
                lu = this.oDA.Login(this.tbUsuario.Text, this.tbContrasena.Text);
                if (lu.IsLogin)
                {
                    CodigoRol = lu.CodRol;
                    return true; 
                }
                this.encabezado.Mensaje = lu.Mensaje;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, encabezado.Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally { lu = null; }
            return false;
        }
        private bool TienePermisoReimpresion()
        {
            HHsvc.SCPP_HH serviceHH = null;
            bool PermisoReimpresion = false;
            bool PermisoReimpresionSpecified = false;
            try
            {
                serviceHH = new LAMOSA.SCPP.Client.View.HandHeld.HHsvc.SCPP_HH();
                serviceHH.TienePermisoReImpresion(CodigoRol, true, out PermisoReimpresion, out PermisoReimpresionSpecified);
                if (!PermisoReimpresion) encabezado.Mensaje = "No tiene permiso de reimpresión.";
                return PermisoReimpresion;
            }
            catch (Exception ex)
            {
                encabezado.Mensaje = ex.Message;
                return false;
            }
        }
        private void KeyPressControlEvent(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (sender.GetType() == typeof(TextBox))
                {
                    TextBox tb = (TextBox)sender;
                    if (tb.TabIndex == 3)
                        this.tbContrasena.Focus();
                    if (tb.TabIndex == 4)
                        this.btnOk.Focus();
                }
            }
        }
        private void frmAutorizacionImpresion_Load(object sender, EventArgs e)
        {
            this.tbUsuario.Focus();
            this.radbtnPieza.Checked = true;
            Etiqueta.TipoEtiqueta = 1;
            Etiqueta.TipoEtiquetaSpecified = true;
            //if (lu.CodPlanta == 4)
            //{
            //    this.radbtnPieza.Visible = false;
            //    this.radbtnTarima.Visible = false;
            //    return;
            //}
            if (string.IsNullOrEmpty(Etiqueta.Tarima) || Etiqueta.Tarima == "0")
            {
                int iCodigoTarima = this.ObtenerTarima(etiqueta.Pieza);
                if (iCodigoTarima == 0)
                    this.radbtnTarima.Enabled = false;
            }
        }
        private void radbtnCheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Name == "radbtnPieza" & (sender as RadioButton).Checked)
            { 
                Etiqueta.TipoEtiqueta = 1;
                Etiqueta.TipoEtiquetaSpecified = true;
            }
            else if ((sender as RadioButton).Name == "radbtnTarima" & (sender as RadioButton).Checked)
            { 
                Etiqueta.TipoEtiqueta = 2;
                Etiqueta.TipoEtiquetaSpecified = true;
            }
        }
        private int ObtenerTarima(string sCodigoBarraPieza)
        {
            if (string.IsNullOrEmpty(sCodigoBarraPieza)) return 0;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                int iResult = 0;
                bool bResult = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.ObtenerTarimaPieza(sCodigoBarraPieza, out iResult, out bResult);
                return iResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return 0;
            }
            finally
            {
                proxy = null;
            }
        }
    }
}