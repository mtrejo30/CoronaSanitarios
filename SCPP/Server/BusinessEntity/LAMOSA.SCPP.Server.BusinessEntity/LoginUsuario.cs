using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "LoginUsuario", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class LoginUsuario : BaseSolutionEntity
    {

        #region Fields

        private string sLogin = String.Empty;
        private string sPassword = String.Empty;
        private int iCodUsuario = -1;
        private string sMensaje = String.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "Login")]
        public string Login { get { return this.sLogin; } set { this.sLogin = value; } }
        [DataMember(Name = "Password")]
        public string Password { get { return this.sPassword; } set { this.sPassword = value; } }
        [DataMember(Name = "CodUsuario")]
        public int CodUsuario { get { return this.iCodUsuario; } set { this.iCodUsuario = value; } }
        [DataMember(Name = "Mensaje")]
        public string Mensaje { get { return this.sMensaje; } set { this.sMensaje = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public LoginUsuario()
        {

        }
        public LoginUsuario(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public LoginUsuario
        (
            string sLogin,
            string sPassword,
            int iCodUsuario,
            string sMensaje
        )
        {
            this.sLogin = sLogin;
            this.sPassword = sPassword;
            this.iCodUsuario = iCodUsuario;
            this.sMensaje = sMensaje;
        }
        ~LoginUsuario()
        {

        }

        #endregion Constructors and Destructor

        #endregion Methods

    } // class
}