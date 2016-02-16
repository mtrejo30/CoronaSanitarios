using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Imagen", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Imagen : BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodImagen = -1;
        private string sNombre = string.Empty;
        private string sDescripcion = string.Empty;
        private string sUbicacion = string.Empty;
        #endregion
        #region Properties
        [DataMember(Name = "CodImagen")]
        public int CodImagen { get { return iCodImagen; } set { iCodImagen = value; } }
        [DataMember(Name = "Nombre")]
        public string Nombre { get { return sNombre; } set { sNombre = value; } }
        [DataMember(Name = "Descripcion")]
        public string Descripcion { get { return sDescripcion; } set { sDescripcion = value; } }
        [DataMember(Name = "Ubicacion")]
        public string Ubicacion { get { return sUbicacion; } set { sUbicacion = value; } }
        #endregion
        #region Methods
        public Imagen(int iCodImagen, string sNombre, string sDescripcion, string sUbicacion)
        {
            this.iCodImagen = iCodImagen;
            this.sNombre = sNombre;
            this.sDescripcion = sDescripcion;
            this.sUbicacion = sUbicacion;
        }
        public Imagen()
        { }
        public Imagen(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Accion());
        }
        ~Imagen()
        { }
        #endregion
    }
}
