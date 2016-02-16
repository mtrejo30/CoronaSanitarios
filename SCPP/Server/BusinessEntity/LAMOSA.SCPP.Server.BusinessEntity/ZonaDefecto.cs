using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ZonaDefecto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ZonaDefecto:BaseSolutionEntity 
    {
        #region PrivateFields
        private int iClaveUnica = -1;
        private string sClaveZonaDefecto = String.Empty;
        private string sDescripcion = String.Empty;
        private string sTipoArticulo = String.Empty;
        private string sClaveTipoArticulo = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "ClaveUnica")]
        public int ClaveUnica { get { return iClaveUnica; } set { iClaveUnica = value; } }
        [DataMember(Name = "ClaveZonaDefecto")]
        public string ClaveZonaDefecto { get { return sClaveZonaDefecto; } set { sClaveZonaDefecto = value; } }
        [DataMember(Name = "Descripcion")]
        public string Descripcion { get { return sDescripcion; } set { sDescripcion = value; } }
        [DataMember(Name = "TipoArticulo")]
        public string TipoArticulo { get { return sTipoArticulo; } set { sTipoArticulo = value; } }
        [DataMember(Name = "ClaveTipoArticulo")]
        public string ClaveTipoArticulo { get { return sClaveTipoArticulo; } set { sClaveTipoArticulo = value; } }

        #endregion

        #region Methods
        public ZonaDefecto(int iClaveUnica,
                string sClaveZonaDefecto,
                string sDescripcion,
                string sTipoArticulo,
                string sClaveTipoArticulo
        )
        {
            this.iClaveUnica = iClaveUnica;
            this.sClaveZonaDefecto = sClaveZonaDefecto;
            this.sDescripcion = sDescripcion;
            this.sTipoArticulo = sTipoArticulo;
            this.sClaveTipoArticulo = sClaveTipoArticulo;
        }
        public ZonaDefecto()
        { }
        public ZonaDefecto(DataRow row)
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
            return GetPropertyNamesArray(new ZonaDefecto());
        }
        ~ZonaDefecto()
        { }

        #endregion
    }// class
}

