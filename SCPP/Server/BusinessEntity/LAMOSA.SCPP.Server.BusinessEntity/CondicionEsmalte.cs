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
    [DataContract(Name = "CondicionEsmalte", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CondicionEsmalte:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCondicionEsmalte = -1;
        private int iCodPlanta = -1;
        private DateTime dtFecha = DateTime.MinValue;
        private double dTiempoEspejo = -1;
        private double dViscosidad = -1;
        private double dDensidad = -1;
        private double dEspesor = -1;
        private int iUsuarioAutoriza = -1;
        private DateTime dtFechaAutorizacion = DateTime.MinValue;
        private int iAutorizacion = -1;
        private int iActivo = -1;
        //--------------------------------------------------
        private int iCodigoTurno = -1;
        private string sDescripcionTurno = string.Empty;
        private int iCodigoColor = -1;
        private string sDescripcionColor = string.Empty;
        private IList<Maquina> lstMaquina = new List<Maquina>();
        private string sNumeroLote = string.Empty;
        private double dTamanoLote = -1D;
        private double dCantidadGoma = -1D;
        private int iMolino = -1;
        private double dGranulometria = -1D;
        //--------------------------------------------------
        #endregion

        #region Properties
        [DataMember(Name = "CodCondicionEsmalte")]
        public int CodCondicionEsmalte { get { return iCodCondicionEsmalte; } set { iCodCondicionEsmalte = value; } }
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "Fecha")]
        public DateTime Fecha { get { return dtFecha; } set { dtFecha = value; } }
        [DataMember(Name = "TiempoEspejo")]
        public double TiempoEspejo { get { return dTiempoEspejo; } set { dTiempoEspejo = value; } }
        [DataMember(Name = "Viscosidad")]
        public double Viscosidad { get { return dViscosidad; } set { dViscosidad = value; } }
        [DataMember(Name = "Densidad")]
        public double Densidad { get { return dDensidad; } set { dDensidad = value; } }
        [DataMember(Name = "Espesor")]
        public double Espesor { get { return dEspesor; } set { dEspesor = value; } }
        [DataMember(Name = "UsuarioAutoriza")]
        public int UsuarioAutoriza { get { return iUsuarioAutoriza; } set { iUsuarioAutoriza = value; } }
        [DataMember(Name = "FechaAutorizacion")]
        public DateTime FechaAutorizacion { get { return dtFechaAutorizacion; } set { dtFechaAutorizacion = value; } }
        [DataMember(Name = "Autorizacion")]
        public int Autorizacion { get { return iAutorizacion; } set { iAutorizacion = value; } }
        [DataMember(Name = "Activo")]
        public int Activo { get { return iActivo; } set { iActivo = value; } }

        [DataMember(Name = "CodigoTurno")]
        public int CodigoTurno { get { return iCodigoTurno; } set { iCodigoTurno = value; } }
        [DataMember(Name = "DescripcionTurno")]
        public string DescripcionTurno { get { return sDescripcionTurno; } set { sDescripcionTurno = value; } }
        [DataMember(Name = "CodigoColor")]
        public int CodigoColor { get { return iCodigoColor; } set { iCodigoColor = value; } }
        [DataMember(Name = "DescripcionColor")]
        public string DescripcionColor { get { return sDescripcionColor; } set { sDescripcionColor = value; } }
        [DataMember(Name = "ListaMaquina")]
        public IList<Maquina> ListaMaquina { get { return lstMaquina; } set { lstMaquina = value; } }
        [DataMember(Name = "NumeroLote")]
        public string NumeroLote { get { return sNumeroLote; } set { sNumeroLote = value; } }
        [DataMember(Name = "TamanoLote")]
        public double TamanoLote { get { return dTamanoLote; } set { dTamanoLote = value; } }
        [DataMember(Name = "CantidadGoma")]
        public double CantidadGoma { get { return dCantidadGoma; } set { dCantidadGoma = value; } }
        [DataMember(Name = "Molino")]
        public int Molino { get { return iMolino; } set { iMolino = value; } }
        [DataMember(Name = "Granulometria")]
        public double Granulometria { get { return dGranulometria; } set { dGranulometria = value; } }
        #endregion

        #region Methods
        public CondicionEsmalte(int iCodCondicionEsmalte,
                int iCodPlanta,
                DateTime dtFecha,
                double dTiempoEspejo,
                double dViscosidad,
                double dDensidad,
                double dEspesor,
                int iUsuarioAutoriza,
                DateTime dtFechaAutorizacion,
                int iAutorizacion,
                int iActivo
        )
        {
            this.iCodCondicionEsmalte = iCodCondicionEsmalte;
            this.iCodPlanta = iCodPlanta;
            this.dtFecha = dtFecha;
            this.dTiempoEspejo = dTiempoEspejo;
            this.dViscosidad = dViscosidad;
            this.dDensidad = dDensidad;
            this.dEspesor = dEspesor;
            this.iUsuarioAutoriza = iUsuarioAutoriza;
            this.dtFechaAutorizacion = dtFechaAutorizacion;
            this.iAutorizacion = iAutorizacion;
            this.iActivo = iActivo;
        }
        public CondicionEsmalte()
        { }
        public CondicionEsmalte(DataRow row)
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
        ~CondicionEsmalte()
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
            return GetPropertyNamesArray(new CondicionEsmalte());
        }

        #endregion
    }
}
