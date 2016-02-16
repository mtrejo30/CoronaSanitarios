using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;
using System.Reflection;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CondicionPasta", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CondicionPasta : BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCondicionPasta = -1;
        private int iCodPlanta = -1;
        private DateTime dtFecha = DateTime.MinValue;
        private double dDensidad = -1;
        private double dBu = -1;
        private int iUsuarioAutoriza = -1;
        private DateTime dtFechaAutorizacion = DateTime.MinValue;
        private int iAutorizacion = -1;
        private int iActivo = -1;
        private int iTurno = -1;
        private string sTurno = string.Empty;
        private int iBaroi = -1;
        private IList<Area> lstAreas = new List<Area>();

        private int iDeposito = -1;
        private DateTime dtPerdidaBrillo = DateTime.MinValue;
        private int iViscosidad = -1;
        private int iCodigoProveedor = -1;
        private string sNombreProveedor = string.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "CodCondicionPasta")]
        public int CodCondicionPasta { get { return iCodCondicionPasta; } set { iCodCondicionPasta = value; } }
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "Fecha")]
        public DateTime Fecha { get { return dtFecha; } set { dtFecha = value; } }
        [DataMember(Name = "Densidad")]
        public double Densidad { get { return dDensidad; } set { dDensidad = value; } }
        [DataMember(Name = "Bu")]
        public double Bu { get { return dBu; } set { dBu = value; } }
        [DataMember(Name = "UsuarioAutoriza")]
        public int UsuarioAutoriza { get { return iUsuarioAutoriza; } set { iUsuarioAutoriza = value; } }
        [DataMember(Name = "FechaAutorizacion")]
        public DateTime FechaAutorizacion { get { return dtFechaAutorizacion; } set { dtFechaAutorizacion = value; } }
        [DataMember(Name = "Autorizacion")]
        public int Autorizacion { get { return iAutorizacion; } set { iAutorizacion = value; } }
        [DataMember(Name = "Activo")]
        public int Activo { get { return iActivo; } set { iActivo = value; } }
        [DataMember(Name = "CodigoBaroi")]
        public int CodigoBaroi { get { return iBaroi; } set { iBaroi = value; } }
        [DataMember(Name = "CodigoTurno")]
        public int CodigoTurno { get { return iTurno; } set { iTurno = value; } }
        [DataMember(Name = "DescripcionTurno")]
        public string DescripcionTurno { get { return sTurno; } set { sTurno = value; } }
        [DataMember(Name = "ListaArea")]
        public IList<Area> ListaArea { get { return lstAreas; } set { lstAreas = value; } }
        [DataMember(Name = "Deposito")]
        public int Deposito { get { return iDeposito; } set { iDeposito = value; } }
        [DataMember(Name = "PerdidaBrillo")]
        public DateTime PerdidaBrillo { get { return dtPerdidaBrillo; } set { dtPerdidaBrillo = value; } }
        [DataMember(Name = "Viscosidad")]
        public int Viscosidad { get { return iViscosidad; } set { iViscosidad = value; } }
        [DataMember(Name = "CodigoProveedor")]
        public int CodigoProveedor { get { return iCodigoProveedor; } set { iCodigoProveedor = value; } }
        [DataMember(Name = "NombreProveedor")]
        public string NombreProveedor { get { return sNombreProveedor; } set { sNombreProveedor = value; } }
        #endregion

        #region Methods
        public CondicionPasta(int iCodCondicionPasta,
                int iCodPlanta,
                DateTime dtFecha,
                double dDensidad,
                double dBu,
                int iUsuarioAutoriza,
                DateTime dtFechaAutorizacion,
                int iAutorizacion,
                int iActivo
        )
        {
            this.iCodCondicionPasta = iCodCondicionPasta;
            this.iCodPlanta = iCodPlanta;
            this.dtFecha = dtFecha;
            this.dDensidad = dDensidad;
            this.dBu = dBu;
            this.iUsuarioAutoriza = iUsuarioAutoriza;
            this.dtFechaAutorizacion = dtFechaAutorizacion;
            this.iAutorizacion = iAutorizacion;
            this.iActivo = iActivo;
        }
        public CondicionPasta()
        { }
        public CondicionPasta(DataRow row)
        {
            PropertyInfo[] propiedades = this.GetType().GetProperties();
            foreach (PropertyInfo p in propiedades)
            {
                if (p.Name == "LocalizacionDefecto") continue;
                if (p.Name == "ExceptionMessage") continue;
                if (row.Table.Columns.Contains(p.Name) && row[p.Name].GetType().Name != "DBNull")
                    this.GetType().GetProperty(p.Name).SetValue(this, row[p.Name], null);
            }
        }
        ~CondicionPasta()
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
            return GetPropertyNamesArray(new CondicionPasta());
        }
        #endregion
    }
}
