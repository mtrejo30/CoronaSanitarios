using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Prueba", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Prueba : BaseSolutionEntity
    {
        #region PrivateFields
        private int iClavePrueba = -1;
        private string sDesPrueba = String.Empty;
        private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private int iCodProcesoFin = -1;
        private string sDesProcesoFin = String.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private int iResidenciaMax = -1;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;

        #endregion

        #region Properties
        [DataMember(Name = "ClavePrueba")]
        public int ClavePrueba { get { return iClavePrueba; } set { iClavePrueba = value; } }
        [DataMember(Name = "DesPrueba")]
        public string DesPrueba { get { return sDesPrueba; } set { sDesPrueba = value; } }
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "CodProcesoFin")]
        public int CodProcesoFin { get { return iCodProcesoFin; } set { iCodProcesoFin = value; } }
        [DataMember(Name = "DesProcesoFin")]
        public string DesProcesoFin { get { return sDesProcesoFin; } set { sDesProcesoFin = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }
        [DataMember(Name = "ResidenciaMax")]
        public int ResidenciaMax { get { return iResidenciaMax; } set { iResidenciaMax = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }

        #endregion

        #region Methods
        public Prueba(int iClavePrueba,
                string sDesPrueba,
                int iCodProceso,
                string sDesProceso,
                int iCodProcesoFin,
                string sDesProcesoFin,
                DateTime dtFechaRegistro,
                bool bActivo,
                int iResidenciaMax,
                DateTime dtFechaBaja
        )
        {
            this.iClavePrueba = iClavePrueba;
            this.sDesPrueba = sDesPrueba;
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
            this.iCodProcesoFin = iCodProcesoFin;
            this.sDesProcesoFin = sDesProcesoFin;
            this.dtFechaRegistro = dtFechaRegistro;
            this.bActivo = bActivo;
            this.iResidenciaMax = iResidenciaMax;
            this.dtFechaBaja = dtFechaBaja;
        }
        #region Constructors and Destructor
        public Prueba()
        { }
        public Prueba(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~Prueba()
        { }
        #endregion Constructors and Destructor

        #endregion
    }
}

