using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CodigoBarra", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CodigoBarra : BaseSolutionEntity
    {
        #region PrivateFields
        private int iClaveCodigoBarra = -1;
        private int iClavePlanta = -1; 
        private int iClaveEmpleado = -1;
        private int iClaveCentroTrabajo = -1;
        private int iClaveMaquina = -1;
        private int iCodigoDesde = -1;


        private int iCodigoHasta = -1;
        private string sDescripcionPlanta = String.Empty;
        private string sDescripcionEmpleado = String.Empty;
        private string sDescripcionCentroTrabajo = String.Empty;
        private string sDescripcionMaquina = String.Empty;
        private DateTime dFecha;



        #endregion

        #region Properties
        [DataMember(Name = "ClaveCodigoBarra")]
        public int ClaveCodigoBarra { get { return iClaveCodigoBarra; } set { iClaveCodigoBarra = value; } }
        [DataMember(Name = "ClavePlanta")]
        public int ClavePlanta { get { return iClavePlanta; } set { iClavePlanta = value; } }
        [DataMember(Name = "ClaveEmpleado")]
        public int ClaveEmpleado { get { return iClaveEmpleado; } set { iClaveEmpleado = value; } }
        [DataMember(Name = "ClaveCentroTrabajo")]
        public int ClaveCentroTrabajo { get { return iClaveCentroTrabajo; } set { iClaveCentroTrabajo = value; } }
        [DataMember(Name = "ClaveMaquina")]
        public int ClaveMaquina { get { return iClaveMaquina; } set { iClaveMaquina = value; } }
        [DataMember(Name = "DescripcionPlanta")]
        public string DescripcionPlanta { get { return sDescripcionPlanta; } set { sDescripcionPlanta = value; } }
        [DataMember(Name = "DescripcionEmpleado")]
        public string DescripcionEmpleado { get { return sDescripcionEmpleado; } set { sDescripcionEmpleado = value; } }
        [DataMember(Name = "DescripcionCentroTrabajo")]
        public string DescripcionCentroTrabajo { get { return sDescripcionCentroTrabajo; } set { sDescripcionCentroTrabajo = value; } }
        [DataMember(Name = "DescripcionMaquina")]
        public string DescripcionMaquina { get { return sDescripcionMaquina; } set { sDescripcionMaquina = value; } }
        [DataMember(Name = "Fecha")]
        public DateTime Fecha { get { return dFecha; } set { dFecha = value; } }
        [DataMember(Name = "CodigoDesde")]
        public int CodigoDesde { get { return iCodigoDesde; } set { iCodigoDesde = value; } }
        [DataMember(Name = "CodigoHasta")]
        public int CodigoHasta { get { return iCodigoHasta; } set { iCodigoHasta = value; } }

        #endregion

        #region Methods
        public CodigoBarra(
                int iClaveCodigoBarra,
                int iClavePlanta,
                string sDescripcionPlanta,
                int iClaveEmpleado,
                String sDescripcionEmpleado,
                int iClaveCentroTrabajo,
                String sDescripcionCentroTrabajo,
                int iClaveMaquina,
                String sDescripcionMaquina,
                DateTime dFecha,
                int iCodigoDesde,
                int iCodigoHasta
            )
        {
            this.iClaveCodigoBarra = iClaveCodigoBarra;
            this.iClavePlanta = iClavePlanta;
            this.iClaveEmpleado = iClaveEmpleado;
            this.iClaveCentroTrabajo = iClaveCentroTrabajo;
            this.iClaveMaquina = iClaveMaquina;
            this.sDescripcionPlanta = sDescripcionPlanta;
            this.sDescripcionEmpleado = sDescripcionEmpleado;
            this.sDescripcionCentroTrabajo = sDescripcionCentroTrabajo;
            this.sDescripcionMaquina = sDescripcionMaquina;
            this.dFecha = dFecha;
            this.iCodigoDesde = iCodigoDesde;
            this.iCodigoHasta = iCodigoHasta;
        }
        public CodigoBarra()
        { }
        public CodigoBarra(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~CodigoBarra()
        { }

        #endregion
    }
}
