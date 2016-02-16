using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CondicionOperacion", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CondicionOperacion:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCondicionOperacion = -1;   
        private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private int iCodArea = -1;
        private string sDesArea = String.Empty;
        private DateTime dtFecha = DateTime.MinValue;
        private double dTemperatura = -1;
        private double dHumedad = -1;
        private int iUsuarioAutoriza = -1;
        private DateTime dtFechaAutorizacion = DateTime.MinValue;
        private int iAutorizacion = -1;
        private int iActivo = -1;

        #endregion

        #region Properties
        [DataMember(Name = "CodCondicionOperacion")]
        public int CodCondicionOperacion { get { return iCodCondicionOperacion; } set { iCodCondicionOperacion = value; } }
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "CodArea")]
        public int CodArea { get { return iCodArea; } set { iCodArea = value; } }
        [DataMember(Name = "DesArea")]
        public string DesArea { get { return sDesArea; } set { sDesArea = value; } }
        [DataMember(Name = "Fecha")]
        public DateTime Fecha { get { return dtFecha; } set { dtFecha = value; } }
        [DataMember(Name = "Temperatura")]
        public double Temperatura { get { return dTemperatura; } set { dTemperatura = value; } }
        [DataMember(Name = "Humedad")]
        public double Humedad { get { return dHumedad; } set { dHumedad = value; } }
        [DataMember(Name = "UsuarioAutoriza")]
        public int UsuarioAutoriza { get { return iUsuarioAutoriza; } set { iUsuarioAutoriza = value; } }
        [DataMember(Name = "FechaAutorizacion")]
        public DateTime FechaAutorizacion { get { return dtFechaAutorizacion; } set { dtFechaAutorizacion = value; } }
        [DataMember(Name = "Autorizacion")]
        public int Autorizacion { get { return iAutorizacion; } set { iAutorizacion = value; } }
        [DataMember(Name = "Activo")]
        public int Activo { get { return iActivo; } set { iActivo = value; } }

        #endregion

        #region Methods
        public CondicionOperacion(int iCodCondicionOperacion,
                int iCodProceso,
                string sDesProceso,
                int iCodArea,
                string sDesArea,
                DateTime dtFecha,
                double dTemperatura,
                double dHumedad,
                int iUsuarioAutoriza,
                DateTime dtFechaAutorizacion,
                int iAutorizacion,
                int iActivo
        )
        {
            this.iCodCondicionOperacion = iCodCondicionOperacion;
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
            this.iCodArea = iCodArea;
            this.sDesArea = sDesArea;
            this.dtFecha = dtFecha;
            this.dTemperatura = dTemperatura;
            this.dHumedad = dHumedad;
            this.iUsuarioAutoriza = iUsuarioAutoriza;
            this.dtFechaAutorizacion = dtFechaAutorizacion;
            this.iAutorizacion = iAutorizacion;
            this.iActivo = iActivo;
        }
        public CondicionOperacion()
        { }
        public CondicionOperacion(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~CondicionOperacion()
        { }

         public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new CondicionOperacion());
        }
        #endregion
         public object[] ToObjectArray()
         {
             return ToObjectArray(this);
         }
       
    }
}
