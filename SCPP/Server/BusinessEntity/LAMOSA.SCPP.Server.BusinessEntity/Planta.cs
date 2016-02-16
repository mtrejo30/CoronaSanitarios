using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Planta", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Planta : BaseSolutionEntity
    {
        #region PrivateFields
        private int iClavePlanta = -1;
        private string sClaveLamosa = String.Empty;
        private string sDescripcionPlanta = String.Empty;
        private int iClaveAlmacen = -1;
        private string sDescripcionAmacen = String.Empty;
        private DateTime dtFechaBaja = DateTime.MinValue;

        #endregion

        #region Properties
        [DataMember(Name = "ClavePlanta")]
        public int ClavePlanta { get { return iClavePlanta; } set { iClavePlanta = value; } }
        [DataMember(Name = "ClaveLamosa")]
        public string ClaveLamosa { get { return sClaveLamosa; } set { sClaveLamosa = value; } }
        [DataMember(Name = "DescripcionPlanta")]
        public string DescripcionPlanta { get { return sDescripcionPlanta; } set { sDescripcionPlanta = value; } }
        [DataMember(Name = "ClaveAlmacen")]
        public int ClaveAlmacen { get { return iClaveAlmacen; } set { iClaveAlmacen = value; } }
        [DataMember(Name = "DescripcionAlmacen")]
        public string DescripcionAlmacen { get { return sDescripcionAmacen; } set { sDescripcionAmacen = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }

        #endregion

        #region Methods
        public Planta(int iClavePlanta,
                      string sClaveLamosa,
                      string sDescripcionPlanta,
                      int iClaveAlmacen,
                      string sDescripcionAmacen,
                      DateTime dtFechaBaja
        )
        {
            this.iClavePlanta = iClavePlanta;
            this.sClaveLamosa = sClaveLamosa;
            this.sDescripcionPlanta = sDescripcionPlanta;
            this.iClaveAlmacen = iClaveAlmacen;
            this.sDescripcionAmacen = sDescripcionAmacen;
            this.dtFechaBaja = dtFechaBaja;
        }
        public Planta()
        { }
        public Planta(DataRow row)
        {
            SetPropertiesFromDataRow(row);
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
            return GetPropertyNamesArray(new Planta());
        }
        ~Planta()
        { }

        #endregion
    } // class
}

