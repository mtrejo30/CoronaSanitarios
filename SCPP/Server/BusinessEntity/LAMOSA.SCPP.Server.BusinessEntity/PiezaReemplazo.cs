using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "PiezaReemplazo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class PiezaReemplazo:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodPlanta = -1;
        private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private string sClaveArticulo = String.Empty;
        private string sDesArticulo = String.Empty;
        private int iCodArticulo = -1;
        private int iCodigoBarras = -1;
        private int iCodPieza = -1;
        private DateTime dtFechaRegistro = DateTime.MinValue;

        #endregion

        #region Properties
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "ClaveArticulo")]
        public string ClaveArticulo { get { return sClaveArticulo; } set { sClaveArticulo = value; } }
        [DataMember(Name = "DesArticulo")]
        public string DesArticulo { get { return sDesArticulo; } set { sDesArticulo = value; } }
        [DataMember(Name = "CodArticulo")]
        public int CodArticulo { get { return iCodArticulo; } set { iCodArticulo = value; } }
        [DataMember(Name = "CodigoBarras")]
        public int CodigoBarras { get { return iCodigoBarras; } set { iCodigoBarras = value; } }
        [DataMember(Name = "CodPieza")]
        public int CodPieza { get { return iCodPieza; } set { iCodPieza = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }

        #endregion

        #region Methods
        public PiezaReemplazo()
        { }
        public PiezaReemplazo(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~PiezaReemplazo()
        { }

        #endregion
    }
}

