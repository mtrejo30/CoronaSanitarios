using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Proceso", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Proceso:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private bool bMFG = false;
        private bool bRequerido = false;
        private int iCodProcesoPadre = -1;
        private string sDesProcesoPadre = String.Empty;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;

        #endregion

        #region Properties
        [DataMember(Name = "CodProceso")]
        public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "MFG")]
        public bool MFG { get { return bMFG; } set { bMFG = value; } }
        [DataMember(Name = "Requerido")]
        public bool Requerido { get { return bRequerido; } set { bRequerido = value; } }
        [DataMember(Name = "CodProcesoPadre")]
        public int CodProcesoPadre { get { return iCodProcesoPadre; } set { iCodProcesoPadre = value; } }
        [DataMember(Name = "DesProcesoPadre")]
        public string DesProcesoPadre { get { return sDesProcesoPadre; } set { sDesProcesoPadre = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return bActivo; } set { bActivo = value; } }

        #endregion

        #region Methods
        public Proceso(int iCodProceso,
                string sDesProceso,
                bool bMFG,
                bool bRequerido,
                int iCodProcesoPadre,
                string sDesProcesoPadre,
                DateTime dtFechaBaja,
                bool bActivo
        )
        {
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
            this.bMFG = bMFG;
            this.bRequerido = bRequerido;
            this.iCodProcesoPadre = iCodProcesoPadre;
            this.sDesProcesoPadre = sDesProcesoPadre;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
        }
        public Proceso()
        { }
        public Proceso(DataRow row)
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
            return GetPropertyNamesArray(new Proceso());
        }
        ~Proceso()
        { }

        #endregion
    }// class
}


