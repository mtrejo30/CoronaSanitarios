using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHEtiqueta", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHEtiqueta : BaseSolutionEntity
    {

        #region Fields

        private string sCodBarras = string.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodBarras")]
        public string CodBarras { get { return this.sCodBarras; } set { this.sCodBarras = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHEtiqueta()
        {

        }
        public HHEtiqueta(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHEtiqueta
        (
            string sCodBarras
        )
        {
            this.sCodBarras = sCodBarras;
        }
        ~HHEtiqueta()
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
            return GetPropertyNamesArray(new HHEtiqueta());
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