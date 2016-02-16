using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Area", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Area:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodarea = -1;
        private string sAreadesc = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "CodArea")]
        public int CodArea { get { return iCodarea; } set { iCodarea = value; } }
        [DataMember(Name = "AreaDesc")]
        public string AreaDesc { get { return sAreadesc; } set { sAreadesc = value; } }

        #endregion

        #region Methods
        public Area(int iCodarea,
                string sAreadesc
        )
        {
            this.iCodarea = iCodarea;
            this.sAreadesc = sAreadesc;
        }
        public Area()
        { }
        public Area(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~Area()
        { }

        #endregion
    }
}

