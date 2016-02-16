using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;


namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "PlantaCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class PlantaCbo:BaseSolutionEntity
    {
        #region PrivateFields
        private int iClavePlanta = -1;
        private string sDescripcionPlanta = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "ClavePlanta")]
        public int ClavePlanta { get { return iClavePlanta; } set { iClavePlanta = value; } }
        [DataMember(Name = "DescripcionPlanta")]
        public string DescripcionPlanta { get { return sDescripcionPlanta; } set { sDescripcionPlanta = value; } }

        #endregion

        #region Methods
        public PlantaCbo(int iClavePlanta,
                string sDescripcionPlanta
        )
        {
            this.iClavePlanta = iClavePlanta;
            this.sDescripcionPlanta = sDescripcionPlanta;
        }
        public PlantaCbo()
        { }
        public PlantaCbo(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~PlantaCbo()
        { }

        #endregion
    }
}

