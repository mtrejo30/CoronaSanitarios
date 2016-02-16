using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "KardexProducto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class KardexProducto : BaseSolutionEntity
    {
        #region PrivateFields
        private DateTime dFecha = DateTime.MinValue;
        private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private int iCodCentroTrabajo = -1;
        private string sDesCentroTrabajo = String.Empty;
        private int iCodMaquina = -1;
        private string sDesMaquina = String.Empty;
        private int iPosicion = -1;
        private int iCodOperador = -1;
        private string sOperador = String.Empty;
        private long lCodPiezaTransaccion = -1;
        

        #endregion

        #region Properties
        [DataMember(Name = "Fecha")]
        public DateTime Fecha { get { return dFecha; } set { dFecha = value; } }
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "CodCentroTrabajo")]
        public int CodCentroTrabajo { get { return iCodCentroTrabajo; } set { iCodCentroTrabajo = value; } }
        [DataMember(Name = "DesCentroTrabajo")]
        public string DesCentroTrabajo { get { return sDesCentroTrabajo; } set { sDesCentroTrabajo = value; } }
        [DataMember(Name = "CodMaquina")]
        public int CodMaquina { get { return iCodMaquina; } set { iCodMaquina = value; } }
        [DataMember(Name = "DesMaquina")]
        public string DesMaquina { get { return sDesMaquina; } set { sDesMaquina = value; } }
        [DataMember(Name = "Posicion")]
        public int Posicion { get { return iPosicion; } set { iPosicion = value; } }
        [DataMember(Name = "CodOperador")]
        public int CodOperador { get { return iCodOperador; } set { iCodOperador = value; } }
        [DataMember(Name = "Operador")]
        public string Operador { get { return sOperador; } set { sOperador = value; } }
        [DataMember(Name = "CodPiezaTransaccion")]
        public long CodPiezaTransaccion { get { return lCodPiezaTransaccion; } set { lCodPiezaTransaccion = value; } }

        #endregion

        #region Methods
        public KardexProducto(DateTime dFecha,
                int iCodProceso,
                string sDesProceso,
                int iCodCentroTrabajo,
                string sDesCentroTrabajo,
                int iCodMaquina,
                string sDesMaquina,
                int iPosicion,
                int iCodOperador,
                string Operador,
                long lCodPiezaTransaccion
        )
        {
            this.dFecha = dFecha;
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
            this.iCodCentroTrabajo = iCodCentroTrabajo;
            this.sDesCentroTrabajo = sDesCentroTrabajo;
            this.iCodMaquina = iCodMaquina;
            this.sDesMaquina = sDesMaquina;
            this.iPosicion = iPosicion;
            this.iCodOperador = iCodOperador;
            this.Operador = Operador;
            this.lCodPiezaTransaccion = lCodPiezaTransaccion;
        }
        public KardexProducto()
        { }
        public KardexProducto(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~KardexProducto()
        { }

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
            return GetPropertyNamesArray(new KardexProducto());
        }

        #endregion
    }
}
