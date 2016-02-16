using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Zona", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Zona:BaseSolutionEntity
    {
        #region Fields
        private int iCodZona = -1;
        private string sClaveZona = string.Empty;
        private string sDesZona = string.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;
        #endregion Fields
        #region Properties
        [DataMember(Name = "CodZona")]
        public int CodZona { get { return this.iCodZona; } set { this.iCodZona = value; } }
        [DataMember(Name = "ClaveZona")]
        public string ClaveZona { get { return this.sClaveZona; } set { this.sClaveZona = value; } }
        [DataMember(Name = "DesZona")]
        public string DesZona { get { return this.sDesZona; } set { this.sDesZona = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return this.dtFechaRegistro; } set { this.dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return this.dtFechaBaja; } set { this.dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }
        #endregion Properties
        #region Methods
        #region Constructors and Destructor

        public Zona()
        { }
        public Zona(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
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
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns></returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Zona());
        }
        public Zona(int iCodZona, string sClaveZona, string sDesZona, DateTime dtFechaRegistro, DateTime dtFechaBaja, bool bActivo)
        {
            this.iCodZona = iCodZona;
            this.sClaveZona = sClaveZona;
            this.sDesZona = sDesZona;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
        }
        ~Zona()
        {
        }
        #endregion Constructors and Destructor

        #endregion Methods
    }
}
