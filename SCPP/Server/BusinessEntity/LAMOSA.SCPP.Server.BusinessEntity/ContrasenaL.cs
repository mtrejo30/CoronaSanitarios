using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;


namespace LAMOSA.SCPP.Server.BusinessEntity
{


    [DataContract(Name = "ContrasenaL", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ContrasenaL : BaseSolutionEntity
    {
        #region PrivateFields
        private string iUsuario = String.Empty;
        private string sContrasena = String.Empty;
        private string sContrasenaNueva = String.Empty;
        private DateTime dtFechaVigPassword = DateTime.MinValue;

        #endregion
        #region Properties
        [DataMember(Name = "Usuario")]
        public string Usuario { get { return iUsuario; } set { iUsuario = value; } }

        [DataMember(Name = "Contrasena")]
        public string Contrasena { get { return sContrasena; } set { sContrasena = value; } }

        [DataMember(Name = "ContrasenaNueva")]
        public string ContrasenaNueva { get { return sContrasenaNueva; } set { sContrasenaNueva = value; } }

        [DataMember(Name = "FechaVigPassword")]
        public DateTime FechaVigPassword { get { return dtFechaVigPassword; } set { dtFechaVigPassword = value; } }

        #endregion
        #region Methods
        public ContrasenaL(string iUsuario,
              string sContrasena, string sContrasenaNueva,
               DateTime dtFechaVigPassword
        )
        {
            this.iUsuario = iUsuario;

            this.sContrasena = sContrasena;

            this.sContrasenaNueva = sContrasenaNueva;

            this.dtFechaVigPassword = dtFechaVigPassword;
        }
        public ContrasenaL()
        { }
        public ContrasenaL(DataRow row)
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
            return GetPropertyNamesArray(new ContrasenaL());
        }
        ~ContrasenaL()
        { }

        #endregion
    }
}

