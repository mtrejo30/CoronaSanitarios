using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Almacen", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Almacen:BaseSolutionEntity
    {
        #region PrivateFields
        private int iClaveAlmacen = -1;
        private string sClaveLamosa = String.Empty;
        private string sDescripcion = String.Empty;
       

        #endregion

        #region Properties
        [DataMember(Name = "ClaveAlmacen")]
        public int ClaveAlmacen { get { return iClaveAlmacen; } set { iClaveAlmacen = value; } }
        [DataMember(Name = "ClaveLamosa")]
        public string ClaveLamosa { get { return sClaveLamosa; } set { sClaveLamosa = value; } }
        [DataMember(Name = "Descripcion")]
        public string Descripcion { get { return sDescripcion; } set { sDescripcion = value; } }


        #endregion

        #region Methods
        public Almacen(int iClaveAlmacen, string sClaveLamosa,
                string sDescripcion
        )
        {
            this.iClaveAlmacen = iClaveAlmacen;
            this.sClaveLamosa = sClaveLamosa;
            this.sDescripcion = sDescripcion;
           
        }
        public Almacen()
        { }
        public Almacen(DataRow row)
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
            return GetPropertyNamesArray(new Almacen());
        }
        ~Almacen()
        { }

        #endregion
    }
}

