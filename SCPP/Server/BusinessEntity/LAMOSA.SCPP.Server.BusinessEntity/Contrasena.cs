
using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;


namespace LAMOSA.SCPP.Server.BusinessEntity
{


    [DataContract(Name = "Contrasena", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Contrasena:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodUsuario = -1;
        private string sContrasena = String.Empty;
        private DateTime dtFechaVigPassword = DateTime.MinValue;

        #endregion
        #region Properties
        [DataMember(Name = "CodUsuario")]
        public int CodUsuario { get { return iCodUsuario; } set { iCodUsuario = value; } }

        [DataMember(Name = "Contrasena")]
        public string Contrasena { get { return sContrasena; } set { sContrasena = value; } }
       
        [DataMember(Name = "FechaVigPassword")]
        public DateTime FechaVigPassword { get { return dtFechaVigPassword; } set { dtFechaVigPassword = value; } }

        #endregion
        #region Methods
        public Contrasena(int iCodUsuario,
              string sContrasena,
               DateTime dtFechaVigPassword
        )
        {
            this.iCodUsuario = iCodUsuario;
            
            this.sContrasena = sContrasena;
           
            this.dtFechaVigPassword = dtFechaVigPassword;
        }
        public Contrasena()
        { }
        public Contrasena(DataRow row)
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
        ~Contrasena()
        { }

        #endregion
    }
}

