using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHCalidad", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHCalidad : BaseSolutionEntity
    {

        #region Fields

        private int iCodCalidad = -1;
        private string sClaveCalidad = string.Empty;
        private string sDesCalidad = string.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodCalidad")]
        public int CodCalidad { get { return this.iCodCalidad; } set { this.iCodCalidad = value; } }
        [DataMember(Name = "ClaveCalidad")]
        public string ClaveCalidad { get { return this.sClaveCalidad; } set { this.sClaveCalidad = value; } }
        [DataMember(Name = "DesCalidad")]
        public string DesCalidad { get { return this.sDesCalidad; } set { this.sDesCalidad = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHCalidad()
        {

        }
        public HHCalidad(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHCalidad
        (
            int iCodCalidad,
            string sClaveCalidad,
            string sDesCalidad
        )
        {
            this.iCodCalidad = iCodCalidad;
            this.sClaveCalidad = sClaveCalidad;
            this.sDesCalidad = sDesCalidad;
        }
        ~HHCalidad()
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
            return GetPropertyNamesArray(new HHCalidad());
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