using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "KardexProductoDefecto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class KardexProductoDefecto : BaseSolutionEntity
    {
        #region PrivateFields
    
        private string sZona = String.Empty;
        private string sDefecto = String.Empty;
        private string sAccionDefecto = String.Empty;
       
        

        #endregion

        #region Properties
        [DataMember(Name = "Zona")]
        public string Zona { get { return sZona; } set { sZona = value; } }
        [DataMember(Name = "Defecto")]
        public string Defecto { get { return sDefecto; } set { sDefecto = value; } }
       [DataMember(Name = "AccionDefecto")]
        public string AccionDefecto { get { return sAccionDefecto; } set { sAccionDefecto = value; } }
       

        #endregion

        #region Methods
        public KardexProductoDefecto(
                string sZona,
                string sDefecto,
                string sAccionDefecto
                
        )
        {
            this.sZona = sZona;
            this.sDefecto = sDefecto;
            this.sAccionDefecto = sAccionDefecto;
          
        }
        public KardexProductoDefecto()
        { }
        public KardexProductoDefecto(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~KardexProductoDefecto()
        { }

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
            return GetPropertyNamesArray(new KardexProductoDefecto());
        }

        #endregion
    }
}
