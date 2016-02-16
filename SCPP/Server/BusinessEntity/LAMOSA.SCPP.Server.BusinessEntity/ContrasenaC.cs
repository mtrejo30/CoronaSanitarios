using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;


namespace LAMOSA.SCPP.Server.BusinessEntity
{


    [DataContract(Name = "ContrasenaC", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ContrasenaC:BaseSolutionEntity
    {
        #region PrivateFields
        private int icodUsuario = -1;
        private string sContrasena = String.Empty;
        private string sContrasenaNueva = String.Empty;
        private DateTime dtFechaVigPassword = DateTime.MinValue;

        #endregion
        #region Properties
        [DataMember(Name = "codUsuario")]
        public int codUsuario { get { return icodUsuario; } set { icodUsuario = value; } }

        [DataMember(Name = "Contrasena")]
        public string Contrasena { get { return sContrasena; } set { sContrasena = value; } }

        [DataMember(Name = "ContrasenaNueva")]
        public string ContrasenaNueva { get { return sContrasenaNueva; } set { sContrasenaNueva = value; } }

        [DataMember(Name = "FechaVigPassword")]
        public DateTime FechaVigPassword { get { return dtFechaVigPassword; } set { dtFechaVigPassword = value; } }

        #endregion
        #region Methods
        public ContrasenaC(int icodUsuario,
              string sContrasena, string sContrasenaNueva,
               DateTime dtFechaVigPassword
        )
        {
            this.icodUsuario = icodUsuario;

            this.sContrasena = sContrasena;

            this.sContrasenaNueva = sContrasenaNueva;
           
            this.dtFechaVigPassword = dtFechaVigPassword;
        }
        public ContrasenaC()
        { }
        public ContrasenaC(DataRow row)
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
            return GetPropertyNamesArray(new ContrasenaC());
        }
        ~ContrasenaC()
        { }

        #endregion
    }
}

