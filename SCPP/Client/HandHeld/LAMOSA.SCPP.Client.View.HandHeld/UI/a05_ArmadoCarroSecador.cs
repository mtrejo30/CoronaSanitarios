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
    public partial class a05_ArmadoCarroSecador : Form
    {

        #region fields

        private LoginUsuario lu = null;
        private c05_ArmadoCarroSecado oDA = new c05_ArmadoCarroSecado();
        private int iCodCarro = -1;

        #endregion fields

        #region properties



        #endregion properties

        #region methods

        #region constructors and destructor

        public a05_ArmadoCarroSecador(LoginUsuario lu)
        {
            InitializeComponent();
            //
            this.lu = lu;
            this.ConfigurarFormulario();
            this.ConfigurarPanelControles();
        }
        ~a05_ArmadoCarroSecador()
        {

        }

        #endregion constructors and destructor

        #region common

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

            this.txCodBarrasPieza.Enabled = false;

            this.txCodCarro.KeyPress += new KeyPressEventHandler(this.txCodCarro_KeyPress);
            this.txCodBarrasPieza.KeyPress += new KeyPressEventHandler(this.txCodBarrasPieza_KeyPress);
            
            this.btTerminar.Click += new EventHandler(this.btTerminar_Click);
            this.btCancelar.Click += new EventHandler(this.btCancelar_Click);
            this.btSalir.Click += new EventHandler(this.btSalir_Click);
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
            int PosY = 55;
            this.pnControles.Location = new Point(PosX, PosY);
        }
        #endregion ConfigurarCabecera

        #endregion common

        #region event handlers

        #region Form_Load
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.lbOperador.Text = lu.NomEmpleado;
                this.lbPuesto.Text = lu.DesPuesto;
                this.lbPlanta.Text = lu.DesPlanta;
                this.lbProceso.Text = "Armado Carro Secador";
                //
                
                //
                this.txCodCarro.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion Form_Load
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

        #region txCodCarro_KeyPress
        private void txCodCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (string.IsNullOrEmpty(this.txCodCarro.Text))
                    {
                        this.lbMensaje.Text = "Capture número de Carro";
                        this.txCodCarro.SelectAll();
                        this.txCodCarro.Focus();
                    }
                    else
                    {
                        // En la primer captura se almacena el numero del Carro.
                        this.iCodCarro = Convert.ToInt32(this.txCodCarro.Text);

                        // Validar que el numero de Carro no este ocupado.
                        DataTable dtRes = this.oDA.ObtenerPiezasCarro(this.lu.CodPlanta, this.lu.CodProceso, this.iCodCarro);
                        if (dtRes.Rows.Count > 0)
                        {
                            this.iCodCarro = -1;
                            this.lbMensaje.Text = "Carro " + this.iCodCarro.ToString() + " ocupado";
                            this.txCodCarro.Text = "";
                            this.txCodCarro.Focus();
                        }
                        else
                        {
                            this.lbMensaje.Text = "Capture las Piezas";
                            this.txCodCarro.Enabled = false;
                            this.txCodBarrasPieza.Enabled = true;
                            this.txCodBarrasPieza.SelectAll();
                            this.txCodBarrasPieza.Focus();
                        }
                    }
                }
                else
                {
                    // Validar ingreso de digitos y el retroceso.
                    if (((int)e.KeyChar) >= 48 && ((int)e.KeyChar) <= 57 || e.KeyChar == 8)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txCodCarro_KeyPress
        #region txCodBarrasPieza_KeyPress
        private void txCodBarrasPieza_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataTable dtRes = null;

            try
            {
                // Validar ingreso del Enter.
                if (e.KeyChar == 13)
                {
                    if (string.IsNullOrEmpty(this.txCodBarrasPieza.Text))
                    {
                        this.lbMensaje.Text = "Capture el número de Pieza.";
                        this.txCodBarrasPieza.SelectAll();
                        this.txCodBarrasPieza.Focus();
                    }
                    else
                    {
                        string sCodBarrasPieza = this.txCodBarrasPieza.Text;

                        // Verificar que la Pieza exista.
                        dtRes = this.oDA.ObtenerCodPieza(this.lu.CodPlanta, sCodBarrasPieza);
                        if (dtRes.Rows.Count > 0)
                        {
                            int iCodPieza = Convert.ToInt32(dtRes.Rows[0]["CodPieza"]);

                            // Verificar que la pieza no este ya asignada a un carro.
                            dtRes = this.oDA.ExistePiezaEnCarro(this.lu.CodPlanta, this.lu.CodProceso, iCodPieza);
                            if (dtRes.Rows.Count == 0)
                            {
                                // Asociar la Pieza con el Carro.
                                this.oDA.InsertarCarroPieza(this.lu.CodPlanta, this.lu.CodProceso, this.iCodCarro, iCodPieza);
                                this.lbMensaje.Text = "Pieza: " + sCodBarrasPieza + " capturada";
                                this.txCodBarrasPieza.Text = "";
                                this.txCodBarrasPieza.Focus();
                            }
                            else
                            {
                                this.lbMensaje.Text = "Pieza ya asignada al carro: " + dtRes.Rows[0]["CodCarro"].ToString();
                                this.txCodBarrasPieza.SelectAll();
                                this.txCodBarrasPieza.Focus();
                            }
                        }
                        else
                        {
                            this.lbMensaje.Text = "Pieza no existente";
                            this.txCodBarrasPieza.SelectAll();
                            this.txCodBarrasPieza.Focus();
                        }
                    }
                }
                else
                {
                    // Validar ingreso de digitos y el retroceso.
                    if (((int)e.KeyChar) >= 48 && ((int)e.KeyChar) <= 57 || e.KeyChar == 8)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion txCodBarrasPieza_KeyPress

        #region btTerminar_Click
        private void btTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                if(this.iCodCarro == -1)
                {
                    this.lbMensaje.Text = "Capture número de Carro";
                    this.txCodCarro.SelectAll();
                    this.txCodCarro.Focus();
                }
                else
                {
                    DataTable dtRes = this.oDA.ObtenerPiezasCarro(this.lu.CodPlanta, this.lu.CodProceso, this.iCodCarro);
                    if (dtRes.Rows.Count == 0)
                    {
                        this.lbMensaje.Text = "Carro sin Piezas registradas";
                        this.txCodBarrasPieza.Text = "";
                        this.txCodBarrasPieza.Focus();
                    }
                    else
                    {
                        // Enviar el carro con las piezas asociadas al Servidor.
                        // ?????????????????????

                        // Borrar el carro armado.
                        this.oDA.EliminarCarro(this.lu.CodPlanta, this.lu.CodProceso, this.iCodCarro);

                        this.lbMensaje.Text = "";
                        DialogResult dr = MessageBox.Show("Armado de Carro para Secado completado.", "SCPP", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);

                        //Regresar a Configuracion Inicial.
                        a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                        frmObj.Show();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btTerminar_Click
        #region btCancelar_Click
        private void btCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.iCodCarro != -1)
                {
                    DialogResult dr = MessageBox.Show("¿Cancelar armado de Carro?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.Yes)
                    {
                        // Borrar el carro armado.
                        this.oDA.EliminarCarro(this.lu.CodPlanta, this.lu.CodProceso, this.iCodCarro);

                        //Regresar a Configuracion Inicial.
                        //a03_ConfigInicial frmObj = new a03_ConfigInicial(this.lu);
                        //frmObj.Show();
                        this.Close();
                    }
                    else
                    {
                        this.txCodBarrasPieza.Text = "";
                        this.txCodBarrasPieza.Focus();
                    }
                }
                else
                {
                    //Regresar a Configuracion Inicial.
                    a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
                    frmObj.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion btCancelar_Click
        #region btSalir_Click
        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        #endregion btSalir_Click

        #endregion event handlers

        #endregion methods

    }
}