using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class a04_Vaciado02 : Form
    {

        LoginUsuario lu = null;
        cVaciado ovaciado = null;
        DataSet odsVaciado = null;
        int int_pos = 0;
        int num_escaneado = 0;
        int cont_pos = 1;
 
        #region Constructors and Destructor

        public a04_Vaciado02(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a04_Vaciado02()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region ConfigurarFormulario
        private void ConfigurarFormulario()
        {
            // Appearance.
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Text = "";

            // Layout.
            this.WindowState = FormWindowState.Maximized;

            // Window Style.
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.KeyPreview = true;

            // Eventos.
            this.Load += new EventHandler(this.Form_Load);
            this.Resize += new EventHandler(this.Form_Resize);
            this.KeyUp += new KeyEventHandler(this.Form_KeyUp);
        }
        #endregion ConfigurarFormulario
        #region ConfigurarPanelControles
        private void ConfigurarPanelControles()
        {
            this.pnControles.BackColor = this.BackColor;
        }
        #endregion ConfigurarPanelControles
        #region ConfigurarCabecera
        private void ConfigurarCabecera()
        {
            // Ajustar Logo.
            this.pbLogo.Top = 0;
            this.pbLogo.Left = this.Width - this.pbLogo.Width;
            // Ajustar Boton Salir.
            this.btSalir.Top = this.pbLogo.Height;
            this.btSalir.Left = this.Width - this.btSalir.Width;
            // Ajustar ProgressBar Procesando.
            this.pbrProcesando.Top = this.pbLogo.Height;
            this.pbrProcesando.Left = this.Width - this.pbLogo.Width;

            // Ajustar Panel.
            int PosX = (int)((this.Width - this.pnControles.Width) / 2);
            int PosY = (int)((this.Height - this.pnControles.Height - this.pbLogo.Height) / 2);
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

        #endregion Common


        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbOperador.Text = lu.NomEmpleado;
                this.lbPuesto.Text = lu.DesPuesto;
                this.lbPlanta.Text = lu.DesPlanta;
                this.lbProceso.Text = "Vaciado - Captura Inicial";

                DataSet ods = null;
                DataRow dr = null;
                ComboBox cbxObj = null;

                this.odsVaciado = this.ovaciado.getVaciado();
                ods = this.ovaciado.getPrueba();
                dr = ods.Tables[0].NewRow();
                dr["codprueba"] = "-1";
                dr["desprueba"] = "Seleccionar...";
                ods.Tables[0].Rows.InsertAt(dr, 0);
                cbxObj = this.cbxPrueba;
                cbxObj.DataSource = ods.Tables[0];
                cbxObj.ValueMember = "codprueba";
                cbxObj.DisplayMember = "desprueba";
                cbxObj.SelectedValue = -1;

                this.odsVaciado.Tables[0].Columns.Add("Bit_Escaneado",typeof(bool));
                this.odsVaciado.Tables[0].Columns.Add("codigo");
                this.odsVaciado.Tables[0].Columns.Add("cvePrueba");
                this.int_pos = this.ovaciado.Posicion;
                txtBanco.Text  = this.ovaciado.Banco;
                txtPosicion.Text = this.int_pos.ToString();
                txtModelo.Text = odsVaciado.Tables[0].Rows[this.int_pos - 1]["codarticulo"].ToString();
                txtTipo.Text = odsVaciado.Tables[0].Rows[this.int_pos - 1]["cve_tipo_articulo"].ToString();


                this.num_escaneado = this.odsVaciado.Tables[0].Rows.Count;

                txtEtiqueta.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }

        #region Form_Resize
        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.ConfigurarCabecera();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion Form_Resize
        #region Form_KeyUp
        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }
        #endregion Form_KeyUp


        public a04_Vaciado02(LoginUsuario lu, cVaciado ovaciado)
        {
            this.lu = lu;
            this.ovaciado = ovaciado;
            InitializeComponent();
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void txtModelo_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnOk_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(this.txtEtiqueta.Text))
            {
                this.lbMensaje.Text = "Ingrese la clave";
                this.txtEtiqueta.Focus();
                return;
            }
            this.odsVaciado.Tables[0].Rows[int_pos-1]["Bit_Escaneado"] = true;
            this.odsVaciado.Tables[0].Rows[int_pos-1]["codigo"] = txtEtiqueta.Text;
            this.odsVaciado.Tables[0].Rows[int_pos - 1]["cvePrueba"] = cbxPrueba.SelectedValue.ToString();
            txtEtiqueta.Text = "";
            if (this.ovaciado.Asc)
            {
                this.int_pos++;
            }
            else
            {
                this.int_pos--;
            }
            if (this.int_pos == 0 && this.ovaciado.Asc == false)
            {
                this.int_pos = this.num_escaneado;
            }
            if (this.int_pos - 1 == this.num_escaneado && this.ovaciado.Asc == true)
            {
                this.int_pos = 1;
            }
            if (this.num_escaneado == cont_pos)
            {
                //terminó
                this.btnOk.Enabled = false;
                this.cbxPrueba.SelectedValue = -1;
                this.txtEtiqueta.Enabled = false;
                this.btnSiguiente.Enabled = false;
                MessageBox.Show("Todos las piezas han sido escaneados.", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return;
            }
            this.cont_pos++;
            this.cbxPrueba.SelectedValue = -1;
            txtPosicion.Text = this.int_pos.ToString();
            txtModelo.Text = odsVaciado.Tables[0].Rows[this.int_pos - 1]["codarticulo"].ToString();
            txtTipo.Text = odsVaciado.Tables[0].Rows[this.int_pos - 1]["cve_tipo_articulo"].ToString();
            if (this.odsVaciado.Tables[0].Rows[int_pos - 1]["Bit_Escaneado"].ToString() != "" && Convert.ToBoolean(this.odsVaciado.Tables[0].Rows[int_pos - 1]["Bit_Escaneado"].ToString()) == true)
            {
                this.txtEtiqueta.Enabled = false;
            }
            else
            {
                this.txtEtiqueta.Focus();
            }
        }
        private void txtEtiqueta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOk_Click(sender, e);
            }
        }
        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            
            if (this.ovaciado.Asc)
            {
                this.int_pos++;
            }
            else
            {
                this.int_pos--;
            }
            if (this.int_pos == 0 && this.ovaciado.Asc == false)
            {
                this.int_pos = this.num_escaneado;
            }
            if (this.int_pos - 1 == this.num_escaneado && this.ovaciado.Asc == true)
            {
                this.int_pos = 1;
            }
            this.cbxPrueba.SelectedValue = -1;
            txtPosicion.Text = this.int_pos.ToString();
            txtModelo.Text = odsVaciado.Tables[0].Rows[this.int_pos - 1]["codarticulo"].ToString();
            txtTipo.Text = odsVaciado.Tables[0].Rows[this.int_pos - 1]["cve_tipo_articulo"].ToString();
            if (this.odsVaciado.Tables[0].Rows[int_pos - 1]["Bit_Escaneado"].ToString() != "" && Convert.ToBoolean(this.odsVaciado.Tables[0].Rows[int_pos - 1]["Bit_Escaneado"].ToString()) == true)
            {
                this.txtEtiqueta.Enabled = false;
            }
            else if (this.odsVaciado.Tables[0].Rows[int_pos - 1]["Bit_Escaneado"].ToString() == "" || Convert.ToBoolean(this.odsVaciado.Tables[0].Rows[int_pos - 1]["Bit_Escaneado"].ToString()) == false)
            {
                this.txtEtiqueta.Enabled = true;
                this.txtEtiqueta.Focus();
            }
        }
        private void btnDefectos_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Defectos", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            a04_Vaciado03 ovacdet = new a04_Vaciado03(this.lu);
            ovacdet.Show();
            
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir del Vaciado?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                a03_ConfiguracionInicial oconfig = new a03_ConfiguracionInicial(this.lu);
                oconfig.Show();
                this.Close();
            }
        }
        private void btnTerminar_Click(object sender, EventArgs e)
        {
            if (this.num_escaneado != cont_pos)
            {
                DialogResult dr = MessageBox.Show("¿Aún hay pendientes, deseas terminar?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    return;
                }
            }




            MessageBox.Show("Banco Terminado!", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);


            a03_ConfiguracionInicial oconfig = new a03_ConfiguracionInicial(this.lu);
            oconfig.Show();
            this.Close();
        }

    }
}