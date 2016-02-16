using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAMOSA.SCPP.Client.View.HandHeld.BusinessComponent;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class frmKardex : Form
    {
        LoginUsuario lu = new LoginUsuario();
        public frmKardex(LoginUsuario lu)
        {
            this.lu = lu;
            InitializeComponent();
        }
        private void frmKardex_Load(object sender, EventArgs e)
        {
            this.encabezado.Operador = this.lu.NomEmpleado;
            this.encabezado.PuestoTurno = this.lu.DesPuesto + " - " + this.lu.DesTurno;
            this.encabezado.Planta = this.lu.DesPlanta;
            this.encabezado.Titulo = "Configuración Inicial";
            txtEtiqueta.Focus();
        }
        private void frmKardex_KeyUp(object sender, KeyEventArgs e)
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
        private bool EsNumero(char caracter)
        {
            return Char.IsNumber(caracter);
        }
        private void txtEtiqueta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 8) return;// Backspace)
                if (e.KeyChar == 13)// Enter
                {
                    txtModelo.Text = string.Empty;
                    txtColor.Text = string.Empty;
                    txtCalidad.Text = string.Empty;
                    dgDetalle.DataSource = null;
                    DataSet dsKardex = new Kardex().ObtenerKardexPieza(null, txtEtiqueta.Text);
                    txtModelo.Text = dsKardex.Tables[0].Rows[0]["Modelo"].ToString();
                    txtColor.Text = dsKardex.Tables[0].Rows[0]["Color"].ToString();
                    txtCalidad.Text = dsKardex.Tables[0].Rows[0]["Calidad"].ToString();
                    dsKardex.Tables[1].TableName = "HistoriaPieza";
                    dgDetalle.DataSource = dsKardex.Tables[1];
                    SetDataGridContiguration();
                    txtEtiqueta.Text = string.Empty;
                    txtEtiqueta.Focus();
                }
                else e.Handled = !EsNumero(e.KeyChar);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Kardex"); }
        }
        private void SetDataGridContiguration()
        {
            try
            {
                DataGridTableStyle tableStyle1 = new DataGridTableStyle();
                tableStyle1.MappingName = "HistoriaPieza";//Se indica a que TableName se aplicaran estas Propiedades

                DataGridColumnStyle cProceso = new DataGridTextBoxColumn();
                cProceso.MappingName = "Proceso";
                cProceso.HeaderText = "Proceso";
                cProceso.Width = (int)(dgDetalle.Width * .3); //Ocupara el 30% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cProceso);

                DataGridColumnStyle cFecha = new DataGridTextBoxColumn();
                cFecha.MappingName = "Fecha";
                cFecha.HeaderText = "Fecha";
                cFecha.Width = (int)(dgDetalle.Width * .25);//Ocupara el 25% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cFecha);

                DataGridColumnStyle cDesperdicio = new DataGridTextBoxColumn();
                cDesperdicio.MappingName = "Desperdicio";
                cDesperdicio.HeaderText = "Desperdicio";
                cDesperdicio.Width = (int)(dgDetalle.Width * .35);//Ocupara el 35% del ancho de la tabla
                tableStyle1.GridColumnStyles.Add(cDesperdicio);
                dgDetalle.TableStyles.Clear();
                dgDetalle.TableStyles.Add(tableStyle1);
            }
            catch (Exception ex) { throw ex; }
        }

        private void btRegresar_Click(object sender, EventArgs e)
        {
            a03_ConfiguracionInicial frmObj = new a03_ConfiguracionInicial(this.lu);
            frmObj.Show();
            this.Close();
        }
    }
}