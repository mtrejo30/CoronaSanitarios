using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ArticuloPars", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ArticuloPars : BaseSolutionEntity
    {

        #region Fields

        private int iCodTipoArticulo = -1;
        private int iCodMolde = -1;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodTipoArticulo")]
        public int CodTipoArticulo { get { return this.iCodTipoArticulo; } set { this.iCodTipoArticulo = value; } }
        [DataMember(Name = "CodMolde")]
        public int CodMolde { get { return this.iCodMolde; } set { this.iCodMolde = value; } }

        #endregion Properties

        #region Methods

        public ArticuloPars()
        {
            
        }
        public ArticuloPars(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public ArticuloPars
        (
            int iCodTipoArticulo,
            int iCodMolde
        )
        {
            this.iCodTipoArticulo = iCodTipoArticulo;
            this.iCodMolde = iCodMolde;
        }
        ~ArticuloPars()
        {
            
        }

        #endregion Methods

    }
}