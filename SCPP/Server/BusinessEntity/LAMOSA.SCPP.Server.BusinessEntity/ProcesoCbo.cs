using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ProcesoCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ProcesoCbo : BaseSolutionEntity
    {
        #region PrivateFields
        private int iClaveProceso = -1;
        private string sDescripcionProceso = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "ClaveProceso")]
        public int ClaveProceso { get { return iClaveProceso; } set { iClaveProceso = value; } }
        [DataMember(Name = "DescripcionProceso")]
        public string DescripcionProceso { get { return sDescripcionProceso; } set { sDescripcionProceso = value; } }

        #endregion

        #region Methods
        public ProcesoCbo(int iClaveProceso,
                string sDescripcionProceso
        )
        {
            this.iClaveProceso = iClaveProceso;
            this.sDescripcionProceso = sDescripcionProceso;
        }
        public ProcesoCbo()
        { }
        public ProcesoCbo(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~ProcesoCbo()
        { }

        #endregion
    }// class
}

