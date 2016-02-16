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
    public partial class frmCarroPendienteSecador : Form
    {
        private LoginUsuario lu = new LoginUsuario();
        public frmCarroPendienteSecador(LoginUsuario lu)
        {
            InitializeComponent();
            this.lu = lu;
            this.WindowState = FormWindowState.Maximized;


            SetDataGridConfiguration();
        }

        private void dgCarroSecador_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                DataGrid.HitTestInfo hitInfo = dgCarroSecador.HitTest(e.X, e.Y);
                if (hitInfo.Type == DataGrid.HitTestType.Cell)
                {
                    dgCarroSecador.Select(hitInfo.Row);
                    string sCarro = dgCarroSecador[hitInfo.Row, 0].ToString();
                    if (!string.IsNullOrEmpty(sCarro))
                    {
                        frmCarroPendienteSecadorDetalle carroPendienteDetalle = new frmCarroPendienteSecadorDetalle(lu, Convert.ToInt32(sCarro));
                        carroPendienteDetalle.ShowDialog();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error al Seleccionar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1); }
        }

        private void btRegresar_Click(object sender, EventArgs e)
        {
            a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
            frmObj.Show();
            this.Close();
        }

        private void frmCarroPendienteSecador__Load(object sender, EventArgs e)
        {
            try
            {
                this.encabezado.Operador = this.lu.NomEmpleado;
                this.encabezado.PuestoTurno = this.lu.DesPuesto + " - " + this.lu.DesTurno;
                this.encabezado.Planta = this.lu.DesPlanta;
                this.encabezado.Titulo = "Carros Pendientes Para Secador";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Carros Pendientes"); }
        }
        private void frmCarroPendienteSecador__KeyUp(object sender, KeyEventArgs e)
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
        private void SetDataGridConfiguration()
        {
            try
            {
                DataTable dtCarroPendienteSecador = new CarroPendienteSecador().ObtenerCarroPendienteSecador(this.lu.CodPlanta);
                dgCarroSecador.DataSource = dtCarroPendienteSecador;
                DataGridTableStyle tableStyle1 = new DataGridTableStyle();
                tableStyle1.MappingName = dtCarroPendienteSecador.TableName;//Se indica a que TableName se aplicaran estas Propiedades

                DataGridColumnStyle cCarro = new DataGridTextBoxColumn();
                cCarro.MappingName = "Carro";
                cCarro.HeaderText = "Carro N°";
                cCarro.Width = (int)(dgCarroSecador.Width * .45); //Ocupara el 45% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cCarro);

                DataGridColumnStyle cCantidadPiezas = new DataGridTextBoxColumn();
                cCantidadPiezas.MappingName = "Piezas";
                cCantidadPiezas.HeaderText = "Cant. Piezas";
                cCantidadPiezas.Width = (int)(dgCarroSecador.Width * .45);//Ocupara el 45% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cCantidadPiezas);

                dgCarroSecador.TableStyles.Clear();
                dgCarroSecador.TableStyles.Add(tableStyle1);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}