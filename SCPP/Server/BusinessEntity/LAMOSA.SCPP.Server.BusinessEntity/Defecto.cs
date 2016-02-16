using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Defecto", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Defecto:BaseSolutionEntity
    {
        #region PrivateFields
        private int iClaveUnica = -1;
        private string sClaveDefecto = String.Empty;
        private string sDescDefecto = String.Empty;
        private int iCodTipoDefecto = -1;
        private string sTipoDefecto = String.Empty;
        private int iCodProceso = -1;
        private string sProceso = String.Empty;
        private int iCodProcesoResp = -1;
        private string sProcesoResponsable = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "ClaveUnica")]
        public int ClaveUnica { get { return iClaveUnica; } set { iClaveUnica = value; } }
        [DataMember(Name = "ClaveDefecto")]
        public string ClaveDefecto { get { return sClaveDefecto; } set { sClaveDefecto = value; } }
        [DataMember(Name = "DescDefecto")]
        public string DescDefecto { get { return sDescDefecto; } set { sDescDefecto = value; } }
        [DataMember(Name = "CodTipoDefecto")]
        public int CodTipoDefecto { get { return iCodTipoDefecto; } set { iCodTipoDefecto = value; } }
        [DataMember(Name = "TipoDefecto")]
        public string TipoDefecto { get { return sTipoDefecto; } set { sTipoDefecto = value; } }
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "Proceso")]
        public string Proceso { get { return sProceso; } set { sProceso = value; } }
        [DataMember(Name = "CodProcesoResp")]
        public int CodProcesoResp { get { return iCodProcesoResp; } set { iCodProcesoResp = value; } }
        [DataMember(Name = "ProcesoResponsable")]
        public string ProcesoResponsable { get { return sProcesoResponsable; } set { sProcesoResponsable = value; } }

        #endregion

        #region Methods
        public Defecto(int iClaveUnica,
                string sClaveDefecto,
                string sDescDefecto,
                int iCodTipoDefecto,
                string sTipoDefecto,
                int iCodProceso,
                string sProceso,
                int iCodProcesoResp,
                string sProcesoResponsable
        )
        {
            this.iClaveUnica = iClaveUnica;
            this.sClaveDefecto = sClaveDefecto;
            this.sDescDefecto = sDescDefecto;
            this.iCodTipoDefecto = iCodTipoDefecto;
            this.sTipoDefecto = sTipoDefecto;
            this.iCodProceso = iCodProceso;
            this.sProceso = sProceso;
            this.iCodProcesoResp = iCodProcesoResp;
            this.sProcesoResponsable = sProcesoResponsable;
        }
        public Defecto()
        { }
        public Defecto(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
       
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
       
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Defecto());
        }
        ~Defecto()
        { }

        #endregion
    }
}

