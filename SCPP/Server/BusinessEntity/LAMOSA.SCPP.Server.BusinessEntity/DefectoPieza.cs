using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "DefectoPieza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class DefectoPieza:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodPiezaTransaccion = -1;
        private string sCodBarra = string.Empty;
        private int iCodPieza = -1;
        private int iCodDefecto = -1;
        private string sClaveDefecto = string.Empty;
        private string sDesDefecto = string.Empty;
        private int iCodZona = -1;
        private string sClaveZona = string.Empty;
        private string sDesZona = string.Empty;
        private int iCodAccionDefecto = -1;
        private string sDesAccion = string.Empty;
        private int? iCodZonaDefectoX;
        private int? iCodZonaDefectoY;
        private int iCodZonaDefectoDet = -1;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;
        private int? iCodPiezaDefectoDetalle;
        private int? iCodImagen;
        private int? iCodModelo;
        private IList<LocalizacionDefecto> listLocalizacionDefecto;
        private int? iCodEmpleado;
        #endregion

        #region Properties
        [DataMember(Name = "CodPiezaTransaccion")]
        public int CodPiezaTransaccion { get { return iCodPiezaTransaccion; } set { iCodPiezaTransaccion = value; } }
        [DataMember(Name = "CodPieza")]
        public int CodPieza { get { return iCodPieza; } set { iCodPieza = value; } }
        [DataMember(Name = "CodBarra")]
        public string CodBarra { get { return sCodBarra; } set { sCodBarra = value; } }
        [DataMember(Name = "ClaveUnica")]
        public int ClaveUnica { get { return iCodDefecto; } set { iCodDefecto = value; } }
        [DataMember(Name = "ClaveDefecto")]
        public string ClaveDefecto { get { return sClaveDefecto; } set { sClaveDefecto = value; } }
        [DataMember(Name = "DesDefecto")]
        public string DesDefecto { get { return sDesDefecto; } set { sDesDefecto = value; } }
        [DataMember(Name = "CodZona")]
        public int CodZona { get { return iCodZona; } set { iCodZona = value; } }
        [DataMember(Name = "ClaveZona")]
        public string ClaveZona { get { return sClaveZona; } set { sClaveZona = value; } }
        [DataMember(Name = "DesZona")]
        public string DesZona { get { return sDesZona; } set { sDesZona = value; } }
        [DataMember(Name = "CodAccion")]
        public int CodAccion { get { return iCodAccionDefecto; } set { iCodAccionDefecto = value; } }
        [DataMember(Name = "DesAccion")]
        public string DesAccion { get { return sDesAccion; } set { sDesAccion = value; } }
        [DataMember(Name = "CodZonaDefectoX")]
        public int? CodZonaDefectoX { get { return iCodZonaDefectoX; } set { iCodZonaDefectoX = value; } }
        [DataMember(Name = "CodZonaDefectoY")]
        public int? CodZonaDefectoY { get { return iCodZonaDefectoY; } set { iCodZonaDefectoY = value; } }
        [DataMember(Name = "CodZonaDefectoDet")]
        public int CodZonaDefectoDet { get { return iCodZonaDefectoDet; } set { iCodZonaDefectoDet = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return bActivo; } set { bActivo = value; } }
        [DataMember(Name = "CodPiezaDefectoDetalle")]
        public int? CodPiezaDefectoDetalle { get { return iCodPiezaDefectoDetalle; } set { iCodPiezaDefectoDetalle = value; } }
        [DataMember(Name = "CodImagen")]
        public int? CodImagen { get { return iCodImagen; } set { iCodImagen = value; } }
        [DataMember(Name = "CodModelo")]
        public int? CodModelo { get { return iCodModelo; } set { iCodModelo = value; } }
        [DataMember(Name = "LocalizacionDefecto")]
        public IList<LocalizacionDefecto> LocalizacionDefecto { get { return listLocalizacionDefecto; } set { listLocalizacionDefecto = value; } }
        [DataMember(Name = "CodEmpleado")]
        public int? CodEmpleado { get { return iCodEmpleado; } set { iCodEmpleado = value; } }
        #endregion
        #region Methods
        public DefectoPieza(int iCodPiezaTrasaccion,
                            int iCodPieza,
                            string sCodBarra,
                            int iCodDefecto,
                            string sClaveDefecto,
                            string sDesDefecto,
                            int iCodZona,
                            string sClaveZona,
                            string sDesZona,
                            int iCodAccionDefecto,
                            string sDesAccion,
                            int iCodZonaDefectoX,
                            int iCodZonaDefectoY,
                            int iCodZonaDefectoDet,
                            DateTime dtFechaBaja,
                            bool bActivo, 
                            int iCodPiezaDefectoDetalle,
                            int iCodImagen,
                            int iCodModelo,
                            int iCodEmpleado)
        {
            this.iCodPiezaTransaccion = iCodPiezaTrasaccion;
            this.iCodPieza = iCodPieza;
            this.sCodBarra = sCodBarra;
            this.iCodDefecto = iCodDefecto;
            this.sClaveDefecto = sClaveDefecto;
            this.sDesDefecto = sDesDefecto;
            this.iCodZona = iCodZona;
            this.sClaveZona = sClaveZona;
            this.sDesZona = sDesZona;
            this.iCodAccionDefecto = iCodAccionDefecto;
            this.sDesAccion = sDesAccion;
            this.iCodZonaDefectoX = iCodZonaDefectoX;
            this.iCodZonaDefectoY = iCodZonaDefectoY;
            this.iCodZonaDefectoDet = iCodZonaDefectoDet;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
            this.iCodPiezaDefectoDetalle = iCodPiezaDefectoDetalle;
            this.iCodImagen = iCodImagen;
            this.iCodModelo = iCodModelo;
            this.iCodEmpleado = iCodEmpleado;
        }
        public DefectoPieza()
        { }
        public DefectoPieza(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new DefectoPieza());
        }
        ~DefectoPieza()
        { }
        #endregion
    }
}
