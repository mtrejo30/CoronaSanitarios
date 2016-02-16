using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Calidad", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Calidad : BaseSolutionEntity
    {

        #region Fields

        private int iCodCalidad = -1;
        private string sClaveCalidad = String.Empty;
        private string sDesCalidad = String.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodCalidad")]
        public int CodCalidad { get { return this.iCodCalidad; } set { this.iCodCalidad = value; } }
        [DataMember(Name = "ClaveCalidad")]
        public string ClaveCalidad { get { return this.sClaveCalidad; } set { this.sClaveCalidad = value; } }
        [DataMember(Name = "DesCalidad")]
        public string DesCalidad { get { return this.sDesCalidad; } set { this.sDesCalidad = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return this.dtFechaRegistro; } set { this.dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return this.dtFechaBaja; } set { this.dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }

        #endregion Properties

        #region Methods

        public Calidad()
        {

        }
        public Calidad(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Calidad());
        }
        public Calidad
        (
            int iCodCalidad,
            string sClaveCalidad,
            string sDesCalidad,
            DateTime dtFechaRegistro,
            DateTime dtFechaBaja,
            bool bActivo
        )
        {
            this.iCodCalidad = iCodCalidad;
            this.sClaveCalidad = sClaveCalidad;
            this.sDesCalidad = sDesCalidad;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
        }
        ~Calidad()
        {
            
        }

        #endregion Methods

    }
}
