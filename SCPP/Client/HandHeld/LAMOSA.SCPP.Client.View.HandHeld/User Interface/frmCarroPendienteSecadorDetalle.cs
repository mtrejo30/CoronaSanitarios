using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAMOSA.SCPP.Client.View.HandHeld.BusinessComponent;

namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    public partial class frmCarroPendienteSecadorDetalle : Form
    {
        private LoginUsuario lu = new LoginUsuario();
        private int iCarro = 0;
        public frmCarroPendienteSecadorDetalle(LoginUsuario lu, int iCarro)
        {
            InitializeComponent();
            this.lu = lu;
            this.iCarro = iCarro;
            SetDataGridConfiguration();
            this.WindowState = FormWindowState.Maximized;
        }
        private void frmCarroPendienteSecadorDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                this.encabezado.Operador = this.lu.NomEmpleado;
                this.encabezado.PuestoTurno = this.lu.DesPuesto + " - " + this.lu.DesTurno;
                this.encabezado.Planta = this.lu.DesPlanta;
                this.encabezado.Titulo = "Detalle del Carro N° " + iCarro;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        private void frmCarroPendienteSecadorDetalle_KeyUp(object sender, KeyEventArgs e)
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
        private void btRegresar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetDataGridConfiguration()
        {
            try
            {
                DataTable dtCarrroPendienteSecadorDetalle = new CarroPendienteSecador().ObtenerCarroPendienteSecadorDetalle(iCarro);
                dgCarroSecadorDetalle.DataSource = dtCarrroPendienteSecadorDetalle;
                DataGridTableStyle tableStyle1 = new DataGridTableStyle();
                tableStyle1.MappingName = dtCarrroPendienteSecadorDetalle.TableName;//Se indica a que TableName se aplicaran estas Propiedades

                DataGridColumnStyle cModelo = new DataGridTextBoxColumn();
                cModelo.MappingName = "Modelo";
                cModelo.HeaderText = "Modelo";
                cModelo.Width = (int)(dgCarroSecadorDetalle.Width * .40); //Ocupara el 45% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cModelo);

                DataGridColumnStyle cTipoArticulo = new DataGridTextBoxColumn();
                cTipoArticulo.MappingName = "TipoArticulo";
                cTipoArticulo.HeaderText = "Tipo Articulo";
                cTipoArticulo.Width = (int)(dgCarroSecadorDetalle.Width * .30); //Ocupara el 45% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cTipoArticulo);

                DataGridColumnStyle cCantidadPiezas = new DataGridTextBoxColumn();
                cCantidadPiezas.MappingName = "Piezas";
                cCantidadPiezas.HeaderText = "Cant. Piezas";
                cCantidadPiezas.Width = (int)(dgCarroSecadorDetalle.Width * .20);//Ocupara el 25% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cCantidadPiezas);

                dgCarroSecadorDetalle.TableStyles.Clear();
                dgCarroSecadorDetalle.TableStyles.Add(tableStyle1);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}