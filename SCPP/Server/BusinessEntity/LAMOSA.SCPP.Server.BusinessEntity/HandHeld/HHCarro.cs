using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHCarro", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHCarro : BaseSolutionEntity
    {

        #region Fields

        private int iCodCarro = -1;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodCarro")]
        public int CodCarro { get { return this.iCodCarro; } set { this.iCodCarro = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHCarro()
        {

        }
        public HHCarro(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHCarro
        (
            int iCodCarro
        )
        {
            this.iCodCarro = iCodCarro;
        }
        ~HHCarro()
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
            return GetPropertyNamesArray(new HHCarro());
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