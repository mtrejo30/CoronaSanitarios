using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CentroTrabajo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CentroTrabajo:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCentroTrabajo = -1;
        private string sClaveLamosa = String.Empty;
        private string sDesCentroTrabajo = String.Empty;
        private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private string sLineaProduccion = String.Empty;
        private int iCodPlanta = -1;
        private DateTime dtFechaBaja = DateTime.MinValue;

        #endregion

        #region Properties

        [DataMember(Name = "CodCentroTrabajo")]
        public int CodCentroTrabajo { get { return iCodCentroTrabajo; } set { iCodCentroTrabajo = value; } }
        [DataMember(Name = "ClaveLamosa")]
        public string ClaveLamosa { get { return sClaveLamosa; } set { sClaveLamosa = value; } }
        [DataMember(Name = "DesCentroTrabajo")]
        public string DesCentroTrabajo { get { return sDesCentroTrabajo; } set { sDesCentroTrabajo = value; } }
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "LineaProduccion")]
        public string LineaProduccion { get { return sLineaProduccion; } set { sLineaProduccion = value; } }
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }
     

        #endregion

        #region Methods
        public CentroTrabajo(int iCodCentroTrabajo,
                string sClaveLamosa,
                string sDesCentroTrabajo, 
                int iCodProceso,
                string sDesProceso,
                string sLineaProduccion,            
                int iCodPlanta,
                DateTime dtFechaBaja
    
        )
        {
            this.iCodCentroTrabajo = iCodCentroTrabajo;
            this.sClaveLamosa = sClaveLamosa;
            this.sDesCentroTrabajo = sDesCentroTrabajo;
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
            this.sLineaProduccion = sLineaProduccion;
            this.iCodPlanta = iCodPlanta;
            this.dtFechaBaja = dtFechaBaja;
        }
        public CentroTrabajo()
        { }
        public CentroTrabajo(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        /// <summary>
        /// Obtiene un arreglo de objetos con los valores de las propiedades
        /// </summary>
        /// <returns>objetct[]</returns>
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns></returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new CentroTrabajo());
        }
        ~CentroTrabajo()
        { }

        #endregion
    }
}

