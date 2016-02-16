using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;
using System.Data;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Accion", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Accion:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodAccion = -1;
        private string sDesAccion = string.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;
        #endregion
        #region Properties
        [DataMember(Name = "CodAccion")]
        public int CodAccion { get { return iCodAccion; } set { iCodAccion = value; } }
        [DataMember(Name = "DesAccion")]
        public string DesAccion { get { return sDesAccion; } set { sDesAccion = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return bActivo; } set { bActivo = value; } }
        #endregion

        #region Methods
        public Accion(int iCodAccion, string sDesAccion, DateTime dtFechaRegistro, DateTime dtFechaBaja)
        {
            this.iCodAccion = iCodAccion;
            this.sDesAccion = sDesAccion;
            this.dtFechaBaja = dtFechaBaja;
            this.dtFechaRegistro = dtFechaRegistro;
        }
        public Accion()
        { }
        public Accion(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
       
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
       
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Accion());
        }
        ~Accion()
        { }

        #endregion
    }
}
