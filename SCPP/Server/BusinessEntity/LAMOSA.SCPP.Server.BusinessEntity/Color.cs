using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Color", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Color : BaseSolutionEntity
    {

        #region Fields

        private int iCodColor = -1;
        private string sClaveColor = String.Empty;
        private string sDesColor = String.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodColor")]
        public int CodColor { get { return this.iCodColor; } set { this.iCodColor = value; } }
        [DataMember(Name = "ClaveColor")]
        public string ClaveColor { get { return this.sClaveColor; } set { this.sClaveColor = value; } }
        [DataMember(Name = "DesColor")]
        public string DesColor { get { return this.sDesColor; } set { this.sDesColor = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return this.dtFechaRegistro; } set { this.dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return this.dtFechaBaja; } set { this.dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }

        #endregion Properties

        #region Methods

        public Color()
        {

        }
        public Color(DataRow row)
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
            return GetPropertyNamesArray(new Color());
        }
        public Color
        (
            int iCodColor,
            string sClaveColor,
            string sDesColor,
            DateTime dtFechaRegistro,
            DateTime dtFechaBaja,
            bool bActivo
        )
        {
            this.iCodColor = iCodColor;
            this.sClaveColor = sClaveColor;
            this.sDesColor = sDesColor;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
        }
        ~Color()
        {

        }

        #endregion Methods

    }
}
