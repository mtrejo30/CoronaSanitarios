using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ArticuloCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ArticuloCbo : BaseSolutionEntity
    {

        #region Fields

        private int iCodArticulo = -1;
        private string sClaveArticulo = String.Empty;
        private string sDesArticulo = String.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodArticulo")]
        public int CodArticulo { get { return this.iCodArticulo; } set { this.iCodArticulo = value; } }
        [DataMember(Name = "ClaveArticulo")]
        public string ClaveArticulo { get { return this.sClaveArticulo; } set { this.sClaveArticulo = value; } }
        [DataMember(Name = "DesArticulo")]
        public string DesArticulo { get { return this.sDesArticulo; } set { this.sDesArticulo = value; } }

        #endregion Properties

        #region Methods

        public ArticuloCbo()
        {

        }
        public ArticuloCbo(DataRow row)
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
            return GetPropertyNamesArray(new ArticuloCbo());
        }

        ~ArticuloCbo()
        {

        }

        #endregion Methods

    }
}