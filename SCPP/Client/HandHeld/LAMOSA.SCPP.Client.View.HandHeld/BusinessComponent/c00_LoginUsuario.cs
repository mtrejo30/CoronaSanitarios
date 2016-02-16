using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class LoginUsuario
    {

        #region fields

        // Login
        private int iCodUsuario = -1;
        private string sLogin = string.Empty;
        private string sPassword = string.Empty;
        private int iCodEmpleado = -1;
        private string sNomEmpleado = string.Empty;
        private int iCodSupervisor = -1;
        private int iCodRol = -1;
        private string sDesRol = string.Empty;
        private int iCodPuesto = -1;
        private string sDesPuesto = string.Empty;
        private bool bBloqueado = false;
        private DateTime dtFechaVigPassword = DateTime.MinValue;
        private bool bIsLogin = false;
        private string sMensaje = string.Empty;

        // Seleccion Planta
        private int iCodPlanta = -1;
        private string sDesPlanta = string.Empty;

        // Configuracion Inicial
        private DateTime dtFecha = DateTime.MinValue;
        private int iCodTurno = -1;
        private string sDesTurno = string.Empty;
        private int iCodProceso = -1;
        private string sDesProceso = string.Empty;
        private int iCodPantalla = -1;
        private long lCodConfigHandHeld = -1;
        private eTipoAuditoria enumTipoAuditoria = eTipoAuditoria.Indeterminado;
        private int iBase = -1;
        private int iMolde = -1;

        // Captura Inicial
        private int iCodCentroTrabajo = -1;
        private int iCodConfigBanco = -1;
        private int iCodMaquina = -1;
        private string sClaveMaquina = string.Empty;
        private string sDesMaquina = string.Empty;
        private int iCodTipoMaquina = -1;
        private string sDesTipoMaquina = string.Empty;
        private int iPosInicial = -1;
        private bool bAscendente = false;
        private int iCodColor = -1;
        private string sClaveColor = string.Empty;
        private string sDesColor = string.Empty;
        private int iCodCarro = -1;

        // Defectos
        private int iCodPieza = -1;
        private string sCodBarras = string.Empty;

        #endregion fields

        #region properties

        // Login 
        public int CodUsuario { get { return this.iCodUsuario; } set { this.iCodUsuario = value; } }
        public string Login { get { return this.sLogin; } set { this.sLogin = value; } }
        public string Password { get { return this.sPassword; } set { this.sPassword = value; } }
        public int CodEmpleado { get { return this.iCodEmpleado; } set { this.iCodEmpleado = value; } }
        public string NomEmpleado { get { return this.sNomEmpleado; } set { this.sNomEmpleado = value; } }
        public int CodSupervisor { get { return this.iCodSupervisor; } set { this.iCodSupervisor = value; } }
        public int CodRol { get { return this.iCodRol; } set { this.iCodRol = value; } }
        public string DesRol { get { return this.sDesRol; } set { this.sDesRol = value; } }
        public int CodPuesto { get { return this.iCodPuesto; } set { this.iCodPuesto = value; } }
        public string DesPuesto { get { return this.sDesPuesto; } set { this.sDesPuesto = value; } }
        public bool Bloqueado { get { return this.bBloqueado; } set { this.bBloqueado = value; } }
        public DateTime FechaVigPassword { get { return this.dtFechaVigPassword; } set { this.dtFechaVigPassword = value; } }
        public bool IsLogin { get { return this.bIsLogin; } set { this.bIsLogin = value; } }
        public string Mensaje { get { return this.sMensaje; } set { this.sMensaje = value; } }

        // Seleccion Planta
        public int CodPlanta { get { return this.iCodPlanta; } set { this.iCodPlanta = value; } }
        public string DesPlanta { get { return this.sDesPlanta; } set { this.sDesPlanta = value; } }

        // Configuracion Inicial
        public DateTime Fecha { get { return this.dtFecha; } set { this.dtFecha = value; } }
        public int CodTurno { get { return this.iCodTurno; } set { this.iCodTurno = value; } }
        public string DesTurno { get { return this.sDesTurno; } set { this.sDesTurno = value; } }
        public int CodProceso { get { return this.iCodProceso; } set { this.iCodProceso = value; } }
        public string DesProceso { get { return this.sDesProceso; } set { this.sDesProceso = value; } }
        public int CodPantalla { get { return this.iCodPantalla; } set { this.iCodPantalla = value; } }
        public long CodConfigHandHeld { get { return this.lCodConfigHandHeld; } set { this.lCodConfigHandHeld = value; } }
        public eTipoAuditoria TipoAuditoria { get { return this.enumTipoAuditoria; } set { this.enumTipoAuditoria = value; } }
        public int CodBase { get { return this.iBase; } set { this.iBase = value; } }
        public int CodMolde { get { return this.iMolde; } set { this.iMolde = value; } }

        // Captura Inicial
        public int CodCentroTrabajo { get { return this.iCodCentroTrabajo; } set { this.iCodCentroTrabajo = value; } }
        public int CodConfigBanco { get { return this.iCodConfigBanco; } set { this.iCodConfigBanco = value; } }
        public int CodMaquina { get { return this.iCodMaquina; } set { this.iCodMaquina = value; } }
        public string ClaveMaquina { get { return this.sClaveMaquina; } set { this.sClaveMaquina = value; } }
        public string DesMaquina { get { return this.sDesMaquina; } set { this.sDesMaquina = value; } }
        public int CodTipoMaquina { get { return this.iCodTipoMaquina; } set { this.iCodTipoMaquina = value; } }
        public string DesTipoMaquina { get { return this.sDesTipoMaquina; } set { this.sDesTipoMaquina = value; } }
        public int PosInicial { get { return this.iPosInicial; } set { this.iPosInicial = value; } }
        public bool Ascendente { get { return this.bAscendente; } set { this.bAscendente = value; } }
        public int CodColor { get { return this.iCodColor; } set { this.iCodColor = value; } }
        public string ClaveColor { get { return this.sClaveColor; } set { this.sClaveColor = value; } }
        public string DesColor { get { return this.sDesColor; } set { this.sDesColor = value; } }
        public int CodCarro { get { return this.iCodCarro; } set { this.iCodCarro = value; } }
        
        // Defectos
        public int CodPieza { get { return this.iCodPieza; } set { this.iCodPieza = value; } }
        public string CodBarras { get { return this.sCodBarras; } set { this.sCodBarras = value; } }
        
        #endregion properties

        #region methods

        #region constructors and destructor
        public LoginUsuario()
        {

        }
        ~LoginUsuario()
        {

        }
        #endregion constructors and destructor

        #endregion methods

    }
}
