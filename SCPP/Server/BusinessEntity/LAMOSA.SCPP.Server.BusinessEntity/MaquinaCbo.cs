using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "MaquinaCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class MaquinaCbo:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodMaquina = -1;
        private string sClaveMaquina = String.Empty;
        private string sDesMaquina = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "CodMaquina")]
        public int CodMaquina { get { return iCodMaquina; } set { iCodMaquina = value; } }
        [DataMember(Name = "ClaveMaquina")]
        public string ClaveMaquina { get { return sClaveMaquina; } set { sClaveMaquina = value; } }
        [DataMember(Name = "DesMaquina")]
        public string DesMaquina { get { return sDesMaquina; } set { sDesMaquina = value; } }

        #endregion

        #region Methods
        public MaquinaCbo()
        { }
        public MaquinaCbo(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~MaquinaCbo()
        { }

        #endregion
    }
}

