using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHModelo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHModelo : BaseSolutionEntity
    {

        #region Fields

        private int iCodModelo = -1;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodModelo")]
        public int CodModelo { get { return this.iCodModelo; } set { this.iCodModelo = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHModelo()
        {

        }
        public HHModelo(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHModelo
        (
            int iCodModelo
        )
        {
            this.iCodModelo = iCodModelo;
        }
        ~HHModelo()
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
            return GetPropertyNamesArray(new HHModelo());
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