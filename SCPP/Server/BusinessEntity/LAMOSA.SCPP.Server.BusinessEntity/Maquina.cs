using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Maquina", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Maquina:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodMaquina = -1;
        private string sClaveMaquina = String.Empty;
        private string sDesMaquina = String.Empty;
        private int iCodTipoMaquina = -1;
        private string sDesTipoMaquina = String.Empty;

        private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private int iCodCentroTrabajo = -1;
        private string sDesCentroTrabajo = String.Empty;
      
      
       

        private int iCodConfigBanco = -1;
        #endregion

        #region Properties
        [DataMember(Name = "CodMaquina")]
        public int CodMaquina { get { return iCodMaquina; } set { iCodMaquina = value; } }
        [DataMember(Name = "ClaveMaquina")]
        public string ClaveMaquina { get { return sClaveMaquina; } set { sClaveMaquina = value; } }
        [DataMember(Name = "DesMaquina")]
        public string DesMaquina { get { return sDesMaquina; } set { sDesMaquina = value; } }
        [DataMember(Name = "CodTipoMaquina")]
        public int CodTipoMaquina { get { return iCodTipoMaquina; } set { iCodTipoMaquina = value; } }
        [DataMember(Name = "DesTipoMaquina")]
        public string DesTipoMaquina { get { return sDesTipoMaquina; } set { sDesTipoMaquina = value; } }
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "CodCentroTrabajo")]
        public int CodCentroTrabajo { get { return iCodCentroTrabajo; } set { iCodCentroTrabajo = value; } }
        [DataMember(Name = "DesCentroTrabajo")]
        public string DesCentroTrabajo { get { return sDesCentroTrabajo; } set { sDesCentroTrabajo = value; } }
     

       

        [DataMember(Name = "CodConfigBanco")]
        public int CodConfigBanco { get { return iCodConfigBanco; } set { iCodConfigBanco = value; } }
        #endregion

        #region Methods
        public Maquina(int iCodMaquina,
                      string sClaveMaquina,
                      string sDesMaquina,
                      int iCodTipoMaquina,
                      string sDesTipoMaquina,
                      int iCodProceso,
                      string sDesProceso,
                      int iCodCentroTrabajo,
                        string sDesCentroTrabajo
       
        )
        {
            this.iCodMaquina = iCodMaquina;
            this.sClaveMaquina = sClaveMaquina;
            this.sDesMaquina = sDesMaquina;
            this.iCodTipoMaquina = iCodTipoMaquina;
            this.sDesTipoMaquina = sDesTipoMaquina;
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
            this.iCodCentroTrabajo = iCodCentroTrabajo;
            this.sDesCentroTrabajo = sDesCentroTrabajo;
           
           

        }
        public Maquina()
        { }
        public Maquina(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~Maquina()
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
            return GetPropertyNamesArray(new Maquina());
        }
       
        #endregion
    }
}
