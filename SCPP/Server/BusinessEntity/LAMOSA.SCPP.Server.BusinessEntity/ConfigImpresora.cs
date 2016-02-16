using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ConfigImpresora", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ConfigImpresora : BaseSolutionEntity
    {
        private int iCodPlanta = -1;
        private int iCodCentroTrabajo = -1;
        private int iCodMaquina = -1;
        private string sIpAddress = string.Empty;
        private int iPuerto = -1;

        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "CodCentroTrabajo")]
        public int CodCentroTrabajo { get { return iCodCentroTrabajo; } set { iCodCentroTrabajo = value; } }
        [DataMember(Name = "CodMaquina")]
        public int CodMaquina { get { return iCodMaquina; } set { iCodMaquina = value; } }
        [DataMember(Name = "IpAddress")]
        public string IpAddress { get { return sIpAddress; } set { sIpAddress = value; } }
        [DataMember(Name = "Puerto")]
        public int Puerto { get { return iPuerto; } set { iPuerto = value; } }        
    }
}
