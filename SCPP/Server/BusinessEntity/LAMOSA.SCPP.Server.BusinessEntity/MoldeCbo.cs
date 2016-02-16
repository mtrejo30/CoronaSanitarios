using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "MoldeCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class MoldeCbo : BaseSolutionEntity
    {

        #region Fields

        private int iCodMolde = -1;
        private string sDesMolde = String.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodMolde")]
        public int CodMolde { get { return this.iCodMolde; } set { this.iCodMolde = value; } }
        [DataMember(Name = "DesMolde")]
        public string DesMolde { get { return this.sDesMolde; } set { this.sDesMolde = value; } }

        #endregion Properties

        #region Methods

        public MoldeCbo()
        {

        }
        public MoldeCbo(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public MoldeCbo
        (
            int iCodMolde,
            string sDesMolde
        )
        {
            this.iCodMolde = iCodMolde;
            this.sDesMolde = sDesMolde;
        }
        ~MoldeCbo()
        {

        }

        #endregion Methods

    }
}