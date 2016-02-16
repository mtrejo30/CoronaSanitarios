using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "TextoEmpaque", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class TextoEmpaque : BaseSolutionEntity
    {

        #region PrivateFields
        private int iCodTextoEmpaque = -1;
        private string sDesTextoEmpaque = String.Empty;
        private string sContenido = String.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;

        #endregion

        #region Properties
        [DataMember(Name = "CodTextoEmpaque")]
        public int CodTextoEmpaque { get { return iCodTextoEmpaque; } set { iCodTextoEmpaque = value; } }
        [DataMember(Name = "DesTextoEmpaque")]
        public string DesTextoEmpaque { get { return sDesTextoEmpaque; } set { sDesTextoEmpaque = value; } }
        [DataMember(Name = "Contenido")]
        public string Contenido { get { return sContenido; } set { sContenido = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }

        #endregion

        #region Methods

        #region Constructors and Destructor

        public TextoEmpaque()
        {

        }
        public TextoEmpaque(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public TextoEmpaque(
            int iCodTextoEmpaque,
            string sDesTextoEmpaque,
            string sContenido,
            DateTime dtFechaRegistro,
            DateTime dtFechaBaja)
        {
            this.iCodTextoEmpaque = iCodTextoEmpaque;
            this.sDesTextoEmpaque = sDesTextoEmpaque;
            this.sContenido = sContenido;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
        }
        ~TextoEmpaque()
        {

        }

        #endregion Constructors and Destructor

        #endregion Methods

    } // class
}
