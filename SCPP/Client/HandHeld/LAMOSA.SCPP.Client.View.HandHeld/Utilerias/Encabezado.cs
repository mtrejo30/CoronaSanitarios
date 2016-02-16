using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.Layout;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public partial class Encabezado : UserControl
    {

        #region fields
        private EstadoConexion eEstadoConexion = EstadoConexion.Indeterminado;
        private Boolean bThreadActivado = false;
        private Thread tEstadoConexion = null;
        #endregion fields

        #region properties

        public string Operador { get { return this.lbOperador.Text; } set { this.lbOperador.Text = value; } }
        public string PuestoTurno { get { return this.lbPuestoTurno.Text; } set { this.lbPuestoTurno.Text = value; } }
        public string Planta { get { return this.lbPlanta.Text; } set { this.lbPlanta.Text = value; } }
        public string Titulo { get { return this.lbTitulo.Text; } set { this.lbTitulo.Text = value; } }
        public EstadoConexion Conexion { get { return this.eEstadoConexion; } set { this.eEstadoConexion = value; this.EstablecerEstadoConexion(); } }
        public string Mensaje { get { return this.lbMensaje.Text; } set { this.lbMensaje.Text = value; this.EstablecerEstiloMensaje(); } }

        #endregion properties

        #region methods

        #region constructors and destructor

        public Encabezado()
        {
            this.Disposed += new EventHandler(Encabezado_Disposed);
            InitializeComponent();
            this.btSalir.Click += new EventHandler(this.btSalir_Click);
        }
        ~Encabezado()
        {
        }

        #endregion constructors and destructor

        #region common

        #region EstablecerEstadoConexion
        private void EstablecerEstadoConexion()
        {
            if (tEstadoConexion == null)
            {
                tEstadoConexion = new Thread(new ThreadStart(EstadoConexionThread));
                tEstadoConexion.Start();
                bThreadActivado = true;
            }
            HelperView.EstablecerEstadoConexion(btEstadoConexion, this.eEstadoConexion);
        }
        #endregion EstablecerEstadoConexion
        #region EstablecerEstiloMensaje
        private void EstablecerEstiloMensaje()
        {
            if (String.IsNullOrEmpty(this.lbMensaje.Text))
            {
                HelperView.SetDefaultMessageStyle(this.lbMensaje);
            }
            else
            {
                HelperView.SetMessageStyle(this.lbMensaje);
            }
        }
        #endregion EstablecerEstiloMensaje
        #region EstadoConexionThread
        private void EstadoConexionThread()
        {
            while (true)
            {
                try
                {
                    this.Invoke(new EventHandler(excecute));
                    Thread.Sleep(1000);
                }
                catch (ObjectDisposedException e) { break; }
            }
        }

        void Encabezado_Disposed(object sender, EventArgs e)
        {
            tEstadoConexion.Abort();
        }

        private void excecute(object sender, EventArgs e)
        {
            try
            {
                if (new c00_Common().EstaServicioDisponible())
                    this.eEstadoConexion = EstadoConexion.Online;
                else
                    this.eEstadoConexion = EstadoConexion.Offline;
                EstablecerEstadoConexion();
            }
            catch (Exception er)
            {
                throw er;
            }
        }
        #endregion
        #endregion common

        #region event handlers

        #region btSalir_Click
        private void btSalir_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("¿Salir de la Aplicación?", "SCPP", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)
            {
                new a01_Login().Dispose();
                Application.Exit();
            }
        }
        #endregion btSalir_Click

        #endregion event handlers

        #endregion methods

    }
}
