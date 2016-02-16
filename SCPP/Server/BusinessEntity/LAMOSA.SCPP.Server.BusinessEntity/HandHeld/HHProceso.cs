using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHProceso", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHProceso : BaseSolutionEntity
    {

        #region Fields

        private int iCodProceso = -1;
        private string sDesProceso = string.Empty;
        private string sCalidad = string.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return this.iCodProceso; } set { this.iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return this.sDesProceso; } set { this.sDesProceso = value; } }
        [DataMember(Name = "Calidad")]
        public string Calidad { get { return this.sCalidad; } set { this.sCalidad = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHProceso()
        {

        }
        public HHProceso(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHProceso
        (
            int iCodProceso,
            string sDesProceso
        )
        {
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
        }
        ~HHProceso()
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
            return GetPropertyNamesArray(new HHProceso());
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