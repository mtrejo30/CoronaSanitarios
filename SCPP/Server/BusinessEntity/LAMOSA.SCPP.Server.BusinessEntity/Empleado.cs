using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Empleado", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Empleado:BaseSolutionEntity
    {
        #region PrivateFields
        private int iClaveMFG = -1;
        private int iClaveNomina = -1;
        private string sNombre = String.Empty;
        private string sApellidoPaterno = String.Empty;
        private string sApellidoMaterno = String.Empty;
        private string sPuesto = String.Empty;
        private string sCentroDeTrabajo = String.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private string sDesPlanta = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "ClaveMFG")]
        public int ClaveMFG { get { return iClaveMFG; } set { iClaveMFG = value; } }
        [DataMember(Name = "ClaveNomina")]
        public int ClaveNomina { get { return iClaveNomina; } set { iClaveNomina = value; } }
        [DataMember(Name = "Nombre")]
        public string Nombre { get { return sNombre; } set { sNombre = value; } }
        [DataMember(Name = "ApellidoPaterno")]
        public string ApellidoPaterno { get { return sApellidoPaterno; } set { sApellidoPaterno = value; } }
        [DataMember(Name = "ApellidoMaterno")]
        public string ApellidoMaterno { get { return sApellidoMaterno; } set { sApellidoMaterno = value; } }
        [DataMember(Name = "Puesto")]
        public string Puesto { get { return sPuesto; } set { sPuesto = value; } }
        [DataMember(Name = "CentroDeTrabajo")]
        public string CentroDeTrabajo { get { return sCentroDeTrabajo; } set { sCentroDeTrabajo = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
        [DataMember(Name = "DesPlanta")]
        public string DesPlanta { get { return sDesPlanta; } set { sDesPlanta = value; } }

        #endregion

        #region Methods
        public Empleado(int iClaveMFG,
                int iClaveNomina,
                string sNombre,
                string sApellidoPaterno,
                string sApellidoMaterno,
                string sPuesto,
                string sCentroDeTrabajo,
                DateTime dtFechaRegistro,
                string sDesPlanta
        )
        {
            this.iClaveMFG = iClaveMFG;
            this.iClaveNomina = iClaveNomina;
            this.sNombre = sNombre;
            this.sApellidoPaterno = sApellidoPaterno;
            this.sApellidoMaterno = sApellidoMaterno;
            this.sPuesto = sPuesto;
            this.sCentroDeTrabajo = sCentroDeTrabajo;
            this.dtFechaRegistro = dtFechaRegistro;
            this.sDesPlanta = sDesPlanta;
        }
        public Empleado()
        { }
        public Empleado(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
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
            return GetPropertyNamesArray(new Empleado());
        }
        ~Empleado()
        { }

        #endregion
    }// class
}

