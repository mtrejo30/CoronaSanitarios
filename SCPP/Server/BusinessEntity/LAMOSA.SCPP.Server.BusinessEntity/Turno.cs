using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Turno", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Turno : BaseSolutionEntity
    {

        #region Fields

        private int iCodTurno = -1;
        private string sDesTurno = String.Empty;
        private DateTime dtHoraInicio = DateTime.MinValue;
        private DateTime dtHoraFin = DateTime.MinValue;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodTurno")]
        public int CodTurno { get { return this.iCodTurno; } set { this.iCodTurno = value; } }
        [DataMember(Name = "DesTurno")]
        public string DesTurno { get { return this.sDesTurno; } set { this.sDesTurno = value; } }
        [DataMember(Name = "HoraInicio")]
        public DateTime HoraInicio { get { return this.dtHoraInicio; } set { this.dtHoraInicio = value; } }
        [DataMember(Name = "HoraFin")]
        public DateTime HoraFin { get { return this.dtHoraFin; } set { this.dtHoraFin = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return this.dtFechaRegistro; } set { this.dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return this.dtFechaBaja; } set { this.dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public Turno()
        {

        }
        public Turno(DataRow row)
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
            return GetPropertyNamesArray(new Turno());
        }
        public Turno
        (
            int iCodTurno,
            string sDesTurno,
            DateTime dtHoraInicio,
            DateTime dtHoraFin,
            DateTime dtFechaRegistro,
            DateTime dtFechaBaja,
            bool bActivo
        )
        {
            this.iCodTurno = iCodTurno;
            this.sDesTurno = sDesTurno;
            this.dtHoraInicio = dtHoraInicio;
            this.dtHoraFin = dtHoraFin;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
        }
        ~Turno()
        {

        }

        #endregion Constructors and Destructor

        #endregion Methods

    }
}