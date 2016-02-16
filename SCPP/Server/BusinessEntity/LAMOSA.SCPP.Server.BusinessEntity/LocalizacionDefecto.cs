using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "LocalizacionDefecto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class LocalizacionDefecto : BaseSolutionEntity
    {
        #region PrivateFields
        private int? iCodPiezaDefectoDetalle;
        private int? iCodImagen;
        private int? iCodModelo;
        private int? iCodZona;
        private int? iPosicionH;
        private int? iPosicionV;
        #endregion
        #region Properties
        [DataMember(Name = "CodPiezaDefectoDetalle")]
        public int? CodPiezaDefectoDetalle { get { return iCodPiezaDefectoDetalle; } set { iCodPiezaDefectoDetalle = value; } }
        [DataMember(Name = "CodImagen")]
        public int? CodImagen { get { return iCodImagen; } set { iCodImagen = value; } }
        [DataMember(Name = "CodZona")]
        public int? CodZona { get { return iCodZona; } set { iCodZona = value; } }
        [DataMember(Name = "CodModelo")]
        public int? CodModelo { get { return iCodModelo; } set { iCodModelo = value; } }
        [DataMember(Name = "PosicionH")]
        public int? PosicionH { get { return iPosicionH; } set { iPosicionH = value; } }
        [DataMember(Name = "PosicionV")]
        public int? PosicionV { get { return iPosicionV; } set { iPosicionV = value; } }
        #endregion
        #region Methods
        public LocalizacionDefecto(int? iCodPiezaDefectoDetalle, int? iCodModelo, int? iCodImagen, int? iCodZona, int? iPosicionH, int? iPosicionV)
        {
            this.iCodPiezaDefectoDetalle = iCodPiezaDefectoDetalle;
            this.iCodModelo = iCodModelo;
            this.iCodImagen = iCodImagen;
            this.iCodZona = iCodZona;
            this.iPosicionH = iPosicionH;
            this.iPosicionV = iPosicionV;
        }
        public LocalizacionDefecto()
        { }
        public LocalizacionDefecto(DataRow row)
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
        ~LocalizacionDefecto()
        { }
        #endregion
    }
}
