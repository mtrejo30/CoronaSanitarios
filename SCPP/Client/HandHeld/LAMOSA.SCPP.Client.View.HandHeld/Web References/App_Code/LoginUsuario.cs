using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class LoginUsuario
    {

        #region fields

        private int iCodUsuario = -1;
        private string sLogin = string.Empty;
        private string sPassword = string.Empty;
        private int iCodEmpleado = -1;
        private string sNomEmpleado = string.Empty;
        private int iCodRol = -1;
        private string sDesRol = string.Empty;
        private int iCodPuesto = -1;
        private string sDesPuesto = string.Empty;
        private bool bBloqueado = false;
        private DateTime dtFechaVigPassword = DateTime.MinValue;
        private bool bIsLogin = false;
        private string sMensaje = string.Empty;

        private int iCodPlanta = -1;
        private string sDesPlanta = string.Empty;

        // Configuracion Inicial
        private DateTime dtFecha = DateTime.MinValue;
        private int iCodTurno = -1;
        private string sDesTurno = string.Empty;
        private int iCodProceso = -1;
        private string sDesProceso = string.Empty;
        private long lCodConfigHandHeld = -1;

        // Captura Inicial
        private int iCodSupervisor = -1;
        private int iCodCentroTrabajo = -1;
        private int iCodMaquina = -1;
        private int iCodConfigBanco = -1;
        private int iPosInicial = -1;
        private bool bAscendente = false;

        #endregion fields

        #region properties

        public int CodUsuario { get { return this.iCodUsuario; } set { this.iCodUsuario = value; } }
        public string Login { get { return this.sLogin; } set { this.sLogin = value; } }
        public string Password { get { return this.sPassword; } set { this.sPassword = value; } }
        public int CodEmpleado { get { return this.iCodEmpleado; } set { this.iCodEmpleado = value; } }
        public string NomEmpleado { get { return this.sNomEmpleado; } set { this.sNomEmpleado = value; } }
        public int CodRol { get { return this.iCodRol; } set { this.iCodRol = value; } }
        public string DesRol { get { return this.sDesRol; } set { this.sDesRol = value; } }
        public int CodPuesto { get { return this.iCodPuesto; } set { this.iCodPuesto = value; } }
        public string DesPuesto { get { return this.sDesPuesto; } set { this.sDesPuesto = value; } }
        public bool Bloqueado { get { return this.bBloqueado; } set { this.bBloqueado = value; } }
        public DateTime FechaVigPassword { get { return this.dtFechaVigPassword; } set { this.dtFechaVigPassword = value; } }
        public bool IsLogin { get { return this.bIsLogin; } set { this.bIsLogin = value; } }
        public string Mensaje { get { return this.sMensaje; } set { this.sMensaje = value; } }

        public int CodPlanta { get { return this.iCodPlanta; } set { this.iCodPlanta = value; } }
        public string DesPlanta { get { return this.sDesPlanta; } set { this.sDesPlanta = value; } }

        // Configuracion Inicial
        public DateTime Fecha { get { return this.dtFecha; } set { this.dtFecha = value; } }
        public int CodTurno { get { return this.iCodTurno; } set { this.iCodTurno = value; } }
        public string DesTurno { get { return this.sDesTurno; } set { this.sDesTurno = value; } }
        public int CodProceso { get { return this.iCodProceso; } set { this.iCodProceso = value; } }
        public string DesProceso { get { return this.sDesProceso; } set { this.sDesProceso = value; } }
        public long CodConfigHandHeld { get { return this.lCodConfigHandHeld; } set { this.lCodConfigHandHeld = value; } }

        // Captura Inicial
        public int CodSupervisor { get { return this.iCodSupervisor; } set { this.iCodSupervisor = value; } }
        public int CodCentroTrabajo { get { return this.iCodCentroTrabajo; } set { this.iCodCentroTrabajo = value; } }
        public int CodMaquina { get { return this.iCodMaquina; } set { this.iCodMaquina = value; } }
        public int CodConfigBanco { get { return this.iCodConfigBanco; } set { this.iCodConfigBanco = value; } }
        public int PosInicial { get { return this.iPosInicial; } set { this.iPosInicial = value; } }
        public bool Ascendente { get { return this.bAscendente; } set { this.bAscendente = value; } }

        #endregion properties

        #region methods

        #region constructors and destructor
        public LoginUsuario()
        {

        }
        public LoginUsuario
        (
            int iCodUsuario,
            string sLogin,
            string sPassword,
            int iCodEmpleado,
            string sNomEmpleado,
            int iCodRol,
            string sDesRol,
            int iCodPuesto,
            string sDesPuesto,
            bool bBloqueado,
            DateTime dtFechaVigPassword
        )
        {
            this.iCodUsuario = iCodUsuario;
            this.sLogin = sLogin;
            this.sPassword = sPassword;
            this.iCodEmpleado = iCodEmpleado;
            this.sNomEmpleado = sNomEmpleado;
            this.iCodRol = iCodRol;
            this.sDesRol = sDesRol;
            this.iCodPuesto = iCodPuesto;
            this.sDesPuesto = sDesPuesto;
            this.bBloqueado = bBloqueado;
            this.dtFechaVigPassword = dtFechaVigPassword;
        }
        ~LoginUsuario()
        {

        }
        #endregion constructors and destructor

        #endregion methods

    }
}
