using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "TipoArticuloCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class TipoArticuloCbo : BaseSolutionEntity
    {

        #region Fields

        private int iCodTipoArticulo = -1;
        private string sDesTipoArticulo = String.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodTipoArticulo")]
        public int CodTipoArticulo { get { return this.iCodTipoArticulo; } set { this.iCodTipoArticulo = value; } }
        [DataMember(Name = "DesTipoArticulo")]
        public string DesTipoArticulo { get { return this.sDesTipoArticulo; } set { this.sDesTipoArticulo = value; } }
        
        #endregion Properties

        #region Methods

        public TipoArticuloCbo()
        {
            
        }
        public TipoArticuloCbo(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public TipoArticuloCbo
        (
            int iCodTipoArticulo,
            string sDesTipoArticulo
        )
        {
            this.iCodTipoArticulo = iCodTipoArticulo;
            this.sDesTipoArticulo = sDesTipoArticulo;
        }
        ~TipoArticuloCbo()
        {

        }

        #endregion Methods

    }
}