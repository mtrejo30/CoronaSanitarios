using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "DistribucionCodBarras", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class DistribucionCodBarras : BaseSolutionEntity
    {

        #region Fields

        private int iCodTurno = -1;
        private string sDesTurno = String.Empty;
        private DateTime dtHoraInicio = DateTime.MinValue;
        private DateTime dtHoraFin = DateTime.MinValue;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;

        #endregion Fields

        #region Properties

        #region CodTurno
        [DataMember(Name = "CodTurno")]
        public int CodTurno { get { return this.iCodTurno; } set { this.iCodTurno = value; } }
        #endregion CodTurno
        #region DesTurno
        [DataMember(Name = "DesTurno")]
        public string DesTurno { get { return this.sDesTurno; } set { this.sDesTurno = value; } }
        #endregion DesTurno
        #region HoraInicio
        [DataMember(Name = "HoraInicio")]
        public DateTime HoraInicio { get { return this.dtHoraInicio; } set { this.dtHoraInicio = value; } }
        #endregion HoraInicio
        #region HoraFin
        [DataMember(Name = "HoraFin")]
        public DateTime HoraFin { get { return this.dtHoraFin; } set { this.dtHoraFin = value; } }
        #endregion HoraFin
        #region FechaRegistro
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return this.dtFechaRegistro; } set { this.dtFechaRegistro = value; } }
        #endregion FechaRegistro
        #region FechaBaja
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return this.dtFechaBaja; } set { this.dtFechaBaja = value; } }
        #endregion FechaBaja

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public DistribucionCodBarras()
        {

        }
        public DistribucionCodBarras(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public DistribucionCodBarras(
            int iCodTurno,
            string sDesTurno,
            DateTime dtHoraInicio,
            DateTime dtHoraFin,
            DateTime dtFechaRegistro,
            DateTime dtFechaBaja)
        {
            this.iCodTurno = iCodTurno;
            this.sDesTurno = sDesTurno;
            this.dtHoraInicio = dtHoraInicio;
            this.dtHoraFin = dtHoraFin;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
        }
        ~DistribucionCodBarras()
        {

        }

        #endregion Constructors and Destructor

        #endregion Methods

    } // class
}