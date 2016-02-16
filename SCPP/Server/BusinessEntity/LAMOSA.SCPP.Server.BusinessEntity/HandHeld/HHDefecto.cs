using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHDefecto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHDefecto : BaseSolutionEntity
    {

        #region Fields

        private int iCodDefecto = -1;
        private string sDesDefecto = string.Empty;
        private int iCodZonaDefecto = -1;
        private string sDesZonaDefecto = string.Empty;
        private int iCodEstadoDefecto = -1;
        private string sDesEstadoDefecto = string.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodDefecto")]
        public int CodDefecto { get { return this.iCodDefecto; } set { this.iCodDefecto = value; } }
        [DataMember(Name = "DesDefecto")]
        public string DesDefecto { get { return this.sDesDefecto; } set { this.sDesDefecto = value; } }
        [DataMember(Name = "CodZonaDefecto")]
        public int CodZonaDefecto { get { return this.iCodZonaDefecto; } set { this.iCodZonaDefecto = value; } }
        [DataMember(Name = "DesZonaDefecto")]
        public string DesZonaDefecto { get { return this.sDesZonaDefecto; } set { this.sDesZonaDefecto = value; } }
        [DataMember(Name = "CodEstadoDefecto")]
        public int CodEstadoDefecto { get { return this.iCodEstadoDefecto; } set { this.iCodEstadoDefecto = value; } }
        [DataMember(Name = "DesEstadoDefecto")]
        public string DesEstadoDefecto { get { return this.sDesEstadoDefecto; } set { this.sDesEstadoDefecto = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHDefecto()
        {

        }
        public HHDefecto(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHDefecto
        (
            int iCodDefecto, 
            string sDesDefecto, 
            int iCodZonaDefecto, 
            string sDesZonaDefecto, 
            int iCodEstadoDefecto, 
            string sDesEstadoDefecto
        )
        {
            this.iCodDefecto = iCodDefecto;
            this.sDesDefecto = sDesDefecto;
            this.iCodZonaDefecto = iCodZonaDefecto;
            this.sDesZonaDefecto = sDesZonaDefecto;
            this.iCodEstadoDefecto = iCodEstadoDefecto;
            this.sDesEstadoDefecto = sDesEstadoDefecto;
        }
        ~HHDefecto()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region GetPropertyNamesArray
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns>string[]</returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new HHDefecto());
        }
        #endregion GetPropertyNamesArray
        #region GetPropertyValuesArray
        /// <summary>
        /// Obtiene un arreglo de objetos con los valores de las propiedades
        /// </summary>
        /// <returns>objetct[]</returns>
        public object[] ToObjectArray()
        {
            return GetPropertyValuesArray(this);
        }
        #endregion GetPropertyValuesArray

        #endregion Common

        #endregion Methods

    }
}