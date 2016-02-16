using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;


namespace LAMOSA.SCPP.Server.BusinessEntity
{


    [DataContract(Name = "Usuario", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Usuario:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodUsuario = -1;
        private int iCodEmpleado = -1;
        private string sNombre = String.Empty;
        private string sAPaterno = String.Empty;
        private string sAMaterno = String.Empty;
        private string sEmail = String.Empty;
        private int iCodRol = -1;
        private string sDesRol = String.Empty;
        private string sNombreUsuario = String.Empty;
        private string sContrasena = String.Empty;
        private int iCodSupervisor = -1;
        private int iActivo = -1;
        private bool bBloqueado = false;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaVigPassword = DateTime.MinValue;

        #endregion

        #region Properties
        [DataMember(Name = "CodUsuario")]
        public int CodUsuario { get { return iCodUsuario; } set { iCodUsuario = value; } }
        [DataMember(Name = "CodEmpleado")]
        public int CodEmpleado { get { return iCodEmpleado; } set { iCodEmpleado = value; } }
        [DataMember(Name = "Nombre")]
        public string Nombre { get { return sNombre; } set { sNombre = value; } }
        [DataMember(Name = "APaterno")]
        public string APaterno { get { return sAPaterno; } set { sAPaterno = value; } }
        [DataMember(Name = "AMaterno")]
        public string AMaterno { get { return sAMaterno; } set { sAMaterno = value; } }
        [DataMember(Name = "Email")]
        public string Email { get { return sEmail; } set { sEmail = value; } }
        [DataMember(Name = "CodRol")]
        public int CodRol { get { return iCodRol; } set { iCodRol = value; } }
        [DataMember(Name = "DesRol")]
        public string DesRol { get { return sDesRol; } set { sDesRol = value; } }
        [DataMember(Name = "NombreUsuario")]
        public string NombreUsuario { get { return sNombreUsuario; } set { sNombreUsuario = value; } }
        [DataMember(Name = "Contrasena")]
        public string Contrasena { get { return sContrasena; } set { sContrasena = value; } }
        [DataMember(Name = "CodSupervisor")]
        public int CodSupervisor { get { return iCodSupervisor; } set { iCodSupervisor = value; } }
        [DataMember(Name = "Activo")]
        public int Activo { get { return iActivo; } set { iActivo = value; } }
        [DataMember(Name = "Bloqueado")]
        public bool Bloqueado { get { return bBloqueado; } set { bBloqueado = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
        [DataMember(Name = "FechaVigPassword")]
        public DateTime FechaVigPassword { get { return dtFechaVigPassword; } set { dtFechaVigPassword = value; } }

        #endregion

        #region Methods
        public Usuario(int iCodUsuario, int iCodEmpleado,
                string sNombre,
                string sAPaterno,
                string sAMaterno,
                string sEmail,
                int iCodRol,
                string sDesRol,
                string sUsuario,
                string sContrasena,
                int iCodSupervisor,
                int iActivo,
                bool bBloqueado,
                DateTime dtFechaRegistro,
                DateTime dtFechaVigPassword
        )
        {
            this.iCodUsuario = iCodUsuario;
            this.iCodEmpleado = iCodEmpleado;

            this.sNombre = sNombre;
            this.sAPaterno = sAPaterno;
            this.sAMaterno = sAMaterno;


            this.sEmail = sEmail;
            this.iCodRol = iCodRol;
            this.sDesRol = sDesRol;
            this.sNombreUsuario = sUsuario;

            this.sContrasena = sContrasena;
            this.iCodSupervisor = iCodSupervisor;
            this.iActivo = iActivo;
            this.bBloqueado = bBloqueado;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaVigPassword = dtFechaVigPassword;
        }
        public Usuario()
        { }
        public Usuario(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        /// <summary>
        /// Obtiene un arreglo de objetos con los valores de las propiedades
        /// </summary>
        /// <returns>objetct[]</returns>
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades para crear columnas de una tabla
        /// </summary>
        /// <remarks>para crear columnas de una tabla</remarks>
        /// <returns>string[]</returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Usuario());
        }
        ~Usuario()
        { }

        #endregion
    }
}

