using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHEstadoPieza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHEstadoPieza : BaseSolutionEntity
    {

        #region Fields

        private int iCodEstadoPieza = -1;
        private string sDesEstadoPieza = string.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodEstadoPieza")]
        public int CodEstadoPieza { get { return this.iCodEstadoPieza; } set { this.iCodEstadoPieza = value; } }
        [DataMember(Name = "DesEstadoPieza")]
        public string DesEstadoPieza { get { return this.sDesEstadoPieza; } set { this.sDesEstadoPieza = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHEstadoPieza()
        {

        }
        public HHEstadoPieza(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHEstadoPieza
        (
            int iCodEstadoPieza, 
            string sDesEstadoPieza
        )
        {
            this.iCodEstadoPieza = iCodEstadoPieza;
            this.sDesEstadoPieza = sDesEstadoPieza;
        }
        ~HHEstadoPieza()
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
            return GetPropertyNamesArray(new HHEstadoPieza());
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
