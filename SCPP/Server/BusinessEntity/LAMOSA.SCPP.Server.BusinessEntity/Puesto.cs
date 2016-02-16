using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Puesto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Puesto : BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodPuesto = -1;
        private string sDesPuesto = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "CodPuesto")]
        public int CodPuesto { get { return iCodPuesto; } set { iCodPuesto = value; } }
        [DataMember(Name = "DesPuesto")]
        public string DesPuesto { get { return sDesPuesto; } set { sDesPuesto = value; } }

        #endregion

        #region Methods
        public Puesto(int iCodPuesto,
                string sDesPuesto
        )
        {
            this.iCodPuesto = iCodPuesto;
            this.sDesPuesto = sDesPuesto;
        }
        public Puesto()
        { }
        public Puesto(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~Puesto()
        { }

        #endregion
    }
}

