using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ConfigBancos", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ConfigBancos:BaseSolutionEntity
    {
        #region PrivateFields
        private string sDesCentroTrabajo = String.Empty;
        private string sDesMaquina = String.Empty;
        private int iLimiteVaciadas = -1;
        private int iVaciadasAcumuladas = -1;
        private DateTime dtFechaInicio = DateTime.MinValue;
        private DateTime dtFechaFin = DateTime.MinValue;
        private bool bAutorizado = false;
        private string sAutoriza = String.Empty;
        private bool bActivo = false;
        private int iCodCT = -1;
        private int iCodMaquina = -1;
        private int iCodUsuarioAutoriza = -1;
        private int iCodUsuarioAlta = -1;
        private int iCodConfigBanco = -1;
        
        #endregion

        #region Properties
        [DataMember(Name = "DesCentroTrabajo")]
        public string DesCentroTrabajo { get { return sDesCentroTrabajo; } set { sDesCentroTrabajo = value; } }
        [DataMember(Name = "DesMaquina")]
        public string DesMaquina { get { return sDesMaquina; } set { sDesMaquina = value; } }
        [DataMember(Name = "LimiteVaciadas")]
        public int LimiteVaciadas { get { return iLimiteVaciadas; } set { iLimiteVaciadas = value; } }
        [DataMember(Name = "VaciadasAcumuladas")]
        public int VaciadasAcumuladas { get { return iVaciadasAcumuladas; } set { iVaciadasAcumuladas = value; } }
        [DataMember(Name = "FechaInicio")]
        public DateTime FechaInicio { get { return dtFechaInicio; } set { dtFechaInicio = value; } }
        [DataMember(Name = "FechaFin")]
        public DateTime FechaFin { get { return dtFechaFin; } set { dtFechaFin = value; } }
        [DataMember(Name = "Autorizado")]
        public bool Autorizado { get { return bAutorizado; } set { bAutorizado = value; } }
        [DataMember(Name = "Autoriza")]
        public string Autoriza { get { return sAutoriza; } set { sAutoriza = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return bActivo; } set { bActivo = value; } }
        [DataMember(Name = "CodCT")]
        public int CodCT { get { return iCodCT; } set { iCodCT = value; } }
        [DataMember(Name = "CodMaquina")]
        public int CodMaquina { get { return iCodMaquina; } set { iCodMaquina = value; } }
        [DataMember(Name = "CodUsuarioAutoriza")]
        public int CodUsuarioAutoriza { get { return iCodUsuarioAutoriza; } set { iCodUsuarioAutoriza = value; } }
        [DataMember(Name = "CodUsuarioAlta")]
        public int CodUsuarioAlta { get { return iCodUsuarioAlta; } set { iCodUsuarioAlta = value; } }
        [DataMember(Name = "CodConfigBanco")]
        public int CodConfigBanco { get { return iCodConfigBanco; } set { iCodConfigBanco = value; } }

        #endregion

        #region Methods
        public ConfigBancos(string sDesCentroTrabajo,
                string sDesMaquina,
                DateTime dtFechaInicio,
                DateTime dtFechaFin,
                bool bAutorizado,
                string sAutoriza,
                bool bActivo,
                int iCodCT,
                int iCodMaquina,
                int iCodUsuarioAutoriza,
                int iCodUsuarioAlta,
                int iCodConfigBanco
        )
        {
            this.sDesCentroTrabajo = sDesCentroTrabajo;
            this.sDesMaquina = sDesMaquina;
            this.dtFechaInicio = dtFechaInicio;
            this.dtFechaFin = dtFechaFin;
            this.bAutorizado = bAutorizado;
            this.sAutoriza = sAutoriza;
            this.bActivo = bActivo;
            this.iCodCT = iCodCT;
            this.iCodMaquina = iCodMaquina;
            this.iCodUsuarioAutoriza = iCodUsuarioAutoriza;
            this.iCodUsuarioAlta = iCodUsuarioAlta;
            this.iCodConfigBanco = iCodConfigBanco;
        }
        public ConfigBancos()
        { }
        public ConfigBancos(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~ConfigBancos()
        { }

        #endregion
    }
}
