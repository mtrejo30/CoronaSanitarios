using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "TipoDefecto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class TipoDefecto:BaseSolutionEntity
    {
        
        #region Fields

        private int iCodTipoDefecto = -1;
        private string sDesTipoDefecto = String.Empty;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodTipoDefecto")]
        public int CodTipoDefecto { get { return this.iCodTipoDefecto; } set { this.iCodTipoDefecto = value; } }
        [DataMember(Name = "DesTipoDefecto")]
        public string DesTipoDefecto { get { return this.sDesTipoDefecto; } set { this.sDesTipoDefecto = value; } }
        
        #endregion Properties

        #region Methods

        public TipoDefecto()
        {

        }

        public TipoDefecto(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }

        public TipoDefecto(int iCodTipoDefecto,string sDesTipoDefecto)
        {
            this.iCodTipoDefecto = iCodTipoDefecto;
            this.sDesTipoDefecto = sDesTipoDefecto;
        }

        ~TipoDefecto()
        {

        }

        #endregion Methods
    }
}
