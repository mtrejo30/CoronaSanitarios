using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHPieza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHPieza : BaseSolutionEntity
    {

        #region Fields

        private int iCodPieza = -1;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodPieza")]
        public int CodPieza { get { return this.iCodPieza; } set { this.iCodPieza = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHPieza()
        {

        }
        public HHPieza(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHPieza
        (
            int iCodPieza
        )
        {
            this.iCodPieza = iCodPieza;
        }
        ~HHPieza()
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
            return GetPropertyNamesArray(new HHPieza());
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