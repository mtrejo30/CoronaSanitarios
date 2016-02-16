using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHColor", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHColor : BaseSolutionEntity
    {

        #region Fields

        private int iCodColor = -1;
        private string sClaveColor = string.Empty;
        private string sDesColor = string.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodColor")]
        public int CodColor { get { return this.iCodColor; } set { this.iCodColor = value; } }
        [DataMember(Name = "ClaveColor")]
        public string ClaveColor { get { return this.sClaveColor; } set { this.sClaveColor = value; } }
        [DataMember(Name = "DesColor")]
        public string DesColor { get { return this.sDesColor; } set { this.sDesColor = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHColor()
        {

        }
        public HHColor(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHColor
        (
            int iCodColor,
            string sClaveColor,
            string sDesColor
        )
        {
            this.iCodColor = iCodColor;
            this.sClaveColor = sClaveColor;
            this.sDesColor = sDesColor;
        }
        ~HHColor()
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
            return GetPropertyNamesArray(new HHColor());
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