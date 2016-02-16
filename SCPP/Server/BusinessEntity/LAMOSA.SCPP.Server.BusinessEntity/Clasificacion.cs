using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Clasificacion", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Clasificacion : BaseSolutionEntity
    {
        #region PrivateFields
        private int? iCodTurno;
        private int? iCodUsuario;
        private int? iCodSupervisor;
        private int? iCodOperador;
        private int? iCodConfigBanco;
        private int? iCodProceso;
        private DateTime? dtFecha = DateTime.Today;
        private int? iCodPlanta;
        private int? iCodPieza;
        private long? lCodConfigHandHeld;
        private int? iCodCalidad;
        private int? iCodPrueba;
        private int? iCodCentroTrabajo;
        private int? iCodMaquina;
        private string sClaveCalidad = string.Empty;
        private int? iCodMaquinaHorno;
        #endregion
        #region Properties
        [DataMember(Name = "CodTurno")]
        public int? CodTurno
        { get { return iCodTurno; } set { iCodTurno = value; } }
        [DataMember(Name = "CodUsuario")]
        public int? CodUsuario
        { get { return iCodUsuario; } set { iCodUsuario = value; } }
        [DataMember(Name = "CodSupervisor")]
        public int? CodSupervisor
        { get { return iCodSupervisor; } set { iCodSupervisor = value; } }
        [DataMember(Name = "CodOperador")]
        public int? CodOperador
        { get { return iCodOperador; } set { iCodOperador = value; } }
        [DataMember(Name = "CodConfigBanco")]
        public int? CodConfigBanco
        { get { return iCodConfigBanco; } set { iCodConfigBanco = value; } }
        [DataMember(Name = "CodProceso")]
        public int? CodProceso
        { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "Fecha")]
        public DateTime? Fecha
        { get { return dtFecha; } set { dtFecha = value; } }
        [DataMember(Name = "CodPlanta")]
        public int? CodPlanta
        { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "CodPieza")]
        public int? CodPieza
        { get { return iCodPieza; } set { iCodPieza = value; } }
        [DataMember(Name = "CodConfigHandHeld")]
        public long? CodConfigHandHeld
        { get { return lCodConfigHandHeld; } set { lCodConfigHandHeld = value; } }
        [DataMember(Name = "CodCalidad")]
        public int? CodCalidad
        { get { return iCodCalidad; } set { iCodCalidad = value; } }
        [DataMember(Name = "CodPrueba")]
        public int? CodPrueba
        { get { return iCodPrueba; } set { iCodPrueba = value; } }
        [DataMember(Name = "CodCentroTrabajo")]
        public int? CodCentroTrabajo
        { get { return iCodCentroTrabajo; } set { iCodCentroTrabajo = value; } }
        [DataMember(Name = "CodMaquina")]
        public int? CodMaquina
        { get { return iCodMaquina; } set { iCodMaquina = value; } }
        [DataMember(Name = "ClaveCalidad")]
        public string ClaveCalidad
        { get { return sClaveCalidad; } set { sClaveCalidad = value; } }
        [DataMember(Name = "CodMaquinaHorno")]
        public int? CodMaquinaHorno { get { return iCodMaquinaHorno; } set { iCodMaquinaHorno = value; } }
        #endregion
        #region Methods
        public Clasificacion(int iCodTurno, int iCodUsuario, int iCodSupervisor, int iCodOperador, int iCodConfigBanco, int iCodProceso, DateTime dtFecha, int iCodPlanta, int iCodPieza, long lCodConfigHandHeld, int iCodCalidad, int iCodPrueba, string sClaveCalidad, int iCodMaquinaHorno)
        {
            this.iCodTurno = iCodTurno;
            this.iCodUsuario = iCodUsuario;
            this.iCodSupervisor = iCodSupervisor;
            this.iCodOperador = iCodOperador;
            this.iCodConfigBanco = iCodConfigBanco;
            this.iCodProceso = iCodProceso;
            this.dtFecha = dtFecha;
            this.iCodPlanta = iCodPlanta;
            this.iCodPieza = iCodPieza;
            this.lCodConfigHandHeld = lCodConfigHandHeld;
            this.iCodCalidad = iCodCalidad;
            this.iCodPrueba = iCodPrueba;
            this.sClaveCalidad = sClaveCalidad;
            this.iCodMaquinaHorno = iCodMaquinaHorno;
        }
        public Clasificacion()
        { }
        public Clasificacion(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Clasificacion());
        }
        ~Clasificacion()
        { }
        #endregion
    }
}
