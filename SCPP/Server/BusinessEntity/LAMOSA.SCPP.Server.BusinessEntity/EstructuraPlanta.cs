using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "EstructuraPlanta", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class EstructuraPlanta : BaseSolutionEntity
    {
        #region PrivateFields
        private string sAlmacen = String.Empty;
        private string sPlanta = String.Empty;
        private string sProceso = String.Empty;
        private string sLineaProduccion = String.Empty;
        private string sCentroTrabajo = String.Empty;
        private string sArea = String.Empty;
        private string sMaquina = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "Almacen")]
        public string Almacen { get { return sAlmacen; } set { sAlmacen = value; } }
        [DataMember(Name = "Planta")]
        public string Planta { get { return sPlanta; } set { sPlanta = value; } }
        [DataMember(Name = "Proceso")]
        public string Proceso { get { return sProceso; } set { sProceso = value; } }
        [DataMember(Name = "LineaProduccion")]
        public string LineaProduccion { get { return sLineaProduccion; } set { sLineaProduccion = value; } }
        [DataMember(Name = "CentroTrabajo")]
        public string CentroTrabajo { get { return sCentroTrabajo; } set { sCentroTrabajo = value; } }
        [DataMember(Name = "Area")]
        public string Area { get { return sArea; } set { sArea = value; } }
        [DataMember(Name = "Maquina")]
        public string Maquina { get { return sMaquina; } set { sMaquina = value; } }

        #endregion

        #region Methods
        public EstructuraPlanta(string sAlmacen,
                string sPlanta,
                string sProceso,
                string sLineaProduccion,
                string sCentroTrabajo,
                string sArea,
                string sMaquina
        )
        {
            this.sAlmacen = sAlmacen;
            this.sPlanta = sPlanta;
            this.sProceso = sProceso;
            this.sLineaProduccion = sLineaProduccion;
            this.sCentroTrabajo = sCentroTrabajo;
            this.sArea = sArea;
            this.sMaquina = sMaquina;
        }
        public EstructuraPlanta()
        { }
        public EstructuraPlanta(DataRow row)
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
            return GetPropertyNamesArray(new EstructuraPlanta());
        }
        ~EstructuraPlanta()
        { }

        #endregion
    }// class
}
