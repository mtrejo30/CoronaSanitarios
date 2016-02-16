using System;
using System.Data;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "EmpleadoBusqueda", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class EmpleadoBusqueda:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodEmpleado = -1;
        private int iCodEmpleadoMFG = -1;
        private string sNombreCompleto = String.Empty;
        private string sNombre = String.Empty;
        private string sApPaterno = String.Empty;
        private string sApMaterno = String.Empty;
        private string sDescPuesto = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "CodEmpleado")]
        public int CodEmpleado { get { return iCodEmpleado; } set { iCodEmpleado = value; } }
        [DataMember(Name = "CodEmpleadoMFG")]
        public int CodEmpleadoMFG { get { return iCodEmpleadoMFG; } set { iCodEmpleadoMFG = value; } }
        [DataMember(Name = "NombreCompleto")]
        public string NombreCompleto { get { return sNombreCompleto; } set { sNombreCompleto = value; } }
        [DataMember(Name = "Nombre")]
        public string Nombre { get { return sNombre; } set { sNombre = value; } }
        [DataMember(Name = "ApPaterno")]
        public string ApPaterno { get { return sApPaterno; } set { sApPaterno = value; } }
        [DataMember(Name = "ApMaterno")]
        public string ApMaterno { get { return sApMaterno; } set { sApMaterno = value; } }
        [DataMember(Name = "DescPuesto")]
        public string DescPuesto { get { return sDescPuesto; } set { sDescPuesto = value; } }

        #endregion

        #region Methods
        public EmpleadoBusqueda()
        { }
        public EmpleadoBusqueda(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~EmpleadoBusqueda()
        { }

        #endregion
    }
}

