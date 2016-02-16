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
    public partial class frmProduccionOperador : Form
    {
        #region Atributos
        LoginUsuario lu = new LoginUsuario();
        private c00_Common oDA0;
        private Produccion produccion;
        private Form frmCaptura;
        #endregion
        #region Propiedades
        private c00_Common DAO { get { return (oDA0 == null) ? oDA0 = new c00_Common() : oDA0; } }
        private Produccion Produccion { get { return (produccion == null) ? produccion = new Produccion() : produccion; } }
        public Form FormaCaptura { get { return frmCaptura; } set { frmCaptura = value; } }
        #endregion
        public frmProduccionOperador(LoginUsuario lu)
        {
            this.lu = lu;
            InitializeComponent();
        }

        #region Eventos
        private void frmProduccionOperador_Load(object sender, EventArgs e)
        {
            if (this.lu == null)
            {
                this.Close();
                return;
            }
            DateTime dtFechaServidor = DAO.ObtenerFechaServidor();
            this.encabezado.Operador = this.lu.NomEmpleado;
            this.encabezado.PuestoTurno = this.lu.DesPuesto + " - " + this.lu.DesTurno;
            this.encabezado.Planta = this.lu.DesPlanta;
            this.encabezado.Titulo = lu.DesProceso + " - " + dtFechaServidor.ToString("dd/MMM/yyyy");
            this.btnRegresar.Focus();
            this.InicializarControl(this.dgDetalleProduccion);
            this.Refresh();
        }
        private void btnRegresar_Click(object sender, EventArgs e)
        {
            this.FormaCaptura.Show();
            this.Close();
            this.Dispose();
        }
        #endregion
        #region Metodos Privados y Funciones
        private void InicializarControl(Control ctrl)
        {
            if (ctrl.GetType() == typeof(DataGrid))
                this.CargarGrid(ctrl as DataGrid);
        }
        private void CargarGrid(DataGrid dg)
        {
            DataTable dtProduccion = null;
            try
            {
                if (!dg.TableStyles.Contains("DetalleProduccion") ||
                    !dg.TableStyles["DetalleProduccion"].GridColumnStyles.Contains("Articulo") ||
                    !dg.TableStyles["DetalleProduccion"].GridColumnStyles.Contains("PiezasBuenas") ||
                    !dg.TableStyles["DetalleProduccion"].GridColumnStyles.Contains("PiezasDesperdicio"))
                    throw new Exception("Problemas con la estructura \ndel detalle de producción.");
                dtProduccion = ObtenerDetalleProduccion(this.lu.CodEmpleado, this.lu.CodProceso);
                if (dtProduccion == null || dtProduccion.Rows.Count == 0)
                    throw new Exception("No se tiene registrada producción.");
                dtProduccion.TableName = dg.TableStyles["DetalleProduccion"].MappingName;
                dtProduccion.Columns[0].ColumnName = dg.TableStyles["DetalleProduccion"].GridColumnStyles["Articulo"].MappingName;
                dtProduccion.Columns[1].ColumnName = dg.TableStyles["DetalleProduccion"].GridColumnStyles["PiezasBuenas"].MappingName;
                dtProduccion.Columns[2].ColumnName = dg.TableStyles["DetalleProduccion"].GridColumnStyles["PiezasDesperdicio"].MappingName;
                DataRow row = dtProduccion.NewRow();
                int iPiezasBuenas = dtProduccion.Select().Sum(x => x.Field<int>("PiezasBuenas"));
                int iPiezasDesperdicio = dtProduccion.Select().Sum(x => x.Field<int>("PiezasDesperdicio"));
                row["Articulo"] = "TOTAL  = " + (iPiezasBuenas + iPiezasDesperdicio).ToString() + "";
                row["PiezasBuenas"] = iPiezasBuenas;
                row["PiezasDesperdicio"] = iPiezasDesperdicio;
                dtProduccion.Rows.Add(row);
                dg.DataSource = dtProduccion;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Detalle de Producción", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (dtProduccion != null) dtProduccion.Dispose(); 
            }
        }
        private DataTable ObtenerDetalleProduccion(int iCodigoOperador, int iCodigoProduccion)
        {
            return Produccion.Obtener(iCodigoOperador, iCodigoProduccion);
        }
        #endregion
    }
}